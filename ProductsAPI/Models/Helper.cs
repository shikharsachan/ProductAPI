using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Data;
using System;

namespace ProductsAPI.Models
{
    public class Helper : IHelper
    {
        private readonly ApplicationDBContext _dbContext;
        private static Random _random = new Random();

        public Helper(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        //This is 1 way to achieve the unique number functionality. 
        
        public int GenerateUniqueId()
        {
            int newProductId;
            do
            {
                newProductId = _random.Next(100000, 999999);
            } while (_dbContext.Products.Any( x => x.ProductNumber == newProductId)); // Ensure uniqueness
            return newProductId;
        }
    }
}
