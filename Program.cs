// Execução
using System;
using InventoryApp.Data;
using InventoryApp.Services;
using InventoryApp.ReportServices;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;";

        var reportservice = new ReportService();
        var repository = new ProductRepository(connectionString);
        var service = new ProductService(repository);

        while (true)
        {
            Console.WriteLine("\n===MENU===");
            Console.WriteLine("1 - Adicionar Produto.");
            Console.WriteLine("2 - Listar Produtos.");
            Console.WriteLine("3 - Produtos Críticos");
            Console.WriteLine("4 - Buscar Produto");
            Console.WriteLine("5 - Atualizar Produto"); 
            Console.WriteLine("6 - Deletar Produto");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            string option = Console.ReadLine();

            switch(option)
            {
                case "1":
                    service.AddProductFromUser();
                    break;

                case "2":
                    service.ShowProductsWithStatus();
                    break;

                case "3":
                    service.ShowCriticalProducts();
                    break;

                case "4":
                    service.SearchProductByName();
                    break;

                case "5":
                    service.UpdateProductFromUser();
                    break;

                case "6":
                    service.DeleteProductFromUser();
                    break;

                case "0":
                    Console.WriteLine("Saindo...");
                    return;

                default:
                    Console.WriteLine("Opção Inválida");
                    break;

            }
        }
    }
}