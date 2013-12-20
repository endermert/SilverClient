using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using MySisEvo.Web.Classes;
namespace MySisEvo.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class KullaniciServis
    {
        public Kullanici kul= new Kullanici();
        public KullaniciPro kulPro =new KullaniciPro();

        [OperationContract]
        public Kullanici getKullanici(string kulAd,string kulSifre)
        {
            kul = kulPro.getKullanici(kulAd, kulSifre);
            return kul;
        }
        
    }

   
}
