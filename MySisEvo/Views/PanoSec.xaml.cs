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
using System.Windows.Navigation;
using Telerik.Windows.Controls;
using MySisEvo.Classes;
using MySisEvo.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.Gauges;

namespace MySisEvo.Views
{
    public partial class PanoSec : RadWindow
    {
        //DispatcherTimer timerr = new DispatcherTimer();
        
        DispatcherTimer timerr2 = new DispatcherTimer();
        DispatcherTimer timerr3 = new DispatcherTimer();

       

        public PanoSec()
        {
            InitializeComponent();

           

        }
        string ham;
        string hamveri;
        string veri, veriilk;
        string istenen;
        int[,] degerler = new int[250, 500];
        int basadres;
        int no, islem, adet;
        int j, i, a1, a2, toplam;
        short svr;
        ushort uvr;
        private SocketAsyncEventArgs MyAsenkronOlay;
        private string gelen;
        private byte[] ServerData;
        public delegate void SetCallBack(string Textim);
        double usvr,indoran,kaporan;
        int konreg;
        uint cbxdeger;
        uint cbxdeger2;
        uint cbxdeger3;
        uint cbxdeger4;
        uint faz1, faz2, faz3;
       
        



        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {


            string input = System.Windows.Browser.HtmlPage.Window.Invoke("prompt", new string[] { "The title!", "Please input a value" }) as string;

            if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show(input);
            }

            for (int i = 0; i < Entities.CihazlarList.Count; i++)
            {
                if (Entities.Arac.ARC_SERINO == Entities.CihazlarList[i].CHZ_SERINO)
                {
                    if (Entities.CihazlarList[i].CHZ_CIHAZAGNO != null)
                    {
                        //MessageBox.Show(Entities.CihazlarList[i].CHZ_CIHAZAGNO.ToString());
                        if (Entities.tablelist == null)
                            Entities.tablelist = new clientfonksiyon();
                        Entities.tablelist.CIHAZAGNO = Entities.CihazlarList[i].CHZ_CIHAZAGNO.ToString();
                        Entities.tablelist.CIHAZADI = Entities.CihazlarList[i].CHZ_CIHAZAD.ToString();
                    }
                }
            }
            
            
            
            
            tbitemana.Header = Entities.tablelist.CIHAZADI;

            txtseri.Text = Entities.Arac.ARC_SERINO.ToString();
            txttime.Text = DateTime.Now.ToShortTimeString();
            Entities.tablelist = new clientfonksiyon();
            SrvIslemler.NormalGonder("enertum" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
            
            timerr2.Interval = TimeSpan.FromMilliseconds(3000);
            timerr2.Tick += new EventHandler(timerr2_Tick);
            timerr2.Start();
            timerr3.Interval = TimeSpan.FromMilliseconds(6000);
            timerr3.Tick += new EventHandler(timerr3_Tick);
            timerr3.Start();
             

        }
        void timerr3_Tick(object sender, EventArgs e)
        {
            SrvIslemler.NormalGonder("kondasatordurum" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
            timerr2.Interval = TimeSpan.FromMilliseconds(3000);
            timerr2.Tick += new EventHandler(timerr2_Tick);
            timerr2.Start();
           
        }
     
    

        void timerr2_Tick(object sender, EventArgs e)
        {
           
           
            if (Entities.tablelist != null)
            {
              
                    lblvr.Text = Entities.tablelist.LBL_VR;
                    lblvs.Text = Entities.tablelist.LBL_VS;
                    lblvt.Text = Entities.tablelist.LBL_VT;
                    lblIr.Text = Entities.tablelist.LBL_IR;
                    lblIs.Text = Entities.tablelist.LBL_IS;
                    lblIt.Text = Entities.tablelist.LBL_IT;
                    lblkwr.Text = Entities.tablelist.LBL_KWR;
                    lblkws.Text = Entities.tablelist.LBL_KWS;
                    lblkwt.Text = Entities.tablelist.LBL_KWT;
                    lblkwet.Text = Entities.tablelist.LBL_KWET;
                    lblkvarr.Text = Entities.tablelist.LBL_KVARR;
                    lblkvars.Text = Entities.tablelist.LBL_KVARS;
                    lblkvart.Text = Entities.tablelist.LBL_KVART;
                    lblkvar.Text = Entities.tablelist.LBL_KVAR;
                    lblkvas.Text = Entities.tablelist.LBL_KVAS;
                    lblkvat.Text = Entities.tablelist.LBL_KVAT;
                    lblkvaet.Text = Entities.tablelist.LBL_KVAET;
                    lblcos.Text = Entities.tablelist.LBL_COS;
                    lblcos1.Text = Entities.tablelist.LBL_COS1;
                    lblcos2.Text = Entities.tablelist.LBL_COS2;
                    lblcos3.Text = Entities.tablelist.LBL_COS3;
                    lblfrekans.Text = Entities.tablelist.LBL_FREKANS;
                    lblsicaklik.Text = Entities.tablelist.LBL_SICAKLIK;
                    lblsnonr.Text = Entities.tablelist.LBL_SNONR;
                    lblsnons.Text = Entities.tablelist.LBL_SNONS;
                    lblsnont.Text = Entities.tablelist.LBL_SNONT;
                    lblsnoffr.Text = Entities.tablelist.LBL_SNOFFR;
                    lblsnoffs.Text = Entities.tablelist.LBL_SNOFFS;
                    lblsnofft.Text = Entities.tablelist.LBL_SNOFFT;
                    lblsnonet.Text = Entities.tablelist.LBL_SNONET;
                    lblsnoffet.Text = Entities.tablelist.LBL_SNOFFET;
                    lblinaket.Text = Entities.tablelist.LBL_AKTIFGUC;
                    lblinaks.Text = Entities.tablelist.LBL_INAKS;
                    lblkaaks.Text = Entities.tablelist.LBL_KAAKS;
                    lblinakt.Text = Entities.tablelist.LBL_INAKT;
                    lblkaakt.Text = Entities.tablelist.LBL_KAAKT;
                    lblinaket.Text = Entities.tablelist.LBL_INAKET1;
                    lblkaaket.Text = Entities.tablelist.LBL_KAAKET;
                    //needle.Value = Convert.ToDouble(lblcos.Text);
                    lblinakr.Text = Entities.tablelist.LBL_INAKR;
                    lblkaakr.Text = Entities.tablelist.LBL_KAAKR;
                    kwht.Text = Entities.tablelist.KWHT;
                    // secili index = 2  durumuna göre ...
                    cbxdeger = Convert.ToUInt32(Entities.tablelist.LBL_CBXDURUM);
                    //-------------
                    kvarlr.Text = Entities.tablelist.KVARLR;
                    kvarls.Text = Entities.tablelist.KVARLS;
                    kvarlt.Text = Entities.tablelist.KVARLT;
                    kwhet.Text = Entities.tablelist.KWHET;
                    kvarlet.Text = Entities.tablelist.KVARLET;
                    kwhr.Text = Entities.tablelist.KWHR;

                    kwhs.Text = Entities.tablelist.KWHS;
                    kvarcr.Text = Entities.tablelist.KVARCR;
                    kvarcs.Text = Entities.tablelist.KVARCS;
                    kvarct.Text = Entities.tablelist.KVARCT;
                    kvarcet.Text = Entities.tablelist.KVARCET;
                    toplamindoran.Text = Entities.tablelist.TOPLAMINDORAN;
                    toplamkaporan.Text = Entities.tablelist.TOPLAMKAPORAN;
                    //txtdatam.Text = Entities.tablelist.abcd;
                    lblaktrafosu.Text = Entities.tablelist.LBL_AKIMTRAFOSU;
                   if(Entities.tablelist.TANFI!=null)
                    txttanfi.Text = Entities.tablelist.TANFI;
                   if (Entities.tablelist.INDCEZA != null)
                    txtindceza.Text = Entities.tablelist.INDCEZA;
                   if (Entities.tablelist.KAPCEZA != null)
                    txtkapceza.Text = Entities.tablelist.KAPCEZA;
                   if (Entities.tablelist.MAXALMASURE != null)
                    txtmaxkadalmasure.Text = Entities.tablelist.MAXALMASURE;
                   if (Entities.tablelist.MAXCIKMASURE != null)
                    txtmaxkdmcikmasure.Text = Entities.tablelist.MAXCIKMASURE;
                   if (Entities.tablelist.MINALCIKSURE != null)
                    txtminalmaciksure.Text = Entities.tablelist.MINALCIKSURE;
                   if (Entities.tablelist.SICAKLIKALARM != null)
                    txtsicaklikalarm.Text = Entities.tablelist.SICAKLIKALARM;
                   if (Entities.tablelist.FANALARM != null)
                    txtfanlarm.Text = Entities.tablelist.FANALARM;
                   if (Entities.tablelist.YUKSEKGERILIM != null)
                    txtasirigerilim.Text = Entities.tablelist.YUKSEKGERILIM;
                   if (Entities.tablelist.DUSUKGERILIM != null)
                    txtdusukgerilim.Text = Entities.tablelist.DUSUKGERILIM;
                   if (Entities.tablelist.YUKSEKTHDV != null)
                    txt_thdv.Text = Entities.tablelist.YUKSEKTHDV;
                   if (Entities.tablelist.YUKSEKHDV != null)
                    txt_hdv.Text = Entities.tablelist.YUKSEKHDV;
                   if (Entities.tablelist.YUKSEKTHDC != null)
                    txt_thdc.Text = Entities.tablelist.YUKSEKTHDC;
                   if (Entities.tablelist.YUKSEKHDC != null)
                    txt_hdc.Text = Entities.tablelist.YUKSEKHDC;

                    if ((Convert.ToInt32(Entities.tablelist.AKIMYONOGREN)) == 1)
                    {
                        txtakimyonogren.Text = "Devrede";


                    }
                    else { txtakimyonogren.Text = "Devrede Deðil"; }

                    if ((Convert.ToInt32(Entities.tablelist.KONDSOGREN)) == 1)
                    {
                        txtkondsogrn.Text = "Devrede";


                    }
                    else { txtkondsogrn.Text = "Devrede Deðil"; }



                    kwhr1.Text = Entities.tablelist.KWHR;
                    kwhs1.Text = Entities.tablelist.KWHS;
                    kwht1.Text = Entities.tablelist.KWHT;
                    kwhet1.Text = Entities.tablelist.KWHET;
                    kvarlr1.Text = Entities.tablelist.KVARLR;
                    kvarls1.Text = Entities.tablelist.KVARLS;
                    kvarlt1.Text = Entities.tablelist.KVARLT;
                    kvarlet1.Text = Entities.tablelist.KVARLET;
                    kvarcr1.Text = Entities.tablelist.KVARCR;
                    kvarcs1.Text = Entities.tablelist.KVARCS;
                    kvarct1.Text = Entities.tablelist.KVARCT;
                    kvarcet1.Text = Entities.tablelist.KVARCET;
                    topindoran.Text = Entities.tablelist.TOPLAMINDORAN;
                    topkaporan.Text = Entities.tablelist.TOPLAMKAPORAN;

                    needle3.Value = Convert.ToDouble(Entities.tablelist.LBL_VR);
                    lblak1.Text = Entities.tablelist.AK1;
                    l1gerilim.Text = Entities.tablelist.LBL_VR;
                    l2gerilim.Text = Entities.tablelist.LBL_VS;
                    l3gerilim.Text = Entities.tablelist.LBL_VT;
                    l1akim.Text = Entities.tablelist.LBL_IR;
                    l2akim.Text = Entities.tablelist.LBL_IS;
                    l3akim.Text = Entities.tablelist.LBL_IT;
                    l1aktifguc.Text = Entities.tablelist.LBL_KWR;
                    l2aktifguc.Text = Entities.tablelist.LBL_KWS;
                    l3aktifguc.Text = Entities.tablelist.LBL_KWT;
                    l1reaktifguc.Text = Entities.tablelist.LBL_KVARR;
                    l2reaktifguc.Text = Entities.tablelist.LBL_KVARS;
                    l3reaktifguc.Text = Entities.tablelist.LBL_KVART;
                    l1gorunenguc.Text = Entities.tablelist.LBL_KVAR;
                    l2gorunenguc.Text = Entities.tablelist.LBL_KVAS;
                    l3gorunenguc.Text = Entities.tablelist.LBL_KVAT;
                    l1cos.Text = Entities.tablelist.LBL_COS1;
                    l2cos.Text = Entities.tablelist.LBL_COS2;
                    l3cos.Text = Entities.tablelist.LBL_COS3;
                    txbfrekans.Text = Entities.tablelist.LBL_FREKANS;
                    txtsicaklik.Text = Entities.tablelist.LBL_SICAKLIK;
                    txtinak1.Text = Entities.tablelist.LBL_INAKR;
                    txtinak2.Text = Entities.tablelist.LBL_INAKS;
                    txtinak3.Text = Entities.tablelist.LBL_INAKT;
                    txtkaak1.Text = Entities.tablelist.LBL_KAAKR;
                    txtkaak2.Text = Entities.tablelist.LBL_KAAKS;
                    txtkaak3.Text = Entities.tablelist.LBL_KAAKT;
                    txttop1.Text = Entities.tablelist.LBL_RL;
                    txttop2.Text = Entities.tablelist.LBL_RC;
                    txttop3.Text = Entities.tablelist.LBL_DELTAR;
                    txtato.Text = Entities.tablelist.LBL_AKIMTRAFOSU;
                    scyaktif1.Text = Entities.tablelist.SYCAKTIF;
                    sycind1.Text = Entities.tablelist.SYCINDKTF;
                    syckap1.Text = Entities.tablelist.SYCKPSTIF;
                    txt_ind_oran.Text = Entities.tablelist.INDORAN;
                    txt_kap_oran.Text = Entities.tablelist.KAPORAN;
                    if(Entities.tablelist.LBL_AKIMTRAFOSU!=null)
                    txtct.Text = Entities.tablelist.LBL_AKIMTRAFOSU;
                    indoran = Convert.ToDouble(Entities.tablelist.INDORAN);
                    kaporan = Convert.ToDouble(Entities.tablelist.KAPORAN);
                    if (Entities.tablelist.KADEMESAYISI != null)
                    txtkulkadmsysi.Text = Entities.tablelist.KADEMESAYISI;
                    indoran = Math.Round(indoran);
                    kaporan = Math.Round(kaporan);


                    if (indoran < 15)
                    {


                        txtdurumind.Foreground = new SolidColorBrush(Colors.Cyan);
                        txtdurumind.Text = "NORMAL";


                    }
                    else
                        if (indoran >= 15 && indoran < 17)
                        {

                            txtdurumind.Foreground = new SolidColorBrush(Colors.Yellow);
                            txtdurumind.Text = "KRÝTÝK";

                        }
                        else if (indoran >= 17 && indoran < 20)
                        {
                            txtdurumind.Foreground = new SolidColorBrush(Colors.Purple);
                            txtdurumind.Text = "ALARM";




                        }
                        else
                        {
                            txtdurumind.Foreground = new SolidColorBrush(Colors.Red);
                            txtdurumind.Text = "CEZA";
                        }
                    if (kaporan < 10)
                    {

                        txtdurumkap.Foreground = new SolidColorBrush(Colors.Cyan);
                        txtdurumkap.Text = "NORMAL";




                    }
                    else if (kaporan >= 10 && kaporan < 12)
                    {
                        txtdurumkap.Foreground = new SolidColorBrush(Colors.Yellow);
                        txtdurumkap.Text = "KRÝTÝK";




                    }
                    else if (kaporan >= 12 && kaporan < 15)
                    {
                        txtdurumkap.Foreground = new SolidColorBrush(Colors.Purple);
                        txtdurumkap.Text = "ALARM";


                    }
                    else
                    {
                        txtdurumkap.Foreground = new SolidColorBrush(Colors.Red);
                        txtdurumkap.Text = "CEZA";
                    }



                    txtcihazno.Text = Entities.Arac.ARC_SERINO.ToString();

                    if ((Convert.ToInt16(Entities.tablelist.CALISMAMODU)) == 1)
                    { }
                    else { }



                    cbxdeger3 = Convert.ToUInt16(Entities.tablelist.CALISMABILGILERI);
                    if ((cbxdeger3 & 1) != 0)
                    {
                        txtcalsmamod.Text = "Manuel Mod";

                        txtcalsmodu.Text = "Manuel Mod";

                    }
                    else { txtcalsmamod.Text = "Otomatik Mod"; txtcalsmodu.Text = "Otomatik Mod"; }
                    if ((cbxdeger3 & 4) != 0)
                    {
                        txtcalshiz.Text = "Normal";


                    }
                    else { txtcalshiz.Text = "Hýzlý"; }

                    if ((cbxdeger3 & 16) != 0)
                    {
                        txtclsmjena.Text = "Sinüs";


                    }
                    else { txtclsmjena.Text = "Sinyal Algýlanýyor"; }

                    cbxdeger4 = Convert.ToUInt16(Entities.tablelist.GENELALARMLAR);

                    if ((cbxdeger4 & 1) != 0) { chxgnl0.IsChecked = true; } else { chxgnl0.IsChecked = false; }
                    if ((cbxdeger4 & 2) != 0) { chxgnl1.IsChecked = true; } else { chxgnl1.IsChecked = false; }
                    if ((cbxdeger4 & 4) != 0) { chxgnl2.IsChecked = true; } else { chxgnl2.IsChecked = false; }
                    if ((cbxdeger4 & 8) != 0) { chxgnl3.IsChecked = true; } else { chxgnl3.IsChecked = false; }
                    if ((cbxdeger4 & 16) != 0) { chxgnl4.IsChecked = true; } else { chxgnl4.IsChecked = false; }
                    if ((cbxdeger4 & 32) != 0) { chxgnl5.IsChecked = true; } else { chxgnl5.IsChecked = false; }
                    if ((cbxdeger4 & 64) != 0) { chxgnl6.IsChecked = true; } else { chxgnl6.IsChecked = false; }
                    if ((cbxdeger4 & 128) != 0) { chxgnl7.IsChecked = true; } else { chxgnl7.IsChecked = false; }
                    if ((cbxdeger4 & 256) != 0) { chxgnl8.IsChecked = true; } else { chxgnl8.IsChecked = false; }
                    if ((cbxdeger4 & 512) != 0) { chxgnl9.IsChecked = true; } else { chxgnl9.IsChecked = false; }
                    if ((cbxdeger4 & 1024) != 0) { chxgnl10.IsChecked = true; } else { chxgnl10.IsChecked = false; }



                    if (((Convert.ToInt32(Entities.tablelist.KADEME1)) & 1) != 0) { kdm1l1.IsChecked = true; } else { kdm1l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME1)) & 2) != 0) { kdm1l2.IsChecked = true; } else { kdm1l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME1)) & 4) != 0) { kdm1l3.IsChecked = true; } else { kdm1l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME2)) & 1) != 0) { kdm2l1.IsChecked = true; } else { kdm2l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME2)) & 2) != 0) { kdm2l2.IsChecked = true; } else { kdm2l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME2)) & 4) != 0) { kdm2l3.IsChecked = true; } else { kdm2l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME3)) & 1) != 0) { kdm3l1.IsChecked = true; } else { kdm3l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME3)) & 2) != 0) { kdm3l2.IsChecked = true; } else { kdm3l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME3)) & 4) != 0) { kdm3l3.IsChecked = true; } else { kdm3l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME4)) & 1) != 0) { kdm4l1.IsChecked = true; } else { kdm4l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME4)) & 2) != 0) { kdm4l2.IsChecked = true; } else { kdm4l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME4)) & 4) != 0) { kdm4l3.IsChecked = true; } else { kdm4l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME5)) & 1) != 0) { kdm5l1.IsChecked = true; } else { kdm5l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME5)) & 2) != 0) { kdm5l2.IsChecked = true; } else { kdm5l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME5)) & 4) != 0) { kdm5l3.IsChecked = true; } else { kdm5l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME6)) & 1) != 0) { kdm6l1.IsChecked = true; } else { kdm6l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME6)) & 2) != 0) { kdm6l2.IsChecked = true; } else { kdm6l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME6)) & 4) != 0) { kdm6l3.IsChecked = true; } else { kdm6l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME7)) & 1) != 0) { kdm7l1.IsChecked = true; } else { kdm7l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME7)) & 2) != 0) { kdm7l2.IsChecked = true; } else { kdm7l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME7)) & 4) != 0) { kdm7l3.IsChecked = true; } else { kdm7l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME8)) & 1) != 0) { kdm8l1.IsChecked = true; } else { kdm8l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME8)) & 2) != 0) { kdm8l2.IsChecked = true; } else { kdm8l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME8)) & 4) != 0) { kdm8l3.IsChecked = true; } else { kdm8l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME9)) & 1) != 0) { kdm9l1.IsChecked = true; } else { kdm9l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME9)) & 2) != 0) { kdm9l2.IsChecked = true; } else { kdm9l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME9)) & 4) != 0) { kdm9l3.IsChecked = true; } else { kdm9l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME10)) & 1) != 0) { kdm10l1.IsChecked = true; } else { kdm10l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME10)) & 2) != 0) { kdm10l2.IsChecked = true; } else { kdm10l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME10)) & 4) != 0) { kdm10l3.IsChecked = true; } else { kdm10l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME11)) & 1) != 0) { kdm11l1.IsChecked = true; } else { kdm11l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME11)) & 2) != 0) { kdm11l2.IsChecked = true; } else { kdm11l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME11)) & 4) != 0) { kdm11l3.IsChecked = true; } else { kdm11l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME12)) & 1) != 0) { kdm12l1.IsChecked = true; } else { kdm12l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME12)) & 2) != 0) { kdm12l2.IsChecked = true; } else { kdm12l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME12)) & 4) != 0) { kdm12l3.IsChecked = true; } else { kdm12l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME13)) & 1) != 0) { kdm13l1.IsChecked = true; } else { kdm13l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME13)) & 2) != 0) { kdm13l2.IsChecked = true; } else { kdm13l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME13)) & 4) != 0) { kdm13l3.IsChecked = true; } else { kdm13l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME14)) & 1) != 0) { kdm14l1.IsChecked = true; } else { kdm14l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME14)) & 2) != 0) { kdm14l2.IsChecked = true; } else { kdm14l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME14)) & 4) != 0) { kdm14l3.IsChecked = true; } else { kdm14l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME15)) & 1) != 0) { kdm15l1.IsChecked = true; } else { kdm15l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME15)) & 2) != 0) { kdm15l2.IsChecked = true; } else { kdm15l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME15)) & 4) != 0) { kdm15l3.IsChecked = true; } else { kdm15l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME16)) & 1) != 0) { kdm16l1.IsChecked = true; } else { kdm16l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME16)) & 2) != 0) { kdm16l2.IsChecked = true; } else { kdm16l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME16)) & 4) != 0) { kdm16l3.IsChecked = true; } else { kdm16l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME17)) & 1) != 0) { kdm17l1.IsChecked = true; } else { kdm17l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME17)) & 2) != 0) { kdm17l2.IsChecked = true; } else { kdm17l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME17)) & 4) != 0) { kdm17l3.IsChecked = true; } else { kdm17l3.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME18)) & 1) != 0) { kdm18l1.IsChecked = true; } else { kdm18l1.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME18)) & 2) != 0) { kdm18l2.IsChecked = true; } else { kdm18l2.IsChecked = false; }
                    if (((Convert.ToInt32(Entities.tablelist.KADEME18)) & 4) != 0) { kdm18l3.IsChecked = true; } else { kdm18l3.IsChecked = false; }



                    if (Convert.ToInt32(Entities.tablelist.KADEME1ISARET) == 0)
                    {
                        kdm1txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm1txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME1L1) + Convert.ToInt32(Entities.tablelist.KADEME1L2) + Convert.ToInt32(Entities.tablelist.KADEME1L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm1txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME1L1) + Convert.ToInt32(Entities.tablelist.KADEME1L2) + Convert.ToInt32(Entities.tablelist.KADEME1L3)).ToString("0.00");

                    }
                    //**************
                    if (Convert.ToInt32(Entities.tablelist.KADEME2ISARET) == 0)
                    {
                        kdm2txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm2txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME2L1) + Convert.ToInt32(Entities.tablelist.KADEME2L2) + Convert.ToInt32(Entities.tablelist.KADEME2L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm2txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME2L1) + Convert.ToInt32(Entities.tablelist.KADEME2L2) + Convert.ToInt32(Entities.tablelist.KADEME2L3)).ToString("0.00");

                    }

                    //------------
                    if (Convert.ToInt32(Entities.tablelist.KADEME3ISARET) == 0)
                    {
                        kdm3txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm3txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME3L1) + Convert.ToInt32(Entities.tablelist.KADEME3L2) + Convert.ToInt32(Entities.tablelist.KADEME3L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm3txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME3L1) + Convert.ToInt32(Entities.tablelist.KADEME3L2) + Convert.ToInt32(Entities.tablelist.KADEME3L3)).ToString("0.00");

                    }

                    //--------------------------


                    if (Convert.ToInt32(Entities.tablelist.KADEME4ISARET) == 0)
                    {
                        kdm4txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm4txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME4L1) + Convert.ToInt32(Entities.tablelist.KADEME4L2) + Convert.ToInt32(Entities.tablelist.KADEME4L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm4txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME4L1) + Convert.ToInt32(Entities.tablelist.KADEME4L2) + Convert.ToInt32(Entities.tablelist.KADEME4L3)).ToString("0.00");

                    }
                    //-----------------------------

                    if (Convert.ToInt32(Entities.tablelist.KADEME5ISARET) == 0)
                    {
                        kdm5txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm5txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME5L1) + Convert.ToInt32(Entities.tablelist.KADEME5L2) + Convert.ToInt32(Entities.tablelist.KADEME5L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm5txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME5L1) + Convert.ToInt32(Entities.tablelist.KADEME5L2) + Convert.ToInt32(Entities.tablelist.KADEME5L3)).ToString("0.00");

                    }

                    //---------------------
                    if (Convert.ToInt32(Entities.tablelist.KADEME6ISARET) == 0)
                    {
                        kdm6txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm6txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME6L1) + Convert.ToInt32(Entities.tablelist.KADEME6L2) + Convert.ToInt32(Entities.tablelist.KADEME6L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm6txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME6L1) + Convert.ToInt32(Entities.tablelist.KADEME6L2) + Convert.ToInt32(Entities.tablelist.KADEME6L3)).ToString("0.00");

                    }

                    //--------------------
                    if (Convert.ToInt32(Entities.tablelist.KADEME7ISARET) == 0)
                    {
                        kdm7txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm7txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME7L1) + Convert.ToInt32(Entities.tablelist.KADEME7L2) + Convert.ToInt32(Entities.tablelist.KADEME7L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm7txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME7L1) + Convert.ToInt32(Entities.tablelist.KADEME7L2) + Convert.ToInt32(Entities.tablelist.KADEME7L3)).ToString("0.00");

                    }
                    //------------------------------

                    if (Convert.ToInt32(Entities.tablelist.KADEME8ISARET) == 0)
                    {
                        kdm8txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm8txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME8L1) + Convert.ToInt32(Entities.tablelist.KADEME8L2) + Convert.ToInt32(Entities.tablelist.KADEME8L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm8txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME8L1) + Convert.ToInt32(Entities.tablelist.KADEME8L2) + Convert.ToInt32(Entities.tablelist.KADEME8L3)).ToString("0.00");

                    }
                    //--------------------------------


                    if (Convert.ToInt32(Entities.tablelist.KADEME9ISARET) == 0)
                    {
                        kdm9txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm9txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME9L1) + Convert.ToInt32(Entities.tablelist.KADEME9L2) + Convert.ToInt32(Entities.tablelist.KADEME9L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm9txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME9L1) + Convert.ToInt32(Entities.tablelist.KADEME9L2) + Convert.ToInt32(Entities.tablelist.KADEME9L3)).ToString("0.00");

                    }
                    //-----------------------------

                    if (Convert.ToInt32(Entities.tablelist.KADEME10ISARET) == 0)
                    {
                        kdm10txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm10txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME10L1) + Convert.ToInt32(Entities.tablelist.KADEME10L2) + Convert.ToInt32(Entities.tablelist.KADEME10L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm10txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME10L1) + Convert.ToInt32(Entities.tablelist.KADEME10L2) + Convert.ToInt32(Entities.tablelist.KADEME10L3)).ToString("0.00");

                    }
                    //--------------

                    if (Convert.ToInt32(Entities.tablelist.KADEME11ISARET) == 0)
                    {
                        kdm11txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm11txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME11L1) + Convert.ToInt32(Entities.tablelist.KADEME11L2) + Convert.ToInt32(Entities.tablelist.KADEME11L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm11txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME11L1) + Convert.ToInt32(Entities.tablelist.KADEME11L2) + Convert.ToInt32(Entities.tablelist.KADEME11L3)).ToString("0.00");

                    }
                    //---------------------------------

                    if (Convert.ToInt32(Entities.tablelist.KADEME12ISARET) == 0)
                    {
                        kdm12txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm12txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME12L1) + Convert.ToInt32(Entities.tablelist.KADEME12L2) + Convert.ToInt32(Entities.tablelist.KADEME12L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm12txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME12L1) + Convert.ToInt32(Entities.tablelist.KADEME12L2) + Convert.ToInt32(Entities.tablelist.KADEME12L3)).ToString("0.00");

                    }

                    //-------------------
                    if (Convert.ToInt32(Entities.tablelist.KADEME13ISARET) == 0)
                    {
                        kdm13txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm13txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME13L1) + Convert.ToInt32(Entities.tablelist.KADEME13L2) + Convert.ToInt32(Entities.tablelist.KADEME13L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm13txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME13L1) + Convert.ToInt32(Entities.tablelist.KADEME13L2) + Convert.ToInt32(Entities.tablelist.KADEME13L3)).ToString("0.00");

                    }
                    //-----------
                    if (Convert.ToInt32(Entities.tablelist.KADEME14ISARET) == 0)
                    {
                        kdm14txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm14txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME14L1) + Convert.ToInt32(Entities.tablelist.KADEME14L2) + Convert.ToInt32(Entities.tablelist.KADEME14L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm14txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME14L1) + Convert.ToInt32(Entities.tablelist.KADEME14L2) + Convert.ToInt32(Entities.tablelist.KADEME14L3)).ToString("0.00");

                    }
                    //------------------

                    if (Convert.ToInt32(Entities.tablelist.KADEME15ISARET) == 0)
                    {
                        kdm15txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm15txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME15L1) + Convert.ToInt32(Entities.tablelist.KADEME15L2) + Convert.ToInt32(Entities.tablelist.KADEME15L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm15txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME15L1) + Convert.ToInt32(Entities.tablelist.KADEME15L2) + Convert.ToInt32(Entities.tablelist.KADEME15L3)).ToString("0.00");

                    }
                    //----------------
                    if (Convert.ToInt32(Entities.tablelist.KADEME16ISARET) == 0)
                    {
                        kdm16txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm16txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME16L1) + Convert.ToInt32(Entities.tablelist.KADEME16L2) + Convert.ToInt32(Entities.tablelist.KADEME16L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm16txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME16L1) + Convert.ToInt32(Entities.tablelist.KADEME16L2) + Convert.ToInt32(Entities.tablelist.KADEME16L3)).ToString("0.00");

                    }

                    //--------------
                    if (Convert.ToInt32(Entities.tablelist.KADEME17ISARET) == 0)
                    {
                        kdm17txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm17txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME17L1) + Convert.ToInt32(Entities.tablelist.KADEME17L2) + Convert.ToInt32(Entities.tablelist.KADEME17L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm17txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME17L1) + Convert.ToInt32(Entities.tablelist.KADEME17L2) + Convert.ToInt32(Entities.tablelist.KADEME17L3)).ToString("0.00");

                    }
                    //---------------------
                    if (Convert.ToInt32(Entities.tablelist.KADEME18ISARET) == 0)
                    {
                        kdm18txt.Foreground = new SolidColorBrush(Colors.Green);
                        kdm18txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME18L1) + Convert.ToInt32(Entities.tablelist.KADEME18L2) + Convert.ToInt32(Entities.tablelist.KADEME18L3)).ToString("0.00");

                    }
                    else
                    {
                        kdm18txt.Text = (Convert.ToInt32(Entities.tablelist.KADEME18L1) + Convert.ToInt32(Entities.tablelist.KADEME18L2) + Convert.ToInt32(Entities.tablelist.KADEME18L3)).ToString("0.00");

                    }












                    if (((Convert.ToInt32(Entities.tablelist.GRPC1)) & 1) != 0)
                    {
                        cbx1_r.IsChecked = true;

                    }
                    else { cbx1_r.IsChecked = false; }


                    if (((Convert.ToInt32(Entities.tablelist.GRPC1)) & 2) != 0)
                    {
                        cbx1_s.IsChecked = true;
                    }
                    else { cbx1_s.IsChecked = false; }


                    if (((Convert.ToInt32(Entities.tablelist.GRPC1)) & 4) != 0)
                    {
                        cbx1_t.IsChecked = true;
                    }
                    else { cbx1_t.IsChecked = false; }


                    c1_r.Text = Entities.tablelist.CR1_R;
                    c1_s.Text = Entities.tablelist.CR1_S;
                    c1_t.Text = Entities.tablelist.CR1_T;

                    if (((Convert.ToInt32(Entities.tablelist.GRPC2)) & 1) != 0)
                    {
                        cbx2_r.IsChecked = true;
                    }
                    else { cbx2_r.IsChecked = false; }


                    if (((Convert.ToInt32(Entities.tablelist.GRPC2)) & 2) != 0)
                    {
                        cbx2_s.IsChecked = true;
                    }
                    else { cbx2_s.IsChecked = false; }


                    if (((Convert.ToInt32(Entities.tablelist.GRPC2)) & 4) != 0)
                    {
                        cbx2_t.IsChecked = true;
                    }
                    else { cbx2_t.IsChecked = false; }

                    c2_r.Text = Entities.tablelist.CR2_R;
                    c2_s.Text = Entities.tablelist.CR2_S;
                    c2_t.Text = Entities.tablelist.CR2_T;

                    if (((Convert.ToInt32(Entities.tablelist.GRPC3)) & 1) != 0)
                    {
                        cbx3_r.IsChecked = true;
                    }
                    else { cbx3_r.IsChecked = false; }


                    if (((Convert.ToInt32(Entities.tablelist.GRPC3)) & 2) != 0)
                    {
                        cbx3_s.IsChecked = true;
                    }
                    else { cbx3_s.IsChecked = false; }


                    if (((Convert.ToInt32(Entities.tablelist.GRPC3)) & 4) != 0)
                    {
                        cbx3_t.IsChecked = true;
                    }
                    else { cbx3_t.IsChecked = false; }

                    c3_r.Text = Entities.tablelist.CR3_R;
                    c3_s.Text = Entities.tablelist.CR3_S;
                    c3_t.Text = Entities.tablelist.CR3_T;

                    if (((Convert.ToInt32(Entities.tablelist.GRPC4)) & 1) != 0)
                    {
                        cbx4_r.IsChecked = true;
                    }
                    else { cbx4_r.IsChecked = false; }


                    if (((Convert.ToInt32(Entities.tablelist.GRPC4)) & 2) != 0)
                    {
                        cbx4_s.IsChecked = true;
                    }
                    else { cbx4_s.IsChecked = false; }


                    if (((Convert.ToInt32(Entities.tablelist.GRPC4)) & 4) != 0)
                    {
                        cbx4_t.IsChecked = true;
                    }
                    else { cbx4_t.IsChecked = false; }

                    c4_r.Text = Entities.tablelist.CR4_R;
                    c4_s.Text = Entities.tablelist.CR4_S;
                    c4_t.Text = Entities.tablelist.CR4_T;










                    cbxdeger = Convert.ToUInt32(Entities.tablelist.LBL_CBXOGREN);

                    if ((cbxdeger & 1024) != 0)
                    {

                        cxbr.IsChecked = true;
                        chcr.IsChecked = true;


                    }
                    else { cxbr.IsChecked = false; chcr.IsChecked = false; }
                    if ((cbxdeger & 2048) != 0)
                    {

                        cxbs.IsChecked = true;
                        chcs.IsChecked = true;


                    }
                    else { cxbs.IsChecked = false; chcs.IsChecked = false; }

                    if ((cbxdeger & 4096) != 0)
                    {

                        cxbt.IsChecked = true;
                        chct.IsChecked = true;


                    }
                    else
                    {
                        cxbt.IsChecked = false; chct.IsChecked = false;
                    }

                    if ((cbxdeger & 8192) != 0)
                    {

                        BitmapImage bi = new BitmapImage(new Uri("/MySisEvo;component/Images/sol.png", UriKind.Relative));
                        img1.Source = bi;
                        imgy1.Source = bi;
                        chcr.Content = "Öðrenildi";



                    }
                    else
                    {
                        BitmapImage bi = new BitmapImage(new Uri("/MySisEvo;component/Images/sag.png", UriKind.Relative));
                        img1.Source = bi;
                        imgy1.Source = bi;
                        chcr.Content = "Öðrenilmedi";

                    }
                    if ((cbxdeger & 16384) != 0)
                    {

                        BitmapImage bi = new BitmapImage(new Uri("/MySisEvo;component/Images/sol.png", UriKind.Relative));
                        img2.Source = bi;

                        imgy2.Source = bi;
                        chcs.Content = "Öðrenildi";

                    }
                    else
                    {
                        BitmapImage bi = new BitmapImage(new Uri("/MySisEvo;component/Images/sag.png", UriKind.Relative));
                        img2.Source = bi;
                        imgy2.Source = bi;
                        chcs.Content = "Öðrenilmedi";
                    }
                    if ((cbxdeger & 32768) != 0)
                    {

                        BitmapImage bi = new BitmapImage(new Uri("/MySisEvo;component/Images/sol.png", UriKind.Relative));
                        img3.Source = bi;
                        imgy3.Source = bi;
                        chct.Content = "Öðrenildi";


                    }
                    else
                    {
                        BitmapImage bi = new BitmapImage(new Uri("/MySisEvo;component/Images/sag.png", UriKind.Relative));
                        img3.Source = bi;
                        imgy3.Source = bi;
                        chct.Content = "Öðrenilmedi";
                    }


                    faz1 = Convert.ToUInt16(Entities.tablelist.FAZALARM1);
                    faz2 = Convert.ToUInt16(Entities.tablelist.FAZALARM2);
                    faz3 = Convert.ToUInt16(Entities.tablelist.FAZALARM3);

                    if ((faz1 & 1) != 0) { cfazasirigerilim1.Foreground = new SolidColorBrush(Colors.Cyan); cfazasirigerilim1.IsChecked = true; } else { cfazasirigerilim1.IsChecked = false; }
                    if ((faz2 & 1) != 0) { cfazasirigerilim2.IsChecked = true; } else { cfazasirigerilim2.IsChecked = false; }
                    if ((faz3 & 1) != 0) { cfazasirigerilim3.IsChecked = true; } else { cfazasirigerilim3.IsChecked = false; }

                    if ((faz1 & 2) != 0) { cfazdusukgerilim1.IsChecked = true; } else { cfazdusukgerilim1.IsChecked = false; }
                    if ((faz2 & 2) != 0) { cfazdusukgerilim2.IsChecked = true; cfazdusukgerilim2.Background = new SolidColorBrush(Colors.Magenta); } else { cfazdusukgerilim2.IsChecked = false; }
                    if ((faz3 & 2) != 0) { cfazdusukgerilim3.Background = new SolidColorBrush(Colors.Orange); cfazdusukgerilim3.IsChecked = true; } else { cfazdusukgerilim3.IsChecked = false; }

                    if ((faz1 & 4) != 0) { cfazasiriakim1.IsChecked = true; } else { cfazasiriakim1.IsChecked = false; }
                    if ((faz2 & 4) != 0) { cfazasiriakim2.IsChecked = true; } else { cfazasiriakim2.IsChecked = false; }
                    if ((faz3 & 4) != 0) { cfazasiriakim3.IsChecked = true; } else { cfazasiriakim3.IsChecked = false; }

                    if ((faz1 & 8) != 0) { cfazasirikopm1.IsChecked = true; } else { cfazasirikopm1.IsChecked = false; }
                    if ((faz2 & 8) != 0) { cfazasirikopm2.IsChecked = true; } else { cfazasirikopm2.IsChecked = false; }
                    if ((faz3 & 8) != 0) { cfazasirikopm3.IsChecked = true; } else { cfazasirikopm3.IsChecked = false; }

                    if ((faz1 & 16) != 0) { cfazdusukkopm1.IsChecked = true; } else { cfazdusukkopm1.IsChecked = false; }
                    if ((faz2 & 16) != 0) { cfazdusukkopm2.IsChecked = true; } else { cfazdusukkopm2.IsChecked = false; }
                    if ((faz3 & 16) != 0) { cfazdusukkopm3.IsChecked = true; } else { cfazdusukkopm3.IsChecked = false; }

                    if ((faz1 & 32) != 0) { cfazfazyoklugu1.IsChecked = true; } else { cfazfazyoklugu1.IsChecked = false; }
                    if ((faz2 & 32) != 0) { cfazfazyoklugu2.IsChecked = true; } else { cfazfazyoklugu2.IsChecked = false; }
                    if ((faz3 & 32) != 0) { cfazfazyoklugu3.IsChecked = true; } else { cfazfazyoklugu3.IsChecked = false; }

                    if ((faz1 & 64) != 0) { cfazasirithdv1.IsChecked = true; } else { cfazasirithdv1.IsChecked = false; }
                    if ((faz2 & 64) != 0) { cfazasirithdv2.IsChecked = true; } else { cfazasirithdv2.IsChecked = false; }
                    if ((faz3 & 64) != 0) { cfazasirithdv3.IsChecked = true; } else { cfazasirithdv3.IsChecked = false; }

                    if ((faz1 & 128) != 0) { cfazasirihdv1.IsChecked = true; } else { cfazasirihdv1.IsChecked = false; }
                    if ((faz2 & 128) != 0) { cfazasirihdv2.IsChecked = true; } else { cfazasirihdv2.IsChecked = false; }
                    if ((faz3 & 128) != 0) { cfazasirihdv3.IsChecked = true; } else { cfazasirihdv3.IsChecked = false; }

                    if ((faz1 & 256) != 0) { cfazasirithdc1.IsChecked = true; } else { cfazasirithdc1.IsChecked = false; }
                    if ((faz2 & 256) != 0) { cfazasirithdc2.IsChecked = true; } else { cfazasirithdc2.IsChecked = false; }
                    if ((faz3 & 256) != 0) { cfazasirithdc3.IsChecked = true; } else { cfazasirithdc3.IsChecked = false; }

                    if ((faz1 & 512) != 0) { cfazasirihdc1.IsChecked = true; } else { cfazasirihdc1.IsChecked = false; }
                    if ((faz2 & 512) != 0) { cfazasirihdc2.IsChecked = true; } else { cfazasirihdc2.IsChecked = false; }
                    if ((faz3 & 512) != 0) { cfazasirihdc3.IsChecked = true; } else { cfazasirihdc3.IsChecked = false; }

















                    cbxdeger2 = Convert.ToUInt32(Entities.tablelist.LBL_CBXDURUM);

                    konreg = 1;
                    for (j = 0; j <= 15; j++)
                    {
                        if ((cbxdeger2 & konreg) != 0)
                        {

                            cbx1.IsChecked = true;
                            cbx2.IsChecked = true;
                            cbx3.IsChecked = true;
                            cbx4.IsChecked = true;
                            cbx5.IsChecked = true;
                            cbx6.IsChecked = true;
                            cbx7.IsChecked = true;
                            cbx8.IsChecked = true;
                            cbx9.IsChecked = true;
                            cbx10.IsChecked = true;
                            cbx11.IsChecked = true;
                            cbx12.IsChecked = true;
                            cbx13.IsChecked = true;
                            cbx14.IsChecked = true;
                            cbx15.IsChecked = true;
                            cbx16.IsChecked = true;


                        }
                        else
                        {

                            cbx1.IsChecked = false;

                            cbx2.IsChecked = false;
                            cbx3.IsChecked = false;
                            cbx4.IsChecked = false;
                            cbx5.IsChecked = false;
                            cbx6.IsChecked = false;
                            cbx7.IsChecked = false;
                            cbx8.IsChecked = false;
                            cbx9.IsChecked = false;
                            cbx10.IsChecked = false;
                            cbx11.IsChecked = false;
                            cbx12.IsChecked = false;
                            cbx13.IsChecked = false;
                            cbx14.IsChecked = false;
                            cbx15.IsChecked = false;
                            cbx16.IsChecked = false;

                            konreg = konreg * 2;
                        }




                    } for (j = 16; j <= 17; j++)
                    {

                        if ((cbxdeger2 & konreg) != 0)
                        {
                            cbx17.IsChecked = true;

                            cbx18.IsChecked = true;





                        }
                        else
                        {
                            cbx17.IsChecked = false;

                            cbx18.IsChecked = false;

                            konreg = konreg * 2;


                        }







                    }






                    //timerr2.Stop();
                    //timerr3.Stop();
                }
         
              
           
           
        
           }

        private bool CheckBox(string p)
        {
            throw new NotImplementedException();
        }
            
  
       private void button1_Click_3(object sender, RoutedEventArgs e)
        {
            SrvIslemler.NormalGonder("enertum" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
            timerr2.Interval = TimeSpan.FromMilliseconds(3000);
            timerr2.Tick += new EventHandler(timerr2_Tick);
            timerr2.Start();

           



        }

       private void CheckBox_Checked(object sender, RoutedEventArgs e)
       {
          
       }

       private void CheckBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
       {

       }

       private void button2_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("enerharcanan" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();

       }

       private void off(object sender, MouseEventArgs e)
       {
           cxbr.IsChecked= false;
       }

       private void button3_Click(object sender, RoutedEventArgs e)
       {

           SrvIslemler.NormalGonder("reaktifener" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + datePicker1.ToString() + Convert.ToChar(1) + datePicker2.ToString() + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();


       }

       private void button4_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kondasatordurum" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();

       }

       private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
       {

       }

       private void radButton1_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("enerharcanan" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void radButton2_Click(object sender, RoutedEventArgs e)
       {

           SrvIslemler.NormalGonder("reaktifener" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + datePicker3.ToString() + Convert.ToChar(1) + datePicker4.ToString() + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();

       }

       private void kdmbtn1_Click(object sender, RoutedEventArgs e)
       {
           

                SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1)+ 1 + Convert.ToChar(1));
                timerr2.Interval = TimeSpan.FromMilliseconds(3000);
                timerr2.Tick += new EventHandler(timerr2_Tick);
                timerr2.Start();
       }

       private void kdmbtn2_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 2 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn3_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 3 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn4_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 4 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn5_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 5 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn6_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 6 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn7_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 7 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn8_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 8 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn9_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 9 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn10_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 10 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn11_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 11 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn12_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 12 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn13_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 13 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn14_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 14 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn15_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 15 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn16_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 16 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn17_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 17 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void kdmbtn18_Click(object sender, RoutedEventArgs e)
       {
           SrvIslemler.NormalGonder("kaeldevreyealcikart" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + 18 + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();
       }

       private void radButton3_Click(object sender, RoutedEventArgs e)
       {
         
           SrvIslemler.NormalGonder("kaelsabitiste" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
           timerr2.Interval = TimeSpan.FromMilliseconds(3000);
           timerr2.Tick += new EventHandler(timerr2_Tick);
           timerr2.Start();



       }
       string ayarkosul;
       private PanoAyar PanoSecWin = new PanoAyar();
     
       private void txtct_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {

           
        
           ayarkosul = "CT";
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;

           PanoSecWin.Show();
           
       }

    

       private void txttanfi_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {


          
           ayarkosul = "TANFI";
           Entities.tablelist.AYARSET = ayarkosul;

           
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;

           PanoSecWin.Show();
       }

       private void txtindceza_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           
           ayarkosul = "INDCEZA";
           Entities.tablelist.AYARSET = ayarkosul;

           
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;

           PanoSecWin.Show();
       }

       private void txtkapceza_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
             
           ayarkosul = "KAPCEZA";
           Entities.tablelist.AYARSET = ayarkosul;

           
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;

           PanoSecWin.Show();
       }

       private void txtmaxkadalmasure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
         
           ayarkosul = "MAXKADAL";
           Entities.tablelist.AYARSET = ayarkosul;

           
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;

           PanoSecWin.Show();
       }

       private void txtmaxkdmcikmasure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
         
           ayarkosul = "MAXKADCIK";
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;

           PanoSecWin.Show();
       }

       private void txtminalmaciksure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           
           ayarkosul = "MAXKADALCIK";
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;

           PanoSecWin.Show();
       }

       private void txtakimyonogren_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           
           ayarkosul = "AKIMYONOGREN";

        
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;

           PanoSecWin.Show();
       }

       private void txtcalsmodu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
          
           ayarkosul = "CALISMAMOD";
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void txtkondsogrn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           
           ayarkosul = "KONDOGREN";
           Entities.tablelist.AYARSET = ayarkosul;

           
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void txtsicaklikalarm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           
           ayarkosul = "SICAKLIKALARM";
           Entities.tablelist.AYARSET = ayarkosul;

         
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void txtfanlarm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
          
           ayarkosul ="FANALARM";
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

     

       private void txtdusukgerilim_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
       {
         
           ayarkosul = "DUSUKGERILIM";
           Entities.tablelist.AYARSET = ayarkosul;

           
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void txtasirigerilim_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
       {
         
           ayarkosul = "ASIRIGERILIM";
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void txt_thdv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           
           ayarkosul = "THDV";
           Entities.tablelist.AYARSET = ayarkosul;

           
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void txt_hdv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           ayarkosul = "HDV";
           Entities.tablelist.AYARSET = ayarkosul;

         
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void txt_thdc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           
           ayarkosul = "THDC";
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void txt_hdc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
       {
           
          
           ayarkosul = "HDC";
           Entities.tablelist.AYARSET = ayarkosul;

          
           PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.Manual;
           PanoSecWin.Width = 301;
           PanoSecWin.Height = 176;
           PanoSecWin.Show();
       }

       private void tabControl1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
       {

       }
       public ScrollViewer RootVisual { get; set; }
    } 
}




            

            //timerr.Interval = TimeSpan.FromMilliseconds(1000);
            //timerr.Tick += new EventHandler(timerr_Tick);
            //timerr.Start();
            //timerr2.Interval = TimeSpan.FromMilliseconds(30000);
            //timerr2.Tick += new EventHandler(timerr2_Tick);
            //timerr2.Start();
            //timerr3.Interval = TimeSpan.FromMilliseconds(1000);
            //timerr3.Tick += new EventHandler(timerr3_Tick);
            //timerr3.Start();
            
            //Yerbilgisi.Text = Entities.Data.DT_ADRES;
            ////Yerbilgisi.Text = "Týðcýlar Mah. Kol Sk. No:32 Adapazarý/SAKARYA  ";
            ////image1.Source = new BitmapImage(new Uri(Entities.Arac.ARC_RES1M, UriKind.Absolute));
            //cbx2.IsChecked = false;
            //cbx1.IsChecked = true;
            //SrvIslemler.ModemKomutGonder("$#katotikoku#$$", Entities.Arac.ARC_SERINO.ToString());
        //    if (Entities.katdegerler!=null)
        //    {
        //        try
        //        {
        //            //tx1.Text = Entities.katdegerler.AKIM.ToString();
        //            //tx2.Text = Entities.katdegerler.VOLTAJ.ToString();
        //            //tx3.Text = Entities.katdegerler.REFVOLTAJ.ToString();
        //        }
        //        catch
        //        { }
        //    }
        //}

        //void timerr2_Tick(object sender, EventArgs e)
        //{
        //    SrvIslemler.ModemKomutGonder("$#katotikoku#$$", Entities.Arac.ARC_SERINO.ToString());
        //}

        //void timerr3_Tick(object sender, EventArgs e)
        //{
        //    //this.lbl1.Text = DateTime.Now.ToShortDateString();
        //    //this.lbl2.Text = DateTime.Now.ToLongTimeString();
        
        
        //}

        //void timerr_Tick(object sender, EventArgs e)
        //{
        //    if (Entities.katdegerler != null)
        //    {
        //        try
        //        {
        //        //tx1.Text = Entities.katdegerler.AKIM.ToString();
        //        //tx2.Text = Entities.katdegerler.VOLTAJ.ToString();
        //        //tx3.Text = Entities.katdegerler.REFVOLTAJ.ToString();


        //        //txt4.Text = Entities.katdegerler.REFVOLTAJ.ToString();
        //        }
        //        catch
        //        { }
        //    }

        //    if (Entities.SayacDegerler != null) // sayac degerleri yazmaca
        //    {
        //        try
        //        {
        //        //txttop.Text = Entities.SayacDegerler.SYC_T;
        //        //txt1nd.Text = Entities.SayacDegerler.SYC_1ND;
        //        //txtKap.Text = Entities.SayacDegerler.SYC_KAP;
        //        //txtt1.Text = Entities.SayacDegerler.SYC_T1;
        //        //txtt2.Text = Entities.SayacDegerler.SYC_T2;
        //        //txtt3.Text = Entities.SayacDegerler.SYC_T3;
        //        //txtsycham.Text = Entities.SayacDegerler.SYC_HAM;
        //        }
        //        catch
        //        { }
        //        if (Entities.SayacDegerler.SYC_DURUM == true)
        //        {
        //            Entities.SayacDegerler.SYC_DURUM = false;
        //            timerr2.Start();
        //        }
        //    }
        //    else
        //        timerr2.Start();

        //}

        //private void btncks_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();

        //}

        //private void cbx1_Click(object sender, RoutedEventArgs e)
        //{
          

        //}

        //private void cbx1_Checked(object sender, RoutedEventArgs e)
        //{
        //    //cbx2.IsChecked= false;
        //    //txt5.IsEnabled = true;
        //    //txt4.IsEnabled = false;
        //    //btn3.IsEnabled = false;
        //    //tx1.IsEnabled = true;
        //    //tx2.IsEnabled = true;
        //    //tx3.IsEnabled = true;
        //    //btn1.IsEnabled = true;
        //    //btn2.IsEnabled = true;


        //}

        //private void cbx2_Checked(object sender, RoutedEventArgs e)
        //{
        //    //cbx1.IsChecked = false;
            //tx1.IsEnabled =true;
            //tx2.IsEnabled = true;
            //tx3.IsEnabled = true;
            //btn1.IsEnabled = false;
            //btn2.IsEnabled = false;
            
            //txt4.IsEnabled = true;
            //btn3.IsEnabled = true;
        //    if (Entities.katdegerler != null)
        //    {
        //        try
        //        {
        //            //tx1.Text = Entities.katdegerler.AKIM.ToString();
        //            //tx2.Text = Entities.katdegerler.VOLTAJ.ToString();
        //            ////txt4.Text = Entities.katdegerler.REFVOLTAJ.ToString();
        //            //txt4.Text = "0-500";
        //        }


        //        catch { }
        //    }

        //}

        //private void RadWindow_Closed_1(object sender, WindowClosedEventArgs e)
        //{

        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    SrvIslemler.ModemKomutGonder("$#katotikoku#$$", Entities.Arac.ARC_SERINO.ToString());
        //    if (timerr2.IsEnabled == false)
        //        timerr2.Start();
            
        //}

        //private void btn1_Click(object sender, RoutedEventArgs e)
        //{
        //    SrvIslemler.ModemKomutGonder("$#katotikyaz#-#$", Entities.Arac.ARC_SERINO.ToString());
            
        //}

        //private void btn2_Click(object sender, RoutedEventArgs e)
        //{
        //    SrvIslemler.ModemKomutGonder("$#katotikyaz#+#$", Entities.Arac.ARC_SERINO.ToString());
        //}

        //private void btn3_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        var gelen = Int32.Parse(txt4.Text);
        //        if (gelen > -1 && gelen < 501)
        //        {
        //            SrvIslemler.ModemKomutGonder("$#katotikyaz#oto#" + gelen + "#$", Entities.Arac.ARC_SERINO.ToString());
        //        }
        //        else
        //        {
        //            MessageBox.Show("Lütfen 0 ile 500 arasýnda bir deðer giriniz..", "Dikkat", MessageBoxButton.OK);
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Lütfen 0 ile 500 arasýnda bir deðer giriniz..","Dikkat",MessageBoxButton.OK);
        //    }

        //}

        //private void radButton1_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
                
        //}







       

    
    
    
    
    


   