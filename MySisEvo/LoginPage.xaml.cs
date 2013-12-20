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
using System.Windows.Navigation;
using System.Windows.Shapes;
//using MySisEvo.KUL_SR;
using MySisEvo.CHZ_RS;
//using MySisEvo.ARC_SR;
//using MySisEvo.DATA_RS;
using System.ServiceModel;
using MySisEvo.Classes;
using MySisEvo.Controls;
using System.Net.Sockets;
using System.Text;
using Telerik.Windows.Controls.Map;
using System.Windows.Threading;

namespace MySisEvo
{
    public partial class LoginPage : UserControl
    {
        //KullaniciServisClient kul_sr = new KullaniciServisClient();
        //AracServisClient arc_sr = new AracServisClient();
        //CihazlarServisClient chz_sr = new CihazlarServisClient();
        string ham;
        string hamveri;
        string veri, veriilk;
        string istenen;
        double[,] degerler = new double[250, 500];
        int basadres;
        int no, islem, adet;
        int j, i, a1, a2, toplam;
        short svr,isaret;
        ushort uvr;
        double usvr;
        uint uuvr; 
        private SocketAsyncEventArgs MyAsenkronOlay;
        private string gelen;
        private byte[] ServerData;
        public delegate void SetCallBack(string Textim);
        public static string[,] mesaj = new string[10, 10];

        public LoginPage()
        {
            InitializeComponent();

            //Entities.DatasList = new System.Collections.ObjectModel.ObservableCollection<Datas>();
            //Entities.DatasYol=new System.Collections.ObjectModel.ObservableCollection<Datas>();
            Entities.AracDataList = new System.Collections.ObjectModel.ObservableCollection<AracData>();
            Entities.CihazlarList = new System.Collections.ObjectModel.ObservableCollection<Cihazlar>();
            MapLink.Visibility = Visibility.Collapsed;
            LoginLink.Visibility = Visibility.Visible;
            DiagnostikLink.Visibility = Visibility.Collapsed;
            RaporPageLink.Visibility = Visibility.Collapsed;
            HakkindaLink.Visibility = Visibility.Collapsed;
            TanimLink.Visibility = Visibility.Collapsed;
            ServisIslemler.chz_sr.getCihazlarCompleted += new EventHandler<getCihazlarCompletedEventArgs>(chz_sr_getCihazlarCompleted);
            ServisIslemler.chz_sr.getKullaniciCompleted += new EventHandler<getKullaniciCompletedEventArgs>(kul_sr_getKullaniciCompleted);
            //ServisIslemler.chz_sr.getAniDegerlerCompleted += new EventHandler<getAniDegerlerCompletedEventArgs>(chz_sr_getAniDegerlerCompleted);
            //chz_sr.getAraclarCompleted += new EventHandler<getAraclarCompletedEventArgs>(arc_sr_getAraclarCompleted);
            //ServisIslemler.data_sr.getDataCompleted+=new EventHandler<getDataCompletedEventArgs>(data_sr_getDataCompleted);
            //ServisIslemler.data_sr.getDatasCompleted += new EventHandler<getDatasCompletedEventArgs>(data_sr_getDatasCompleted);

            MyAsenkronOlay = new SocketAsyncEventArgs();

            MyAsenkronOlay.RemoteEndPoint = SocketIslemler.MyEndPoint;
            MyAsenkronOlay.Completed +=new EventHandler<SocketAsyncEventArgs>(MyAsenkronOlay_Completed);

            SocketIslemler.MysisSocket.ConnectAsync(MyAsenkronOlay);

            DispatcherTimer conControl = new DispatcherTimer();
            conControl.Interval = TimeSpan.FromSeconds(2);
            conControl.Tick += new EventHandler(conControl_Tick);

            if (ClientIslem.LoginIn==false)
            {
                //ContentFrame.Navigate(new Uri("Home", UriKind.Relative));
                ContentFrame.Navigate(LoginLink.NavigateUri);
            }

            ClientIslem.setAracImages();
            
        }

        

        void chz_sr_getCihazlarCompleted(object sender, getCihazlarCompletedEventArgs e)
        {
            if(e.Result!=null)
            {

                Entities.CihazlarList = e.Result;
                Entities.AraclarList = new System.Collections.ObjectModel.ObservableCollection<Araclar>();
                for (int i = 0; i < Entities.CihazlarList.Count;i++ )
                {
                    //if (Entities.AracDataList.Count < i+1)
                        AracData ad=new AracData();
                        ad.CHZDATA = Entities.CihazlarList[i];
                        ad.SYCDATA = new Sayac();
                        if (i==0)
                        {
                            ad.SYCDATA.syc_kGuc = "2000 kVA";
                            ad.SYCDATA.syc_demand = "001.442";
                            ad.SYCDATA.syc_takt = "03541.362";
                            ad.SYCDATA.syc_tind = "02100.321";
                            ad.SYCDATA.syc_tkap = "00146.654";

                        }
                        if (i == 1)
                        {
                            ad.SYCDATA.syc_kGuc = "1500 kVA";
                            ad.SYCDATA.syc_demand = "001.068";
                            ad.SYCDATA.syc_takt = "04305.819";
                            ad.SYCDATA.syc_tind = "03062.614";
                            ad.SYCDATA.syc_tkap = "00102.148";

                        }
                        if (i == 2)
                        {
                            ad.SYCDATA.syc_kGuc = "500 kVA";
                            ad.SYCDATA.syc_demand = "000.116";
                            ad.SYCDATA.syc_takt = "01086.486";
                            ad.SYCDATA.syc_tind = "00143.459";
                            ad.SYCDATA.syc_tkap = "00058.859";

                        }
                        if (i == 3)
                        {
                            ad.SYCDATA.syc_kGuc = "2000 kVA";
                            ad.SYCDATA.syc_demand = "001.416";
                            ad.SYCDATA.syc_takt = "04679.057";
                            ad.SYCDATA.syc_tind = "00578.956";
                            ad.SYCDATA.syc_tkap = "00403.340";

                        }
                        if (i == 4)
                        {
                            ad.SYCDATA.syc_kGuc = "1000 kVA";
                            ad.SYCDATA.syc_demand = "000.500";
                            ad.SYCDATA.syc_takt = "00541.380";
                            ad.SYCDATA.syc_tind = "00100.347";
                            ad.SYCDATA.syc_tkap = "00046.377";

                        }
                        Entities.AracDataList.Add(ad);
                        Araclar arc = new Araclar();
                        arc.ARC_SERINO = ad.CHZDATA.CHZ_SERINO;
                        arc.ARC_PLAKA =ad.CHZDATA.CHZ_FIRMA;
                        if (arc.ARC_PLAKA.Length>23)
                            arc.ARC_PLAKA = arc.ARC_PLAKA.Substring(0,23)+"...";
                        Entities.AraclarList.Add(arc);
                    //Entities.AracDataList[i].CHZDATA = Entities.CihazlarList[i];
                }
                // login olduktan sonra giren kullanıcıya tanımlı olan cihazlar cihazlarlİste ekleniyor
                // seri no larıda var artık oradan seçtiğin serino lu cihazı bulur bilgileri alabilirsin

                loginBusy.IsBusy = false;
                loginBusy.Visibility = Visibility.Collapsed;
                loginBusy.IsEnabled = false;
                ContentFrame.Navigate(MapLink.NavigateUri);
            }
        }

       

        void conControl_Tick(object sender, EventArgs e)
        {
            
        }

        //void data_sr_getDataCompleted (object sender,getDataCompletedEventArgs e)
        //{
        //    if (e.Result != null)
        //    {
        //        //Entities.DatasList.Add(e.Result);
        //        for (int i = 0; i < Entities.AraclarList.Count; i++)
        //        {
        //            if (Entities.AraclarList[i].ARC_SERINO == e.Result.ARC_SER1NO)
        //            {
        //                Entities.DatasList[i] = e.Result;
        //                Entities.DatasYol[i] = e.Result;
        //                Entities.AracDataList[i].Data = e.Result;
                        
        //                ClientIslem.geoIndexList.Add(i);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Entities.DatasList.Add(new Datas());
        //        Entities.DatasYol.Add(new Datas());
        //    }
        //}

        //void data_sr_getDatasCompleted(object sender, getDatasCompletedEventArgs e)
        //{
        //    if (e.Result != null)
        //    {
        //        //Entities.DatasList.Add(e.Result);
        //        Entities.DatasList = e.Result;
        //        Entities.DatasYol= e.Result;

        //        for (int i = 0; i < Entities.AraclarList.Count; i++)
        //        {
        //            Entities.AracDataList[i].Data = e.Result[i];
        //             ClientIslem.geoIndexList.Add(i);
        //        }
        //    }

        //    loginBusy.IsBusy = false;

        //}

        void MyAsenkronOlay_Completed(object sender, SocketAsyncEventArgs e)
        {
            SocketIslemler.MysisSocketOlay = new SocketAsyncEventArgs();
            SocketIslemler.MysisSocketOlay.RemoteEndPoint = SocketIslemler.MyEndPoint;
            e.Completed -= new EventHandler<SocketAsyncEventArgs>(MyAsenkronOlay_Completed);
            e.Completed += new EventHandler<SocketAsyncEventArgs>(Veriyi_Al);
            ServerData = new byte[512];
            e.SetBuffer(ServerData, 0, ServerData.Length);
            SocketIslemler.MysisSocket.ReceiveAsync(e);
        }

        void Veriyi_Al(object sender, SocketAsyncEventArgs e)
        {
            gelen = Encoding.UTF8.GetString(ServerData, e.Offset, e.BytesTransferred);
            SetText(gelen);
            SocketIslemler.MysisSocket.ReceiveAsync(e);

        }

        void SetText(string Data)
        {
            //this.Dispatcher.BeginInvoke(new SetCallBack(TextboxGuncelle), Data);
           this.Dispatcher.BeginInvoke(new SetCallBack(SokettenGelen), Data);
            
        }

        private string syc_ham = "";
        string strTedas = "";
        void SokettenGelen(string gelen)
        {
            if (gelen.IndexOf("Hattayok") > -1)
            {
                syc_ham = "";
                gelen = "";
                Entities.t_okunanveri = "";
                Entities.istenen = "";
                SocketIslemler.myDispatcher.BeginInvoke(SocketIslemler.sk, "Hata");
                MessageBox.Show("Cihaz hatta değil. Lütfen 4 dk sonra tekrar deneyiniz.", "Dikkat", MessageBoxButton.OK);
            }

            if ((gelen.IndexOf("sayacal") > -1) || (syc_ham.IndexOf("sayacal") > -1) || Entities.istenen == "sayacal")
            {
                syc_ham = syc_ham + gelen;
                syc_ham = syc_ham.Replace("sayacal","");
                int syc2 = 0;
                if (syc_ham.IndexOf("tedassycal") > -1)
                {
                    strTedas = syc_ham.Substring(syc_ham.IndexOf("tedassycal"));
                    for (int i=11;i<strTedas.Length;i++)
                    {
                        if (strTedas[i] == Convert.ToChar(1))
                        {
                            syc2++;
                            if (syc2 == 9)
                            {
                                strTedas = strTedas.Substring(0, i + 2);
                                syc_ham = syc_ham.Replace(strTedas, "");
                                break;
                            }
                                
                        }
                        
                            
                    }
                }
                if (syc_ham.IndexOf(Convert.ToChar(0))>-1)
                    syc_ham = syc_ham.Replace(Convert.ToChar(0), '.');
                SocketIslemler.myDispatcher.BeginInvoke(SocketIslemler.sk, syc_ham);
                
                if (syc_ham.IndexOf("!") > -1)
                {
                    if (syc_ham.IndexOf("!") > -1)
                        syc_ham = syc_ham.Replace(syc_ham.Substring(syc_ham.IndexOf("!")), "");

                    strTedas = "";
                    Entities.t_okunanveri = syc_ham;
                    ServisIslemler.chz_sr.getSayacDegerlerAsync(syc_ham);
                    
                    syc_ham = "";
                    gelen = "";
                    Entities.istenen = "";
                }else if (syc_ham.IndexOf("96.77.4") > -1 || syc_ham.IndexOf("8.8.4") > -1)
                {
                    Entities.t_okunanveri = syc_ham;
                    ServisIslemler.chz_sr.getSayacDegerlerAsync(syc_ham);
                }
            }
            if (gelen.IndexOf("enerjial") > -1)
            {
                if (Entities.istenen == "anideger")
                {
                    gelen=gelen.Substring(11);
                    gelen = gelen;
                    ServisIslemler.chz_sr.getAniDegerlerAsync(gelen);
                }
            }
        }
        void TextboxGuncelle(string Datam)
        {

            //MessageBox.Show(Datam);
            string digital_direnc;
            string voltaj;
            string referans_voltaj;
            string akim;
            string remove;
            string glnveri = Datam;
            string sycveri = Datam;
            veri = Datam;
          
            //ôôô  $#katotikoku#100#000#050#000#$
            if (glnveri.IndexOf("sayacal") != -1) // sayac okuma
            {
                remove = glnveri.Remove(0, 9);
                if (glnveri.IndexOf("sayacal") > 0)
                    remove = glnveri.Remove(0, glnveri.IndexOf("sayacal") + 9);
                sycveri = sycveri + glnveri;

                if (sycveri.IndexOf("1.8.0(") != -1)
                {
                    Entities.SayacDegerler = new SayacData();
                    sycveri = sycveri.Replace("sayacal", "");
                    Entities.SayacDegerler.SYC_HAM = sycveri;
                    Entities.SayacDegerler.SYC_DURUM = true;
                    Entities.SayacDegerler.SYC_T = sycveri.Substring(sycveri.IndexOf("1.8.0(") + 6, 9);
                }
                if (sycveri.IndexOf("5.8.0(") != -1)
                    Entities.SayacDegerler.SYC_1ND = sycveri.Substring(sycveri.IndexOf("5.8.0(") + 6, 9);
                if (sycveri.IndexOf("8.8.0(") != -1)
                    Entities.SayacDegerler.SYC_KAP = sycveri.Substring(sycveri.IndexOf("8.8.0(") + 6, 9);
                if (sycveri.IndexOf("1.8.1(") != -1)
                    Entities.SayacDegerler.SYC_T1 = sycveri.Substring(sycveri.IndexOf("1.8.1(") + 6, 9);
                if (sycveri.IndexOf("1.8.2(") != -1)
                    Entities.SayacDegerler.SYC_T2 = sycveri.Substring(sycveri.IndexOf("1.8.2(") + 6, 9);
                if (sycveri.IndexOf("1.8.3(") != -1)
                    Entities.SayacDegerler.SYC_T3 = sycveri.Substring(sycveri.IndexOf("1.8.3(") + 6, 9);



            }

            if (glnveri.IndexOf("$#katotikoku#") != -1) // katotik okuma
            {
                glnveri = glnveri.Substring(glnveri.IndexOf("$#katotikoku#"));
                remove = glnveri.Remove(0, 13);
                digital_direnc = remove.Substring(0, remove.IndexOf("#"));
                remove = remove.Remove(0, remove.IndexOf("#") + 1);
                voltaj = remove.Substring(0, remove.IndexOf("#"));
                remove = remove.Remove(0, remove.IndexOf("#") + 1);
                referans_voltaj = remove.Substring(0, remove.IndexOf("#"));
                remove = remove.Remove(0, remove.IndexOf("#") + 1);
                akim = remove.Substring(0, remove.IndexOf("#"));

                Entities.katdegerler = new katotik();
                if (digital_direnc.Length > 0)
                    Entities.katdegerler.DIGITAL = digital_direnc;
                if (voltaj.Length > 0)
                    Entities.katdegerler.VOLTAJ = voltaj.Substring(0, 2) + "," + voltaj.Substring(2);
                if (referans_voltaj.Length > 0)
                    Entities.katdegerler.REFVOLTAJ = referans_voltaj.Substring(0, 1) + "," + referans_voltaj.Substring(1);
                if (akim.Length > 0)
                    Entities.katdegerler.AKIM = akim.Substring(0, 2) + "," + akim.Substring(2);


            }

            string metin = gelen;

            string strbas = "$#katotikoku#";
            string strson = "#$";

            while (metin.Length > 0)
            {

                if (metin.IndexOf(strson) != -1)
                {
                    string paket = metin.Substring(0, metin.IndexOf(strson) + strson.Length);
                   // SrvIslemler.GelenGenelPro(paket);
                    metin = metin.Substring(metin.IndexOf(strson) + strson.Length);
                    if (metin.IndexOf(strbas) != -1)
                        metin = metin.Substring(metin.IndexOf(strbas));
                }
                else
                    metin = "";

            }
//--------------------------------------------------------------------------------------------------------------------------
            string abir, a1t, aiki, a2t, i1, i1t, i2, k1, k2, serino;


            
            if (veri.IndexOf("reaktifenerji")!=-1)
            {
                
                veri = veri.Remove(0, 14);
          
                serino = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)));


                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1)));
               

                veri = veri.Remove(0, 3);
             
                a1t = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) );
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1))+1);
                a2t = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)));
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1))+1);
                abir = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) );
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1))+1);
                aiki = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) );
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1))+1);
                i1 = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) );
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1))+1);
                i2 = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) );
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1))+1);
                k1 = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)));
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1))+1);
                k2 = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) );
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1))+1);
                veri = veri.Remove(0, 1);

                Entities.tablelist.AK1 = abir;
                Entities.tablelist.AK2 = aiki;
                Entities.tablelist.IN1 = i1;
                Entities.tablelist.IN2 = i2;
                Entities.tablelist.KAP1 = k1;
                Entities.tablelist.KAP2 = k2;


                if (((Convert.ToDouble(aiki)) - (Convert.ToDouble(abir)))>0) 
                
                {

                    Entities.tablelist.INDORAN = (((Convert.ToDouble(i2)) - (Convert.ToDouble(i1))) / ((Convert.ToDouble(a2)) - (Convert.ToDouble(a1))) * 100).ToString("##.00");
                    Entities.tablelist.KAPORAN = (((Convert.ToDouble(k2)) - (Convert.ToDouble(k1))) / ((Convert.ToDouble(a2)) - (Convert.ToDouble(a1))) * 100).ToString("##.00");
                
                
                
                
                
                }
            
            
            
            
            
            }


            if (veri.IndexOf("Sycdegerleri") != -1)
            {
                // serverdan gelen veri görününce yapılacak.
                veri = veri.Remove(0, 13);
                Entities.tablelist.SYCAKTIF = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) - 1);
                veri=veri.Remove(0,veri.IndexOf(Convert.ToChar(1)));
                Entities.tablelist.SYCINDKTF = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) - 1);
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1)));
                Entities.tablelist.SYCKPSTIF = veri.Substring(0, veri.IndexOf(Convert.ToChar(1)) - 1);
                veri = veri.Remove(0, veri.IndexOf(Convert.ToChar(1)));
            
            
            
            
            
            
            
            }


//------------------------------------------------------------------------------------------------------------------------

            if (veri.IndexOf("$3$52$") != -1)

            {
                veri = veri.Remove(0, veri.IndexOf("$") - 1);
                ham = veri;

                istenen = "sabitler";
            
            }
            else

            if (veri.IndexOf("$3$180$") != -1)
            {

                veri = veri.Remove(0, veri.IndexOf("$") - 1);
                ham = veri;

                istenen = "enertum";

            }
            else
                if (veri.IndexOf("$3$100$") != -1)
                {
                    veri = veri.Remove(0, veri.IndexOf("$") - 1);
                    ham = veri;

                    istenen = "enerharcanan";

                }
                else
                    if (veri.IndexOf("$3$150$") != -1)
                    {
                       
                        istenen = "kondansator";

                    }
                    else
                        if (veri.IndexOf("$3$204$") != -1)
                        {
                          
                            istenen = "harmonikkael";

                        }
                        else
                        {
                            istenen = "";

                        }
            if (istenen == "")
            {


            }

            if (istenen == "enertum")
            {

                basadres = 0;


            }
            if (istenen == "enerharcanan")
            {

                basadres = 90;


            }
            if (istenen == "kondansator")
            {

                basadres = 157;


            }
            if (istenen == "harmonikkael")
            {

                basadres = 300;


            }
            if (istenen == "sabitler")
            {

                basadres = 129;
            
            }

            try
            {

                if (ham != null)
                {
                    no = Int32.Parse(ham.Substring(0, ham.IndexOf("$")));

                    //for (j = 0; j <= 110; j++)
                    //{
                    //    degerler[no, j] = 0;
                    //}
                    ham = ham.Remove(1, ham.IndexOf("$"));


                    islem = Int32.Parse(ham.Substring(1, ham.IndexOf("$") - 1));
                    ham = ham.Remove(1, ham.IndexOf("$"));
                    adet = Int32.Parse(ham.Substring(1, ham.IndexOf("$") - 1));
                    ham = ham.Remove(1, ham.IndexOf("$"));
                    for (i = 1; i <= (adet / 2); i++)
                    {
                        a1 = Int32.Parse(ham.Substring(1, ham.IndexOf("$") - 1));
                        ham = ham.Remove(1, ham.IndexOf("$"));

                        a2 = Int32.Parse(ham.Substring(1, ham.IndexOf("$") - 1));
                        ham = ham.Remove(1, ham.IndexOf("$"));
                        toplam = a1 * 256 + a2;
                        degerler[no, basadres] = toplam;
                        basadres = basadres + 1;
                    }

                }
            }
            catch { }
               
            Entities.tablelist = new clientfonksiyon();


                Entities.tablelist.abcd = Datam;

               
                uvr = Convert.ToUInt16(degerler[1, 0]);
                Entities.tablelist.LBL_AKIMTRAFOSU = uvr.ToString();






                uvr = Convert.ToUInt16(degerler[1, 2]);
                usvr = uvr * 0.1;
                Entities.tablelist.LBL_VR = usvr.ToString("##000.0");
                uvr = Convert.ToUInt16(degerler[1, 3]);
                usvr = uvr * 0.1;
                Entities.tablelist.LBL_VS = usvr.ToString("##000.0");

                uvr = Convert.ToUInt16(degerler[1, 4]);
                usvr = uvr * 0.1;
                Entities.tablelist.LBL_VT = usvr.ToString("##000.0");

                uvr = Convert.ToUInt16(degerler[1, 5]);
                usvr = (uvr * 0.001)*(Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_IR = usvr.ToString("##000.0");

                uvr = Convert.ToUInt16(degerler[1, 6]);
                usvr = (uvr * 0.001) * (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_IS = usvr.ToString("##000.0");
                uvr = Convert.ToUInt16(degerler[1, 7]);
                usvr = (uvr * 0.001) * (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_IT = usvr.ToString("##000.0");
                uvr = Convert.ToUInt16(degerler[1, 8]);
                usvr = (uvr * 0.001) * (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KWR = usvr.ToString("##000.00");
                uvr = Convert.ToUInt16(degerler[1, 9]);
                usvr = (uvr * 0.001) * (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KWS = usvr.ToString("##000.00");
                uvr = Convert.ToUInt16(degerler[1, 10]);
                usvr = (uvr * 0.001) * (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KWT = usvr.ToString("##000.00");
                uvr = Convert.ToUInt16(degerler[1, 25]);
                usvr = (uvr * 0.001) * (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KWET = usvr.ToString("##00000.00");

                //svr bas------------------

                degerler[1, 11] = Math.Round(degerler[1, 11], 0);
                //svr = Convert.ToInt16(degerler[1, 11]);
                svr = UnsignedToInteger(Convert.ToUInt16(degerler[1, 11]));
                usvr = (svr * 0.001) / (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KVARR = usvr.ToString("##000.00");
                
                degerler[1, 12] = Math.Round(degerler[1, 12]);
                //svr = Convert.ToInt16(degerler[1, 12]);
                svr = UnsignedToInteger(Convert.ToUInt16(degerler[1, 12]));
                usvr = (svr * 0.001)/(Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KVARS = usvr.ToString("##000.00");


                degerler[1, 13] = Math.Round(degerler[1, 13]);
                //svr = Convert.ToInt16(degerler[1, 13]);
                svr = UnsignedToInteger(Convert.ToUInt16(degerler[1, 13]));
                usvr = (svr * 0.001) / (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KVART = usvr.ToString("##000.00");
                //svr end-------------------

                uvr = Convert.ToUInt16(degerler[1, 14]);
                usvr = (uvr * 0.001) / (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KVAR = usvr.ToString("##000.00");
                uvr = Convert.ToUInt16(degerler[1, 15]);
                usvr = (uvr * 0.001) / (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KVAS = usvr.ToString("##000.00");
                uvr = Convert.ToUInt16(degerler[1, 16]);
                usvr = (uvr * 0.001) / (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KVAT = usvr.ToString("##000.00");
                uvr = Convert.ToUInt16(degerler[1, 29]);

                usvr = (uvr * 0.001) / (Convert.ToUInt16(Entities.tablelist.LBL_AKIMTRAFOSU));
                Entities.tablelist.LBL_KVAET = usvr.ToString("##00000.00");

                //svr start-----------------------

                if (degerler[1, 17] > 32767)
                {
                   usvr = Math.Round(degerler[1, 17]);
                    unchecked
                    {
                        svr = (short)usvr;


                    }

                    usvr = svr * 0.001;
                    Entities.tablelist.LBL_COS1 = usvr.ToString("F3");
                }
                else
                {
                    svr = Convert.ToInt16(degerler[1, 17]);
                    usvr = svr * 0.001;
                    Entities.tablelist.LBL_COS1 = usvr.ToString("F3");
                
                
                }
                if (degerler[1, 18] > 32767)
                {
                   usvr= Math.Round(degerler[1, 18]);
                    unchecked
                    {
                        svr = (short)usvr;


                    }
                    
                    usvr = svr * 0.001;
                    Entities.tablelist.LBL_COS2 = usvr.ToString("F3");
                }
                else
                {
                    svr = Convert.ToInt16(degerler[1, 18]);
                    usvr = svr * 0.001;
                    Entities.tablelist.LBL_COS2 = usvr.ToString("F3");


                }


                if (degerler[1, 19] > 32767)
                {
                   usvr = Math.Round(degerler[1, 19]);
                    unchecked
                    {
                        svr = (short)usvr;


                    }
                    
                    usvr = svr * 0.001;
                    Entities.tablelist.LBL_COS3 = usvr.ToString("F3");

                }
                else
                {
                    svr = Convert.ToInt16(degerler[1, 19]);
                    usvr = svr * 0.001;
                    Entities.tablelist.LBL_COS3 = usvr.ToString("F3");


                }

                if (degerler[1, 20] > 32767)
                {
                    usvr = Math.Round(degerler[1, 20]);

                    unchecked
                    {
                        svr = (short)usvr;


                    }
                    
                   
                    usvr = svr * 0.001;
                    Entities.tablelist.LBL_COS = usvr.ToString("F3");
                }
                else
                {
                    svr = Convert.ToInt16(degerler[1, 20]);
                    usvr = svr * 0.001;
                    Entities.tablelist.LBL_COS = usvr.ToString("F3");


                }
                uvr = Convert.ToUInt16(degerler[1, 26]);
                usvr = uvr * 0.001;

                Entities.tablelist.LBL_RL = usvr.ToString("##000.0");
               
                uvr = Convert.ToUInt16(degerler[1, 27]);
                usvr = uvr * 0.001;

                Entities.tablelist.LBL_RC = usvr.ToString("##000.0");

                degerler[1, 28] = Math.Round(degerler[1, 28]);
                uvr = Convert.ToUInt16(degerler[1, 28]);
                usvr = uvr * 0.001;
                Entities.tablelist.LBL_DELTAR = usvr.ToString("##000.0");


                // Burdada sıkıntı
                uvr = Convert.ToUInt16(degerler[1, 30]);
                usvr = uvr * 0.1;
                Entities.tablelist.LBL_FREKANS = usvr.ToString();
                svr = Convert.ToInt16(degerler[1, 31]);

                Entities.tablelist.LBL_SICAKLIK = svr.ToString() + "⁰";

                // Bitis..

                uvr = Convert.ToUInt16(degerler[1, 62]);
                Entities.tablelist.LBL_INAKR = "%" + uvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 63]);
                Entities.tablelist.LBL_KAAKR = "%" + uvr.ToString();

                uvr = Convert.ToUInt16(degerler[1, 64]);
                Entities.tablelist.LBL_INAKS = "%" + uvr.ToString();


                uvr = Convert.ToUInt16(degerler[1, 65]);
                Entities.tablelist.LBL_KAAKS = "%" + uvr.ToString();

                uvr = Convert.ToUInt16(degerler[1, 66]);
                Entities.tablelist.LBL_INAKT = "%" + uvr.ToString();

                uvr = Convert.ToUInt16(degerler[1, 67]);
                Entities.tablelist.LBL_KAAKT = "%" + uvr.ToString();

                uvr = Convert.ToUInt16(degerler[1, 68]);
                Entities.tablelist.LBL_INAKET1 = "%" + uvr.ToString();

                uvr = Convert.ToUInt16(degerler[1, 69]);
                Entities.tablelist.LBL_KAAKET = "%" + uvr.ToString();


                usvr= Math.Round(degerler[1, 1]);
                uuvr = Convert.ToUInt32(usvr);
                Entities.tablelist.LBL_CBXOGREN = uuvr.ToString();

         
                usvr=Math.Round(degerler[1,47]);
                uuvr = Convert.ToUInt32(usvr);
                Entities.tablelist.LBL_CBXDURUM = uuvr.ToString();


                usvr = Math.Round(degerler[1, 46]);
                uuvr = Convert.ToUInt32(usvr);
                Entities.tablelist.LBL_CBXDURUM2 = uuvr.ToString();


                







                uvr = Convert.ToUInt16(degerler[1, 82]);
                usvr = uvr * 0.2;
                Entities.tablelist.LBL_SNONR = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 83]);
                usvr = uvr * 0.2;
                Entities.tablelist.LBL_SNOFFR = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 84]);
                usvr = uvr * 0.2;
                Entities.tablelist.LBL_SNONS = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 85]);
                usvr = uvr * 0.2;
                Entities.tablelist.LBL_SNOFFS = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 86]);
                usvr = uvr * 0.2;
                Entities.tablelist.LBL_SNONT = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 87]);
                usvr = uvr * 0.2;
                Entities.tablelist.LBL_SNOFFT = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 88]);
                usvr = uvr * 0.2;
                Entities.tablelist.LBL_SNONET = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 89]);
                usvr = uvr * 0.2;
                Entities.tablelist.LBL_SNOFFET = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 25]);
                usvr=uvr*0.001*degerler[1,0];
                Entities.tablelist.LBL_AKTIFGUC = usvr.ToString();
                uvr = Convert.ToUInt16(degerler[1, 26]);
                usvr = uvr * 0.001;
                Entities.tablelist.LBL_INAKET = usvr.ToString();


                usvr = (Convert.ToDouble(Entities.tablelist.LBL_AKTIFGUC)) / (Convert.ToDouble(Entities.tablelist.LBL_INAKET));
                Entities.tablelist.LBL_INAKET = usvr.ToString();
                // ====
                usvr = degerler[1, 104] + degerler[1, 103] * 65536 + degerler[1, 102] * 65536 * 65536;
               usvr = usvr / 3600000;
                
                Entities.tablelist.KWHR = usvr.ToString("0.000");
                //----------- sıkıntı
                usvr = degerler[1, 107] + degerler[1, 106] * 65536 + degerler[1, 105] * 65536 * 65536;
                usvr = usvr / 3600000;
               
                Entities.tablelist.KWHS = usvr.ToString("0.000");

                usvr = usvr = degerler[1, 110] + degerler[1, 109] * 65536 + degerler[1, 108] * 65536 * 65536;
                usvr = usvr / 3600000;

                Entities.tablelist.KWHT = usvr.ToString("0.000");
                Entities.tablelist.KWHET = (Convert.ToDouble(degerler[1, 92] + degerler[1, 91] * 65536 + degerler[1, 90] * 65536 * 65536) / 3600000).ToString("0.000");

                usvr = degerler[1, 113] + degerler[1, 112] * 65536 + degerler[1, 111] * 65536 * 65536;
                usvr = usvr / 3600000;

                Entities.tablelist.KVARLR = usvr.ToString("0.000");
               
                usvr = usvr = degerler[1, 116] + degerler[1, 115] * 65536 + degerler[1, 114] * 65536 * 65536;
                usvr = usvr / 3600000;

                Entities.tablelist.KVARLS = usvr.ToString("0.000");

                usvr = usvr = degerler[1, 119] + degerler[1, 118] * 65536 + degerler[1, 117] * 65536 * 65536;
                usvr = usvr / 3600000;

                Entities.tablelist.KVARLT = usvr.ToString("0.000");

                Entities.tablelist.KVARLET = (Convert.ToDouble(degerler[1, 95] + degerler[1, 94] * 65536 + degerler[1, 93] * 65536 * 65536) / 3600000).ToString("0.000");


                usvr = usvr = degerler[1, 122] + degerler[1, 121] * 65536 + degerler[1, 120] * 65536 * 65536;
                usvr = usvr / 3600000;

                Entities.tablelist.KVARCR = usvr.ToString("0.000");

                usvr = usvr = degerler[1, 125] + degerler[1, 124] * 65536 + degerler[1, 123] * 65536 * 65536;
                usvr = usvr / 3600000;

                Entities.tablelist.KVARCS = usvr.ToString("0.000");

                usvr = usvr = degerler[1, 128] + degerler[1, 127] * 65536 + degerler[1, 126] * 65536 * 65536;
                usvr = usvr / 3600000;

                Entities.tablelist.KVARCT = usvr.ToString("0.000");

                Entities.tablelist.KVARCET = (Convert.ToDouble(degerler[1, 98] + degerler[1, 97] * 65536 + degerler[1, 96] * 65536 * 65536) / 3600000).ToString("0.000");

                Entities.tablelist.TOPLAMINDORAN = ((Convert.ToDouble(Entities.tablelist.KVARLET)) / (Convert.ToDouble(Entities.tablelist.KWHET)) * 100).ToString("0.000");
                Entities.tablelist.TOPLAMKAPORAN = ((Convert.ToDouble(Entities.tablelist.KVARCET)) / (Convert.ToDouble(Entities.tablelist.KWHET)) * 100).ToString("0.000");
            //----------------Kademe Başlangıç---------------




                Entities.tablelist.KADEMESAYISI = degerler[1, 155].ToString();
            
            usvr = Math.Round(degerler[1, 160]);
                Entities.tablelist.KADEME1 = usvr.ToString();
                Entities.tablelist.GRPC1 = usvr.ToString();
                                             
                usvr = degerler[1, 161];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR1_R = usvr.ToString();
                    Entities.tablelist.KADEME1L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME1ISARET = isaret.ToString();



                }
                else {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR1_R = usvr.ToString();
                    Entities.tablelist.KADEME1L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME1ISARET = isaret.ToString();
                
                
             }
                usvr = degerler[1, 162];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR1_S = usvr.ToString();
                    Entities.tablelist.KADEME1L2 = usvr.ToString();



                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR1_S = usvr.ToString();
                    Entities.tablelist.KADEME1L2 = usvr.ToString();


                }
                usvr = degerler[1, 163];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR1_T = usvr.ToString();
                    Entities.tablelist.KADEME1L3 = usvr.ToString();



                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR1_T = usvr.ToString();
                    Entities.tablelist.KADEME1L3 = usvr.ToString();


                }

                usvr = Math.Round(degerler[1, 164]);
                Entities.tablelist.GRPC2 = usvr.ToString();
                Entities.tablelist.KADEME2 = usvr.ToString();
                usvr = degerler[1, 165];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR2_R = usvr.ToString();
                    Entities.tablelist.KADEME2L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME2ISARET = isaret.ToString();


                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR2_R = usvr.ToString();
                    Entities.tablelist.KADEME2L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME2ISARET = isaret.ToString();

                }
                usvr = degerler[1, 166];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR2_S = usvr.ToString();
                    Entities.tablelist.KADEME2L2 = usvr.ToString();
               }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR2_S = usvr.ToString();
                    Entities.tablelist.KADEME2L2 = usvr.ToString();


                }
                usvr = degerler[1, 167];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR2_T = usvr.ToString();
                    Entities.tablelist.KADEME2L3 = usvr.ToString();



                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR2_T = usvr.ToString();
                    Entities.tablelist.KADEME2L3 = usvr.ToString();


                }

                usvr = Math.Round(degerler[1, 168]);
                Entities.tablelist.GRPC3 = usvr.ToString();
                Entities.tablelist.KADEME3 = usvr.ToString();
                usvr = degerler[1, 169];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR3_R = usvr.ToString();
                    Entities.tablelist.KADEME3L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME3ISARET = isaret.ToString();
                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR3_R = usvr.ToString();
                    Entities.tablelist.KADEME3L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME3ISARET = isaret.ToString();


                }
                usvr = degerler[1, 170];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR3_S = usvr.ToString();
                    Entities.tablelist.KADEME3L2 = usvr.ToString();
                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR3_S = usvr.ToString();
                    Entities.tablelist.KADEME3L2 = usvr.ToString();


                }
                usvr = degerler[1, 171];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR3_T = usvr.ToString();
                    Entities.tablelist.KADEME3L3 = usvr.ToString();
                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR3_T = usvr.ToString();
                    Entities.tablelist.KADEME3L3 = usvr.ToString();


                }
                usvr = Math.Round(degerler[1, 172]);
                Entities.tablelist.GRPC4 = usvr.ToString();
                Entities.tablelist.KADEME4 = usvr.ToString();


                usvr = degerler[1, 173];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR4_R = usvr.ToString();
                    Entities.tablelist.KADEME4L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME4ISARET = isaret.ToString();
                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR4_R = usvr.ToString();
                    Entities.tablelist.KADEME4L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME4ISARET = isaret.ToString();

                }
                usvr = degerler[1, 174];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR4_S = usvr.ToString();
                    Entities.tablelist.KADEME4L2 = usvr.ToString();
                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR4_S = usvr.ToString();
                    Entities.tablelist.KADEME4L2 = usvr.ToString();


                }
                usvr = degerler[1, 175];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.CR4_T = usvr.ToString();
                    Entities.tablelist.KADEME4L3 = usvr.ToString();
                }
                else
                {

                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.CR4_T = usvr.ToString();
                    Entities.tablelist.KADEME4L3 = usvr.ToString();


                }

                usvr = Math.Round(degerler[1, 176]);
                Entities.tablelist.KADEME5 = usvr.ToString();

                usvr = degerler[1, 177];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME5L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME5ISARET = isaret.ToString();

                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME5L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME5ISARET = isaret.ToString();
                }

                usvr = degerler[1, 178];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME5L2 = usvr.ToString();


                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME5L2 = usvr.ToString();
                
                
                
                }
                usvr = degerler[1, 179];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME5L3 = usvr.ToString();


                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME5L3 = usvr.ToString();



                }

                usvr = Math.Round(degerler[1, 180]);
                Entities.tablelist.KADEME6 = usvr.ToString();

                usvr = degerler[1, 181];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME6L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME6ISARET = isaret.ToString();

                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME6L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME6ISARET = isaret.ToString();
                }

                usvr = degerler[1, 182];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME6L2 = usvr.ToString();


                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME6L2 = usvr.ToString();



                }
                usvr = degerler[1, 183];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME6L3 = usvr.ToString();


                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME6L3 = usvr.ToString();



                }


                usvr = Math.Round(degerler[1, 184]);
                Entities.tablelist.KADEME7 = usvr.ToString();

                usvr = degerler[1, 185];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME7L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME7ISARET = isaret.ToString();

                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME7L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME7ISARET = isaret.ToString();
                }

                usvr = degerler[1, 186];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME7L2 = usvr.ToString();


                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME7L2 = usvr.ToString();



                }
                usvr = degerler[1, 187];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME7L3 = usvr.ToString();


                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME7L3 = usvr.ToString();



                }


                usvr = Math.Round(degerler[1, 188]);
                Entities.tablelist.KADEME8 = usvr.ToString();
                usvr = degerler[1, 189];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME8L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME8ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME8L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME8ISARET = isaret.ToString();
                }

                usvr = degerler[1, 190];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME8L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME8L2 = usvr.ToString();
                }
                usvr = degerler[1, 191];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME8L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME8L3 = usvr.ToString();
                }

//---------------------------------
                usvr = Math.Round(degerler[1, 192]);
                Entities.tablelist.KADEME9 = usvr.ToString();
                usvr = degerler[1, 193];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME9L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME9ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME9L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME9ISARET = isaret.ToString();
                }

                usvr = degerler[1, 194];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME9L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME9L2 = usvr.ToString();
                }
                usvr = degerler[1, 195];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME9L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME9L3 = usvr.ToString();
                }

            //-----------------

                usvr = Math.Round(degerler[1, 196]);
                Entities.tablelist.KADEME10 = usvr.ToString();
                usvr = degerler[1, 197];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME10L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME10ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME10L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME10ISARET = isaret.ToString();
                }

                usvr = degerler[1, 198];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME10L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME10L2 = usvr.ToString();
                }
                usvr = degerler[1, 199];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME10L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME10L3 = usvr.ToString();
                }


            //-----------------------------

                usvr = Math.Round(degerler[1, 200]);
                Entities.tablelist.KADEME11 = usvr.ToString();
                usvr = degerler[1, 201];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME11L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME11ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME11L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME11ISARET = isaret.ToString();
                }

                usvr = degerler[1, 202];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME11L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME11L2 = usvr.ToString();
                }
                usvr = degerler[1, 203];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME11L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME11L3 = usvr.ToString();
                }

            //--------------

                usvr = Math.Round(degerler[1, 204]);
                Entities.tablelist.KADEME12 = usvr.ToString();
                usvr = degerler[1, 205];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME12L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME12ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME12L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME12ISARET = isaret.ToString();
                }

                usvr = degerler[1, 206];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME12L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME12L2 = usvr.ToString();
                }
                usvr = degerler[1, 207];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME12L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME12L3 = usvr.ToString();
                }


            //-----------------


                usvr = Math.Round(degerler[1, 208]);
                Entities.tablelist.KADEME13 = usvr.ToString();
                usvr = degerler[1, 209];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME13L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME13ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME13L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME13ISARET = isaret.ToString();
                }

                usvr = degerler[1, 210];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME13L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME13L2 = usvr.ToString();
                }
                usvr = degerler[1, 211];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME13L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME13L3 = usvr.ToString();
                }


            //----------------------


                usvr = Math.Round(degerler[1, 212]);
                Entities.tablelist.KADEME14 = usvr.ToString();
                usvr = degerler[1, 213];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME14L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME14ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME14L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME14ISARET = isaret.ToString();
                }

                usvr = degerler[1, 214];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME14L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME14L2 = usvr.ToString();
                }
                usvr = degerler[1, 215];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME14L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME14L3 = usvr.ToString();
                }


            //--------------------------

                usvr = Math.Round(degerler[1, 216]);
                Entities.tablelist.KADEME15 = usvr.ToString();
                usvr = degerler[1, 217];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME15L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME15ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME15L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME15ISARET = isaret.ToString();
                }

                usvr = degerler[1, 218];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME15L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME15L2 = usvr.ToString();
                }
                usvr = degerler[1, 219];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME15L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME15L3 = usvr.ToString();
                }


            //**---------


                usvr = Math.Round(degerler[1, 220]);
                Entities.tablelist.KADEME16 = usvr.ToString();
                usvr = degerler[1, 221];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME16L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME16ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME16L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME16ISARET = isaret.ToString();
                }

                usvr = degerler[1, 222];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME16L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME16L2 = usvr.ToString();
                }
                usvr = degerler[1, 223];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME16L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME16L3 = usvr.ToString();
                }

            //-----------------------------

                usvr = Math.Round(degerler[1, 224]);
                Entities.tablelist.KADEME17 = usvr.ToString();
                usvr = degerler[1, 225];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME17L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME17ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME17L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME17ISARET = isaret.ToString();
                }

                usvr = degerler[1, 226];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME17L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME17L2 = usvr.ToString();
                }
                usvr = degerler[1, 227];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME17L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME17L3 = usvr.ToString();
                }

            //----------------------------


                usvr = Math.Round(degerler[1, 228]);
                Entities.tablelist.KADEME18 = usvr.ToString();
                usvr = degerler[1, 229];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME18L1 = usvr.ToString();
                    isaret = 0;
                    Entities.tablelist.KADEME18ISARET = isaret.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME18L1 = usvr.ToString();
                    isaret = 1;
                    Entities.tablelist.KADEME18ISARET = isaret.ToString();
                }

                usvr = degerler[1, 230];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME18L2 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME18L2 = usvr.ToString();
                }
                usvr = degerler[1, 231];
                if (usvr < 32768)
                {
                    usvr = usvr * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME18L3 = usvr.ToString();
                }
                else
                {
                    usvr = (65536 - usvr) * degerler[1, 0] / 10000;
                    Entities.tablelist.KADEME18L3 = usvr.ToString();
                }

            //------kademe bitis -------------



//-------------------------------
                usvr = degerler[1, 129];
                if (usvr != -1)
                {
                    Entities.tablelist.TANFI = usvr.ToString();

                }
                else

                {
                    Entities.tablelist.TANFI = (usvr - 65536).ToString();
                }

//________________________________________

                Entities.tablelist.INDCEZA = degerler[1, 132].ToString();
                Entities.tablelist.KAPCEZA = degerler[1, 133].ToString();


                Entities.tablelist.MAXALMASURE = degerler[1, 134].ToString();
                Entities.tablelist.MAXCIKMASURE = degerler[1, 135].ToString();
                Entities.tablelist.MINALCIKSURE = degerler[1, 136].ToString();

                Entities.tablelist.YUKSEKGERILIM = degerler[1, 143].ToString();
                Entities.tablelist.DUSUKGERILIM = degerler[1, 144].ToString();
                Entities.tablelist.YUKSEKTHDV = degerler[1, 145].ToString();
                Entities.tablelist.YUKSEKHDV = degerler[1, 146].ToString();
                Entities.tablelist.YUKSEKTHDC = degerler[1, 147].ToString();
                Entities.tablelist.YUKSEKHDC = degerler[1, 148].ToString();
                Entities.tablelist.SICAKLIKALARM = degerler[1, 149].ToString();
                Entities.tablelist.FANALARM = degerler[1, 150].ToString();


                Entities.tablelist.KONDSOGREN = degerler[1, 139].ToString();
                Entities.tablelist.AKIMYONOGREN = degerler[1, 140].ToString();
                Entities.tablelist.CALISMAMODU = degerler[1, 157].ToString();

           




























                usvr = degerler[1, 1];
                Entities.tablelist.CALISMABILGILERI = usvr.ToString();

                usvr = degerler[1, 35];
                Entities.tablelist.GENELALARMLAR = usvr.ToString();

                Entities.tablelist.FAZALARM1 = degerler[1, 32].ToString();
                Entities.tablelist.FAZALARM2 = degerler[1, 33].ToString();
                Entities.tablelist.FAZALARM3 = degerler[1, 34].ToString();






                if (Datam.IndexOf("yok") > -1)
                {
                    //Entities.tablelist = null;
                    Entities.tablelist = new clientfonksiyon();
                }
           


        }



        //void arc_sr_getAraclarCompleted(object sender, getAraclarCompletedEventArgs e)
        //{
        //    Entities.AraclarList = e.Result;
        //    Entities.AracDataList = new System.Collections.ObjectModel.ObservableCollection<AracData>();
        //    SrvIslemler.DataIstek();

        //    loginBusy.BusyContent = "Veriler Yükleniyor.";

        //    Entities.AraclarList = e.Result;
        //    Entities.AracDataList = new System.Collections.ObjectModel.ObservableCollection<AracData>();
        //    Entities.DatasList = new System.Collections.ObjectModel.ObservableCollection<Datas>();
        //    if (Entities.AraclarList != null)
        //        for (int i = 0; i < Entities.AraclarList.Count; i++)
        //        {
        //            try
        //            {
        //                AracData arcDatam = new AracData();
        //                //arcDatam.Arac = Entities.AraclarList[i];--------------
        //                Entities.AraclarList[i].ARC_PLAKA = Entities.AraclarList[i].ARC_PLAKA + " - " + Entities.AraclarList[i].ARC_KONTAK;
        //                String[] enboy = Entities.AraclarList[i].ARC_RENK.Split(';');
        //                Datas dt = new Datas();
        //                dt.DT_ENLEM = enboy[0];
        //                dt.DT_BOYLAM = enboy[1];
        //                dt.DT_ADRES = Entities.AraclarList[i].ARC_TEL;
        //                //arcDatam.Data = dt;-------------
        //                Entities.AracDataList.Add(arcDatam);
        //                Entities.DatasList.Add(dt);
        //            }
        //            catch { }
        //        }
        //    //SrvIslemler.DataIstek();------

        //    loginBusy.IsBusy = false;
        //    loginBusy.Visibility = Visibility.Collapsed;
        //    loginBusy.IsEnabled = false;







            
        //}
        
        void kul_sr_getKullaniciCompleted(object sender, getKullaniciCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                ClientIslem.LoginIn = false;
                LayoutRoot2.Visibility = Visibility.Visible;

                MapLink.Visibility = Visibility.Collapsed;
                RaporPageLink.Visibility = Visibility.Collapsed;

                LoginLink.Visibility = Visibility.Visible;
                TanimLink.Visibility = Visibility.Collapsed;

                DiagnostikLink.Visibility = Visibility.Collapsed;
                kulTxt.Text = "Kullanıcı Adı ya da Şifre Hatalı..";
                //ContentFrame.Navigate(new Uri("Home", UriKind.Relative));
                ContentFrame.Navigate(LoginLink.NavigateUri);
            }
            else
            {
                ClientIslem.LoginIn = true;
                //ContentFrame.Navigate(new Uri("Home", UriKind.Relative));
                //SrvIslemler.LoginGonder(textBox1.Text,passwordBox1.Password);
                LayoutRoot2.Visibility= Visibility.Collapsed;
                LoginLink.Visibility = Visibility.Collapsed;
                MapLink.Visibility = Visibility.Visible;
                RaporPageLink.Visibility = Visibility.Collapsed;
                //RaporPageLink.Visibility = Visibility.Collapsed;
                TanimLink.Visibility = Visibility.Collapsed;//**
                Entities.Kullanici = e.Result;
                ServisIslemler.chz_sr.getCihazlarAsync(Entities.Kullanici.KUL_AD);
                //arc_sr.getAraclarAsync(Entities.Kullanici.KUL_AD);
                //ContentFrame.Navigate(new Uri("MapPage", UriKind.Relative));
                //ContentFrame.Navigate(MapLink.NavigateUri);

                loginBusy.BusyContent = "Cihazlar Yükleniyor..";
            }
        }
       
        private void hyperlinkButton1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text.Trim().Length > 0 && passwordBox1.Password.Trim().Length > 0)
            {
                ServisIslemler.chz_sr.getKullaniciAsync(textBox1.Text, passwordBox1.Password);
                LayoutRoot2.Visibility = Visibility.Collapsed;
                
                loginBusy.Visibility = Visibility.Visible;
                loginBusy.IsBusy = true;
                loginBusy.BusyContent = "Kullanıcı Yükleniyor..";
            }
        }

        // After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (ContentFrame.UriMapper.MapUri(e.Uri).ToString().Equals(ContentFrame.UriMapper.MapUri(hb.NavigateUri).ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
        }

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }
        
        public Int16 UnsignedToInteger(UInt16 value)
        {
            if (value <= 32767)
                return Convert.ToInt16(value);
            else
                return Convert.ToInt16(value - 65536);
        }
    }
}