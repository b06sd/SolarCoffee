using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Serialization
{
    public class ProductMapper
    {

        /// <summary>
        /// Maps a Product data model to a ProductModel view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeProductModel(Data.Models.Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived,
                DateArchived = product.DateArchived,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn
            };
        }

        /// <summary>
        /// Maps a ProductModel view model to a Product data model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Data.Models.Product SerializeProductModel(ProductModel product)
        {
            return new Data.Models.Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived,
                DateArchived = product.DateArchived,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn
            };
        }

    }
}
