// Estrutura de dados
namespace InventoryApp.Models
{public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; // nunca vão ser nulas

        public string Brand { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public DateTime LastPurchase { get; set; }

        public string Origin { get; set; } = string.Empty;
    }
}