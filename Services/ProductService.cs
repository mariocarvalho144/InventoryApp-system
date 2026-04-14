// Regras
using System;
using System.Collections.Generic;
using InventoryApp.Models;
using InventoryApp.Data;
using System.Linq;



namespace InventoryApp.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public void ShowProductsWithStatus()
        {
            Console.WriteLine(">>>> NOVO RELATÓRIO");
            var products = _repository.GetAllProducts()
                                      .OrderBy(p => p.Quantity)
                                      .ToList();

            Console.WriteLine("\n===== RELATÓRIO DE ESTOQUE (ORDENADO POR QUANTIDADE) =====\n");

            Console.WriteLine($"{"Id",-5}{"Nome",-18} {"Marca",-18} {"Origem",-18} {"Qtd",-10} {"Data", -20} {"Status",-10}");
            Console.WriteLine(new string('-', 120));

            int total = 0;
            int criticalCount = 0;
            int totalQuantity = 0;

            Product highestStock = null;
            Product lowestStock = null;
            

            foreach (var product in products)
            {
                string status = product.Quantity < 10 ? "CRÍTICO" : "OK";

                if (status == "CRÍTICO")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    criticalCount++;
                }    
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                 Console.WriteLine($"{product.Id,-5}{product.Name,-18} {product.Brand,-18} {product.Origin,-18} {product.Quantity,-10} {product.LastPurchase,-20:dd/MM/yyyy HH:mm} {status,-18}");

                 Console.ResetColor();
                 total++;
                 totalQuantity+= product.Quantity;

                 //Maior estoque
                 if (highestStock == null || product.Quantity > highestStock.Quantity)
                    highestStock = product;

                if (lowestStock == null || product.Quantity < lowestStock.Quantity)
                    lowestStock = product;
                
            }
            Console.WriteLine("\n================ RESUMO ================\n");
            Console.WriteLine($"Total de produtos: {total}");
            Console.WriteLine($"Quantidade total em estoque: {totalQuantity}");
            Console.WriteLine($"Produtos críticos: {criticalCount}");

            if (highestStock != null)
            {
                Console.WriteLine($"\nMaior estoque: {highestStock.Name} - ({highestStock.Quantity})");
            }

            if (lowestStock != null)
            {
                Console.WriteLine($"Menor estoque: {lowestStock.Name} - ({lowestStock.Quantity})");
            }
        }

        private string ReadRequiredString(string message)
        {
            string input;

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while(string.IsNullOrWhiteSpace(input));
            return input;
        }

        private int ReadInt(string message)
        {
            int value;

            while(true)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out value))
                return value;

                Console.WriteLine("Valor inválido! Digite um número válido.");
            }
        }
        public void AddProductFromUser()
        {
            string name = ReadRequiredString("Nome do Produto: ");
            
            string brand = ReadRequiredString("Nome da Marca: ");

            int quantity = ReadInt("Quantidade: ");
           
            string origin = ReadRequiredString("Origem: ");

         

            var existingProduct = _repository.GetProductByNameAndBrandAndOrigin(name, brand, origin);

            if (existingProduct != null)
            {
                int newQuantity = existingProduct.Quantity + quantity;

                _repository.UpdateProduct(existingProduct.Id,newQuantity);

                Console.WriteLine("Produto já existente na lista. Quantidade atualizada.");
            }
            else
            {
                var product = new Product
                {
                    Name = name,
                    Brand = brand,
                    Quantity = quantity,
                    LastPurchase = DateTime.Now,
                    Origin = origin
                };
                _repository.AddProduct(product);

                Console.WriteLine("Produto cadastrado com sucesso!");
            }
        }

        public void ShowCriticalProducts()
        {
            var products = _repository.GetAllProducts();

            Console.WriteLine("\n=================================== PRODUTOS CRÍTICOS ===================================\n");

            Console.WriteLine($"{"Nome",-18} {"Marca",-18} {"Origem",-18} {"Qtd",-6} {"Data", -20} {"Status",-10}");
            Console.WriteLine(new string('-', 120));

            int count = 0;

            foreach (var product in products)
            { 
                string status = product.Quantity < 10 ? "CRÍTICO" : "OK";
                if (product.Quantity < 10)
                {   
                    
                    Console.WriteLine($"{product.Name,-18} {product.Brand,-18} {product.Origin,-18} {product.Quantity,-6} {product.LastPurchase:dd/MM/yyyy HH:mm,-20} {status,-10}");
                    count++;
                }
            }

            if (count == 0)
            {
                Console.WriteLine("Nenhum produto crítico encontrado.");
            }
            else
            {
                Console.WriteLine($"\nTotal de produtos críticos: {count}");
            }
        }

        public void SearchProductByName()
        {
            Console.Write("Digite o nome do produto: ");
            string name = Console.ReadLine();
            
            var products = _repository.SearchByName(name);

            if (products.Count == 0)
            {
                Console.WriteLine("Nenhum produto foi encontrado.");
                return;
            }
            
            Console.WriteLine("\n========== RESULTADO DA BUSCA ==========\n");
            foreach(var product in products)
            {
                {
                    Console.WriteLine($"{product.Name} | {product.Brand} | {product.Origin} | {product.Quantity}");
                }
                
            }
        }

        public void UpdateProductFromUser()
        {
            int id = ReadInt("Digite o ID do produto que deseja atualizar: ");

            var existingProduct = _repository.GetProductById(id);
            if (existingProduct == null)
            {
                Console.WriteLine("❌ Produto não encontrado!");
                return;
            }

            string name = ReadRequiredString("Novo nome: ");
            string brand = ReadRequiredString("Nova marca: ");
            int quantity = ReadInt("Nova quantidade: ");
            string origin = ReadRequiredString("Nova origem: ");

            var product = new Product
            {
                Id = id,
                Name = name,
                Brand = brand,
                Quantity = quantity,
                Origin = origin,
                LastPurchase = DateTime.Now
            };

            _repository.UpdateFullProduct(product);

            Console.WriteLine("✅ Produto atualizado com sucesso!");
        }

        public void DeleteProductFromUser()
        {
            int id = ReadInt("Digite o ID do produto que deseja deletar: ");

            var product = _repository.GetProductById(id);

            if (product == null)
            {
                Console.WriteLine("❌ Produto não encontrado!");
                return;
            }

            Console.WriteLine($"\nProduto encontrado: {product.Name} | {product.Brand} | {product.Quantity}");

            Console.WriteLine("Tem certeza que deseja deletar?(s/n) ");
            string confirm = Console.ReadLine().ToLower();

            if (confirm != "s")
            {
                Console.WriteLine("Operação cancelada!");
                return;
            }

            _repository.DeleteProduct(id);
            Console.WriteLine("Produto deletado com sucesso!");


        }
    }
}