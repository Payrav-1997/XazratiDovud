// using Domain.Entities;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
// using Persistence.Contexts;

namespace Project.SignalRHubs
{
    public class CommentHub : Hub
    {
        public static ConcurrentDictionary<string, List<string>> ConnectedUsers = new ConcurrentDictionary<string, List<string>>();
        private readonly IServiceProvider _serviceProvider;

        public CommentHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string path = Context.GetHttpContext().Request.Path;
            string conId = Context.ConnectionId;
            string fullConId = conId + path;
            string userId = Context.User.Identity.Name;
            var userCons = ConnectedUsers[userId];
            int index = -1;
            for (int i = 0; i < userCons.Count; i++)
            {
                if (userCons[i] == fullConId)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                ConnectedUsers[userId].RemoveAt(index);
            }

            if (userCons.Count == 0)
            {
                ConnectedUsers.TryRemove(userId,out userCons);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public override Task OnConnectedAsync()
        {
            Trace.TraceInformation("MapHub started. ID: {0}", Context.ConnectionId + Context.GetHttpContext().Request.Path);

            var path = Context.GetHttpContext().Request.Path;
            
            var userId = Context.User.Identity.Name; // or get it from Context.User.Identity.Name;

            // Try to get a List of existing user connections from the cache
            List<string> existingUserConnectionIds;
            ConnectedUsers.TryGetValue(userId, out existingUserConnectionIds);

            // happens on the very first connection from the user
            if(existingUserConnectionIds == null)
            {
                existingUserConnectionIds = new List<string>();
            }

            // First add to a List of existing user connections (i.e. multiple web browser tabs)
            existingUserConnectionIds.Add(Context.ConnectionId + Context.GetHttpContext().Request.Path);

    
            // Add to the global dictionary of connected users
            ConnectedUsers.TryAdd(userId, existingUserConnectionIds);

            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string message)
        {
            string connectionPath = Context.GetHttpContext().Request.Path;
            int userId = int.Parse(Context?.UserIdentifier ?? "0");
            int requestId = int.Parse(string.Join("", connectionPath.Where(Char.IsDigit)));
            string fullConnectionPath = Context.ConnectionId + connectionPath;
            var cons = new List<string>();
            foreach (var connectedUser in ConnectedUsers)
            {
                foreach (var conId in connectedUser.Value)
                {
                    int connectionRequestId = int.Parse(conId.Split('/').Last());
                    if (requestId == connectionRequestId)
                    {
                        cons.Add(conId.Replace(connectionPath, ""));
                    }
                }
            }
            using var scope = _serviceProvider.CreateScope();
            // var context = scope.ServiceProvider.GetRequiredService<ManagerAlifDbContext>();
            // var comment = new Comment
            // { Text = message, CreateDate = DateTime.Now, RequestId = requestId, UserId = userId };
            // await context.Comments.AddAsync(comment);
            // await context.SaveChangesAsync();
            DateTime date = DateTime.Now;
            await Clients.Clients(cons).SendAsync("ReceiveMessage", Context.User.Identity.Name,message, date.ToString("dd-MM-yyyy hh:mm tt"));
        }
    }
}
