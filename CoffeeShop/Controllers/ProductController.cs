using CoffeeShopDomain;
using CoffeeShopDTO;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using CoffeeShopWebApp.Models;

namespace CoffeeShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private ProductsInteractor _interactor;

        public ProductController()
        {
            _interactor = new ProductsInteractor();
        }

        // GET: ProductController
        public IActionResult Index()
        {
            List<ProductInformationViewModel> products = new List<ProductInformationViewModel>();

            
            List<Products> dbItems = _interactor.GetAllProducts();
            foreach (Products item in dbItems)
            {
                ProductInformationViewModel viewItem = ProductInformationViewModel.ViewModelMapper(item);
                products.Add(viewItem);
            }

            return View(products);
        }

        // GET: ProductController/Details/5
        public IActionResult Details(int id)
        {
            if (_interactor.GetProductIfExists(id, out Products dbItem))
            {
                ProductInformationViewModel viewItem = ProductInformationViewModel.ViewModelMapper(dbItem);
                return View(viewItem);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProductController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                Products productToAdd = ProductInformationViewModel.ProductDtoMapperForCreate(collection);
                _interactor.AddNewProduct(productToAdd);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public IActionResult Edit(int id)
        {
			if (_interactor.GetProductIfExists(id, out Products dbItem))
			{
				ProductInformationViewModel viewItem = ProductInformationViewModel.ViewModelMapper(dbItem);
				return View(viewItem);
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}
		}

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            if (_interactor.GetProductIfExists(id, out Products dbItem))
            {
                ProductInformationViewModel viewItem = ProductInformationViewModel.ViewModelMapper(dbItem);
                return View(viewItem);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ProductController/Delete/5
        public IActionResult Delete(int id)
        {
            if (_interactor.GetProductIfExists(id, out Products dbItem))
            {
                ProductInformationViewModel viewItem = ProductInformationViewModel.ViewModelMapper(dbItem);
                return View(viewItem);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _interactor.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
