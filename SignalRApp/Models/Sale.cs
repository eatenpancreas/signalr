using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRApp.Models;

public class Sale {
    public int Id { get; set; }
    
    [ForeignKey(nameof(CustomerId))]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = new();
    
    [ForeignKey(nameof(ProductId))]
    public int ProductId { get; set; }
    public Product Product { get; set; } = new();
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    public DateTime PurchasedOn { get; set; }
}