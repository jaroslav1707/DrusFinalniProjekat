using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Threading;

namespace MeasureClient
{

    [CallbackBehavior(UseSynchronizationContext = false)]
    
    class Measurer:ServiceReference1.IMeasureServiceCallback
    {
        private int id;
        private ServiceReference1.MeasureServiceClient measurer;
        public Measurer(int  id)
        {
            
            this.id = id;
            InstanceContext context = new InstanceContext(this);
            measurer = new ServiceReference1.MeasureServiceClient(context);
            Kreni_Merenje();
            
        }

        public int getId(){ return id;  }

        //ova metoda nije eksplicitno potrebna za klijente merace ali ukoliko
        //je potrebna ovde se moze implementirati
        public void NotifikacijaMerenja(int id,double value,string type)
        {

        }
  
        private void Kreni_Merenje()
        {
           int i=0;
            while(true)
            {
                i++;
                Thread.Sleep(2000);
                if (i % 6 == 0)     //svake 6 sekunde izmeri vlaznost
                { 
                    int humidity = Generisi_slucajan_broj(30, 95);
                    Console.WriteLine("Merac [ID = " + this.id + "]     " + "Izmerena vlaznost: " + humidity + " %.");
                    measurer.DodajMerenje(this.id, humidity, "Vlaznost");
                }
                int temp = Generisi_slucajan_broj(-5, 45);
                Console.WriteLine("Merac [ID = " + this.id + "]     " + "Izmerena temperatura: " + temp + " C.");
                measurer.DodajMerenje(this.id, temp, "Temperatura");
            }
        }

        private int Generisi_slucajan_broj(int low,int up)
        {
            int broj = new Random().Next(low,up);
            return broj;
        }
        
    }
}
