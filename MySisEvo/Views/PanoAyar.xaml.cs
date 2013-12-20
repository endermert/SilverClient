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
using CRC;

namespace MySisEvo.Views
{
    public partial class PanoAyar : RadWindow
    {
        public PanoAyar()
        {
            InitializeComponent();
        }


        string bilgi;
        int sayi1, sayi2,sayislid,x,k;
        private SocketAsyncEventArgs MyAsenkronOlay;
        private byte[] ServerData;
        public delegate void SetCallBack(string Textim);
        int chzno;
        string yedek = "";
         
        private void Radwindow1_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (Entities.tablelist.CIHAZAGNO != null) chzno = Convert.ToInt32(Entities.tablelist.CIHAZAGNO);
            if (Entities.tablelist.AYARSET!=null)   //form 2 kere yükleniyor 1. de dolu geliyor 2 de ayar set null old. bilgi tekrar temizleniyor
                bilgi = Entities.tablelist.AYARSET; // nul geldini bilgi değişmesin yeter şimdilik
            //Entities1.yedek= Entities.tablelist.AYARSET;
              
                if (bilgi == "CT") 
                { lblbilgi.Text = "CT Akım Trafosu"; k =Convert.ToInt32(Entities.tablelist.LBL_AKIMTRAFOSU) ;
                slider1.Maximum = 2000;
                slider1.Minimum = 0;
                slider1.Visibility = Visibility.Visible;
                txtveri.Visibility = Visibility.Visible;

                Rabtn1.Visibility = Visibility.Collapsed;
                Rabtn2.Visibility = Visibility.Collapsed;

                }
                if (bilgi == "TANFI")
                {
                    lblbilgi.Text = "Tanjant Fi"; k = Convert.ToInt32(Entities.tablelist.TANFI);

                    slider1.Maximum = 75;
                    slider1.Minimum = -75;
                slider1.Visibility = Visibility.Visible;
                txtveri.Visibility = Visibility.Visible;

                Rabtn1.Visibility = Visibility.Collapsed;
                Rabtn2.Visibility = Visibility.Collapsed;
                
                }
                if (bilgi == "INDCEZA") 
                {
                    lblbilgi.Text = "Ind. Ceza Sınırı"; k = Convert.ToInt32(Entities.tablelist.INDCEZA);

                    slider1.Maximum = 50;
                    slider1.Minimum = 3;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;
                
                }
                if (bilgi == "KAPCEZA") {
                    lblbilgi.Text = "Kap. Ceza Sınırı"; k = Convert.ToInt32(Entities.tablelist.KAPCEZA);
                    slider1.Maximum = 50;
                    slider1.Minimum = 3;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;
                
                }
                if (bilgi == "MAXKADAL")
                {
                    lblbilgi.Text = "Max. Kademe Alma Süresi"; k = Convert.ToInt32(Entities.tablelist.MAXALMASURE);
                    slider1.Maximum = 600;
                    slider1.Minimum = 1;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;
                
                }
                if (bilgi == "MAXKADCIK") 
                {
                    lblbilgi.Text = "Max. Kademe Çıkma Süresi"; k = Convert.ToInt32(Entities.tablelist.MAXCIKMASURE);
                    slider1.Maximum = 600;
                    slider1.Minimum = 1;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;
                
                }
                if (bilgi == "MAXKADALCIK") 
                {
                    lblbilgi.Text = "Min. Kademe Alma Çıkma Süresi"; k = Convert.ToInt32(Entities.tablelist.MINALCIKSURE);
                    slider1.Maximum = 600;
                    slider1.Minimum = 1;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;
                
                }
                if (bilgi == "AKIMYONOGREN")
                {
                    lblbilgi.Text = "Akım Trafo Yön Öğrenme";
                    slider1.Visibility = Visibility.Collapsed;
                    txtveri.Visibility = Visibility.Collapsed;

                    Rabtn1.Visibility = Visibility.Visible;
                    Rabtn2.Visibility = Visibility.Visible;
                    if ((Convert.ToInt32(Entities.tablelist.AKIMYONOGREN)) == 1) { Rabtn1.IsChecked = true; }
                    else { Rabtn2.IsChecked = true; }
                    Rabtn1.Content = "Devrede";
                    Rabtn2.Content = "Devrede Değil";
               
                }
                if (bilgi == "CALISMAMOD")
                {
                    lblbilgi.Text = "Calışma Modu";
                    slider1.Visibility = Visibility.Collapsed;
                    txtveri.Visibility = Visibility.Collapsed;

                    Rabtn1.Visibility = Visibility.Visible;
                    Rabtn2.Visibility = Visibility.Visible;

                  k=  Convert.ToUInt16(Entities.tablelist.CALISMABILGILERI);
                  if ((k & 1) != 0)
                  {
                      Rabtn2.IsChecked = true;
                  }
                  else Rabtn1.IsChecked = true;

                    Rabtn1.Content = "Otomatik";
                    Rabtn2.Content = "Manual";

                }
                if (bilgi == "KONDOGREN")
                {
                    lblbilgi.Text = "Kondasatör Öğrenme";
                    slider1.Visibility = Visibility.Collapsed;
                    txtveri.Visibility = Visibility.Collapsed;

                    Rabtn1.Visibility = Visibility.Visible;
                    Rabtn2.Visibility = Visibility.Visible;
                    k = Convert.ToInt32(Entities.tablelist.KONDSOGREN);
                    if (k == 1) { Rabtn1.IsChecked = true; } else { Rabtn2.IsChecked = true; }
                    Rabtn1.Content = "Devrede";
                    Rabtn2.Content = "Devrede Değil";

                }
                if (bilgi == "SICAKLIKALARM")
                {
                    lblbilgi.Text = "Sıcaklık Alarm Değeri"; k = Convert.ToInt32(Entities.tablelist.SICAKLIKALARM);
                    slider1.Maximum = 85;
                    slider1.Minimum = 5;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;

                }
                if (bilgi == "FANALARM")
                {
                    lblbilgi.Text = "Fanın Devreye Girme Sıcaklığı"; k = Convert.ToInt32(Entities.tablelist.FANALARM);
                    slider1.Maximum = 85;
                    slider1.Minimum = 5;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;

                }

                if (bilgi == "ASIRIGERILIM")
                {
                    lblbilgi.Text = "Aşırı Gerilim Set Değeri"; k = Convert.ToInt32(Entities.tablelist.YUKSEKGERILIM);
                    slider1.Maximum = 270;
                    slider1.Minimum = 230;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;

                }
                if (bilgi == "DUSUKGERILIM")
                {
                    lblbilgi.Text = "Düşük Gerilim Set Değeri"; k = Convert.ToInt32(Entities.tablelist.DUSUKGERILIM);
                    slider1.Maximum = 210;
                    slider1.Minimum = 170;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;

                }



                if (bilgi == "THDV")
                {
                    lblbilgi.Text = "Aşırı THDV"; k = Convert.ToInt32(Entities.tablelist.YUKSEKTHDV);
                    slider1.Maximum = 100;
                    slider1.Minimum = 1;

                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;

                }


                if (bilgi == "HDV")
                {
                    lblbilgi.Text = "Aşırı HDV"; k = Convert.ToInt32(Entities.tablelist.YUKSEKHDV);
                    slider1.Maximum = 100;
                    slider1.Minimum = 1;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;

                }


                if (bilgi == "THDC")
                {
                    lblbilgi.Text = "Aşırı THDC"; k = Convert.ToInt32(Entities.tablelist.YUKSEKTHDC);
                    slider1.Maximum = 100;
                    slider1.Minimum = 1;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;

                }
                if (bilgi == "HDC")
                {
                    lblbilgi.Text = "Aşırı HDC"; k = Convert.ToInt32(Entities.tablelist.YUKSEKHDC);
                        
                    slider1.Maximum = 100;
                    slider1.Minimum = 1;
                    slider1.Visibility = Visibility.Visible;
                    txtveri.Visibility = Visibility.Visible;

                    Rabtn1.Visibility = Visibility.Collapsed;
                    Rabtn2.Visibility = Visibility.Collapsed;

                }
           
                slider1.Value = k;
         
        }
        


        private void btnsetle_Click(object sender, RoutedEventArgs e)
        {

       //---------------------------------------
            if (bilgi == "CT")
            {
                sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                SrvIslemler.NormalGonder("kaelctyaz" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + sayi2.ToString() + Convert.ToChar(1) + sayi1.ToString() + Convert.ToChar(1));
                this.Close();
            
            }

            //------------------------------ct
            if(bilgi=="TANFI")
            {

                x = Convert.ToInt32(txtveri.Text);

                if (x < 0) { x = x + 65536; }

                sayi1 = x % 256;
                sayi2 = x / 256;
                SrvIslemler.NormalGonder("kaeltanfiyaz" + Convert.ToChar(1) + Entities.Arac.ARC_SERINO + Convert.ToChar(1) + sayi2.ToString() + Convert.ToChar(1) + sayi1.ToString() + Convert.ToChar(1));
                this.Close();
            }
            //---------------------------------tanfi
            if (bilgi == "INDCEZA")
            {

                sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;
                byte[] modbus_bytes = new byte[8];
                modbus_bytes[0] = Convert.ToByte(chzno);
                modbus_bytes[1] = 0x06;
                modbus_bytes[2] = 0x40;
                modbus_bytes[3] = 0x03;
                modbus_bytes[4] = Convert.ToByte((sayi2));
                modbus_bytes[5] = Convert.ToByte((sayi1));
                CRC_Modbus crchesapla = new CRC_Modbus();
                int crc_result = crchesapla.crc16(modbus_bytes, 6);
                string chrveri = "";
                byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                modbus_bytes[6] = crc_bytes[1];
                modbus_bytes[7] = crc_bytes[0];
             
                for (int i = 0; i < 8; i++)
                {
                    chrveri += Convert.ToChar(modbus_bytes[i]);
                }
                
                SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                this.Close();
          
            }
            //----------------------------------------------------------------------
            if (bilgi == "KAPCEZA")
            {
                          
                sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;
                byte[] modbus_bytes = new byte[8];
                modbus_bytes[0] = Convert.ToByte(chzno);
                modbus_bytes[1] = 0x06;
                modbus_bytes[2] = 0x40;
                modbus_bytes[3] = 0x04;
                modbus_bytes[4] = Convert.ToByte((sayi2));
                modbus_bytes[5] = Convert.ToByte((sayi1));
                CRC_Modbus crchesapla = new CRC_Modbus();
                int crc_result = crchesapla.crc16(modbus_bytes, 6);
                string chrveri = "";
                byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                modbus_bytes[6] = crc_bytes[1];
                modbus_bytes[7] = crc_bytes[0];
             
                for (int i = 0; i < 8; i++)
                {
                    chrveri += Convert.ToChar(modbus_bytes[i]);
                }

                SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());

                this.Close();



            }
           //------------------------------------------------------------------kapceza
            if (bilgi == "MAXKADAL")
            
            {
                sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;
                byte[] modbus_bytes=new byte[8];
                modbus_bytes[0] = Convert.ToByte(chzno);
                modbus_bytes[1] = 0x06;
                modbus_bytes[2] = 0x40;
                modbus_bytes[3] = 0x05;
                modbus_bytes[4] = Convert.ToByte((sayi2));
                modbus_bytes[5] = Convert.ToByte((sayi1));
                CRC_Modbus crchesapla = new CRC_Modbus();
              
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri="";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];
                   
                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());

                    this.Close();
                                   
            }
            //----------------------------------------------------------------
            if (bilgi == "MAXKADCIK") 
            {

                sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;


                byte[] modbus_bytes = new byte[8];


                modbus_bytes[0] = Convert.ToByte(chzno);
                modbus_bytes[1] = 0x06;
                modbus_bytes[2] = 0x40;
                modbus_bytes[3] = 0x06;
                modbus_bytes[4] = Convert.ToByte((sayi2));
                modbus_bytes[5] = Convert.ToByte((sayi1));

                CRC_Modbus crchesapla = new CRC_Modbus();



                int crc_result = crchesapla.crc16(modbus_bytes, 6);

                string chrveri = "";
                byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                modbus_bytes[6] = crc_bytes[1];
                modbus_bytes[7] = crc_bytes[0];

                for (int i = 0; i < 8; i++)
                {
                    chrveri += Convert.ToChar(modbus_bytes[i]);
                }

                SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                this.Close();
            
            }
            //----------------------------------------------------------------------------------------------------
            if (bilgi == "MAXKADALCIK")
            
            
            {
            
            sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                byte[] modbus_bytes = new byte[8];

                modbus_bytes[0] = Convert.ToByte(chzno);
                modbus_bytes[1] = 0x06;
                modbus_bytes[2] = 0x40;
                modbus_bytes[3] = 0x07;
                modbus_bytes[4] = Convert.ToByte((sayi2));
                modbus_bytes[5] = Convert.ToByte((sayi1));
                 CRC_Modbus crchesapla = new CRC_Modbus();
                int crc_result = crchesapla.crc16(modbus_bytes, 6);
                string chrveri = "";
                byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                modbus_bytes[6] = crc_bytes[1];
                modbus_bytes[7] = crc_bytes[0];

                for (int i = 0; i < 8; i++)
                {
                    chrveri += Convert.ToChar(modbus_bytes[i]);
                }

                SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                this.Close();
            
         
            
            }
            //--------------------------------------------------------------------
            if (bilgi == "KONDOGREN")
            {
                if (Rabtn1.IsChecked == true)
                {

                    byte[] modbus_bytes = new byte[8];


                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x0A;
                    modbus_bytes[4] = Convert.ToByte(0);
                    modbus_bytes[5] = Convert.ToByte(1);

                    CRC_Modbus crchesapla = new CRC_Modbus();



                    int crc_result = crchesapla.crc16(modbus_bytes, 6);

                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();



                }

                else
                {


                    byte[] modbus_bytes = new byte[8];


                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x0A;
                    modbus_bytes[4] = Convert.ToByte(0);
                    modbus_bytes[5] = Convert.ToByte(0);

                    CRC_Modbus crchesapla = new CRC_Modbus();



                    int crc_result = crchesapla.crc16(modbus_bytes, 6);

                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();

                }

            }
                //----------------------------------------------------------------
                if (bilgi == "AKIMYONOGREN")
                {

                    if (Rabtn1.IsChecked == true)
                    {

                        byte[] modbus_bytes = new byte[8];


                        modbus_bytes[0] = Convert.ToByte(chzno);
                        modbus_bytes[1] = 0x06;
                        modbus_bytes[2] = 0x40;
                        modbus_bytes[3] = 0x0B;
                        modbus_bytes[4] = Convert.ToByte(0);
                        modbus_bytes[5] = Convert.ToByte(1);

                        CRC_Modbus crchesapla = new CRC_Modbus();



                        int crc_result = crchesapla.crc16(modbus_bytes, 6);

                        string chrveri = "";
                        byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                        modbus_bytes[6] = crc_bytes[1];
                        modbus_bytes[7] = crc_bytes[0];

                        for (int i = 0; i < 8; i++)
                        {
                            chrveri += Convert.ToChar(modbus_bytes[i]);
                        }

                        SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                        this.Close();



                    }

                    else
                    {


                        byte[] modbus_bytes = new byte[8];


                        modbus_bytes[0] = Convert.ToByte(chzno);
                        modbus_bytes[1] = 0x06;
                        modbus_bytes[2] = 0x40;
                        modbus_bytes[3] = 0x0B;
                        modbus_bytes[4] = Convert.ToByte(0);
                        modbus_bytes[5] = Convert.ToByte(0);

                        CRC_Modbus crchesapla = new CRC_Modbus();



                        int crc_result = crchesapla.crc16(modbus_bytes, 6);

                        string chrveri = "";
                        byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                        modbus_bytes[6] = crc_bytes[1];
                        modbus_bytes[7] = crc_bytes[0];

                        for (int i = 0; i < 8; i++)
                        {
                            chrveri += Convert.ToChar(modbus_bytes[i]);
                        }

                        SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                        this.Close();


                    }

                }
                //--------------------------------- akımogren---------------
                if (bilgi == "CALISMAMOD")
                {



                    if (Rabtn1.IsChecked == true)
                    {

                        byte[] modbus_bytes = new byte[8];


                        modbus_bytes[0] = Convert.ToByte(chzno);
                        modbus_bytes[1] = 0x06;
                        modbus_bytes[2] = 0x30;
                        modbus_bytes[3] = 0x05;
                        modbus_bytes[4] = Convert.ToByte(0);
                        modbus_bytes[5] = Convert.ToByte(0);

                        CRC_Modbus crchesapla = new CRC_Modbus();



                        int crc_result = crchesapla.crc16(modbus_bytes, 6);

                        string chrveri = "";
                        byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                        modbus_bytes[6] = crc_bytes[1];
                        modbus_bytes[7] = crc_bytes[0];

                        for (int i = 0; i < 8; i++)
                        {
                            chrveri += Convert.ToChar(modbus_bytes[i]);
                        }

                        SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                        this.Close();



                    }

                    else
                    {


                        byte[] modbus_bytes = new byte[8];


                        modbus_bytes[0] = Convert.ToByte(chzno);
                        modbus_bytes[1] = 0x06;
                        modbus_bytes[2] = 0x30;
                        modbus_bytes[3] = 0x05;
                        modbus_bytes[4] = Convert.ToByte(0);
                        modbus_bytes[5] = Convert.ToByte(1);

                        CRC_Modbus crchesapla = new CRC_Modbus();



                        int crc_result = crchesapla.crc16(modbus_bytes, 6);

                        string chrveri = "";
                        byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                        modbus_bytes[6] = crc_bytes[1];
                        modbus_bytes[7] = crc_bytes[0];

                        for (int i = 0; i < 8; i++)
                        {
                            chrveri += Convert.ToChar(modbus_bytes[i]);
                        }

                        SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                        this.Close();


                    }
                
                         
                
                
                }

                if (bilgi == "SICAKLIKALARM") 
                
                {



                    sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                    sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                    byte[] modbus_bytes = new byte[8];

                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x14;
                    modbus_bytes[4] = Convert.ToByte((sayi2));
                    modbus_bytes[5] = Convert.ToByte((sayi1));
                    CRC_Modbus crchesapla = new CRC_Modbus();
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                   this.Close();
            
                                              
               
                }
                if (bilgi == "FANALARM")
                {

                    sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                    sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                    byte[] modbus_bytes = new byte[8];

                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x15;
                    modbus_bytes[4] = Convert.ToByte((sayi2));
                    modbus_bytes[5] = Convert.ToByte((sayi1));
                    CRC_Modbus crchesapla = new CRC_Modbus();
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();
                
                
                
                
                
                }

                if (bilgi == "ASIRIGERILIM")
                {

                    sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                    sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                    byte[] modbus_bytes = new byte[8];

                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x0E;
                    modbus_bytes[4] = Convert.ToByte((sayi2));
                    modbus_bytes[5] = Convert.ToByte((sayi1));
                    CRC_Modbus crchesapla = new CRC_Modbus();
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();
                
                
                
                }

                if (bilgi == "DUSUKGERILIM")
                {



                    sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                    sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                    byte[] modbus_bytes = new byte[8];

                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x0F;
                    modbus_bytes[4] = Convert.ToByte((sayi2));
                    modbus_bytes[5] = Convert.ToByte((sayi1));
                    CRC_Modbus crchesapla = new CRC_Modbus();
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();
                
                
                }
                if (bilgi == "THDV")
                {

                    
                    sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                    sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                    byte[] modbus_bytes = new byte[8];

                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x10;
                    modbus_bytes[4] = Convert.ToByte((sayi2));
                    modbus_bytes[5] = Convert.ToByte((sayi1));
                    CRC_Modbus crchesapla = new CRC_Modbus();
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();
                
                
                
                }


                if (bilgi == "HDV")
                {


                    sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                    sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                    byte[] modbus_bytes = new byte[8];

                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x11;
                    modbus_bytes[4] = Convert.ToByte((sayi2));
                    modbus_bytes[5] = Convert.ToByte((sayi1));
                    CRC_Modbus crchesapla = new CRC_Modbus();
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();
                
                
                
                }

                if (bilgi == "THDC")
                {


                    sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                    sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                    byte[] modbus_bytes = new byte[8];

                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x12;
                    modbus_bytes[4] = Convert.ToByte((sayi2));
                    modbus_bytes[5] = Convert.ToByte((sayi1));
                    CRC_Modbus crchesapla = new CRC_Modbus();
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();



                }


                if (bilgi == "HDC")
                {


                    sayi1 = (Convert.ToInt32(txtveri.Text)) % 256;
                    sayi2 = ((Convert.ToInt32(txtveri.Text)) - sayi1) / 256;

                    byte[] modbus_bytes = new byte[8];

                    modbus_bytes[0] = Convert.ToByte(chzno);
                    modbus_bytes[1] = 0x06;
                    modbus_bytes[2] = 0x40;
                    modbus_bytes[3] = 0x13;
                    modbus_bytes[4] = Convert.ToByte((sayi2));
                    modbus_bytes[5] = Convert.ToByte((sayi1));
                    CRC_Modbus crchesapla = new CRC_Modbus();
                    int crc_result = crchesapla.crc16(modbus_bytes, 6);
                    string chrveri = "";
                    byte[] crc_bytes = BitConverter.GetBytes(crc_result);
                    modbus_bytes[6] = crc_bytes[1];
                    modbus_bytes[7] = crc_bytes[0];

                    for (int i = 0; i < 8; i++)
                    {
                        chrveri += Convert.ToChar(modbus_bytes[i]);
                    }

                    SrvIslemler.ModemKomutGonderson(chrveri, Entities.Arac.ARC_SERINO.ToString());
                    this.Close();



                }
            
            

        
        
        
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }

        private void Radwindow1_Closed(object sender, WindowClosedEventArgs e)
        {

        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
          
            sayislid = Convert.ToInt32(slider1.Value);
            txtveri.Text = sayislid.ToString();

        }

     
        private void txtveri_KeyUp(object sender, KeyEventArgs e)
        {

            

       
       
        
        
        
        
        
           
          

        }

        private void txtveri_KeyDown(object sender, KeyEventArgs e)
        {
            if (((e.Key > Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Back || e.Key == Key.Tab)  )
                e.Handled = false;

            else
            {

                e.Handled = true;
            }
        }


        


    }
}
