using CoffeeShopRepository;
using CoffeeShopDTO;


namespace CoffeeShopDomain
{
	public class UserInformationInteractor
	{
		private UserInformationRepository _repository;

		public UserInformationInteractor()
		{
			_repository = new UserInformationRepository();
		}

		public bool AddNewUser(UserInformation userToAdd)
		{
			if (string.IsNullOrEmpty(userToAdd.FirstName) || string.IsNullOrEmpty(userToAdd.LastName))
			{
				throw new ArgumentException("First and last name must contain valid text.");
			}
			return _repository.AddUser(userToAdd);
		}
		public List<UserInformation> GetAllUsers()
		{
			return _repository.GetAllUsers();
		}
		public bool GetUserIfExists(int userId, out UserInformation userToReturn)
		{
			UserInformation user = _repository.GetUserById(userId);
			userToReturn = user;
			return userToReturn != null;
		}
		public bool UpdateUser(UserInformation userToUpdate)
		{
			if (string.IsNullOrEmpty(userToUpdate.FirstName) || string.IsNullOrEmpty(userToUpdate.LastName))
			{
				throw new ArgumentException("First and last name must contain valid text.");
			}

			UserInformation user = _repository.GetUserById(userToUpdate.Id);

			if (user == null)
			{				
				return false;
			}
			_repository.UpdateUser(userToUpdate);
			return true;
		}
		public bool DeleteUser(int userId)
		{
			UserInformation user = _repository.GetUserById(userId);
			if (user == null)
			{
				return false;
			}
			_repository.DeleteUser(user);
			return true;
		}

	}
}