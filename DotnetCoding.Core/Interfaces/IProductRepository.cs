using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetails>
    {
        Task<List<ProductDetails>> GetProductByName(string value);

        Task<List<ProductDetails>> GetProductByPriceRange(int minValue, int maxValue);

        Task<List<ProductDetails>> GetProductByPostedDateRange(DateTime startDate, DateTime endDate);

        Task<List<Approval>> GetAllApprovals();

        int CreateProduct(ProductDetails product);

        void UpdateProdcut(ProductDetails product);

        void CreateApproval(Approval approval);

        void UpdateApproval(Approval approval);
    }
}
