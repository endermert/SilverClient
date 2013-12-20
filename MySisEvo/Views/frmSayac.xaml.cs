using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MySisEvo.Classes;
namespace MySisEvo.Views
{
    public partial class frmSayac : ChildWindow
    {
        public frmSayac()
        {
            InitializeComponent();
        }

        private void SayacGoster()
        {
            syc_serino.Text = Entities.Sayac.syc_serino;
            syc_tarih.Text = Entities.Sayac.syc_tarih;
            syc_saat.Text = Entities.Sayac.syc_saat;
            syc_gun.Text = Entities.Sayac.syc_gun;
            syc_govactar.Text = Entities.Sayac.syc_govactar;
            syc_kkactar.Text = Entities.Sayac.syc_kkactar;
            syc_kkacsay.Text = Entities.Sayac.syc_kkacsay;
            syc_enyukolcsur.Text = Entities.Sayac.syc_enyukolc;
            syc_dem0say.Text = Entities.Sayac.syc_demand0say;
            syc_demand.Text = Entities.Sayac.syc_demand;
            syc_urtar.Text = Entities.Sayac.syc_uretimtar;
            syc_kaliberetar.Text = Entities.Sayac.syc_kalibretar;
            syc_tarifedeg.Text = Entities.Sayac.syc_tarifedegtar;
            syc_fazkes1.Text = Entities.Sayac.syc_fazkessay1;
            syc_fazkes2.Text = Entities.Sayac.syc_fazkessay2;
            syc_fazkes3.Text = Entities.Sayac.syc_fazkessay3;
            syc_ucfazuyari.Text = Entities.Sayac.syc_fazkessaytop;
            syc_auyari.Text = Entities.Sayac.syc_Auyarisay;
            syc_vuyari.Text = Entities.Sayac.syc_Vuyarisay;
            syc_pil.Text = Entities.Sayac.syc_pildurumu;

            syc_t1akt.Text = Entities.Sayac.syc_t1akt;
            syc_t2akt.Text = Entities.Sayac.syc_t2akt;
            syc_t3akt.Text = Entities.Sayac.syc_t3akt;
            syc_takt.Text = Entities.Sayac.syc_takt;

            syc_t1ind.Text = Entities.Sayac.syc_t1ind;
            syc_t2ind.Text = Entities.Sayac.syc_t2ind;
            syc_t3ind.Text = Entities.Sayac.syc_t3ind;
            syc_tind.Text = Entities.Sayac.syc_tind;

            syc_t1kap.Text = Entities.Sayac.syc_t1kap;
            syc_t2kap.Text = Entities.Sayac.syc_t2kap;
            syc_t3kap.Text = Entities.Sayac.syc_t3kap;
            syc_tkap.Text = Entities.Sayac.syc_tkap;
            
        }
      
        
        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SayacGoster();
        }
    }
}

