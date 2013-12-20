using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using MySisEvo.Web.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MySisEvo.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AracServis
    {
        public AraclarPro AraclarPro = new AraclarPro();
        [OperationContract]
        public ObservableCollection<Araclar> getAraclar(string kulAd)
        {
            ObservableCollection<Araclar> arcList = new ObservableCollection<Araclar>();
            arcList = AraclarPro.getAraclarList(kulAd);
            return arcList;
        }
        [OperationContract]
        public void updateArac(Araclar arac)
        {
            AraclarPro.updateArac(arac);
        }



    }
}
