using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop2.Core;
using MyShop2.Core.Models;

namespace MyShop2.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products==null)
            {
                products = new List<Product>();
            }
        }

        //Commit, Insert, Update, Delete, Find, Collection

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product product) {
            products.Add(product);
        }


        public void Update(Product product)
        {
            var productToUpdate = Find(product.Id);
            if(productToUpdate!= null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public Product Find(string Id)
        {
            var productToFind = products.Find(p => p.Id == Id);
            if (productToFind != null)
            {
                return productToFind;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public void Delete(string Id)
        {
            var productToDelete = Find(Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }


    }
}
