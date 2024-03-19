using SignalRApp.Models;
using SignalRApp.Models.ViewModels;

namespace SignalRApp.Repositories;

public interface IProductRepository {
    public Task<IEnumerable<Product>> GetProducts();
    public Task<Product> GetProduct(int id);
    public Task<List<ProductGraphData>> GetProductGraphData();
}