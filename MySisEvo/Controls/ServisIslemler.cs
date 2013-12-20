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
//using MySisEvo.DATA_RS;
//using MySisEvo.SES_SR;
using MySisEvo.CHZ_RS;
namespace MySisEvo.Controls
{
    public class ServisIslemler
    {

        //public static DataServisClient data_sr = new DataServisClient();
        //public static SessionServisClient ses_sr = new SessionServisClient();
        public static CihazlarServisClient chz_sr = new CihazlarServisClient();
    }
}
