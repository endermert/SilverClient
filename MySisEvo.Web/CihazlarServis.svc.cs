using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using MySisEvo.Web.Classes;
using System.Collections.ObjectModel;
namespace MySisEvo.Web
{
    [ServiceContract(Namespace = "")]
    [SilverlightFaultBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CihazlarServis
    {
        public CihazlarPro CihazlarPro = new CihazlarPro();
        [OperationContract]
        public ObservableCollection<Cihazlar> getCihazlar(string kulAd)
        {
            ObservableCollection<Cihazlar> arcList = new ObservableCollection<Cihazlar>();
           
            arcList = CihazlarPro.getAraclarList(kulAd);
            return arcList;
        }

        public AraclarPro AraclarPro = new AraclarPro();
        [OperationContract]
        public ObservableCollection<Araclar> getAraclar(string kulAd)
        {
            ObservableCollection<Araclar> arcList = new ObservableCollection<Araclar>();
            arcList = AraclarPro.getAraclarList(kulAd);
            return arcList;
        }

        public Kullanici kul = new Kullanici();
        public KullaniciPro kulPro = new KullaniciPro();

        [OperationContract]
        public Kullanici getKullanici(string kulAd, string kulSifre)
        {
            kul = kulPro.getKullanici(kulAd, kulSifre);
            return kul;
        }

        public RolePro rp=new RolePro();
        [OperationContract]
        public String[] DegerAyikla(string value)
        {
            return rp.DegerAyikla(value);
        }
        
        [OperationContract]
        public Role getAniDegerler(string value)
        {
            return rp.getAniDegerler(value);
        }

        SayacPro sycpro = new SayacPro();
        [OperationContract]
        public Sayac getSayacDegerler(string value)
        {
            return sycpro.getSayacDegerleri(value);
        }
    }
}
