using System;
using System.Linq;
using LINQ.Entites;
using System.Collections.Generic;

namespace LINQ
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> colletion)
        {
            Console.WriteLine(message);
            foreach (T obj in colletion) 
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Category c1 = new Category()
            { Id = 1, Name = "Tools", Tier = 1 };
            Category c2 = new Category()
            { Id = 2, Name = "Computers", Tier = 2 };
            Category c3 = new Category()
            { Id = 3, Name = "Eletronics", Tier = 1 };

            List<product> products = new List<product>() {
                 new product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new product() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }
            };

            //CLASULA WHERE COM SELECT SQL
            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900);
            Print("TIER 1 AND PRICE < 900: ", r1);

            var r2 = products.Where(p => p.Category.Name == "Tools")
                .Select(n => n.Name);
            Print("NAMES OF PRODUCTS FROM TOOLS", r2);

            var r3 = products.Where(p => p.Name[0] == 'C')
                .Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name });
            Print("NAMES STARTED WITH 'C' AND ANYNOMOUS: ", r3);

            //CLAUSULA WHERE COM ORDER SQL
            var r4 = products.Where(p => p.Category.Tier == 1)
                .OrderBy(p => p.Category).ThenBy(p => p.Name);
            Print("TIER 1, ORDER PRICE AND NAME : ", r4);

            // CLAUSULA WHERE COM PULA UM E ADICIONA O SEGUINTE
            var r5 = r4.Skip(2).Take(4);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKUPE 2 TAKE 4: ", r5);


            //CLAUSULA WHERE COM PEGUE O PRIMEIRO
            var r6 = products.First();
            Console.WriteLine("FIRTS TEST 1: ", r6);

            //CLAUSULA WHERE COM PEGUE O PRIMEIRO OU NENHUM
            var r7 = products.Where(p => p.Price > 3000.0)
                .FirstOrDefault();
            Console.WriteLine("FIRTS TEST 2: ", r7);

            //CLAUSULA WHERE COM PEGUE O ÚNICO OU NENHUM
            var r8 = products.Where(p => p.Id == 3)
                .SingleOrDefault();
            Console.WriteLine("SINGLE OR DEFAULT: ", r8);

            var r9 = products.Where(p => p.Id == 30)
                .SingleOrDefault();
            Console.WriteLine("SINGLE OR DEFAULT FAILURE TEST: ", r9);

            //CLAUSULA WHERE COM NÚMERO MÁXIMO
            var r10 = products.Max(p => p.Price);
            Console.WriteLine("MAX PRICE: ", r10);

            //CLAUSULA WHERE COM NÚMERO MÍNIMO
            var r11 = products.Min(p => p.Price);
            Console.WriteLine("MIN PRICE: ", r11);

            //CLAUSULA WHERE COM SOMA DE PREÇOS
            var r12 = products.Where(p => p.Category.Id == 1)
                .Sum(p => p.Price);
            Console.WriteLine("CATEGORY ID 1", r12);

            //CLAUSULA WHERE COM MÉDIA DE PREÇOS (OBS: NECESSARIO TRATAR ESTÁ CLAUSULA)
            var r13 = products.Where(p => p.Category.Id == 1)
                .Average(p => p.Price);
            Console.WriteLine("AVERAGE ID 1", r13);

            //TRATANDO MÉDIA COM DEFAULT EMPTY
            var r14 = products.Where(p => p.Category.Id == 5)
                .Select(p => p.Price).DefaultIfEmpty();
            Console.WriteLine("Category 5 Average Prices: " + r14);

            //CLAUSULA WHERE COM SOMA PERSONALIZADA E TRATAMENTO DE EXCEÇÃO
            var r15 = products.Where(p => p.Category.Id == 1)
                .Select(p => p.Price)
                .Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("CATEGORY 1 AGREGATE: ", r15);

            //AGRUPAMENTO DE SELEÇÃO
            var r16 = products.GroupBy(p => p.Category);
            foreach(IGrouping<Category, product> group in r16)
            {
                Console.WriteLine("CATEGORY " + group.Key.Name + ":");
                foreach(product p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }
    }
}
