using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MonitorClient.ServiceReference1;

namespace MonitorClient
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    class MonitorClient : ServiceReference1.IMeasureServiceCallback
    {
        private ServiceReference1.MeasureServiceClient observer;

        public MonitorClient()
        {
            InstanceContext context = new InstanceContext(this);
            observer = new ServiceReference1.MeasureServiceClient(context);
            Kreni_posmatranje();
        }

        private void Kreni_posmatranje()
        {
            while (true)
            {
                Prikazi_meni();
                int odg = int.Parse(Console.ReadLine());

                switch(odg)
                {
                    case 1:
                        Console.WriteLine("Unesite ID meraca na koga zelite da se pretplatite!");
                        int id = int.Parse(Console.ReadLine());
                        observer.Pretplati(id);
                        break;
                    case 2:
                        Console.WriteLine("Unesite ID meraca sa koga zelite da se odjavite!");
                        int id2 = int.Parse(Console.ReadLine());
                        observer.Odjavi(id2);
                        break;
                    case 3:
                        Prikazi_izvestaje();
                        int izv = int.Parse(Console.ReadLine());
                        if(izv == 1)
                        {
                            //1. izvestaj
                            Console.WriteLine("Unesi ID meraca: ");
                            int stanica = int.Parse(Console.ReadLine());
                           
                            Console.WriteLine("Unesite datum od kada zelite merenja: [mm/dd/yyyy hh:mm:ss.stst]");
                            
                            String datum_od = Console.ReadLine();
                            DateTime dt = new DateTime();
                            try
                            {
                                dt = Convert.ToDateTime(datum_od);
                            }
                            catch
                            {
                                Console.WriteLine("Greska! Uneli ste pogresan datum!");
                                continue;
                            }
                            Console.WriteLine("Unesite datum do kada zelite merenja: [mm/dd/yyyy hh:mm:ss.stst]");
                            String datum_do = Console.ReadLine();
                            DateTime dt2 = new DateTime();
                            try
                            {
                                dt2 = Convert.ToDateTime(datum_do);
                            }
                            catch
                            {
                                Console.WriteLine("Greska! Uneli ste pogresan datum!");
                                continue;
                            }
                            PrikaziMerenjaSaStanice(stanica, dt, dt2, 3);
                            continue;
                        }
                        else if (izv == 2)
                        {
                            //2. izvestaj
                            Console.WriteLine("Unesi ID meraca: ");
                            int stanica = int.Parse(Console.ReadLine());

                            Console.WriteLine("Unesite datum od kada zelite merenja: [mm/dd/yyyy hh:mm:ss.stst]");

                            String datum_od = Console.ReadLine();
                            DateTime dt = new DateTime();
                            try
                            {
                                dt = Convert.ToDateTime(datum_od);
                            }
                            catch
                            {
                                Console.WriteLine("Greska! Uneli ste pogresan datum!");
                                continue;
                            }
                            Console.WriteLine("Unesite datum do kada zelite merenja: [mm/dd/yyyy hh:mm:ss.stst]");
                            String datum_do = Console.ReadLine();
                            DateTime dt2 = new DateTime();
                            try
                            {
                                dt2 = Convert.ToDateTime(datum_do);
                            }
                            catch
                            {
                                Console.WriteLine("Greska! Uneli ste pogresan datum!");
                                continue;
                            }

                            Console.WriteLine("Odaberite: \n1.Vlaznost\n2.Temperatura\n3.Oba");
                            int vrednost = int.Parse(Console.ReadLine());

                            PrikaziMerenjaSaStanice(stanica, dt, dt2, vrednost);
                            continue;
                        }
                        else if(izv == 3)
                        {
                            //3. izvestaj
                            Console.WriteLine("Unesite ID meraca: ");
                            int stanica = int.Parse(Console.ReadLine());
                            
                            Console.WriteLine("Odaberite: \n1.Vlaznost\n2.Temperatura");
                            int odabir = int.Parse(Console.ReadLine());
                            
                            Console.WriteLine("Odaberite:\n1. =< \n2. >= ");
                            int znak = int.Parse(Console.ReadLine());
                            
                            Console.WriteLine("Unesite vrednost: ");
                            double vrednost = double.Parse(Console.ReadLine());
                            
                            PrikaziLimitStanica(stanica, odabir, znak, vrednost );
                            continue;
                        }
                        else if(izv == 4)
                        {
                            //4. izvestaj
                            Console.WriteLine("Unesite naziv stanice: ");
                            String lokacija = Console.ReadLine();

                            Console.WriteLine("Odaberite: \n1.Vlaznost\n2.Temperatura");
                            int odabir = int.Parse(Console.ReadLine());

                            Console.WriteLine("Unesite datum od kada zelite merenja: [mm/dd/yyyy hh:mm:ss.stst]");

                            String datum_od = Console.ReadLine();
                            DateTime dt = new DateTime();
                            try
                            {
                                dt = Convert.ToDateTime(datum_od);
                            }
                            catch
                            {
                                Console.WriteLine("Greska! Uneli ste pogresan datum!");
                                continue;
                            }
                            Console.WriteLine("Unesite datum do kada zelite merenja: [mm/dd/yyyy hh:mm:ss.stst]");
                            String datum_do = Console.ReadLine();
                            DateTime dt2 = new DateTime();
                            try
                            {
                                dt2 = Convert.ToDateTime(datum_do);
                            }
                            catch
                            {
                                Console.WriteLine("Greska! Uneli ste pogresan datum!");
                                continue;
                            }

                            PrikaziProsek(odabir, lokacija, dt, dt2);
                            continue;
                        }
                        else if(izv == 5)
                        {
                            //5. izvestaj
                            Console.WriteLine("Unesite naziv lokacije");

                            String lokacija = Console.ReadLine();
                            Console.WriteLine("Odaberite: \n1.Vlaznost\n2.Temperatura");
                            int odabir = int.Parse(Console.ReadLine());

                            Console.WriteLine("Odaberite:\n 1. =< \n2. >= ");
                            int znak = int.Parse(Console.ReadLine());

                            Console.WriteLine("Unesite vrednost: ");
                            double vrednost = double.Parse(Console.ReadLine());
                            PrikaziLimitLokacija(lokacija, odabir, znak, vrednost);
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Greska! Pogresan odabir!");
                        }
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Greska! Pogresan odabir!");
                        break;
                }
            }
        }
        
        public void Prikazi_meni()
        {
            string meni = "\nOdaberite zeljenu opciju:";
            meni += "\n1. Pretplata na meraca.";
            meni += "\n2. Odjava pracenja meraca.";
            meni += "\n3. Trazenje izvestaja.";
            meni += "\n4. Izlaz.";
            Console.WriteLine(meni);
        }

        private void Prikazi_izvestaje()
        {
            String opcija = "Izvestaji:";
            opcija += "\n1. Prikupljanje podataka za sva merenja za odredjeni period.";
            opcija += "\n2. Prikupljanje podataka za zeljeno merenje za odredjeni period.";
            opcija += "\n3. Prikupljanje podataka o merenju kada je vrednost merenja bila veca/manja od zeljene vrednosti.";
            opcija += "\n4. Racunanje proseka za odredjeno merenje za odredjeni period zeljenog meraca.";
            opcija += "\n5. Merenja kada je vrednost merenja bila veca/manja od zeljene vrednosti za odredjenu lokaciju.\n";
            Console.Write(opcija);
        }

        public void NotifikacijaMerenja(int id, double value, string type)//promeni naziv
        {
            PrikaziStatusPosmatraca("Sa meraca [" + id + "] " + type + " = " + value);
        }

        private void PrikaziMerenjaSaStanice(int stationId, DateTime dateFrom, DateTime dateUntil, int type)
        {
            Console.Write(observer.VratiSvaMerenjaMeraca(stationId, dateFrom, dateUntil, type));
        }
        
        private void PrikaziLimitStanica(int stationId, int type, int limitType, double limit)
        {
            Console.Write(observer.VratiSveTrenutkeGdeJeMerenje(stationId, type, limitType, limit));
        }

        private void PrikaziLimitLokacija(String location, int type, int limitType, double limit)
        {
            Console.Write(observer.VratiSveTrenutkeGdeJeMerenjeNaLokaciji(location, type, limitType, limit));
        }

        private void PrikaziProsek(int type, String locationName, DateTime dFrom, DateTime dUntil)
        {
            Console.Write(observer.ProsekNaLokaciji(locationName, type, dFrom, dUntil));
        }

        private void PrikaziStatusPosmatraca(string status)
        {
            Console.WriteLine("Posmatrac : " + status);
        }
    }
}
