using System;
using System.Net;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
//using MySisEvo.DATA_RS;
//using MySisEvo.ARC_SR;
using MySisEvo.CHZ_RS;
//using MySisEvo.KUL_SR;
using System.ComponentModel;
using CRC;
 
namespace MySisEvo.Classes
{
    public class Entities
    {
        public static Role Rolem { get; set; }
        public static Sayac Sayac { get; set; }
        public static string istenen = "";
        public static string t_okunanveri;
        public static Kullanici Kullanici { get; set; }
        public static ObservableCollection<Araclar> AraclarList { get; set; }
        public static ObservableCollection<Cihazlar> CihazlarList { get; set; }
        public static ObservableCollection<Cihazlar> CihazlarYol { get; set; }
        public static Cihazlar Cihaz { get; set; }
        public static Araclar Arac { get; set; }
  //      public static ObservableCollection<Datas> DatasList { get; set; }
  //    public static ObservableCollection<Datas> DatasYol { get; set; }
  //      public static Datas Data {get;set;}
        public static ObservableCollection<AracData> AracDataList { get; set; }
        public static ObservableCollection<Adres> AdresList1 { get; set; }
        public static ObservableCollection<ImageSource> AracIconList { get; set; }
        public static ImageSource AracIcon { get; set; }
      
        public static Visibility DetayGridVisible { get; set; }
        public static string deneme { get; set; }
        public static GecmisYol GecmisYollar;
        public static clientfonksiyon tablelist { get; set; }

        public static iskioku iskiDegerler { get; set; }
        public static katotik katdegerler { get; set; }
        public static SayacData SayacDegerler { get; set; }
        public static CRC_Modbus crchespla { get; set; }
        public static ObservableCollection<CRC.CRC_Modbus> crchesap { get; set; }
        

        public static Araclar getArac(string plaka)
        {
            Araclar arc = new Araclar();
            for (int i = 0; i < AraclarList.Count; i++)
            {
                if (AraclarList[i].ARC_PLAKA == plaka)
                {
                    arc = AraclarList[i];
                    break;
                }
            }
                return arc;
        }
        public static Araclar getArac(int serino)
        {
            Araclar arc = new Araclar();
            for (int i = 0; i < AraclarList.Count; i++)
            {
                if (AraclarList[i].ARC_SERINO == serino)
                {
                    arc = AraclarList[i];
                    break;
                }
            }
            return arc;
        }

        public static int getArcListIndex(int serino)
        {
            int index=-1;
            for (int i = 0; i < AraclarList.Count; i++)
            {
                if (AraclarList[i].ARC_SERINO == serino)
                {
                    index=i;
                    break;
                }
            }
            return index;
        }
     
    }
}
