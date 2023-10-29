using Azure.Core;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Caching;
using Core.Aspects.AutoFac.Performance;
using Core.Aspects.AutoFac.Transaction;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //burada attiribute  tipleri typeoflatmak zorundayız kural
        //claim
        [SecuredOperation("admin,product.Add")]
        [ValidationAspect(typeof(ProductValidator))]
        //ekleme işlemi yaptığında cache deki tüm getleri siler
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
           IResult result= BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId)
               ,CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());
            if (result!=null)
            {
              return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            /*if (DateTime.Now.Hour == 23) 
              { 
                  return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
              }*/
            return new DataResult<List<Product>>(_productDal.GetAll(), true, Messages.ProductListed);

        }

        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));

        }

        [CacheAspect]
        //bu metodun çalışma süresi 10 saniyeyi geçerse beni uyarmasını istiyorum
        //[PerformanceAspect(10)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {   //bir kategoride 10dan faza ürün bulunamaz kuralı
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            // uyan kayıt var mı
            var result = _productDal.GetAll(p=>p.ProductName==productName).Any();
            if(result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        //başka bir servisden alınan şarta göre kural koyma
        private IResult CheckIfCategoryLimitExceded() 
        {
            var result = _categoryService.GetAll();

            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
        //transaction işlemi yaptığımız işlem gerçekleşmediğinde geri dönülmesi için kullanılmakta
        //örneğin havale işlemi gerçekleşmediğine paramızın hesabımıza geri gelmesi gibi

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
          
            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }
            Add(product);

            return null;
        }
    }
}
