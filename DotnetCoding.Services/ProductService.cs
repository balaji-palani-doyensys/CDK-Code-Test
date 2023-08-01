using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            return await _unitOfWork.Products.GetAll();
        }

        public async Task<List<ProductDetails>> GetProductByName(string name)
        {
            return await _productRepository.GetProductByName(name);
        }

        public async Task<List<ProductDetails>> GetProductByPriceRange(int minValue, int maxValue)
        {
            return await _productRepository.GetProductByPriceRange(minValue, maxValue);

        }

        public async Task<List<ProductDetails>> GetProductByPostedOn(DateTime startDate, DateTime endDate)
        {
            return await _productRepository.GetProductByPostedDateRange(startDate, endDate);
        }

        public async Task<List<Approval>> GetApprovals()
        {
            return await _productRepository.GetAllApprovals();
        }

        public void CreateProduct(ProductDetails product)
        {
            var approval = CheckProductExists(ref product);

            product.ProductStatus = 1;

            int newProduct = _productRepository.CreateProduct(product);

            if (approval)
            {
                var approvalProduct = new Approval { Product_id = newProduct, RequestedOn = DateTime.Now, IsApproved = false, Action = 1 };

                _productRepository.CreateApproval(approvalProduct);
            }
        }

        public async Task<ProductDetails> Update(int Id, ProductDetails product)
        {
            var products = await _unitOfWork.Products.GetAll();

            var prdct = products.Where(p => p.Id == Id).FirstOrDefault();

            if (prdct == null)
            {
                throw new InvalidOperationException("No products exists to update");
            }

            product.Id = Id;

            _productRepository.UpdateProdcut(product);

            if (CheckProductExists(ref product))
            {
                var approvalProduct = new Approval { Product_id = product.Id, RequestedOn = DateTime.Now, IsApproved = false, Action = 1 };

                _productRepository.CreateApproval(approvalProduct);
            }

            return product;
        }

        public async void DeleteProduct(int Id)
        {
            var products = await _unitOfWork.Products.GetAll();

            var product = products.Where(p => p.Id == Id).FirstOrDefault();

            if (product == null)
            {
                throw new InvalidOperationException("No products exists to delete");
            }

            var approvalProduct = new Approval { Product_id = product.Id, RequestedOn = DateTime.Now, IsApproved = false, Action = 0 };

            _productRepository.CreateApproval(approvalProduct);
        }


        public async void ApproveReject(int Id, Boolean isApproved)
        {
            var approvals = await _productRepository.GetAllApprovals();

            var approval = approvals.Where(a => a.Id == Id).FirstOrDefault();

            if (approval == null)
            {
                throw new InvalidOperationException("No approval exists for given Id");
            }

            var products = await _productRepository.GetAll();

            if (isApproved)
            {
                var product = products.Where(p => p.Id == Id).FirstOrDefault();

                if (product == null)
                {
                    throw new InvalidOperationException("No products exists");
                }

                product.ProductStatus = approval.Action;

                _productRepository.UpdateProdcut(product);
            }

            approval.Id = Id;
            approval.IsApproved = isApproved;

            _productRepository.UpdateApproval(approval);

        }

        private Boolean CheckProductExists(ref ProductDetails newProduct, ProductDetails? oldProduct = null) 
        {
            var approvalFLow = false;

            if (newProduct.ProductPrice > 10000)
            {
                throw new InvalidOperationException("Product with price greater than 10000 is allowed");
            }

            var currentProduct = GetProductByName(newProduct.ProductName) ;

            if (currentProduct != null)
            {
                throw new InvalidOperationException("Product by same name already exists");
            }

            if (newProduct.ProductPrice > 5000 |CheckOldProductPrice(newProduct.ProductPrice, oldProduct) > 50)
            {
                approvalFLow = true;
            }
            else
            {   approvalFLow = false;
            }

            return approvalFLow;
        }

        private int CheckOldProductPrice(int newPrice, ProductDetails? oldProduct = null)
        {
            int percentage = 0;

            if (oldProduct != null)
            {
                int oldPrice = oldProduct.ProductPrice;

                if (oldProduct.ProductPrice < newPrice)
                {
                    percentage = ((newPrice - oldPrice)/((newPrice+oldPrice)/2))*100;
                }
            }

            return percentage;
        }
    }
}
