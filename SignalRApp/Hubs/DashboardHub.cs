using Microsoft.AspNetCore.SignalR;
using SignalRApp.Repositories;

namespace SignalRApp.Hubs;

public class DashboardHub: Hub {
    private readonly IProductRepository _productRepository;
    public DashboardHub(IProductRepository productRepository) {
        _productRepository = productRepository;
    }
    public async Task SendProducts() {
        var products = _productRepository.GetProducts();
        await Clients.All.SendAsync("ReceivedProducts", products);

        var graphData = _productRepository.GetProductGraphData();
        await Clients.All.SendAsync("ReceivedProductGraphData", graphData);
    }
}