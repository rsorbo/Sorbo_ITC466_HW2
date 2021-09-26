using System;
using System.Collections.Generic;


namespace HW2
{
    class Program
    {
        static void Main(string[] args)
        {
            var appointment = new Appointment()
            {
                Name = "Bob",
                StartDateTime = DateTime.Now.AddHours(1),
                EndDateTime = DateTime.Now.AddHours(2),
                Price = 100D
            };

            var book = new Book()
            {
                Title = "How to Implement Interfaces",
                Price = 50D,
                TaxRate = 0.0825D,
                ShippingRate = 5D
            };

            var snack = new Snack()
            {
                Price = 2D
            };

            var tshirt = new TShirt()
            {
                Size = "2X",
                Price = 25D,
                TaxRate = 0.0625D,
                ShippingRate = 2D
            };

            var items = new List<IPurchasable>();
            items.Add(appointment);
            items.Add(book);
            items.Add(snack);
            items.Add(tshirt);

            double subTotal = CalculateSubTotal(items);


            var taxableItems = new List<ITaxable>();
            foreach (var item in items)
            {
                if (item is ITaxable)
                {
                    taxableItems.Add(item as ITaxable);
                }
            }
            var taxAmount = CalculateTax(taxableItems);
            Console.WriteLine($"Total tax amount: {taxAmount.ToString("C")}");
            Console.WriteLine();

            var shipItems = new List<IShippable>();
            shipItems.Add(book);
            shipItems.Add(tshirt);


            var shipAmount = CalculateShipping(shipItems);
            Console.WriteLine($"Total shipping amount: {shipAmount.ToString("C")}");
            Console.WriteLine();


            CompleteTransaction(items);



            double grandTotal = taxAmount + shipAmount + subTotal;
            Console.WriteLine($"Grand Total : {grandTotal.ToString("C")}");

            Console.ReadLine();
        }

        private static double CalculateSubTotal(List<IPurchasable> items)
        {

            double subTotal = 0;

            foreach (var item in items)
            {
                subTotal += item.SubTotal();
            }

            return subTotal;
        }

        static double CalculateTax(List<ITaxable> items)
        {
            double tax = 0D;

            foreach (var item in items)
            {
                tax += item.Tax();
            }

            return tax;

        }

        static double CalculateShipping(List<IShippable> items)
        {
            double shipping = 0D;

            foreach (var item in items)
            {
                shipping += item.Ship();
            }

            return shipping;
        }

        static void CompleteTransaction(List<IPurchasable> items)
        {

            items.ForEach(p => p.Purchase());


            Console.WriteLine("==========");

        }


    }

    public class Appointment : IPurchasable
    {
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double Price { get; set; }

        public string Purchase()
        {
            Console.WriteLine($"Payment for Appointment for {Name} from {StartDateTime} to {EndDateTime} for {Price.ToString("C0")}.");
            return $"{Name}, {Price}";
        }

        public double SubTotal()
        {
            return Price;
        }
    }

    public class Book : IPurchasable, ITaxable, IShippable
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public double TaxRate { get; set; }
        public double ShippingRate { get; set; }

        public string Purchase()
        {
            Console.WriteLine($"Purchasing {Title} for {Price.ToString("C0")}.");
            return $"{Title}, {Price}";
        }

        public double Ship()
        {
            Console.WriteLine($"\tShipping Rate: {ShippingRate.ToString("C")}");
            return ShippingRate;
        }

        public double Tax()
        {
            var tax = Price * TaxRate;
            Console.WriteLine($"\tTaxRate: {TaxRate} = {tax}");
            return tax;
        }
        public double SubTotal()
        {
            return Price;
        }
    }

    public class TShirt : IPurchasable, ITaxable, IShippable
    {
        public double Price { get; set; }
        public string Size { get; set; }
        public double TaxRate { get; set; }
        public double ShippingRate { get; set; }

        public string Purchase()
        {
            Console.WriteLine($"Purchasing TShirt {Size} for {Price.ToString("C0")}.");
            return $"{Size}, {Price}";
        }

        public double Ship()
        {
            Console.WriteLine($"\tShipping Rate: {ShippingRate.ToString("C")}");
            return ShippingRate;
        }

        public double Tax()
        {
            var tax = Price * TaxRate;
            Console.WriteLine($"\tTaxRate: {TaxRate} = {tax}");
            return tax;
        }
        public double SubTotal()
        {
            return Price;
        }
    }

    public class Snack : IPurchasable
    {
        public double Price { get; set; }

        public string Purchase()
        {
            Console.WriteLine($"Purchasing a snack for {Price.ToString("C0")}.");
            return $"{Price}";
        }
        public double SubTotal()
        {
            return Price;
        }
    }

    interface IPurchasable
    {
        double Price { get; set; }

        double SubTotal();

        string Purchase();
    }

    interface IShippable
    {
        double ShippingRate { get; set; }
        double Ship();
    }

    interface ITaxable
    {
        double TaxRate { get; set; }
        double Tax();
    }

}



