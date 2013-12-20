using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySisEvo.Web.Classes
{
    public class SayacPro
    {
        private string arayiGetir(string kaynak, string basstr, string bitstr)
        {
            string value = "";
            if (kaynak.IndexOf(basstr) != -1)
            {
                int bas = kaynak.IndexOf(basstr);
                int son = kaynak.IndexOf(bitstr,bas+basstr.Length);
                if (son>-1 && bas >-1)
                    value = kaynak.Substring(bas + basstr.Length, son - (bas + basstr.Length));
            }
            return value;
        }

        public Sayac getSayacDegerleri(string kaynak)
        {
            Sayac syc = new Sayac();
            syc.syc_serino = arayiGetir(kaynak,"0.0.0(",")");
            syc.syc_saat = arayiGetir(kaynak, "0.9.1(", ")");
            syc.syc_tarih = arayiGetir(kaynak, "0.9.2(", ")");
            syc.syc_gun = arayiGetir(kaynak, "0.9.5(", ")");
            if (syc.syc_gun == "1")
                syc.syc_gun = "PAZARTESİ";
            if (syc.syc_gun == "2")
                syc.syc_gun = "SALI";
            if (syc.syc_gun == "3")
                syc.syc_gun = "ÇARŞAMBA";
            if (syc.syc_gun == "4")
                syc.syc_gun = "PERŞEMBE";
            if (syc.syc_gun == "5")
                syc.syc_gun = "CUMA";
            if (syc.syc_gun == "6")
                syc.syc_gun = "CUMARTESİ";
            if (syc.syc_gun == "7")
                syc.syc_gun = "PAZAR";
            syc.syc_uretimtar = arayiGetir(kaynak, "96.1.3(", ")");
            syc.syc_kalibretar = arayiGetir(kaynak, "96.2.5(", ")");
            syc.syc_tarifedegtar = arayiGetir(kaynak, "96.2.2(", ")");
            syc.syc_govactar = arayiGetir(kaynak, "96.70(", ")");
            syc.syc_kkactar = arayiGetir(kaynak, "96.71(", ")");
            syc.syc_kkacsay = arayiGetir(kaynak, "96.71("+syc.syc_kkactar+")(", ")");
            syc.syc_enyukolc = arayiGetir(kaynak, "0.8.0(", "*");
            syc.syc_demand0say = arayiGetir(kaynak, "0.1.0(", ")");
            syc.syc_demand = arayiGetir(kaynak, "1.6.0(", "*");
            syc.syc_pildurumu = arayiGetir(kaynak, "96.6.1(", ")");
            if (syc.syc_pildurumu == "1")
                syc.syc_pildurumu = "DOLU";
            else
                syc.syc_pildurumu = "ZAYIF";
            syc.syc_fazkessaytop = arayiGetir(kaynak, "96.7.0(", ")");
            syc.syc_fazkessay1 = arayiGetir(kaynak, "96.7.1(", ")");
            syc.syc_fazkessay2 = arayiGetir(kaynak, "96.7.2(", ")");
            syc.syc_fazkessay3 = arayiGetir(kaynak, "96.7.3(", ")");
            syc.syc_Vuyarisay = arayiGetir(kaynak, "96.77.4(", ")");
            syc.syc_Auyarisay = arayiGetir(kaynak, "96.77.5(", ")");
            syc.syc_takt = arayiGetir(kaynak, "1.8.0(", "*");
            syc.syc_t1akt = arayiGetir(kaynak, "1.8.1(", "*");
            syc.syc_t2akt = arayiGetir(kaynak, "1.8.2(", "*");
            syc.syc_t3akt = arayiGetir(kaynak, "1.8.3(", "*");

            syc.syc_tind = arayiGetir(kaynak, "5.8.0(", "*");
            syc.syc_t1ind = arayiGetir(kaynak, "5.8.1(", "*");
            syc.syc_t2ind = arayiGetir(kaynak, "5.8.2(", "*");
            syc.syc_t3ind = arayiGetir(kaynak, "5.8.3(", "*");

            syc.syc_tkap = arayiGetir(kaynak, "8.8.0(", "*");
            syc.syc_t1kap = arayiGetir(kaynak, "8.8.1(", "*");
            syc.syc_t2kap = arayiGetir(kaynak, "8.8.2(", "*");
            syc.syc_t3kap = arayiGetir(kaynak, "8.8.3(", "*");

            return syc;
        }
    }
}