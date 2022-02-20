using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finclusion.Database.Models;

public class Order
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    [ForeignKey("AccountId")]
    public Account Account { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    public DateTime DateOrdered { get; set; }
}