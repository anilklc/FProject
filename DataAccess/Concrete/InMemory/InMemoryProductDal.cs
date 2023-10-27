using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products=new List<Product>();
        public InMemoryProductDal()
        {   //Proje çalıştığında bellekte bir veritabanı oluşturdu hayali
            _products=new List<Product> { 
                new Product{ProductId=1,ProductName="Computer",CategoryId=1,UnitPrice=100000,UnitsInStock=10},
                new Product{ProductId=2,ProductName="Klavye",CategoryId=1,UnitPrice=1000,UnitsInStock=8},
                new Product{ProductId=3,ProductName="Monitör",CategoryId=1,UnitPrice=7000,UnitsInStock=15},
                new Product{ProductId=4,ProductName="Masa",CategoryId=2,UnitPrice=1000,UnitsInStock=5},
                new Product{ProductId=5,ProductName="Sandalye",CategoryId=2,UnitPrice=1500,UnitsInStock=4},
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            var productDelete = _products.SingleOrDefault(p=>p.ProductId.Equals(product.ProductId));
            _products.Remove(productDelete); 
            

        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p=>p.CategoryId.Equals(categoryId)).ToList();
        }

        public List<Product> GetAllProduct()
        {
            return _products;
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            var productUpdate= _products.SingleOrDefault(p => p.ProductId.Equals(product.ProductId));
            productUpdate.ProductName=product.ProductName;
            productUpdate.CategoryId=product.CategoryId;
            productUpdate.UnitPrice=product.UnitPrice;
            productUpdate.UnitsInStock=product.UnitsInStock;

            
        }
    }
}
