using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using InventoryApp.Models;
using System.Linq;


namespace InventoryApp.ReportServices
{
    public class ReportService
    {
        public void GenerateReport(List<Product> products)
        {
            Console.Clear();

            PrintHeader();
            PrintSummary(products);
            PrintTable(products);

        }

        private void PrintHeader()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("              INVENTORY REPORT");
            Console.WriteLine("==============================================");
            Console.WriteLine($"Generated at: {DateTime.Now}");
            Console.WriteLine();

        }

        private void PrintSummary(List<Product> products)
        {
            int total = products.Count;
            int critical = products.Count(p => p.Quantity <= 5);
            
            Console.WriteLine("SUMMARY");
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Total Products: {total}");
            Console.WriteLine($"Critical Products: {critical}");
            Console.WriteLine();

        }

        private void PrintTable(List<Product> products)
        {
            Console.WriteLine("==========================");
            Console.WriteLine("      PRODUCT LIST");
            Console.WriteLine("==========================");

            Console.WriteLine("ID | Name      | Brand     | Qty | Status   | Last Purchase | Origin   ");
            Console.WriteLine("======================================================================");

            foreach (var p in products)
            {
                string status = p.Quantity <= 5 ? "CRITICAL" : "OK";

                Console.WriteLine(
                    $"{p.Id,-3}|" +
                    $" {p.Name,-10}|" +
                    $" {p.Brand,-10}|" +
                    $" {p.Quantity,-4}|" +
                    $" {status,-9}|" +
                    $" {p.LastPurchase, -13:dd/MM/yyyy} |" +
                    $" {p.Origin,-16}"
                );
            }
            Console.WriteLine();
        }
    }
}