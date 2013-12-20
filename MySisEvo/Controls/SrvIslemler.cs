using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MySisEvo.Classes;
using System.Collections.Generic;
using System.Text;
//using MySisEvo.DATA_RS;
using Telerik.Windows.Controls.Map;
using System.Collections.ObjectModel;

namespace MySisEvo.Controls
{
    public static class SrvIslemler
    {
        public static void LoginGonder(string kulAd,string Sifre)
        {
            String gidenVeri = "kulsifre" + Convert.ToChar(1) + kulAd+ Convert.ToChar(1) + Sifre + Convert.ToChar(2) + "xxx";

            byte[] gidenByte = new byte[100];

            List<ArraySegment<byte>> buffer = new List<ArraySegment<byte>>();
            gidenByte = Encoding.UTF8.GetBytes(gidenVeri);
            gidenByte[gidenByte.Length - 1] = 244;
            gidenByte[gidenByte.Length - 2] = 244;
            gidenByte[gidenByte.Length - 3] = 244;
            buffer.Add(new ArraySegment<byte>(gidenByte));

           
            SocketIslemler.MysisSocketOlay.UserToken = SocketIslemler.MysisSocket;
            SocketIslemler.MysisSocketOlay.RemoteEndPoint = SocketIslemler.MyEndPoint;
            SocketIslemler.MysisSocketOlay.BufferList = buffer;
            SocketIslemler.MysisSocket.SendAsync(SocketIslemler.MysisSocketOlay);
           

        }

        //public static void DataIstek()
        //{
            
        //    List<ArraySegment<byte>> buffer = new List<ArraySegment<byte>>();
        //    ObservableCollection<String> serinolar = new ObservableCollection<string>();
        //    for (int i = 0; i < Entities.AraclarList.Count; i++)
        //    {
        //        string gidenVeri = "modemkomut" + Convert.ToChar(1) + Entities.AraclarList[i].ARC_SERINO.ToString() + Convert.ToChar(1) + "Deneme" + Convert.ToChar(1) + "]]]";
        //        byte[] gidenByte = new byte[100];
        //        gidenByte = Encoding.UTF8.GetBytes(gidenVeri);
        //        gidenByte[gidenByte.Length - 1] = 244;
        //        gidenByte[gidenByte.Length - 2] = 244;
        //        gidenByte[gidenByte.Length - 3] = 244;
        //        buffer.Add(new ArraySegment<byte>(gidenByte));
        //        Entities.DatasList.Add(new Datas());
        //        Entities.DatasYol.Add(new Datas());
                
        //        //ServisIslemler.data_sr.getDataAsync(Entities.AraclarList[i].ARC_SERINO);
        //        serinolar.Add(Entities.AraclarList[i].ARC_SERINO.ToString());

        //        AracData arcData = new AracData();
        //        //arcData.Arac = Entities.AraclarList[i];
        //        //arcData.Data = new Datas();
        //        //arcData.Adres = new Adres();
        //        Entities.AracDataList.Add(arcData);
        //    }
        //    ServisIslemler.data_sr.getDatasAsync(serinolar);

        //    SocketIslemler.MysisSocketOlay.UserToken = SocketIslemler.MysisSocket;
        //    SocketIslemler.MysisSocketOlay.RemoteEndPoint = SocketIslemler.MyEndPoint;
        //    SocketIslemler.MysisSocketOlay.BufferList = buffer;

        //    SocketIslemler.MysisSocket.SendAsync(SocketIslemler.MysisSocketOlay);
        //}

        //public static void GelenGenelPro(string gelen)
        //{
        //    if (gelen.IndexOf("<CHZ>") != -1)
        //    {
        //        int seriNo = int.Parse(tagDanGetir(gelen, "SERINO"));
        //        string enlem = tagDanGetir(gelen, "ENLEM");
        //        string boylam = tagDanGetir(gelen, "BOYLAM");
        //        string hiz = tagDanGetir(gelen, "HIZ");
        //        string bos = tagDanGetir(gelen, "KONTAK");
        //        string kontak = "-1";
        //        string kontaki = "";
        //        if (bos == "1")
        //        {
        //            kontak = "Kapalı";
        //            kontaki = "Collapsed";
        //        }
        //        if (bos == "0")
        //        {
        //            kontak = "Açık";
        //            kontaki = "Visible";
        //        }
                
        //        enlem = enlem.Replace(".", ",");
        //        boylam = boylam.Replace(".", ",");
               
        //        double en;
        //        double boy;
        //        if (enlem.Length > 0)
        //        {
        //            en = double.Parse(enlem);
        //            boy = double.Parse(boylam);

        //            for (int i = 0; i < Entities.CihazlarList.Count; i++)
        //            {
        //                if (Entities.CihazlarList[i].CHZ_SERINO == seriNo)
        //                {
        //                    Entities.CihazlarList[i].CHZ_ENLEM = en.ToString();
        //                    //Entities.DatasList[i].DT_ENLEM_HAM = enlem;
        //                    Entities.CihazlarList[i].CHZ_BOYLAM = boy.ToString();
        //                    //Entities.DatasList[i].DT_BOYLAM_HAM = boylam;
        //                    //Entities.DatasList[i].DT_H1Z = hiz;
        //                    //Entities.DatasList[i].DT_KONTAK = kontak;
        //                    //Entities.DatasList[i].DT_TAR1H = DateTime.Now;
        //                    Entities.AracDataList[i].CHZDATA = Entities.CihazlarList[i];
        //                    ClientIslem.dtYolindex.Add(i);
        //                    ClientIslem.geoIndexList.Add(i);

        //                    Location yer = new Location(en, boy);
                            
        //                    if (ClientIslem.dtYolIndex == -1)
        //                        ClientIslem.yolBul(yer);

        //                    Entities.AraclarList[i].ARC_KONTAK = kontaki;   //plaka listesindeki kontak durumu icin
        //                }

        //                if (ClientIslem.dtIlkYol == -1)
        //                {
        //                    Location yer = new Location(en, boy);
        //                    ClientIslem.dtYolindex.Add(i);

        //                    if (ClientIslem.dtYolIndex == -1)
        //                        ClientIslem.yolBul(yer);

        //                    if (i == Entities.DatasList.Count - 1)
        //                        ClientIslem.dtIlkYol = 1;

        //                }
        //            }
        //        }
        //    }

        //}

        public static string tagDanGetir(string gelen, string tag)
        {
            int i, j = 0;
            string veri = "";
            i = gelen.IndexOf("<" + tag + ">");
            if (i != -1)
                j = gelen.IndexOf("</" + tag + ">", i);

            if (i != -1 && j != -1)
            {
                veri = gelen.Substring(i + tag.Length + 2, j - i - tag.Length - 2);
            }

            return veri;
        }
        public static void ModemKomutGonder(string komut, string srn)
        {

            List<ArraySegment<byte>> buffer = new List<ArraySegment<byte>>();
            string gidenVeri = "modemkomut" + Convert.ToChar(1) + srn + Convert.ToChar(1) + komut + Convert.ToChar(1) + "]]]";
            byte[] gidenByte = new byte[100];
            UTF8Encoding encoder = new UTF8Encoding();
            gidenByte = Encoding.UTF8.GetBytes(gidenVeri);
            gidenByte[gidenByte.Length - 1] = 244;
            gidenByte[gidenByte.Length - 2] = 244;
            gidenByte[gidenByte.Length - 3] = 244;
            buffer.Add(new ArraySegment<byte>(gidenByte));
            SocketIslemler.MysisSocketOlay.UserToken = SocketIslemler.MysisSocket;
            SocketIslemler.MysisSocketOlay.RemoteEndPoint = SocketIslemler.MyEndPoint;
            SocketIslemler.MysisSocketOlay.BufferList = buffer;
            SocketIslemler.MysisSocket.SendAsync(SocketIslemler.MysisSocketOlay);
        }

        public static void ModemKomutGonderson(string komut, string srn)
        {

            List<ArraySegment<byte>> buffer = new List<ArraySegment<byte>>();
            string gidenVeri = "modemkomut" + Convert.ToChar(1) + srn + Convert.ToChar(1) + komut + Convert.ToChar(1) + "\u00F4" + "\u00F4" + "\u00F4";

            byte[] gidenByte = new byte[gidenVeri.Length];
            for (int i = 0; i < gidenVeri.Length; i++)
            {
                gidenByte[i] = Convert.ToByte(gidenVeri[i]);
            }


            buffer.Add(new ArraySegment<byte>(gidenByte));
            SocketIslemler.MysisSocketOlay.UserToken = SocketIslemler.MysisSocket;
            SocketIslemler.MysisSocketOlay.RemoteEndPoint = SocketIslemler.MyEndPoint;
            SocketIslemler.MysisSocketOlay.BufferList = buffer;
            SocketIslemler.MysisSocket.SendAsync(SocketIslemler.MysisSocketOlay);
        }
        public static void NormalGonder(string veri)
        {

            List<ArraySegment<byte>> buffer = new List<ArraySegment<byte>>();
            string gidenVeri = veri + "]]]";
            byte[] gidenByte = new byte[100];
            gidenByte = Encoding.UTF8.GetBytes(gidenVeri);
            gidenByte[gidenByte.Length - 1] = 244;
            gidenByte[gidenByte.Length - 2] = 244;
            gidenByte[gidenByte.Length - 3] = 244;
            buffer.Add(new ArraySegment<byte>(gidenByte));
            SocketIslemler.MysisSocketOlay.UserToken = SocketIslemler.MysisSocket;
            SocketIslemler.MysisSocketOlay.RemoteEndPoint = SocketIslemler.MyEndPoint;
            SocketIslemler.MysisSocketOlay.BufferList = buffer;
            SocketIslemler.MysisSocket.SendAsync(SocketIslemler.MysisSocketOlay);
        }
    }
}
