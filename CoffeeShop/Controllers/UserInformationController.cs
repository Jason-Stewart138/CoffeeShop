using CoffeeShopDomain;
using CoffeeShopDTO;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using CoffeeShopWebApp.Models;

namespace CoffeeShopWebApp.Controllers
{
	public class UserInformationController : Controller	
	{
		private UserInformationInteractor _interactor;

		public UserInformationController()
		{
			_interactor = new UserInformationInteractor();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Welcome(int userId)
		{
			_interactor.GetUserIfExists(userId, out var user);
			return View(user);
		}



		// GET: UserInformationController
		public IActionResult Index()
		{
			List<UserInformationViewModel> items = new List<UserInformationViewModel>();

			List<UserInformation> dbItems = _interactor.GetAllUsers();
			foreach (UserInformation item in dbItems)
			{
				UserInformationViewModel viewItem = UserInformationViewModel.ViewModelMapper(item);
				items.Add(viewItem);
			}

			return View(items);
		}

		// GET: UserInformationController/Details/5
		public IActionResult Details(int id)
		{
			if (_interactor.GetUserIfExists(id, out UserInformation dbItem))
			{
				UserInformationViewModel viewItem = UserInformationViewModel.ViewModelMapper(dbItem);
				return View(viewItem);
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}
		}

		// GET: UserInformationController/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: UserInformationController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(IFormCollection collection)
		{
			try
			{
				UserInformation userToAdd = UserInformationViewModel.UserDtoMapperForCreate(collection);
				_interactor.AddNewUser(userToAdd);
				var userToView = UserInformationViewModel.ViewModelMapper(userToAdd);
				return RedirectToAction("Welcome");
			}
			catch (Exception ex)
			{
				return View();
			}
		}

		// GET: UserInformationController/Edit/5
		public IActionResult Edit(int id)
		{
			if (_interactor.GetUserIfExists(id, out UserInformation dbItem))
			{
				UserInformationViewModel viewItem = UserInformationViewModel.ViewModelMapper(dbItem);
				return View(viewItem);
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: UserInformationController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				UserInformation itemToUpdate = UserInformationViewModel.UserDtoMapperForUpdate(collection);
				_interactor.UpdateUser(itemToUpdate);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}

		}

		// GET: UserInformationController/Delete/5
		public IActionResult Delete(int id)
		{
			if (_interactor.GetUserIfExists(id, out UserInformation dbItem))
			{
				UserInformationViewModel viewItem = UserInformationViewModel.ViewModelMapper(dbItem);
				return View(viewItem);
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: UserInformationController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				_interactor.DeleteUser(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
