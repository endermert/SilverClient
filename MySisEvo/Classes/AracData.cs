using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
//using MySisEvo.ARC_SR;
//using MySisEvo.DATA_RS;
using MySisEvo.CHZ_RS;
namespace MySisEvo.Classes
{
    public class AracData
    {
        //public Araclar Arac { get; set; }
        //public Datas Data { get; set; }
        public Cihazlar CHZDATA { get; set; }
        //public Adres Adres { get; set; }
        public ImageSource AracIcon { get; set; }
        //public Boolean Secili { get; set; }
        public Sayac SYCDATA { get; set; }
    }
}
