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

namespace MySisEvo.Classes
{
    public class clientfonksiyon
    {
        public string CIHAZADI { get; set; } 
        public string CIHAZAGNO { get; set; } 
        public   string LBL_VR { get; set; } 
        public string LBL_VS { get; set; }
        public string LBL_VT { get; set; }
        public string LBL_IR { get; set; } 
        public string LBL_IS { get; set; }
        public string LBL_IT { get; set; }
        public string LBL_KWR { get; set; }
        public string LBL_KWS { get; set; }
        public string LBL_KWT { get; set; }
        public string LBL_KWET { get; set; }
        public string LBL_KVAR { get; set; }
        public string LBL_KVAS { get; set; }
        public string LBL_KVAT { get; set; }
        public string LBL_KVAET { get; set; }
        public string LBL_KVARR { get; set; }
        public string LBL_KVARS { get; set; }
        public string LBL_KVART { get; set; }
        public string LBL_RL { get; set; }
        public string LBL_RC { get; set; }
        public string LBL_DELTAR { get; set; }
        public string LBL_INAKR { get; set; }
        public string LBL_INAKS { get; set; }
        public string LBL_INAKT { get; set; }
        public string LBL_INAKET { get; set; }
        public string LBL_KAAKR { get; set; }
        public string LBL_KAAKS { get; set; }
        public string LBL_KAAKT { get; set; }
        public string LBL_KAAKET { get; set; }
        public string LBL_SNONR { get; set; }
        public string LBL_SNONS { get; set; }
        public string LBL_SNONT { get; set; }
        public string LBL_SNONET { get; set; }
        public string LBL_SNOFFR { get; set; }
        public string LBL_SNOFFS { get; set; }
        public string LBL_SNOFFT { get; set; }
        public string LBL_SNOFFET { get; set; }
        public string LBL_COS { get; set; }
        public string LBL_COS1 { get; set; }
        public string LBL_COS2 { get; set; }
        public string LBL_COS3 { get; set; }
        public string LBL_FREKANS { get; set; }
        public string LBL_SICAKLIK { get; set; }
        public string LBL_AKTIFGUC { get; set; }
        public string LBL_CBXOGREN { get; set; }
        public string LBL_AKIMTRAFOSU { get; set; }
        public string LBL_CBXDURUM { get; set; }
        public string KWHR { get; set; }
        public string KWHS { get; set; }
        public string KWHT { get; set; }
        public string KWHET { get; set; }
        public string abcd { get; set; }
        public string LBL_INAKET1 { get; set; }
        public string KVARLR { get; set; }
        public string KVARLS { get; set; }
        public string KVARLT { get; set; }
        public string KVARLET { get; set; }
        public string LBL_CBXDURUM2 { get; set; }
        public string TOPLAMINDORAN { get; set; }
        public string TOPLAMKAPORAN { get; set; }
        public string KVARCR { get; set; }
        public string KVARCS { get; set; }
        public string KVARCT { get; set; }
        public string KVARCET { get; set; }
        public string AK1 { get; set; }
        public string AK2 { get; set; }
        public string IN1 { get; set; }
        public string IN2 { get; set; }
        public string KAP1 { get; set; }
        public string KAP2 { get; set; }
        public string INDORAN { get; set; }
        public string KAPORAN { get; set; }
        public string SYCAKTIF { get; set; }
        public string SYCINDKTF { get; set; }
        public string SYCKPSTIF { get; set; }

        public string GRPC1 { get; set; }
        public string CR1_R { get; set; }
        public string CR1_S { get; set; }
        public string CR1_T { get; set; }

        public string GRPC2 { get; set; }

        public string CR2_R { get; set; }
        public string CR2_S { get; set; }
        public string CR2_T { get; set; }

        public string GRPC3 { get; set; }

        public string CR3_R { get; set; }
        public string CR3_S { get; set; }
        public string CR3_T { get; set; }

        public string GRPC4 { get; set; }

        public string CR4_R { get; set; }
        public string CR4_S { get; set; }
        public string CR4_T { get; set; }
        public string FAZALARM1 { get; set; }
        public string FAZALARM2 { get; set; }
        public string FAZALARM3 { get; set; }
        public string CALISMABILGILERI { get; set; }
        public string GENELALARMLAR { get; set; }

        public string KADEME1 { get; set; }
        public string KADEME1L1 { get; set; }
        public string KADEME1L2 { get; set; }
        public string KADEME1L3 { get; set; }
        public string KADEME1ISARET { get; set; }
        public string KADEME2 { get; set; }
        public string KADEME2L1 { get; set; }
        public string KADEME2L2 { get; set; }
        public string KADEME2L3 { get; set; }
        public string KADEME2ISARET { get; set; }
        public string KADEME3 { get; set; }
        public string KADEME3L1 { get; set; }
        public string KADEME3L2 { get; set; }
        public string KADEME3L3 { get; set; }
        public string KADEME3ISARET { get; set; }
        public string KADEME4 { get; set; }
        public string KADEME4L1 { get; set; }
        public string KADEME4L2 { get; set; }
        public string KADEME4L3 { get; set; }
        public string KADEME4ISARET { get; set; }

        public string KADEME5 { get; set; }
        public string KADEME5L1 { get; set; }
        public string KADEME5L2 { get; set; }
        public string KADEME5L3 { get; set; }
        public string KADEME5ISARET { get; set; }

        public string KADEME6 { get; set; }
        public string KADEME6L1 { get; set; }
        public string KADEME6L2 { get; set; }
        public string KADEME6L3 { get; set; }
        public string KADEME6ISARET { get; set; }

        public string KADEME7 { get; set; }
        public string KADEME7L1 { get; set; }
        public string KADEME7L2 { get; set; }
        public string KADEME7L3 { get; set; }
        public string KADEME7ISARET { get; set; }

        public string KADEME8 { get; set; }
        public string KADEME8L1 { get; set; }
        public string KADEME8L2 { get; set; }
        public string KADEME8L3 { get; set; }
        public string KADEME8ISARET { get; set; }

        public string KADEME9 { get; set; }
        public string KADEME9L1 { get; set; }
        public string KADEME9L2 { get; set; }
        public string KADEME9L3 { get; set; }
        public string KADEME9ISARET { get; set; }


        public string KADEME10 { get; set; }
        public string KADEME10L1 { get; set; }
        public string KADEME10L2 { get; set; }
        public string KADEME10L3 { get; set; }
        public string KADEME10ISARET { get; set; }

        public string KADEME11 { get; set; }
        public string KADEME11L1 { get; set; }
        public string KADEME11L2 { get; set; }
        public string KADEME11L3 { get; set; }
        public string KADEME11ISARET { get; set; }


        public string KADEME12 { get; set; }
        public string KADEME12L1 { get; set; }
        public string KADEME12L2 { get; set; }
        public string KADEME12L3 { get; set; }
        public string KADEME12ISARET { get; set; }

        public string KADEME13 { get; set; }
        public string KADEME13L1 { get; set; }
        public string KADEME13L2 { get; set; }
        public string KADEME13L3 { get; set; }
        public string KADEME13ISARET { get; set; }

        public string KADEME14 { get; set; }
        public string KADEME14L1 { get; set; }
        public string KADEME14L2 { get; set; }
        public string KADEME14L3 { get; set; }
        public string KADEME14ISARET { get; set; }



        public string KADEME15 { get; set; }
        public string KADEME15L1 { get; set; }
        public string KADEME15L2 { get; set; }
        public string KADEME15L3 { get; set; }
        public string KADEME15ISARET { get; set; }

        public string KADEME16 { get; set; }
        public string KADEME16L1 { get; set; }
        public string KADEME16L2 { get; set; }
        public string KADEME16L3 { get; set; }
        public string KADEME16ISARET { get; set; }

        public string KADEME17 { get; set; }
        public string KADEME17L1 { get; set; }
        public string KADEME17L2 { get; set; }
        public string KADEME17L3 { get; set; }
        public string KADEME17ISARET { get; set; }

        public string KADEME18 { get; set; }
        public string KADEME18L1 { get; set; }
        public string KADEME18L2 { get; set; }
        public string KADEME18L3 { get; set; }
        public string KADEME18ISARET { get; set; }

        public string KADEMESAYISI { get; set; }

        public string TANFI { get; set; }
        public string INDCEZA { get; set; }
        public string KAPCEZA { get; set; }
        public string MAXALMASURE { get; set; }
        public string MAXCIKMASURE { get; set; }
        public string MINALCIKSURE { get; set; }
        public string YUKSEKGERILIM { get; set; }
        public string DUSUKGERILIM { get; set; }
        public string YUKSEKTHDV { get; set; }
        public string YUKSEKHDV { get; set; }
        public string YUKSEKTHDC { get; set; }
        public string YUKSEKHDC { get; set; }
        public string SICAKLIKALARM { get; set; }
        public string FANALARM { get; set; }
        public string KONDSOGREN{ get; set; }
        public string AKIMYONOGREN { get; set; }
        public string CALISMAMODU { get; set; }

        public string AYARSET { get; set; }
    }
}
