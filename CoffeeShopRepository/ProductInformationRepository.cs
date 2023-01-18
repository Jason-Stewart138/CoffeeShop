using CoffeeShopDTO;
using CoffeeShopRepository.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopRepository
{
    public class ProductInformationRepository
    {

        private IConfigurationRoot _configuration;
        private DbContextOptionsBuilder<ApplicationDBContext> _optionsBuilder;

        public ProductInformationRepository()
        {
            BuildOptions();
        }

        private void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CoffeeShopDbManager"));
        }
        public bool AddProduct(Products productToAdd)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                //determine if item exists
                Products existingItem = db.Products.FirstOrDefault(x => x.ProductName.ToLower() == productToAdd.ProductName.ToLower());

                if (existingItem == null)
                {
                    // doesn't exist, add it
                    db.Products.Add(productToAdd);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public List<Products> GetAllProducts()
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                return db.Products.ToList();
            }
        }
        public Products GetProductById(int productId)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                return db.Products.FirstOrDefault(x => x.Id == productId);
            }
        }
        public void UpdateProduct(Products productToUpdate)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                db.Products.Update(productToUpdate);
                db.SaveChanges();
            }
        }
        public void DeleteProduct(Products productToDelete)
        {
            using (ApplicationDBContext db = new ApplicationDBContext(_optionsBuilder.Options))
            {
                db.Products.Remove(productToDelete);
                db.SaveChanges();
            }
        }
    }
}
