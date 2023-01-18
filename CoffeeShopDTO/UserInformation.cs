﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopDTO
{
	public class UserInformation
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }

		
	}
}