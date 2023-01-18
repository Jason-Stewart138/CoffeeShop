using CoffeeShopDTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopWebApp.Models
{
    public class ProductInformationViewModel
    {
        public int Id { get; set; }
        [DisplayName("Product Name")]
        [Required]
        public string ProductName { get; set; }
        [DisplayName("Product Description")]
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Category { get; set; }

        public static ProductInformationViewModel ViewModelMapper(Products productDTO)
        {
            return new ProductInformationViewModel
            {
                Id = productDTO.Id,
                ProductName= productDTO.ProductName,
                Description= productDTO.Description,
                Price= productDTO.Price,
                Category= productDTO.Category,
            };
        }

        //send to the dB as an update (id is required)
        public static Products ProductDtoMapperForUpdate(IFormCollection collection)
        {
            return new Products
            {                
                Id = int.Parse(collection["Id"]),
                ProductName = collection["ProductName"],
                Description = collection["Description"],
                Price = Convert.ToDouble(collection["Price"]), 
                Category = collection["Category"]
            };
        }

        //send to the db as a create (it is generated in the db)
        public static Products ProductDtoMapperForCreate(IFormCollection collection)
        {
            return new Products
            {
                ProductName = collection["ProductName"],
                Description = collection["Description"],
                Price = Convert.ToDouble(collection["Price"]),
                Category = collection["Category"]
            };
        }
    }
}
