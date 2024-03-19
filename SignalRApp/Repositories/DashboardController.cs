using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRApp.Data;
using SignalRApp.Models;
using SignalRApp.Models.ViewModels;
using SignalRApp.Repositories;

namespace SignalRApp.Controllers;

public class ProductRepository : IProductRepository {
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db) {
        _db = db;
    }

    public async Task<IEnumerable<Product>> GetProducts() {
        return await _db.Products.AsNoTracking().ToListAsync();
    }
    
    public async Task<Product> GetProduct(int id) {
        return await _db.Products.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<ProductGraphData>> GetProductGraphData() {
        var category = await _db.Products.GroupBy(s => s.Category)
            .Select(s => new ProductGraphData {
                Category = s.Key,
                Count = s.Count()
            }).OrderBy(p => p.Count).ToListAsync();

        return category.Select(item => new ProductGraphData {
            Category = item.Category,
            Count = item.Count
        }).ToList();
    }
}