using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using MySisEvo.Web.Classes;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace MySisEvo.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataServis
    {
        private DatasPro DataPro = new DatasPro();
        [OperationContract]
        public Datas getData(int serino)
        {
            Datas data1 = new Datas();
            data1 = DataPro.getData(serino);

            return data1;
        }

        [OperationContract]
        public ObservableCollection<Datas> getDatas(List<String> serinolar)
        {
            ObservableCollection<Datas> data1 = new ObservableCollection<Datas>();
            data1 = DataPro.getData(serinolar);
            return data1;
        }

        [OperationContract]
        public ObservableCollection<Datas> getDataList(int serino, DateTime basTar, DateTime bitTar)
        {
            ObservableCollection<Datas> data1 = new ObservableCollection<Datas>();
            data1 = DataPro.getData(serino, basTar, bitTar);
            return data1;
        }
        
    }
}

