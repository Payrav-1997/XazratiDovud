using Core.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account.Repository
{
	public class UsersRepository : Repository<DataContext, User, int>
	{
		private readonly UserManager<User> _userManager;
		public UsersRepository(DataContext context, UserManager<User> userManager) : base(context)
		{
			this._userManager = userManager;

		}


		protected override IQueryable<User> GetQueryable()
		{
			return this._userManager.Users;
		}



		public async Task<User> Create(User model, string password)
		{
			var result = await this._userManager
				.CreateAsync(model, password);

			if (!result.Succeeded)
			{
				throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(m => m.Description)));
			}

			return model;
		}



		public override async Task<User> Update(User model)
		{
			var result = await this._userManager
				.UpdateAsync(model);

			if (!result.Succeeded)
			{
				throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(m => m.Description)));
			}

			return model;
		}



		public override async Task<User> Delete(User model)
		{
			var result = await this._userManager
				.DeleteAsync(model);

			if (!result.Succeeded)
			{
				throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(m => m.Description)));
			}

			return model;
		}


		public async Task<User> NoActive(User model)
		{
			model.Active = false;
			return await this.Update(model);
		}

	}
}
