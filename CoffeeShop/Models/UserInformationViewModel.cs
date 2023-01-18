using CoffeeShopDTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopWebApp.Models
{
	public class UserInformationViewModel
	{
		public int Id { get; set; }
		[DisplayName("First Name")]
		[Required]
		public string FirstName { get; set; }
		[DisplayName("Last Name")]
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }

		public static UserInformationViewModel ViewModelMapper(UserInformation userDTO)
		{
			return new UserInformationViewModel
			{
				Id = userDTO.Id,
				FirstName = userDTO.FirstName,
				LastName= userDTO.LastName,
				Email = userDTO.Email,
				Password = userDTO.Password,
			};
		}

		//send to the dB as an update (id is required)
		public static UserInformation UserDtoMapperForUpdate(IFormCollection collection)
		{
			return new UserInformation
			{
				Id = int.Parse(collection["Id"]),
				FirstName = collection["FirstName"],
				LastName = collection["LastName"],
				Email = collection["Email"],
				Password = collection["Password"]
			};
		}

		//send to the db as a create (it is generated in the db)
		public static UserInformation UserDtoMapperForCreate(IFormCollection collection)
		{
			return new UserInformation
			{
				FirstName = collection["FirstName"],
				LastName = collection["LastName"],
				Email = collection["Email"],
				Password = collection["Password"]
			};
		}



	}
}
