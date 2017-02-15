using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
namespace MeasureClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Unesite ID meraca: ");
            int broj=int.Parse(Console.ReadLine());
            while (broj != 0)
            {
                Measurer cl = new Measurer(broj);
                broj = int.Parse(Console.ReadLine());
            }

           
        }
    }
}
