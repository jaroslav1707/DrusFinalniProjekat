using System.ServiceModel;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MeasureService
{
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Reentrant)]
    public class MeasureService : IMeasureService
    {
     
        private static MeasuringDBEntities db = new MeasuringDBEntities();
        private static Dictionary<int, List<IMeasureServiceCallback>> observers = new Dictionary<int, List<IMeasureServiceCallback>>();

        public void Pretplati(int id)
        {
            IMeasureServiceCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IMeasureServiceCallback>();

            if (!observers.ContainsKey(id))
            {
                observers[id] = new List<IMeasureServiceCallback>();
                Console.WriteLine("Ne postoji id.");
            }
            else
            {
                observers[id].Add(callbackChannel);
            }
        }
        public void Odjavi(int id)
        {
            IMeasureServiceCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IMeasureServiceCallback>();
            observers[id].Remove(callbackChannel);
        }

        public void DodajMerenje(int id,int value,string type)
        {
            if (observers.ContainsKey(id))
            {
                foreach (IMeasureServiceCallback callbackChannel in observers[id])
                {
                    callbackChannel.NotifikacijaMerenja(id,value,type);
                }
            }
            else
            {
                observers.Add(id, new List<IMeasureServiceCallback>());
            }
            SacuvajMerenja(id, value, type, DateTime.Now);
        }

        public String VratiSvaMerenjaMeraca(int measurerId, DateTime fromDate, DateTime untilDate, int type)
        {
            String typeStr="";
            List<Measurement> measurements = new List<Measurement>();
            String jedinica_mere = "";
            if (type == 1)
            {
                typeStr = "Vlaznost";
                jedinica_mere = "[%]";
                measurements = (from m in db.Measurements
                          where m.MeasuringStation.Id == measurerId &&
                                       m.Time > fromDate &&
                                       m.Time < untilDate &&
                                       m.Type.Equals(typeStr)
                          select m).ToList<Measurement>();
            }
            else if (type == 2)
            {
                typeStr = "Temperatura";
                jedinica_mere = "[C*]";
                measurements = (from m in db.Measurements
                          where m.MeasuringStation.Id == measurerId &&
                                       m.Time > fromDate &&
                                       m.Time < untilDate &&
                                       m.Type.Equals(typeStr)
                           select m).ToList<Measurement>();
            }
            else
            {
                if (type == 3)
                {
                    measurements = (from m in db.Measurements
                              where m.MeasuringStation.Id == measurerId &&
                                           m.Time > fromDate &&
                                           m.Time < untilDate
                                   select m).ToList<Measurement>();
                }
                else
                {
                    Console.WriteLine("Greska! Pogresan unos.");
                }
            }

            String retval = "";
            for (var i = 0; i < measurements.Count; i++)
            {
                if(measurements[i].Type == "Temperatura")
                {
                    jedinica_mere = "[C*]";
                }
                else
                {
                    jedinica_mere = "[%]";
                }

                retval += " [Id: " + measurements[i].Id + "]" + " Tip : " + measurements[i].Type + " Vrednost : " + measurements[i].Value + jedinica_mere + "\n";
            }
            return retval;
        }

        public String VratiSveTrenutkeGdeJeMerenje(int measurerId,int type, int limitType, double limit)
        {
            List<Measurement> measurements=new List<Measurement>();
            String typeStr="";
            String retVal="";
            String jedinica_mere = "";
            if (type == 2)
            {
                typeStr = "Temperatura";
                jedinica_mere = "[C*]";

            }
            else if(type == 1)
            {
                typeStr = "Vlaznost";
                jedinica_mere = "[%]";
            }
  
            if (limitType == 1)
            {
                Console.WriteLine("1");
                measurements = db.Measurements.
                Where(m => m.MeasuringStation.Id == measurerId
                        && m.Type.Equals(typeStr)
                        && m.Value < limit).ToList();
            }
            else if (limitType == 2)
            {
                Console.WriteLine("2");
                measurements = db.Measurements.
                Where(m => m.MeasuringStation.Id == measurerId
                        && m.Type.Equals(typeStr)
                        && m.Value > limit).ToList();
            }
            //ovde mozda dodas else

            foreach (Measurement measure in measurements)
            {
                retVal += "[Id: " + measure.Id + "]" + "    Tip: " + measure.Type + "   Vrednost: " + measure.Value + jedinica_mere + " Vreme: " +  measure.Time +"\n";
            }

            return retVal;
        }

        public String VratiSveTrenutkeGdeJeMerenjeNaLokaciji(String locationName, int type, int limitType, double limit)
        {
            List<Measurement> measurements = new List<Measurement>();
            String typeStr = "";
            String retVal = "";
            String jedinica_mere = "";
            if (type == 2)
            {
                typeStr = "Temperatura";
                jedinica_mere = "[C*]";

            }
            else if (type == 1)
            {
                typeStr = "Vlaznost";
                jedinica_mere = "[%]";
            }
            
            if (limitType == 1)
            {
                Console.WriteLine("1");
                measurements = db.Measurements.
                Where(m => m.MeasuringStation.Location.Address.Equals(locationName)
                        && m.Type.Equals(typeStr)
                        && m.Value < limit).ToList();
            }
            else if (limitType == 2)
            {
                Console.WriteLine("2");
                measurements = db.Measurements.
                Where(m => m.MeasuringStation.Location.Address.Equals(locationName)
                        && m.Type.Equals(typeStr)
                        && m.Value > limit).ToList();
            }

            foreach (Measurement measure in measurements)
            {
                retVal += "[Id: " + measure.Id + "]" + "    Tip: " + measure.Type + "   Vrednost: " + measure.Value + jedinica_mere + " Vreme: " + measure.Time + "\n";
            }

            return retVal;
        }


        public String ProsekNaLokaciji(String locationName, int type,DateTime dateFrom,DateTime dateUntil)
        {
            List<Measurement> measurements = new List<Measurement>();
            String typeStr = "";
            String jedinica_mere = "";
            if (type == 2)
            {
                typeStr = "Temperatura";
                jedinica_mere = "[C*]";
            }
            else if (type == 1)
            {
                typeStr = "Vlaznost";
                jedinica_mere = "[%]";
            }
            
            double AverageResult = db.Measurements.Where(m => m.MeasuringStation.Location.Address.Equals(locationName)
                                                                && m.Type.Equals(typeStr)
                                                                && m.Time>dateFrom
                                                                && m.Time<dateUntil)
                                                                .Average(m =>m.Value);

            return "[Ime Lokacije: " + locationName + "]" + "   Tip: " + typeStr + "    Prosek:" + AverageResult + jedinica_mere;
        }
        
        private void SacuvajMerenja(int id, double value, string type, DateTime currentTime)
        {
            Measurement measurement = new Measurement();
            MeasuringStation measuringStation = (from station in db.MeasuringStations.ToList() where station.Id == id select station).FirstOrDefault();

            measurement.Type = type;
            measurement.Value = value;
            measurement.MeasuringStation = measuringStation;
            measurement.Time = currentTime;

            db.AddToMeasurements(measurement);
            db.SaveChanges();
        }
    }
}