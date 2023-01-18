using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CoffeeShopDTO;

namespace CoffeeShopRepository
{
	public class UserInformationRepository
	{
		private IConfigurationRoot _configuration;
		private DbContextOptionsBuilder<ApplicationDBContext> _optionsBuilder;

		public UserInformationRepository()
		{
			BuildOptions();
		}

		private void BuildOptions()
		{
			_configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
			_optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
			_optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CoffeeShopDbManager"));
		}
		public bool AddUser(UserInformation userToAdd)
		{
			using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
			{
				//determine if item exists
				UserInformation existingItem = db.Users.FirstOrDefault(x => x.FirstName.ToLower() == userToAdd.FirstName.ToLower());

				if (existingItem == null)
				{
					// doesn't exist, add it
					db.Users.Add(userToAdd);
					db.SaveChanges();
					return true;
				}
				return false;
			}			
		}
		public List<UserInformation> GetAllUsers()
		{
			using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
			{
				return db.Users.ToList();
			}
		}
		public UserInformation GetUserById(int userId)
		{
			using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
			{
				return db.Users.FirstOrDefault(x => x.Id == userId);
			}
		}
		public void UpdateUser(UserInformation userToUpdate)
		{
			using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
			{
				db.Users.Update(userToUpdate);
				db.SaveChanges();
			}
		}
		public void DeleteUser(UserInformation userToDelete)
		{
			using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
			{
				db.Users.Remove(userToDelete);
				db.SaveChanges();
			}
		}
	}
}