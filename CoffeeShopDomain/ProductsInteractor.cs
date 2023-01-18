using CoffeeShopDTO;
using CoffeeShopRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDomain
{
    public class ProductsInteractor
    {
        private ProductInformationRepository _repository;

        public ProductsInteractor()
        {
            _repository = new ProductInformationRepository();
        }

        public bool AddNewProduct(Products productToAdd)
        {
            if (string.IsNullOrEmpty(productToAdd.ProductName) || string.IsNullOrEmpty(productToAdd.Description))
            {
                throw new ArgumentException("Product name and description must contain valid text.");
            }
            return _repository.AddProduct(productToAdd);
        }
        public List<Products> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }
        public bool GetProductIfExists(int productId, out Products productToReturn)
        {
            Products product = _repository.GetProductById(productId);
            productToReturn = product;
            return productToReturn != null;
        }
        public bool UpdateProduct(Products productToUpdate)
        {
            if (string.IsNullOrEmpty(productToUpdate.ProductName) || string.IsNullOrEmpty(productToUpdate.Description))
            {
                throw new ArgumentException("Product name and description must contain valid text.");
            }

            Products product = _repository.GetProductById(productToUpdate.Id);

            if (product == null)
            {
                return false;
            }
            _repository.UpdateProduct(productToUpdate);
            return true;
        }
        public bool DeleteProduct(int productId)
        {
            Products product = _repository.GetProductById(productId);
            if (product == null)
            {
                return false;
            }
            _repository.DeleteProduct(product);
            return true;
        }
    }
}
