using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetAllProducts();

        Task<List<ProductDetails>> GetProductByName (string name);

        Task<List<ProductDetails>> GetProductByPriceRange(int minValue, int maxValue);

        Task<List<ProductDetails>> GetProductByPostedOn(DateTime startDate, DateTime endDate);

        Task<List<Approval>> GetApprovals();

        void CreateProduct(ProductDetails product);

        Task<ProductDetails> Update(int Id, ProductDetails product);

        void DeleteProduct(int Id);

        void ApproveReject(int Id, Boolean isApproved);
    }
}
