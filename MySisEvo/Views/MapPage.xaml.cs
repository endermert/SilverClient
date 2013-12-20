using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls.Map;
using System.Windows.Threading;
using MySisEvo.Classes;
using MySisEvo.Controls;
//using MySisEvo.DATA_RS;
//using MySisEvo.ARC_SR;
using MySisEvo.CHZ_RS;
using System.Windows.Ink;
using System.Windows.Media.Imaging;
//using MySisEvo.FUP_SR;
using System.IO;
using System.ComponentModel;

namespace MySisEvo.Views
{
    public partial class MapPage : Page
    {
        DispatcherTimer zoomOutTimer = new DispatcherTimer();
        DispatcherTimer zoomInTimer = new DispatcherTimer();
        DispatcherTimer moveTimer = new DispatcherTimer();
        DispatcherTimer DataTimer = new DispatcherTimer();
        DispatcherTimer AnimeTimer = new DispatcherTimer();
        DispatcherTimer geoTimer = new DispatcherTimer();
        DispatcherTimer layerStartTimer = new DispatcherTimer();
        DispatcherTimer isteTimeout = new DispatcherTimer();
        public delegate void SetCallBack(string Textim);

        public Location arcyer=new Location();
        private string VEKey1;
        private string VEKey2;
        private string VEKey3;

        private const int minZoomLevel = 8;
        private const int maxZoomLevel = 18;

        private InformationLayer infoLayer1 = new InformationLayer();

        //AracServisClient arc_sr = new AracServisClient();
        //FileUploadServisSoapClient fup_sr = new FileUploadServisSoapClient();

        FileStream FS;
        String FileName;
        private int sycGecmisReq = 0;
        private int sycGecmisRes = 0;
        public MapPage()
        {
            //if (ClientIslem.LoginIn == true)
            //{
                
                InitializeComponent();

                ServisIslemler.chz_sr.getAniDegerlerCompleted += new EventHandler<getAniDegerlerCompletedEventArgs>(chz_sr_getAniDegerlerCompleted);
                ServisIslemler.chz_sr.getSayacDegerlerCompleted += new EventHandler<getSayacDegerlerCompletedEventArgs>(chz_sr_getSayacDegerlerCompleted);
            //}
        }

        void SokettenGelen(string gelen)
        {
            isteTimeout.Stop();
            if (gelen.IndexOf("Hattayok")>-1 || gelen=="Hata")
            {
                
                PaneSelect("HideAll");
                PaneOkunan.IsHidden = true;
                PaneOkunan.Visibility = Visibility.Collapsed;
                GloBusy.IsBusy = false;
                t_okunanveri.Text = "";
            }
            else
            {
                GloBusy.BusyContent = "Sayac Verileri Alınıyor..";
                PaneSelect("PaneOkunan");
                t_okunanveri.Text = gelen;
            }
        }
        private frmSayac sayacFrm;
        void chz_sr_getSayacDegerlerCompleted(object sender, getSayacDegerlerCompletedEventArgs e)
        {
            t_okunanveri.Text = Entities.t_okunanveri;
            GloBusy.IsBusy = false;
            String bos = Entities.Sayac.syc_kGuc;
            Entities.Sayac = new Sayac();
            Entities.Sayac = e.Result;
            Entities.Sayac.syc_kGuc = bos;
            Entities.AracDataList[Entities.getArcListIndex(Entities.Arac.ARC_SERINO)].SYCDATA = Entities.Sayac;
            
            if (sayacFrm == null)
            {
                sayacFrm = new frmSayac();
                sayacFrm.Title = "Sayaç Okuma > " + Entities.Arac.ARC_PLAKA + " - " + Entities.Sayac.syc_serino;
                sayacFrm.Closed += new EventHandler(sayacFrm_Closed);
                sayacFrm.Show();
            }
            else
            {
                
                sayacFrm.Close();
                sayacFrm = new frmSayac();
                sayacFrm.Title = "Sayaç Okuma > " + Entities.Arac.ARC_PLAKA + " - " + Entities.Sayac.syc_serino;
                sayacFrm.Closed += new EventHandler(sayacFrm_Closed);
                sayacFrm.Show();
            }

              //  PaneSelect("HideAll");
            //PaneSayac.Header = "Aktif Sayaç : " + Entities.Arac.ARC_PLAKA;
            //PaneSayac.IsHidden=false;
            //PaneSayac.IsSelected=true;
            //SayacGoster();
        }

        void sayacFrm_Closed(object sender, EventArgs e)
        {
            sayacFrm.Closed -= this.sayacFrm_Closed;
            sayacFrm = null;
        }

        void chz_sr_getAniDegerlerCompleted(object sender, getAniDegerlerCompletedEventArgs e)
        {
            Entities.Rolem = new Role();
            Entities.Rolem = e.Result;
            RoleGoster();
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


            PaneMasterSelect("PaneSayac");
        }
        private void RoleGoster()
        {
            t_v1.Text = Entities.Rolem.v1;
            t_v2.Text = Entities.Rolem.v2;
            t_v3.Text = Entities.Rolem.v3;
            t_a1.Text = Entities.Rolem.akm1;
            t_a2.Text = Entities.Rolem.akm2;
            t_a3.Text = Entities.Rolem.akm3;
            t_kw1.Text = Entities.Rolem.kw1;
            t_kw2.Text = Entities.Rolem.kw2;
            t_kw3.Text = Entities.Rolem.kw3;
            t_kvar1.Text = Entities.Rolem.kr1;
            t_kvar2.Text = Entities.Rolem.kr2;
            t_kvar3.Text = Entities.Rolem.kr3;
            t_kva1.Text = Entities.Rolem.kVA1;
            t_kva2.Text = Entities.Rolem.kVA2;
            t_kva3.Text = Entities.Rolem.kVA3;
            
            t_l1ov.IsChecked = Entities.Rolem.A_L1OVolt;
            t_l2ov.IsChecked = Entities.Rolem.A_L2OVolt;
            t_l3ov.IsChecked = Entities.Rolem.A_L3OVolt;

            t_l1uv.IsChecked = Entities.Rolem.A_L1UVolt;
            t_l2uv.IsChecked = Entities.Rolem.A_L2UVolt;
            t_l3uv.IsChecked = Entities.Rolem.A_L3UVolt;

            t_l1ocu.IsChecked = Entities.Rolem.A_L1OCurrent;
            t_l2ocu.IsChecked = Entities.Rolem.A_L2OCurrent;
            t_l3ocu.IsChecked = Entities.Rolem.A_L3OCurrent;

            t_l1okomp.IsChecked = Entities.Rolem.A_L1OComp;
            t_l2okomp.IsChecked = Entities.Rolem.A_L2OComp;
            t_l3okomp.IsChecked = Entities.Rolem.A_L3OComp;

            t_l1ukomp.IsChecked = Entities.Rolem.A_L1UComp;
            t_l2ukomp.IsChecked = Entities.Rolem.A_L2UComp;
            t_l3ukomp.IsChecked = Entities.Rolem.A_L3UComp;

            t_l1fazyok.IsChecked = Entities.Rolem.A_L1MissP;
            t_l2fazyok.IsChecked = Entities.Rolem.A_L2MissP;
            t_l3fazyok.IsChecked = Entities.Rolem.A_L3MissP;

            t_l1ythdv.IsChecked = Entities.Rolem.A_L1OTHDV;
            t_l2ythdv.IsChecked = Entities.Rolem.A_L2OTHDV;
            t_l3ythdv.IsChecked = Entities.Rolem.A_L3OTHDV;

            t_l1yhdv.IsChecked = Entities.Rolem.A_L1OHDV;
            t_l2yhdv.IsChecked = Entities.Rolem.A_L2OHDV;
            t_l3yhdv.IsChecked = Entities.Rolem.A_L3OHDV;

            t_l1yhdc.IsChecked = Entities.Rolem.A_L1OHDC;
            t_l2yhdc.IsChecked = Entities.Rolem.A_L2OHDC;
            t_l3yhdc.IsChecked = Entities.Rolem.A_L3OHDC;

            t_l1ythdc.IsChecked = Entities.Rolem.A_L1OTHDC;
            t_l2ythdc.IsChecked = Entities.Rolem.A_L2OTHDC;
            t_l3ythdc.IsChecked = Entities.Rolem.A_L3OTHDC;
            PaneMasterSelect("PaneAktif");
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SocketIslemler.myDispatcher = this.Dispatcher;
            SocketIslemler.SetCallBack asd = new SocketIslemler.SetCallBack(SokettenGelen);
            SocketIslemler.sk = asd;
            //GloBusy.Visibility = Visibility.Visible;
            //GloBusy.IsBusy = false;
            PaneSelect("HideAll");
            PaneMasterSelect("PaneMap");
            ClientIslem.setAracImages();

            this.GetVEServiceKey1();
            //this.GetVEServiceKey2();
            //this.GetVEServiceKey3();

            isteTimeout.Interval = TimeSpan.FromSeconds(1.0);
            isteTimeout.Tick += new EventHandler(isteTimeout_Tick);
            RadMap1.Center = new Location(40.6680784, 30.739936);
            RadMap1.ZoomLevel = 9;
            //RadAniMap.Center = new Location(40.000, 32.000);
            zoomOutTimer.Interval = TimeSpan.FromMilliseconds(150);
            zoomInTimer.Interval = TimeSpan.FromMilliseconds(150);
            moveTimer.Interval = TimeSpan.FromMilliseconds(800);


            //AnimeTimer.Interval = TimeSpan.FromMilliseconds(500);
            //AnimeTimer.Tick += new EventHandler(AnimeTimer_Tick);
            zoomInTimer.Tick += new EventHandler(zoomInTimer_Tick);
            zoomOutTimer.Tick += new EventHandler(zoomOutTimer_Tick);
            moveTimer.Tick += new EventHandler(moveTimer_Tick);

            //geoTimer.Interval = TimeSpan.FromMilliseconds(1000);
            //geoTimer.Tick += new EventHandler(geoTimer_Tick);
            //geoTimer.Start();

            DataTimer.Interval = TimeSpan.FromSeconds(4);
            DataTimer.Tick += new EventHandler(DataTimer1_Tick);
            //DataTimer.Start();
            RadMap1.Items.Clear();
            LayerKro();
            //ServisIslemler.data_sr.getDataListCompleted += new EventHandler<getDataListCompletedEventArgs>(data_sr_getDataListCompleted);
            listBox1.ItemsSource = Entities.AraclarList;
           // fup_sr.DosyaBoyutCompleted += new EventHandler<DosyaBoyutCompletedEventArgs>(fup_sr_DosyaBoyutCompleted);
            //fup_sr.KaydetCompleted += new EventHandler<KaydetCompletedEventArgs>(fup_sr_KaydetCompleted);
        }

        int syc1=11;
        void isteTimeout_Tick(object sender, EventArgs e)
        {
            syc1--;
            GloBusy.BusyContent = "Sayac Okuma İsteği Gönderildi. Bekliyor..("+syc1.ToString()+")";
            if (syc1 == 0)
            {
                MessageBox.Show("Zaman aşımına uğradı..Lütfen daha sonra tekrar deneyiniz..","Sayaç Okuma",MessageBoxButton.OK);
                Entities.t_okunanveri = "";
                GloBusy.IsBusy = false;
                isteTimeout.Stop();
                syc1 = 16;
            }
        }
       
        //void data_sr_getDataListCompleted(object sender, getDataListCompletedEventArgs e)
        //{
            
        //    ObservableCollection<Datas> dataList1 = new ObservableCollection<Datas>(); 
        //    LocationCollection bos = new LocationCollection();

        //    dataList1 = e.Result;

        //    if (e.Result.Count > 0)
        //    {
        //        if (Entities.GecmisYollar == null)
        //            ClientIslem.initVariable("GecmisYollar");

        //        int index = Entities.getArcListIndex(e.Result[0].ARC_SER1NO);
        //        Entities.GecmisYollar.DataList.Add(e.Result);
        //        Entities.GecmisYollar.AracData.Add(Entities.AracDataList[index]);
        //        int yolIndex = Entities.GecmisYollar.DataList.Count - 1;
        //        Entities.GecmisYollar.HamYol.Add(new MapPolyline());
        //        Entities.GecmisYollar.HamYol[yolIndex].Points = new LocationCollection();

        //        PaneMasterSelect("PaneAnime");

        //        for (int i = 0; i < dataList1.Count; i++)
        //        {
        //            double en = Double.Parse(dataList1[i].DT_ENLEM);
        //            double boy = Double.Parse(dataList1[i].DT_BOYLAM);
        //            Location loc = new Location(en, boy);
        //            Entities.GecmisYollar.HamYol[yolIndex].Points.Add(loc);

        //        }
        //        GecmisYolCiz();
        //    }
        //    else
        //        MessageBox.Show("Belirtilen Tarihler İçin Hiçbir Kayıt Bulunamadı..","Bilgi!",MessageBoxButton.OK);

            /*
            if (e.Result.Count>0)
            {
                if (Entities.GecmisYollar == null)
                    ClientIslem.initVariable("GecmisYollar");

                int index = Entities.getArcListIndex(e.Result[0].ARC_SER1NO);
                Entities.GecmisYollar.DataList.Add(e.Result);
                Entities.GecmisYollar.AracData.Add(Entities.AracDataList[index]);
                int yolIndex = Entities.GecmisYollar.DataList.Count - 1;
                Entities.GecmisYollar.HamYol.Add(new MapPolyline());
                Entities.GecmisYollar.HamYol[yolIndex].Points = new LocationCollection();

                PaneMasterSelect("PaneAnime");

                int part = 0;
                if (dataList1.Count > 24)
                    part = dataList1.Count / 25;

                for (int i = 0; i < dataList1.Count; i++)
                {
                    double en = Double.Parse(dataList1[i].DT_ENLEM);
                    double boy = Double.Parse(dataList1[i].DT_BOYLAM);
                    Location loc = new Location(en, boy);
                    Entities.GecmisYollar.HamYol[yolIndex].Points.Add(loc);
                    //bos.Add(loc);
                    if (i % (part + 1) == 0)
                    {
                        //ClientIslem.yolIstek(bos);
                        bos.Add(loc);
                        //bos.Clear();
                        //i--;
                    }


                }
                ClientIslem.yolIstek(bos);

            }
        */
        //    GloBusy.IsBusy = false;
        //}

        //private void eskiYolCiz()
        //{
        //    layerYol.Items.Clear();

        //    if (cvarYol.IsChecked==true)
        //    {
        //        if (ClientIslem.calYol.Points != null)
        //        if (ClientIslem.calYol.Points.Count > 1)
        //        {
        //            SetStyle(ClientIslem.calYol, "mavi");                    
        //            layerYol.Items.Add(ClientIslem.calYol);
        //            RadAniMap.ZoomLevel = 10;
        //            RadAniMap.Center = ClientIslem.calYol.Points[0];

        //            if (AnimeTimer.IsEnabled)
        //            {
        //                btnPlay.IsEnabled = false;
        //                btnStop.IsEnabled = true;
        //            }
        //            else
        //            {
        //                btnPlay.IsEnabled = true;
        //                btnStop.IsEnabled = false;

        //            }
        //        }

        //    }

        //    if(chamYol.IsChecked==true)
        //    {
        //        if (ClientIslem.hamYol.Points!=null)
        //        if (ClientIslem.hamYol.Points.Count > 1)
        //        {
        //            SetStyle(ClientIslem.hamYol, "yesil");
        //            layerYol.Items.Add(ClientIslem.hamYol);
        //            RadAniMap.ZoomLevel = 10;
        //            RadAniMap.Center = ClientIslem.hamYol.Points[0];
                    
        //            if (AnimeTimer.IsEnabled)
        //            {
        //                btnPlay.IsEnabled = false;
        //                btnStop.IsEnabled = true;
        //            }
        //            else
        //            {
        //                btnPlay.IsEnabled = true;
        //                btnStop.IsEnabled = false;

        //            }
        //        }

        //    }

            
        //}

        //private void GecmisYolCiz()
        //{
        //    RadAniMap.Items.Clear();
        //    layerCalYol.Items.Clear();
        //    layerHamYol.Items.Clear();
        //    layerArac.Items.Clear();
        //    layerTools.Items.Clear();
        //    RadAniMap.Items.Add(layerTools);

        //    if (cvarYol.IsChecked == true)
        //    {
        //        for (int i = 0; i < Entities.GecmisYollar.CalYol.Count; i++)
        //        {
        //            if (Entities.GecmisYollar.CalYol[i].Points != null)
        //                if (Entities.GecmisYollar.CalYol[i].Points.Count > 1)
        //                {
        //                    SetStyle(Entities.GecmisYollar.CalYol[i], "mavi");
        //                    layerCalYol.Items.Add(Entities.GecmisYollar.CalYol[i]);

        //                    RadAniMap.ZoomLevel = 10;
        //                    RadAniMap.Center = Entities.GecmisYollar.CalYol[i].Points[0];
                            
        //                    if (AnimeTimer.IsEnabled)
        //                    {
        //                        btnPlay.IsEnabled = false;
        //                        btnStop.IsEnabled = true;
        //                    }
        //                    else
        //                    {
        //                        btnPlay.IsEnabled = true;
        //                        btnStop.IsEnabled = false;

        //                    }
                            

        //                }
        //        }

        //    }

        //    if (chamYol.IsChecked == true)
        //    {
        //        for (int i = 0; i < Entities.GecmisYollar.DataList.Count; i++)
        //        {
        //            if (Entities.GecmisYollar.HamYol[i].Points != null)
        //                if (Entities.GecmisYollar.HamYol[i].Points.Count > 1)
        //                {
        //                    SetStyle(Entities.GecmisYollar.HamYol[i], "mavi");
        //                    layerHamYol.Items.Add(Entities.GecmisYollar.HamYol[i]);

        //                    RadAniMap.ZoomLevel = 10;
        //                    RadAniMap.Center = Entities.GecmisYollar.HamYol[i].Points[0];
                            
        //                    if (AnimeTimer.IsEnabled)
        //                    {
        //                        btnPlay.IsEnabled = false;
        //                        btnStop.IsEnabled = true;
        //                    }
        //                    else
        //                    {
        //                        btnPlay.IsEnabled = true;
        //                        btnStop.IsEnabled = false;

        //                    }
                            
        //                }
        //        }

        //    }

            
        //    RadAniMap.Items.Add(layerHamYol);
        //    RadAniMap.Items.Add(layerCalYol);

           
        //    for (int i = 0; i < Entities.GecmisYollar.DataList.Count; i++)
        //    {
        //        if (Entities.GecmisYollar.HamYol[i].Points != null)
        //            if (Entities.GecmisYollar.HamYol[i].Points.Count > 1)
        //            {

        //                InformationLayer lyr = new InformationLayer();
        //                lyr.ItemTemplate = layerArac.ItemTemplate;
        //                //lyr.Tag = Entities.GecmisYollar.AracData[i].Arac.ARC_SERINO.ToString();
        //                lyr.Items.Add(Entities.GecmisYollar.HamYol[i].Points[0]);
        //                lyr.Items.Add(Entities.GecmisYollar.AracData[i]);
        //                RadAniMap.Items.Add(lyr);
        //            }
        //    }



        //}

        //void geoTimer_Tick(object sender, EventArgs e)
        //{
        //    if (ClientIslem.geoDurum)
        //    if (ClientIslem.geoIndexList.Count > 0)
        //    {
        //        int i = ClientIslem.geoIndexList[0];
        //        ClientIslem.geoIndex = i;
               
        //        if (Entities.AracDataList[i].CHZDATA != null)
        //            if (Entities.AracDataList[i].CHZDATA.CHZ_ENLEM.Length > 1)
        //            {
        //                Double en = Double.Parse(Entities.AracDataList[i].CHZDATA.CHZ_ENLEM);
        //                Double boy = Double.Parse(Entities.AracDataList[i].CHZDATA.CHZ_BOYLAM);

        //                Location location = new Location(en, boy);
        //                ReverseGeocodeRequest reverseGeocodeRequest = new ReverseGeocodeRequest();
        //                reverseGeocodeRequest.Location = location;
                       
        //                ClientIslem.geoDurum = false;
        //                ClientIslem.geoProvider.ReverseGeocodeAsync(reverseGeocodeRequest);

        //                List<int> bos = new List<int>();

        //                for (int j = 1;j<ClientIslem.geoIndexList.Count; j++)
        //                    bos.Add(ClientIslem.geoIndexList[j]);

        //                ClientIslem.geoIndexList = bos;

        //            }
        //    }
        //}

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }



        private void GetVEServiceKey1()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc1_DownloadStringCompleted);
            //Uri keyURI = new Uri(URIHelper.CurrentApplicationURL, "VEKey1.txt");
            Uri keyURI = new Uri("http://uzoksis.azurewebsites.net/VEKey1.txt");
            wc.DownloadStringAsync(keyURI);
        }

        //private void GetVEServiceKey2()
        //{
        //    WebClient wc = new WebClient();
        //    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc2_DownloadStringCompleted);
        //    //Uri keyURI = new Uri(URIHelper.CurrentApplicationURL, "VEKey2.txt");
        //    Uri keyURI = new Uri("http://localhost:2564/VEKey2.txt");
        //    wc.DownloadStringAsync(keyURI);
        //}

        //private void GetVEServiceKey3()
        //{
        //    WebClient wc = new WebClient();
        //    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc3_DownloadStringCompleted);
        //    //Uri keyURI = new Uri(URIHelper.CurrentApplicationURL, "VEKey3.txt");
        //    Uri keyURI = new Uri("http://88.247.221.186:9080/VEKey3.txt");
        //    wc.DownloadStringAsync(keyURI);
        //}

        void wc1_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            this.VEKey1 = e.Result;
            this.SetProvider1();
        }
        //void wc2_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    this.VEKey2 = e.Result;
        //    this.SetProvider2();
        //}

        //void wc3_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    this.VEKey3 = e.Result;
        //    this.SetProvider3();
        //}

        void moveTimer_Tick(object sender, EventArgs e)
        {

            this.moveTimer.Stop();
            this.ZoomIn();
        }

        void zoomInTimer_Tick(object sender, EventArgs e)
        {
            if (this.RadMap1.ZoomLevel < maxZoomLevel)
            {
                this.RadMap1.ZoomLevel++;
            }
            else
            {
                this.zoomInTimer.Stop();
            }
        }

        void zoomOutTimer_Tick(object sender, EventArgs e)
        {
            if (this.RadMap1.ZoomLevel > minZoomLevel)
                this.RadMap1.ZoomLevel--;
                
            else
            {
                this.zoomOutTimer.Stop();
                this.ZoomOutFinished();
            }
        }

        void ZoomOutFinished()
        {
            //this.SetProvider();
            RadMap1.Center = arcyer;
            this.moveTimer.Start();
            
        }
        private BingMapProvider provider1;
        private void SetProvider1()
        {
            provider1 = new BingMapProvider(MapMode.Road, true, this.VEKey1);
            this.RadMap1.Provider = provider1;

            //// Init route provider.
            // ClientIslem.routeProvider = new BingRouteProvider();
            // ClientIslem.routeProvider.ApplicationId = this.VEKey1;
            // ClientIslem.routeProvider.MapControl = this.RadMap1;
            // ClientIslem.routeProvider.RoutingCompleted += new EventHandler<RoutingCompletedEventArgs>(Provider_RoutingCompleted);

        }

        //private void SetProvider2()
        //{
  
        //    BingMapProvider provider2 = new BingMapProvider(MapMode.Aerial, true, this.VEKey2);
        //    this.RadAniMap.Provider = provider2;
        //    // Init route provider.
           
        //    ClientIslem.yolAniProvider = new BingRouteProvider();
        //    ClientIslem.yolAniProvider.ApplicationId = this.VEKey2;
        //    ClientIslem.yolAniProvider.MapControl = this.RadAniMap;
        //    ClientIslem.yolAniProvider.RoutingCompleted += new EventHandler<RoutingCompletedEventArgs>(Provider_Yol2Completed);
           
           
        //    // Init search provider.
        //    ClientIslem.searchProvider = new BingSearchProvider();
        //    ClientIslem.searchProvider.ApplicationId = this.VEKey2;
        //    ClientIslem.searchProvider.MapControl = this.RadAniMap;
        //    ClientIslem.searchProvider.SearchCompleted += new EventHandler<SearchCompletedEventArgs>(searchProvider_SearchCompleted);
        //}

        //void searchProvider_SearchCompleted(object sender, SearchCompletedEventArgs e)
        //{
        //    SearchResponse response = e.Response;
        //    RadAniMap.Items.Clear();
        //    if (response != null && response.ResultSets.Count > 0)
        //    {
        //        if (response.ResultSets[0].Results.Count > 0)
        //        {
        //            Entities.AdresList1=new ObservableCollection<Adres>();
        //            foreach (SearchResultBase result in response.ResultSets[0].Results)
        //            {
						
        //                Adres item=new Adres()
        //                {
        //                    ADR_ADRES=result.Name,
        //                    ADR_LOCATION=result.LocationData.Locations[0]
        //                };

        //                Entities.AdresList1.Add(item);
        //            }
        //            for (int i = 0; i < response.ResultSets[0].Results.Count; i++)
        //            {
        //                SearchResultBase result = response.ResultSets[0].Results[0];
        //                Adres item = new Adres()
        //                {
        //                    ADR_ADRES = result.Name,
        //                    ADR_LOCATION = result.LocationData.Locations[0]
        //                };

        //                Entities.AdresList1.Add(item);
        //            }

        //        }

        //        if (response.ResultSets[0].SearchRegion != null && response.ResultSets[0].SearchRegion.GeocodeLocation != null)
        //        {
        //            // Set map viewport to the best view returned in the search result.
        //            this.RadAniMap.SetView(response.ResultSets[0].SearchRegion.GeocodeLocation.BestView);

        //            // Show map shape around bounding area
        //            if (response.ResultSets[0].SearchRegion.BoundingArea != null)
        //            {
        //                MapShape boundingArea = response.ResultSets[0].SearchRegion.BoundingArea;
        //                boundingArea.Stroke = new SolidColorBrush(Colors.Red);
        //                boundingArea.StrokeThickness = 1;                        
        //                layerKare.Items.Add(boundingArea);
        //            }

        //            if (response.ResultSets[0].SearchRegion.GeocodeLocation.Address != null
        //                && response.ResultSets[0].SearchRegion.GeocodeLocation.Locations.Count > 0)
        //            {
        //                Entities.AdresList1 = new ObservableCollection<Adres>();
        //                foreach (Location location in response.ResultSets[0].SearchRegion.GeocodeLocation.Locations)
        //                {
        //                    Adres item = new Adres()
        //                    {
        //                        ADR_ADRES=response.ResultSets[0].SearchRegion.GeocodeLocation.Address.FormattedAddress,
        //                        ADR_LOCATION=location,
        //                    };
        //                    layerKare.Items.Add(location);
        //                    Entities.AdresList1.Add(item);
        //                }
        //                RadAniMap.Items.Add(layerKare);
        //            }
        //        }
        //    }
        //}

        //private void SetProvider3()
        //{

        //    BingMapProvider provider3 = new BingMapProvider(MapMode.Aerial, true, this.VEKey3);
        //    this.RadGeoMap.Provider = provider3;

        //    ClientIslem.geoProvider = new BingGeocodeProvider();
        //    ClientIslem.geoProvider.ApplicationId = this.VEKey3;
        //    ClientIslem.geoProvider.MapControl = RadGeoMap;
        //    ClientIslem.geoProvider.GeocodeCompleted += new EventHandler<GeocodeCompletedEventArgs>(Provider_GeocodeCompleted);
        //}

        //private void Provider_GeocodeCompleted(object sender, GeocodeCompletedEventArgs e)
        //{
        //        GeocodeResponse response = e.Response;
        //        if (response.Results.Count > 0)
        //        {
        //            //Entities.AracDataList[ClientIslem.geoIndex].Adres.ADR_ADRES = e.Response.Results[0].Address.FormattedAddress;
        //            //Entities.AracDataList[ClientIslem.geoIndex].Adres.ADR_ULKE = e.Response.Results[0].Address.CountryRegion;
        //            //Entities.AracDataList[ClientIslem.geoIndex].Adres.ADR_IL = e.Response.Results[0].Address.AdminDistrict;
        //            //Entities.AracDataList[ClientIslem.geoIndex].Adres.ADR_SEMT = e.Response.Results[0].Address.Locality;
        //            //Entities.AracDataList[ClientIslem.geoIndex].Adres.ADR_CAD = e.Response.Results[0].Address.AddressLine;
        //            //sonKonumGrid.Rebind();
        //        }
        //        ClientIslem.geoDurum = true;
        //}
        //// önemli 2
        //private void Provider_Yol2Completed(object sender, RoutingCompletedEventArgs e)
        //{
        //    RouteResponse routeResponse = e.Response as RouteResponse;
        //    if (routeResponse != null &&
        //        routeResponse.Result != null &&
        //        routeResponse.Result.RoutePath != null)
        //    {
        //        if (!PaneAnimeOptions.IsHidden)
        //        {
        //            if (routeResponse.Result.RoutePath.Points.Count >= 2)
        //            {
        //                Entities.GecmisYollar.CalYol.Add(new MapPolyline());
        //                int yolIndex = Entities.GecmisYollar.CalYol.Count - 1;
        //                Entities.GecmisYollar.CalYol[yolIndex].Points = new LocationCollection();

        //                for (int i = 0; i < routeResponse.Result.RoutePath.Points.Count; i++)
        //                {
        //                    //ClientIslem.calYol.Points.Add(routeResponse.Result.RoutePath.Points[i]);
        //                    Entities.GecmisYollar.CalYol[yolIndex].Points.Add(routeResponse.Result.RoutePath.Points[i]);
        //                }
                        
        //            }

        //            sycGecmisRes++;
        //            if (sycGecmisReq == sycGecmisRes)
        //                GecmisYolCiz();
        //        }

        //        if (!PaneYol.IsHidden)
        //        {
        //            ClientIslem.yolLine = new MapPolyline();
        //            ClientIslem.yolLine.Points = routeResponse.Result.RoutePath.Points;

        //            if (ClientIslem.yolLine.Points.Count > 2)
        //            {
        //                this.SetDefaultStyle(ClientIslem.yolLine);
        //                layerYol.Items.Clear();
        //                layerYol.Items.Add(ClientIslem.yolLine);
        //            }

        //        }

        //    }
        //else
        //{
            
        //    if (!PaneAnimeOptions.IsHidden)
        //    {
        //        /*
        //        btnPlay.IsEnabled = false;
        //        btnStop.IsEnabled = false;
        //        MessageBox.Show("Bu tarihler için sonuç bulunamıyor..", "Dikkat!", MessageBoxButton.OK);
        //         */
        //    }
        //    if (!PaneYol.IsHidden)
        //    {
        //        layerYol.Items.Clear();
        //        MessageBox.Show("Bu ayarlar için sonuç bulunamıyor..", "Dikkat!", MessageBoxButton.OK);
        //    }
        //}
        //}

       

        //private void Provider_RoutingCompleted(object sender, RoutingCompletedEventArgs e)
        //{

        //    RouteResponse routeResponse = e.Response as RouteResponse;
        //    if (routeResponse != null &&
        //        routeResponse.Result != null &&
        //        routeResponse.Result.RoutePath != null)
        //    {

        //    Location loc = new Location();
        //    loc = routeResponse.Result.RoutePath.Points[0];
            
        //    Entities.CihazlarYol[ClientIslem.dtYolindex[0]].CHZ_ENLEM = loc.Latitude.ToString();
        //    Entities.CihazlarYol[ClientIslem.dtYolindex[0]].CHZ_BOYLAM= loc.Longitude.ToString();

        //    //sonKonum verisinde aracın yol ustunde olması
        //    Entities.AracDataList[ClientIslem.dtYolindex[0]].CHZDATA= Entities.CihazlarYol[ClientIslem.dtYolindex[0]];

        //    List<int> bos = new List<int>();
        //    for (int i = 1; i < ClientIslem.dtYolindex.Count; i++)
        //    {
        //        bos.Add(ClientIslem.dtYolindex[i]);
        //    }

        //    ClientIslem.dtYolindex = bos;

        //    if (ClientIslem.dtYolindex.Count > 0)
        //    {
        //        double en = double.Parse(Entities.CihazlarList[ClientIslem.dtYolindex[0]].CHZ_ENLEM);
        //        double boy = double.Parse(Entities.CihazlarList[ClientIslem.dtYolindex[0]].CHZ_BOYLAM);
        //        Location yer = new Location(en, boy);
        //        ClientIslem.yolBul(yer);
        //    }

        //    if (ClientIslem.dtYolindex.Count == 0)
        //        ClientIslem.dtYolIndex = -1;

        //    }
        //}


        //private BingMapProvider InitializeBingMapProvider(MapMode providerMode, bool isLabelVisible)
        //{
        //    if (string.IsNullOrEmpty(this.VEKey1))
        //        return null;

        //    return new BingMapProvider(providerMode, isLabelVisible, this.VEKey1);
        //}

        private void ZoomOut()
        {
            if (this.zoomOutTimer.IsEnabled || this.zoomInTimer.IsEnabled)
                return;

            this.zoomOutTimer.Start();
        }

        private void ZoomIn()
        {
            if (this.zoomOutTimer.IsEnabled || this.zoomInTimer.IsEnabled)
                return;

            this.zoomInTimer.Start();
        }

        //private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    this.zoomInTimer.Tick -= this.zoomInTimer_Tick;
        //    this.zoomOutTimer.Tick -= this.zoomOutTimer_Tick;
        //    this.moveTimer.Tick -= this.moveTimer_Tick;
        //    this.DataTimer.Tick -= this.DataTimer1_Tick;
        //    this.AnimeTimer.Tick -= this.AnimeTimer_Tick;
        //}

        private int syc = 0;
        int hiz,mxhiz = 0;

        //private InformationLayer AniArcLayer;
        string kontak = null;
        DateTime kkt = new DateTime();
        Location kkl = new Location();
        //int sycKP = 0;
        //void AnimeTimer_Tick(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < Entities.GecmisYollar.HamYol.Count; i++)
        //    {
        //        if (Entities.GecmisYollar.HamYol[i].Points != null)
        //        {
        //            stackAniDetay.Visibility = Visibility.Visible;
        //            syc++;
        //            if (syc >= Entities.GecmisYollar.HamYol[i].Points.Count)
        //            {
        //                /*
        //                syc = 0;
        //                btnStop.IsEnabled = false;
        //                btnPlay.IsEnabled = true;
        //                stackAniDetay.Visibility = Visibility.Collapsed;
        //                AnimeTimer.Stop();
        //                 */
        //            }
        //            else
        //            {
                        
        //                InformationLayer lyr = layerBul(Entities.GecmisYollar.AracData[i].Arac.ARC_SERINO.ToString());
        //                if (lyr != null)
        //                {
        //                    lyr.Items.Clear();
        //                    lyr.Items.Add(Entities.GecmisYollar.AracData[i]);
        //                    lyr.Items.Add(Entities.GecmisYollar.HamYol[i].Points[syc]);
        //                    RadAniMap.Center = Entities.GecmisYollar.HamYol[i].Points[syc];
        //                    hiz = int.Parse(Entities.GecmisYollar.DataList[i][syc].DT_H1Z);

        //                    if (hiz > mxhiz)
        //                        mxhiz = hiz;

        //                    aTarih.Text = Entities.GecmisYollar.DataList[i][syc].DT_TAR1H.ToString();
        //                    aHiz.Text = Entities.GecmisYollar.DataList[i][syc].DT_H1Z.ToString() + " km/s";
        //                    aMaxHiz.Text = mxhiz.ToString() + " km/s";
        //                    aAdres.Text = Entities.GecmisYollar.DataList[i][syc].DT_ADRES;
        //                    aKontak.Text = Entities.GecmisYollar.DataList[i][syc].DT_KONTAK;

                            
        //                    if (kontak != Entities.GecmisYollar.DataList[i][syc].DT_KONTAK)
        //                    {
        //                        if (kontak == "Kapalı")
        //                        {
        //                            InformationLayer lt = new InformationLayer();

        //                            if (kkl != null)
        //                            {
        //                                Entities.GecmisYollar.HamYol[i].Points[syc] = kkl;
        //                                Entities.GecmisYollar.DataList[i][syc].DT_TAR1HK =kkt;
        //                            }
        //                            lt.Items.Add(Entities.GecmisYollar.HamYol[i].Points[syc]);
        //                            lt.Items.Add(Entities.GecmisYollar.DataList[i][syc]);
        //                            lt.ItemTemplate = layerTools.ItemTemplate;
        //                            RadAniMap.Items.Add(lt);
        //                        }
        //                        else
        //                        {
        //                            InformationLayer lt = new InformationLayer();
        //                            lt.Items.Add(Entities.GecmisYollar.HamYol[i].Points[syc]);
        //                            lt.Items.Add(Entities.GecmisYollar.DataList[i][syc]);
        //                            kkl = Entities.GecmisYollar.HamYol[i].Points[syc];
        //                            kkt = Entities.GecmisYollar.DataList[i][syc].DT_TAR1H;
        //                            lt.ItemTemplate = lyrKK.ItemTemplate;
        //                            RadAniMap.Items.Add(lt);
        //                        }
                               
        //                    }
        //                        /*
        //                    else 
        //                    {
        //                        sycKP++;
        //                        if (sycKP == 5)
        //                        {
        //                            InformationLayer lt = new InformationLayer();
        //                            lt.Items.Add(Entities.GecmisYollar.HamYol[i].Points[syc]);
        //                            lt.Items.Add(Entities.GecmisYollar.DataList[i][syc]);
        //                            lt.ItemTemplate = lyrKP.ItemTemplate;
        //                            RadAniMap.Items.Add(lt);
        //                            sycKP = 0;
        //                        }
        //                    }
        //                    */
                            
        //                    kontak = Entities.GecmisYollar.DataList[i][syc].DT_KONTAK;
                            
                            

        //                }
        //            }
        //        }
        //    }
        //}

        //private InformationLayer layerBul(string serino)
        //{
        //    InformationLayer lyr1=new InformationLayer();
        //    for (int i = 0; i < RadAniMap.Items.Count; i++)
        //    {
        //        try
        //        {
        //            lyr1 = ((InformationLayer)RadAniMap.Items[i]);
        //            if (lyr1.Tag.ToString() == serino)
        //                return lyr1;
        //            else
        //                lyr1 = null;
        //        }
        //        catch
        //        { 

        //        }
        //    }
        //    return lyr1;
        //}

        void DataTimer1_Tick(object sender, EventArgs e)
        {
            
            if (listBox1.Items.Count == 0)
            {
                listBox1.ItemsSource = Entities.AraclarList;
                if (PaneMap.IsHidden==false)
                    PaneSelect("HideAll");
                ClientIslem.setAracImages();
            }
            layerArcGuncelle();
            listBox1.ItemsSource = Entities.AraclarList;
            
        }


        private void layerArcGuncelle()
        {
            if (RadMap1.Items.Count == 0)
                LayerKro();
            if (Entities.CihazlarYol!=null)
            for (int i = 0; i < Entities.CihazlarYol.Count; i++)
            {

                Entities.CihazlarList[i].CHZ_ENLEM = Entities.CihazlarList[i].CHZ_ENLEM;
                Entities.CihazlarYol[i].CHZ_ENLEM = Entities.CihazlarYol[i].CHZ_ENLEM;

                if (Entities.CihazlarYol[i].CHZ_ENLEM != null && Entities.CihazlarYol[i].CHZ_BOYLAM != null)
                {
                    double en = double.Parse(Entities.CihazlarYol[i].CHZ_ENLEM);
                    double boy = double.Parse(Entities.CihazlarYol[i].CHZ_BOYLAM);
                    Location yer = new Location(en, boy);

                    for (int j = 0; j < RadMap1.Items.Count; j++)
                    {
                        try
                        {
                            InformationLayer layer = new InformationLayer();
                            layer = (InformationLayer)RadMap1.Items[j];
                            if (layer.Tag != null)
                            {
                                if (layer.Tag.ToString() == i.ToString())
                                {
                                    for (int k = 0; k < layer.Items.Count; k++)
                                    {
                                        Location loc1 = new Location();
                                        Boolean bul = false;
                                        try
                                        {
                                            loc1 = (Location)layer.Items[k];
                                            bul = true;
                                            layer.Items.Clear();
                                            layer.Items.Add(Entities.AracDataList[i]);
                                            layer.Items.Add(yer);
                                        }
                                        catch
                                        {
                                            if (k == layer.Items.Count - 1 && bul == false)
                                                layerPro(yer, i);
                                        }
                                    }
                                    break;
                                }
                                if (j == RadMap1.Items.Count - 1)
                                    layerPro(yer, i);
                            }
                            else
                                if (j == RadMap1.Items.Count - 1)
                                {
                                    layerPro(yer, i);
                                }


                        }
                        catch { }
                    }

                }
            }
        }

        private void layerPro(Location yer, int i)
        {
                InformationLayer layer = new InformationLayer();
                layer.Tag = i.ToString();
                layer.ItemTemplate = layerDetayOut.ItemTemplate;
                //layer.MouseEnter += new MouseEventHandler(informationLayer_MouseEnter);
                layer.MouseLeftButtonUp += new MouseButtonEventHandler(layer_MouseLeftButtonUp);
                layer.MouseLeave += new MouseEventHandler(informationLayer_MouseLeave);
                //layer.MouseLeftButtonUp += new MouseButtonEventHandler(layer_MouseLeftButtonDown);
                //iski

               // layer.MouseLeftButtonUp+=new MouseButtonEventHandler(layer_MouseLeftButtonUpiski);                   
                layer.Cursor = Cursors.Hand;
                layer.Items.Add(yer);
                layer.Items.Add(Entities.AracDataList[i]);
                RadMap1.Items.Add(layer);
              
        }



        private void LayerKro()
        {
            RadMap1.Items.Clear();


            for (int i = 0; i < Entities.AracDataList.Count; i++)
            {
                if (Entities.AracDataList[i].CHZDATA.CHZ_ENLEM != null && Entities.AracDataList[i].CHZDATA.CHZ_BOYLAM != null)
                {
                    double en = double.Parse(Entities.AracDataList[i].CHZDATA.CHZ_ENLEM);
                    double boy = double.Parse(Entities.AracDataList[i].CHZDATA.CHZ_BOYLAM);
                    Location yer = new Location(en, boy);
                    layerPro(yer, i);
                }

            }
        }
        Storyboard sb = new Storyboard();
        private void DoubleAniEkle(InformationLayer layer)
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
            DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
            sb.Duration = duration;
            myDoubleAnimation1 = myDoubleAni;
            
            Storyboard.SetTarget(myDoubleAnimation1, layer);
            sb.Children.Add(myDoubleAnimation1);
            sb.Begin();

        }

        private void informationLayer_MouseEnter(object sender, MouseEventArgs e)
        {

            if (infoLayer1.Tag !=null)
            {
                infoLayer1.ItemTemplate = layerDetayOut.ItemTemplate;
            }

                int index = int.Parse(((InformationLayer)sender).Tag.ToString());
                //Entities.Data = Entities.DatasList[index];
                Entities.Cihaz = Entities.CihazlarList[index];
                Entities.Arac = Entities.AraclarList[index];
              //  layerDetayIn.Items.Clear();
                ((InformationLayer)sender).ItemTemplate = layerDetayIn.ItemTemplate;
                infoLayer1 = ((InformationLayer)sender);

           
        }
        
        private void informationLayer_MouseLeave(object sender, MouseEventArgs e)
        {

         
                int index = int.Parse(((InformationLayer)sender).Tag.ToString());
                //Entities.Data = Entities.DatasList[index];
                Entities.Cihaz = Entities.CihazlarList[index];
                Entities.Arac = Entities.AraclarList[index];
                ((InformationLayer)sender).ItemTemplate = layerDetayOut.ItemTemplate;
                infoLayer1.ItemTemplate=layerDetayOut.ItemTemplate;
                
                infoLayer1 = new InformationLayer();
 
        }

        void layer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int index = int.Parse(((InformationLayer)sender).Tag.ToString());
            if (infoLayer1.Tag != null)
            {
                //infoLayer1.Items.Clear();
                //((InformationLayer)sender).Items.Add(Entities.AracDataList[index]);
                //infoLayer1.ItemTemplate = layerDetayOut.ItemTemplate;
                LayerKro();
                infoLayer1 = null;
              
            }

            //int index = int.Parse(((InformationLayer)sender).Tag.ToString());
            //Entities.Data = Entities.DatasList[index];
            Entities.Cihaz = Entities.CihazlarList[index];
            Entities.Arac = Entities.AraclarList[index];
            Entities.Sayac = Entities.AracDataList[index].SYCDATA;
            layerDetayIn.Items.Clear();
            ((InformationLayer)sender).ItemTemplate = null;
            Entities.AracDataList[index].SYCDATA = Entities.AracDataList[index].SYCDATA;
            ((InformationLayer)sender).Items.Add(Entities.AracDataList[index]);
            ((InformationLayer)sender).ItemTemplate = layerDetayIn.ItemTemplate;
            infoLayer1 = ((InformationLayer)sender);
           
        }
        private void btnSycOkuLyr_Click(object sender, RoutedEventArgs e)
        {
            SrvIslemler.NormalGonder("sayac" + Convert.ToChar(1) + Entities.Cihaz.CHZ_SERINO.ToString() + Convert.ToChar(1) + "tdshyr" + Convert.ToChar(1) + "kythyr" + Convert.ToChar(1));
            GloBusy.IsBusy = true;
            GloBusy.BusyContent = "Sayac Okuma İsteği Gönderildi. Bekliyor..";
            Entities.istenen = "sayacal";
            syc1 = 21;
            isteTimeout.Start();
        }
  // pano ac
        //private PanoSec PanoSecWin = new PanoSec();
        void layer_MouseLeftButtonUpiski(object sender, MouseButtonEventArgs e)
        {

            //PanoSecWin.Close();
            //PanoSecWin = new PanoSec();
            //PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
            //PanoSecWin.Width = 965;
            //PanoSecWin.Height = 740;
            //PanoSecWin.Show();
      
            //PaneMasterSelect("PaneAktif");

            //if (infoLayer1.Tag != null)
            //{
            //    infoLayer1.ItemTemplate = layerDetayOut.ItemTemplate;
            //}

            int index = int.Parse(((InformationLayer)sender).Tag.ToString());
            Entities.Cihaz= Entities.CihazlarList[index];
            Entities.Arac = Entities.AraclarList[index];


            //SrvIslemler.NormalGonder("enertum" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
            //SrvIslemler.NormalGonder("sayac" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + "tdshyr" + Convert.ToChar(1) + "kythyr" + Convert.ToChar(1));
            //GloBusy.IsBusy=true;
            //GloBusy.BusyContent = "Sayac Okuma İsteği Gönderildi. Bekliyor..";
            //Entities.istenen = "sayacal";
            //syc1 = 21;
            //isteTimeout.Start();

            //layerDetayIn.Items.Clear();
            //((InformationLayer)sender).Items.Add(Entities.AracDataList[index]);
            //((InformationLayer)sender).ItemTemplate = layerDetayIn.ItemTemplate;
            //infoLayer1 = ((InformationLayer)sender);
        }
        //private void btnAni_Click(object sender, RoutedEventArgs e)
        //{
        //    layerYol.Items.Clear();
        //    layerCalYol.Items.Clear();
        //    layerHamYol.Items.Clear();
        //    RadAniMap.Items.Clear();
        //    AnimeTimer.Stop();
        //    RadAniMap.Items.Add(layerTools);

        //    ClientIslem.initVariable("GecmisYollar");
        //    ClientIslem.yolLine = new MapPolyline();
        //    if (AniBasTar.SelectedValue != null && AniBitTar.SelectedValue != null)
        //    {
        //        Boolean seciliVar = false;
        //        sycGecmisReq = 0;
        //        sycGecmisRes = 0;
        //        if (aComboArc.SelectedIndex!=-1)
        //        {
        //            int i = aComboArc.SelectedIndex;
        //            seciliVar = true;
        //            GloBusy.IsBusy = true;
        //            ServisIslemler.data_sr.getDataListAsync(Entities.AracDataList[i].Arac.ARC_SERINO, DateTime.Parse(AniBasTar.SelectedValue.ToString()), DateTime.Parse(AniBitTar.SelectedValue.ToString()));
        //            sycGecmisReq++;
                   
        //        }

        //        if (seciliVar==false)
        //            MessageBox.Show("Lütfen bir araç seçin..", "Bilgi!", MessageBoxButton.OK);


        //    }
        //    else
        //    {
        //        MessageBox.Show("Bu işlem için Başlangıç ve Bitiş tarihlerini girmelisiniz..", "Hata!", MessageBoxButton.OK);
        //    }


        //}

        private void SetDefaultStyle(MapShape shape)
        {
            if (shape is MapLine)
            {
                shape.Style = this.Resources["selectedLineStyle"] as Style;
            }
            else
            {
                shape.Style = this.Resources["defaultPolylineStyle"] as Style;
            }
        }
        private void SetStyle(MapShape shape,string source)
        {
            shape.Style = Resources[source] as Style;
        }

        //private void btnPlay_Click(object sender, RoutedEventArgs e)
        //{
        //    btnPlay.IsEnabled = false;
        //    btnStop.IsEnabled = true;
        //    btnduraklat.IsEnabled = true;
        //    btnAniCiz.IsEnabled = false;
        //    AnimeTimer.Start();
        //}

        //private void btnduraklat_Click(object sender, RoutedEventArgs e)
        //{
        //    btnStop.IsEnabled = true;
        //    btnPlay.IsEnabled = true;
        //    btnduraklat.IsEnabled = false;
        //    btnAniCiz.IsEnabled = false;
        //    AnimeTimer.Stop();
        //}

        //private void btnStop_Click(object sender, RoutedEventArgs e)
        //{
        //    syc = 0;
        //    btnStop.IsEnabled = false;
        //    btnPlay.IsEnabled = true;
        //    btnduraklat.IsEnabled = false;
        //    btnAniCiz.IsEnabled = true;
        //    AnimeTimer.Stop();
        //     for (int i = 0; i < Entities.GecmisYollar.HamYol.Count; i++)
        //    {
        //        if (Entities.GecmisYollar.HamYol[i].Points != null)
        //        {
        //            stackAniDetay.Visibility = Visibility.Collapsed;
        //            InformationLayer lyr = layerBul(Entities.GecmisYollar.AracData[i].Arac.ARC_SERINO.ToString());
        //                if (lyr != null)
        //                {
        //                    lyr.Items.Clear();
        //                    lyr.Items.Add(Entities.GecmisYollar.AracData[i]);
        //                    lyr.Items.Add(Entities.GecmisYollar.HamYol[i].Points[0]);
        //                }
        //        }
        //    }
        //}

        //private void ImagePin_Click(object sender, MouseButtonEventArgs e)
        //{
        //    PaneSelect("HideAll");
        //    PaneMasterSelect("PaneMap");

        //    int serino = int.Parse(((Image)sender).Tag.ToString());
        //    if (PaneMap.IsSelected)
        //    {
        //        PaneAnimeOptions.Visibility = Visibility.Collapsed;
        //        Entities.Data = Entities.DatasList[Entities.getArcListIndex(serino)];
        //        double en = double.Parse(Entities.Data.DT_ENLEM);
        //        double boy = double.Parse(Entities.Data.DT_BOYLAM);

        //        Location yer = new Location(en, boy);
        //        arcyer = new Location(en, boy);
        //        ZoomOut();
        //    }

        //}
        private void ImageAni_Click(object sender, MouseButtonEventArgs e)
        {
            //PaneSelect("PaneAnimeOptions");
        }
        private void ImageDetay_Click(object sender, MouseButtonEventArgs e)
        {
            //PaneMasterSelect("PaneArcDetay");

            int serino = int.Parse(((Image)sender).Tag.ToString());
            //aracDetayGetir(serino);

            //SrvIslemler.NormalGonder("enertum" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1));
            SrvIslemler.NormalGonder("sayac" + Convert.ToChar(1) + serino.ToString() + Convert.ToChar(1) + "tdshyr" + Convert.ToChar(1) + "kythyr" + Convert.ToChar(1));
            GloBusy.IsBusy = true;
            GloBusy.BusyContent = "Sayac Okuma İsteği Gönderildi. Bekliyor..";
            Entities.istenen = "sayacal";
            syc1 = 21;
            isteTimeout.Start();
          
        }

        //private void aracDetayGetir(int serino)
        //{
        //    Entities.Data = Entities.DatasList[Entities.getArcListIndex(serino)];
        //    Entities.Arac = Entities.AraclarList[Entities.getArcListIndex(serino)];
        //    /*
        //    dEnlem.Text = Entities.Data.DT_ENLEM;
        //    dBoylam.Text = Entities.Data.DT_BOYLAM;
        //    dHiz.Text = Entities.Data.DT_H1Z;
        //    */
        //    dSeriNo.Text = Entities.Arac.ARC_SERINO.ToString();
        //    dPlaka.Text = Entities.Arac.ARC_PLAKA;
        //    dTel.Text = Entities.Arac.ARC_TEL;
        //    dSahibi.Text = Entities.Arac.ARC_SAHIBI;
        //    dRenk.Text = Entities.Arac.ARC_RENK;
        //    dMarka.Text = Entities.Arac.ARC_MARKA;
        //    dModel.Text = Entities.Arac.ARC_MODEL;
        //    dCins.Text = Entities.Arac.ARC_CINS;
        //    dimgUpload.Source = new BitmapImage(new Uri(Entities.Arac.ARC_RES1M, UriKind.Absolute));
        //    if (Entities.Arac.ARC_ICON != null && dComboImg.Items.Count > 0)
        //    {
        //        dComboImg.SelectedIndex = int.Parse(Entities.Arac.ARC_ICON);
        //    }
        //}
        private void PaneSelect(string PaneName)
        {
            //if (listBox1.Items.Count == 0)
                //PaneAraclar.IsHidden = true;
            //else
                PaneAraclar.IsHidden = false;

            if (PaneName == "HideAll")
            {
                PaneOkunan.Visibility = Visibility.Collapsed;
                PaneOkunan.IsHidden = true;
                //PaneAnimeOptions.IsHidden = true;
                //PaneAnimeOptions.Visibility = Visibility.Collapsed;
                //PaneArama.IsHidden = true;
                //PaneYol.IsHidden = true;
                //PaneTanim1.IsHidden = true;
            }

            if (PaneName == "PaneOkunan")
            {
                PaneOkunan.IsHidden = false;
                PaneOkunan.Visibility = Visibility.Visible;
                PaneOkunan.IsSelected = true;
            }
            //if (PaneName == "PaneArama")
            //{
            //    PaneArama.IsHidden = false;
            //    PaneArama.Visibility = Visibility.Visible;
            //    PaneArama.IsSelected = true;
            //}

            //if (PaneName == "PaneYol")
            //{
            //    PaneYol.IsHidden = false;
            //    PaneYol.Visibility = Visibility.Visible;
            //    PaneYol.IsSelected = true;
            //}
            //if (PaneName == "PaneTanim1")
            //{
            //    PaneTanim1.IsHidden = false;
            //    PaneTanim1.Visibility = Visibility.Visible;
            //    PaneTanim1.IsSelected = true;
             
            //}

        }
        private void PaneMasterSelect(string PaneName)
        {
            PaneMap.IsHidden = true;
            PaneMap.Visibility = Visibility.Collapsed;

            PaneAktif.IsHidden = true;
            PaneAktif.Visibility = Visibility.Collapsed;

            PaneSayac.IsHidden = true;
            PaneSayac.Visibility = Visibility.Collapsed;

            //PaneGeo.IsHidden = true;
            //PaneGeo.Visibility = Visibility.Collapsed;

            //PaneSonKonum.IsHidden = true;
            //PaneSonKonum.Visibility = Visibility.Collapsed;

            //PaneSonDurum.IsHidden = true;
            //PaneSonDurum.Visibility = Visibility.Collapsed;

            //PaneArcDetay.IsHidden = true;
            //PaneArcDetay.Visibility = Visibility.Collapsed;

            if (PaneName == "PaneMap")
            {
                PaneMap.IsHidden = false;
                PaneMap.Visibility = Visibility.Visible;
                PaneMap.IsSelected = true;
               
            }

            if (PaneName == "PaneAktif")
            {
                PaneSayac.IsHidden = true;
                PaneSayac.Visibility = Visibility.Collapsed;

                PaneAktif.IsHidden = false;
                PaneAktif.Visibility = Visibility.Visible;
                PaneAktif.IsSelected = true;
            }

            if (PaneName == "PaneSayac")
            {
                PaneAktif.IsHidden = true;
                PaneAktif.Visibility = Visibility.Collapsed;

                PaneSayac.IsHidden = false;
                PaneSayac.Visibility = Visibility.Visible;
                PaneSayac.IsSelected = true;
            }

            
            //if (PaneName == "PaneAnime")
            //{
            //    PaneAnime.IsHidden = false;
            //    PaneAnime.Visibility = Visibility.Visible;
            //    PaneAnime.IsSelected = true;
            //}
            //if (PaneName == "PaneGeo")
            //{
            //    PaneGeo.IsHidden = false;
            //    PaneGeo.Visibility = Visibility.Visible;
            //    PaneGeo.IsSelected = true;
            //}
            //if (PaneName == "PaneSonKonum")
            //{
            //    PaneSonKonum.IsHidden = false;
            //    PaneSonKonum.Visibility = Visibility.Visible;
            //    PaneSonKonum.IsSelected = true;
            //}

            //if (PaneName == "PaneSonDurum")
            //{
            //    PaneSonDurum.IsHidden = false;
            //    PaneSonDurum.Visibility = Visibility.Visible;
            //    PaneSonDurum.IsSelected = true;
            //}
            //if (PaneName == "PaneArcDetay")
            //{
            //    PaneArcDetay.IsHidden = false;
            //    PaneArcDetay.Visibility = Visibility.Visible;
            //    PaneArcDetay.IsSelected = true;
            //}
        }

        private void BtnTamEkran_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = !Application.Current.Host.Content.IsFullScreen;
            if (Application.Current.Host.Content.IsFullScreen)
            {
                btnTamEkran2.Visibility=Visibility.Visible;
                btnTamEkran1.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnTamEkran1.Visibility = Visibility.Visible;
                btnTamEkran2.Visibility = Visibility.Collapsed;
            }
        }

        //private void btnSonKonum_Click(object sender, RoutedEventArgs e)
        //{
        //    PaneMasterSelect("PaneSonKonum");
        //    PaneSelect("HideAll");
        //}

        private void btnHome_click(object sender, RoutedEventArgs e)
        {
            GloBusy.IsBusy = false;
            PaneSelect("HideAll");
            PaneMasterSelect("PaneMap");
            
        }

        //private void btnGecmis_Click(object sender, RoutedEventArgs e)
        //{
        //    PaneSelect("HideAll");
        //    PaneSelect("PaneAnimeOptions");
        //    PaneMasterSelect("PaneAnime");
        //    PaneAraclar.IsHidden = true;

        //    AniBasTar.SelectedValue = DateTime.Now.Date;
        //    AniBitTelectedVaar.Slue = DateTime.Now;
        //}

        private void Car_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Canvas)sender).Visibility = Visibility.Visible;
        }

        private void Car_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Canvas)sender).Visibility = Visibility.Collapsed;
        }

        private void Cars_MouseLeave()
        {
            //for (int i = 0; i < Entities.DatasYol.Count; i++)
            //{
            //    for (int j = 0; j < RadMap1.Items.Count; j++)
            //    {
            //        try
            //        {
            //            InformationLayer layer = new InformationLayer();
            //            layer = (InformationLayer)RadMap1.Items[j];
            //            if (layer.Tag != null)
            //            {
            //                if (layer.Tag.ToString() == i.ToString())
            //                {
            //                    for (int k = 0; k < layer.Items.Count; k++)
            //                    {
            //                        Location loc1 = new Location();
            //                        try
            //                        {
            //                            loc1 = (Location)layer.Items[k];
            //                            layer.ItemTemplate = layerDetayOut.ItemTemplate;
            //                        }
            //                        catch
            //                        {

            //                        }
            //                    }
            //                    break;
            //                }
            //            }


            //        }
            //        catch { }
            //    }
            //}
        }

        private void hizSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {       
                AnimeTimer.Interval = TimeSpan.FromMilliseconds(1001-e.NewValue);
        }

        //private void btnAdrsArama_Click(object sender, RoutedEventArgs e)
        //{
        //    PaneSelect("HideAll");
        //    PaneSelect("PaneArama");
        //    PaneMasterSelect("PaneAnime");
        //    RadAniMap.Items.Clear();
        //    layerKare.Items.Clear();
        //    layerYol.Items.Clear();
        //    RadAniMap.Items.Add(layerKare);
        //    PaneAnime.Header = "Harita";
        //}

        //private void btnAdrsAra_Click(object sender, RoutedEventArgs e)
        //{
        //    string query = txtAdrsAra.Text;

        //    if (!string.IsNullOrEmpty(query))
        //    {
        //        SearchRequest request = new SearchRequest();
        //        request.Culture = new System.Globalization.CultureInfo("en-US");
        //        request.Query = query;
        //        ClientIslem.searchProvider.SearchAsync(request);
        //    }

        //}

        //private void btnYol_Click(object sender, RoutedEventArgs e)
        //{
        //    PaneSelect("HideAll");
        //    PaneSelect("PaneYol");
        //    PaneMasterSelect("PaneAnime");
        //    RadAniMap.Items.Clear();
        //    layerKare.Items.Clear();
        //    layerYol.Items.Clear();
        //    RadAniMap.Items.Add(layerKare);
        //    RadAniMap.Items.Add(layerYol);
        //    PaneAnime.Header = "Harita";
        //}

        //private void RadAniMap_Click(object sender, MapMouseRoutedEventArgs e)
        //{
        //    if (!PaneYol.IsHidden) 
        //    {
        //        ClientIslem.rotaPoints.Add(e.Location);
        //        layerKare.Items.Add(e.Location);
        //    }
        //    if (!PaneTanim1.IsHidden)
        //    {
        //        RadAniMap.Items.Clear();
        //        layerTanim1.Items.Add(e.Location);
        //        RadAniMap.Items.Add(layerTanim1);
        //    }
        //}

        //private void btnMap2Yol_Click(object sender, RoutedEventArgs e)
        //{
        //    RouteRequest routeRequest = new RouteRequest();
        //    routeRequest.Culture = new System.Globalization.CultureInfo("en-US");
        //    routeRequest.Options.RoutePathType = RoutePathType.Points;
        //    if (radioKYol.IsChecked == true)
        //        routeRequest.Options.Optimization = RouteOptimization.MinimizeDistance;
        //    else
        //        routeRequest.Options.Optimization = RouteOptimization.MinimizeTime;

        //    if (radioOpTrafik.IsChecked == true)
        //        routeRequest.Options.TrafficUsage = TrafficUsage.TrafficBasedTime;
        //    if (radioOpSTrafik.IsChecked == true)
        //        routeRequest.Options.TrafficUsage = TrafficUsage.TrafficBasedRouteAndTime;
        //    if (radioNOpTrafik.IsChecked == true)
        //        routeRequest.Options.TrafficUsage = TrafficUsage.None;

        //    if (radioDrvType.IsChecked == true)
        //        routeRequest.Options.Mode = TravelMode.Driving;
        //    else
        //        routeRequest.Options.Mode = TravelMode.Walking;

        //    for (int i = 0; i < ClientIslem.rotaPoints.Count; i++)
        //    {
        //        routeRequest.Waypoints.Add(ClientIslem.rotaPoints[i]);
        //    }

        //    if (ClientIslem.rotaPoints.Count < 2)
        //        MessageBox.Show("Harita üzerinde en az iki nokta belirlemelisiniz..","Dikkat!",MessageBoxButton.OK);
        //    else
        //        ClientIslem.yolAniProvider.CalculateRouteAsync(routeRequest);
        //}

        //private void btnYolSil_Click(object sender, RoutedEventArgs e)
        //{
        //    layerYol.Items.Clear();
        //    layerKare.Items.Clear();
        //    ClientIslem.rotaPoints = new LocationCollection();
        //}

        //private void btnArcUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBoxResult cvp = new MessageBoxResult();
        //    cvp=MessageBox.Show("Detaylar Güncellensin mi?", "Onay", MessageBoxButton.OKCancel);

        //    if (cvp == MessageBoxResult.OK)
        //    {
        //        if (Entities.Kullanici.KUL_AD.ToLower() == "demo")
        //            MessageBox.Show("Demo kullanıcısı bu özelliği kullanamaz..", "Dikkat!", MessageBoxButton.OK);
        //        else
        //        {
        //            if (dComboImg.SelectedIndex == -1)
        //            {
        //                MessageBox.Show("Lütfen aracınıza ikon seçin..", "Dikkat!", MessageBoxButton.OK);
        //            }
        //            else
        //            {
        //                Entities.Arac.ARC_TEL = dTel.Text;
        //                Entities.Arac.ARC_RENK = dRenk.Text;
        //                Entities.Arac.ARC_PLAKA = dPlaka.Text;
        //                Entities.Arac.ARC_MODEL = dModel.Text;
        //                Entities.Arac.ARC_CINS = dCins.Text;
        //                Entities.Arac.ARC_SAHIBI = dSahibi.Text;
        //                Entities.Arac.ARC_MARKA = dMarka.Text;
        //                Entities.Arac.ARC_ICON = dComboImg.SelectedIndex.ToString();

        //                arc_sr.updateAracAsync(Entities.Arac);
        //                int index = Entities.getArcListIndex(Entities.Arac.ARC_SERINO);
        //                Entities.AracDataList[index].Arac = Entities.Arac;
        //                Entities.AracDataList[index].AracIcon = Entities.AracIconList[dComboImg.SelectedIndex];
        //                LayerKro();
        //            }
        //        }
        //    }
        //}

        //private void btnDene_Click(object sender, RoutedEventArgs e)
        //{
           
        //}

        //private void cYol_C(object sender, RoutedEventArgs e)
        //{
        //    eskiYolCiz();
        //}

        //private void ArcSec_Checked(object sender, RoutedEventArgs e)
        //{
        //    string sn = ((CheckBox)sender).Tag.ToString();
        //    int index = Entities.getArcListIndex(int.Parse(sn));
        //    if (((CheckBox)sender).IsChecked == true)
        //    {
        //        Entities.AracDataList[index].Secili = true;
        //    }
        //    else
        //    {
        //        Entities.AracDataList[index].Secili = false;
        //    }

        //    if (AniBasTar.SelectedValue != null && AniBitTar.SelectedValue != null)
        //    {
        //        layerCalYol.Items.Clear();
        //        layerHamYol.Items.Clear();
        //        layerArac.Items.Clear();
        //        ClientIslem.initVariable("GecmisYollar");
        //        sycGecmisReq = 0;
        //        sycGecmisRes = 0;
        //        for (int i = 0; i < Entities.AracDataList.Count; i++)
        //        {
        //            if (Entities.AracDataList[i].Secili == true)
        //            {
        //                ServisIslemler.data_sr.getDataListAsync(Entities.AracDataList[i].Arac.ARC_SERINO, DateTime.Parse(AniBasTar.SelectedValue.ToString()), DateTime.Parse(AniBitTar.SelectedValue.ToString()));
        //                sycGecmisReq++;
        //            }
        //        }

        //    }

        //}

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.Items.Count>0)
            if (PaneAraclar.IsHidden)
                PaneAraclar.IsHidden = false;
            else
                PaneAraclar.IsHidden = true;
        }

        //private void btnDetaylar_Click(object sender, RoutedEventArgs e)
        //{
        //    PaneSelect("HideAll");
        //    PaneMasterSelect("PaneArcDetay");
        //    if (listBox1.SelectedIndex == -1)
        //        if (listBox1.Items.Count>0)
        //            listBox1.SelectedIndex = 0;
        //}
        
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!PaneArcDetay.IsHidden)
            //    if (listBox1.SelectedIndex!=-1)
            //        aracDetayGetir(Entities.AraclarList[listBox1.SelectedIndex].ARC_SERINO);

            if (!PaneMap.IsHidden)
               if (listBox1.SelectedIndex != -1)
               {
                   int index = Entities.getArcListIndex(Entities.Arac.ARC_SERINO);
                   double en1 = double.Parse(Entities.AracDataList[index].CHZDATA.CHZ_ENLEM);
                   double boy1 = double.Parse(Entities.AracDataList[index].CHZDATA.CHZ_BOYLAM);
                   Location lm = new Location(en1, boy1);
                   RadMap1.Center = lm;

                   for (int i = 0; i < Entities.CihazlarList.Count; i++)
                   {
                       if (Entities.Arac.ARC_SERINO == Entities.CihazlarList[i].CHZ_SERINO)
                       {
                           if (Entities.CihazlarList[i].CHZ_CIHAZAGNO != null)
                           {
                               if (Entities.tablelist == null)
                                   Entities.tablelist = new clientfonksiyon();
                               //MessageBox.Show(Entities.CihazlarList[i].CHZ_CIHAZAGNO.ToString());
                               Entities.tablelist.CIHAZAGNO = Entities.CihazlarList[i].CHZ_CIHAZAGNO.ToString();
                               Entities.tablelist.CIHAZADI = Entities.CihazlarList[i].CHZ_CIHAZAD.ToString();
                           }
                       }
                   }
            

               }


          
         
         
            //if (!PaneMap.IsHidden)
            //    if (listBox1.SelectedIndex != -1)
            //    {
            //        int index = Entities.getArcListIndex(Entities.Arac.ARC_SERINO);
            //        double en1 = double.Parse(Entities.AracDataList[index].CHZDATA.CHZ_ENLEM);
            //        double boy1 = double.Parse(Entities.AracDataList[index].CHZDATA.CHZ_BOYLAM);
            //        Location lm = new Location(en1, boy1);
            //        RadMap1.Center = lm;
            //    }



        }

        //private void btnFup_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog Filedialog = new OpenFileDialog();
        //    Filedialog.Multiselect = false;
        //    if (Filedialog.ShowDialog()==true)
        //    {
        //        FS = Filedialog.File.OpenRead();
        //        FileName = Filedialog.File.Name;
        //        fup_sr.DosyaBoyutAsync(FileName);
        //    }
        //}


        //void fup_sr_DosyaBoyutCompleted(object sender, DosyaBoyutCompletedEventArgs e)
        //{
        //    Byte[] Data = new byte[10000];
        //    FS.Seek(0, SeekOrigin.Begin);
        //    FS.Read(Data, 0, Data.Length);
        //    fup_sr.KaydetAsync(Data,FileName);
        //}

        //void fup_sr_KaydetCompleted(object sender, KaydetCompletedEventArgs e)
        //{
        //    Byte[] Data = new byte[10000];
        //    if (e.Result < FS.Length)
        //    {
        //        FS.Read(Data, 0, Data.Length);
        //        fupBusy.IsBusy = true;
        //        fupBusy.BusyContent = "Yükleniyor.." + fupHesapla(FS.Position) + " \\ " + fupHesapla(FS.Length);
        //        fup_sr.KaydetAsync(Data, FileName);
        //    }
        //    else
        //    {
        //        fupBusy.IsBusy = false;
        //        string resPath = @"http://localhost:2564/documents/images/" + FileName;
        //        Entities.Arac.ARC_RES1M = resPath;
        //        dimgUpload.Source = new BitmapImage(new Uri(resPath, UriKind.Absolute));
        //    }
        //}

        //string fupHesapla(long BBB)
        //{
        //    return Math.Round(BBB/1000000.0,2).ToString();
        //}

        private void SizeChanged1(object sender, SizeChangedEventArgs e)
        {

            if (RadDockAna.Height != App.Current.Host.Content.ActualHeight - 100.0)
                if (App.Current.Host.Content.ActualHeight - 100.0 > 0.0)
                {
                    RadDockAna.Height = App.Current.Host.Content.ActualHeight - 100.0;
                    scroll1.Height = RadDockAna.Height - 30;
                }
        }

        //private void btnTanimlar_Click(object sender, RoutedEventArgs e)
        //{
        //    PaneSelect("HideAll");
        //    PaneSelect("PaneTanim1");
        //    PaneMasterSelect("PaneAnime");
        //    PaneAraclar.IsHidden = true;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (infoLayer1.Tag != null)
            {
                infoLayer1.ItemTemplate = layerDetayOut.ItemTemplate;
            }
        }

        //private void btnTablo_Click(object sender, RoutedEventArgs e)
        //{
        //    PaneMasterSelect("PaneSonDurum");
        //    PaneSelect("HideAll");
        //}

        private void RadPaneGroup_SelectionChanged(object sender, Telerik.Windows.Controls.RadSelectionChangedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //PanoSecWin.Close();

                if (listBox1.SelectedIndex != -1)
                {


                    int index = listBox1.SelectedIndex;

                    Entities.Cihaz = Entities.CihazlarList[index];
                    Entities.Arac = Entities.AraclarList[index];
                   
                    //PanoSecWin.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterScreen;
                    //PanoSecWin.Width = 965;
                    //PanoSecWin.Height = 740;
                   
                    //PanoSecWin.Show();

                 
                  
                    //if (infoLayer1.Tag != null)
                    //{
                    //    infoLayer1.ItemTemplate = layerDetayOut.ItemTemplate;
                    //}


                  
                    //layerDetayIn.Items.Clear();
                    //((InformationLayer)sender).Items.Add(Entities.AracDataList[index]);
                    //((InformationLayer)sender).ItemTemplate = layerDetayIn.ItemTemplate;
                    //infoLayer1 = ((InformationLayer)sender);
                }
            
        }

       

       
 
       

      


       
    }

}
