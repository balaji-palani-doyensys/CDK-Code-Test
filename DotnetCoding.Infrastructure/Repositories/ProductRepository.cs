using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<ProductDetails>> GetProductByName(string value)
        {
            return await _dbContext.Products.Where(p => p.ProductName == value && p.ProductStatus == 1).ToListAsync();
        }

        public async Task<List<ProductDetails>> GetProductByPriceRange(int minValue, int maxValue)
        {
            return await _dbContext.Products.Where(p => (p.ProductPrice >= minValue && p.ProductPrice <= maxValue) && p.ProductStatus == 1).ToListAsync();
        }

        public async Task<List<ProductDetails>> GetProductByPostedDateRange(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Products.Where(p => (p.PostedOn >= startDate && p.PostedOn <= endDate) && p.ProductStatus == 1).ToListAsync();
        }

        public async Task<List<Approval>> GetAllApprovals()
        {
            return await _dbContext.Approvals.OrderBy(p => p.RequestedOn).ToListAsync();
        }

        public int CreateProduct(ProductDetails product)
        {
            _dbContext.Add(product);
            return _dbContext.SaveChanges();
        }

        public void UpdateProdcut(ProductDetails product)
        {
            _dbContext.Update(product);
            _dbContext.SaveChanges();
        }

        public void CreateApproval(Approval approval)
        {
            _dbContext.Add(approval);

            _dbContext.SaveChanges();
        }

        public void UpdateApproval(Approval approval)
        {
            _dbContext.Update(approval);
            _dbContext.SaveChanges();
        }
    }
}
