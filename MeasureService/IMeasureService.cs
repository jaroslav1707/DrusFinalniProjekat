using System.ServiceModel;
using System.Collections.Generic;
using System;


namespace MeasureService
{
    
    [ServiceContract(CallbackContract = typeof(IMeasureServiceCallback))]
    public interface IMeasureService
    {
        [OperationContract(IsOneWay=true)]
        void Pretplati(int id);

        [OperationContract(IsOneWay = true)]
        void Odjavi(int id);

        [OperationContract]
        void DodajMerenje(int id,int value,string type);

        [OperationContract]
        String VratiSvaMerenjaMeraca(int measurerId, DateTime from, DateTime until,int type);

        [OperationContract]
        String VratiSveTrenutkeGdeJeMerenje(int measurerId,int type, int limitType, double limit);

        [OperationContract]
        String VratiSveTrenutkeGdeJeMerenjeNaLokaciji(String locationName, int type, int limitType, double limit);

        [OperationContract]
        String ProsekNaLokaciji(String locationName, int type, DateTime dateFrom, DateTime dateUntil);
  
    }
}