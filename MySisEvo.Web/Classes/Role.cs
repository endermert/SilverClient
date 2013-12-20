using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace MySisEvo.Web.Classes
{
    public class Role
    {
        public int akimtrafosu { get; set; }
        public string v1 { get; set; }
        public string v2 { get; set; }
        public string v3{ get; set; }
        public string akm1 { get; set; }
        public string akm2 { get; set; }
        public string akm3 { get; set; }
        public string kw1 { get; set; }
        public string kw2 { get; set; }
        public string kw3 { get; set; }
        public string kr1 { get; set; }
        public string kr2 { get; set; }
        public string kr3 { get; set; }
        public string kVA1 { get; set; }
        public string kVA2 { get; set; }
        public string kVA3 { get; set; }
        public string Cos1 { get; set; }
        public string Cos2 { get; set; }
        public string Cos3 { get; set; }
        public string CosE { get; set; }
        public int CosFi { get; set; }
        public int VMeter { get; set; }
        public string topAktif { get; set; }
        public string er { get; set; }
        public string erl { get; set; }
        public string erc { get; set; }
        public string topGorunen { get; set; }
        public double frekans { get; set; }
        public string sicaklik { get; set; }

        public string i_mod { get; set; }
        public string i_ogrmod { get; set; }
        public string i_hiz { get; set; }
        public string i_kademeKoru { get; set; }
        public string i_jenarator { get; set; }
        public string i_tanfi { get; set; }
        public string i_L1ReaktifYon { get; set; }
        public string i_L2ReaktifYon { get; set; }
        public string i_L3ReaktifYon { get; set; }
        public string i_ELReaktifYon { get; set; }
        public string i_L1DogruYon { get; set; }
        public string i_L2DogruYon { get; set; }
        public string i_L3DogruYon { get; set; }
        public string i_L1Ogr { get; set; }
        public string i_L2Ogr { get; set; }
        public string i_L3Ogr { get; set; }

        public Boolean A_L1OVolt { get; set; }
        public Boolean A_L1UVolt { get; set; }
        public Boolean A_L1OCurrent { get; set; }
        public Boolean A_L1OComp { get; set; }
        public Boolean A_L1UComp { get; set; }
        public Boolean A_L1MissP { get; set; }
        public Boolean A_L1OTHDV { get; set; }
        public Boolean A_L1OHDV { get; set; }
        public Boolean A_L1OTHDC { get; set; }
        public Boolean A_L1OHDC { get; set; }

        public Boolean A_L2OVolt { get; set; }
        public Boolean A_L2UVolt { get; set; }
        public Boolean A_L2OCurrent { get; set; }
        public Boolean A_L2OComp { get; set; }
        public Boolean A_L2UComp { get; set; }
        public Boolean A_L2MissP { get; set; }
        public Boolean A_L2OTHDV { get; set; }
        public Boolean A_L2OHDV { get; set; }
        public Boolean A_L2OTHDC { get; set; }
        public Boolean A_L2OHDC { get; set; }

        public Boolean A_L3OVolt { get; set; }
        public Boolean A_L3UVolt { get; set; }
        public Boolean A_L3OCurrent { get; set; }
        public Boolean A_L3OComp { get; set; }
        public Boolean A_L3UComp { get; set; }
        public Boolean A_L3MissP { get; set; }
        public Boolean A_L3OTHDV { get; set; }
        public Boolean A_L3OHDV { get; set; }
        public Boolean A_L3OTHDC { get; set; }
        public Boolean A_L3OHDC { get; set; }
        #region Genel alarmlar
        public Boolean GA_MissL1Err { get; set; }
        public Boolean GA_MissL2Err { get; set; }
        public Boolean GA_MissL3Err { get; set; }
        public Boolean GA_OCompErr { get; set; }
        public Boolean GA_UCompErr { get; set; }
        public Boolean GA_SysErr { get; set; }
        public Boolean GA_OHeatErr { get; set; }
        public Boolean GA_FazConErr { get; set; }
        public Boolean GA_KdmSfrErr { get; set; }
        public Boolean GA_KdmChgErr { get; set; }
        public Boolean GA_YetkiErr { get; set; }
        #endregion
        #region Kademe Koruma Sebepleri

        public Boolean K_sicaklik { get; set; }
        public Boolean K_dVolt { get; set; }
        public Boolean K_vHarm { get; set; }
        public Boolean K_vBagHata { get; set; }
        public Boolean K_akimYok { get; set; }
        public Boolean K_fabrika { get; set; }
        #endregion
        #region Faz Bağlantı ve akım yok uyarıları
        public Boolean F_A1var { get; set; }
        public Boolean F_A2var { get; set; }
        public Boolean F_A3var { get; set; }
        public Boolean F_F1var { get; set; }
        public Boolean F_F2var { get; set; }
        public Boolean F_F3var { get; set; }
        public Boolean F_KayipF { get; set; }
        public Boolean F_SiraF { get; set; }
        public Boolean F_F12kisa { get; set; }
        public Boolean F_F23kisa { get; set; }
        public Boolean F_F13kisa { get; set; }
        public Boolean F_NyeF { get; set; }
        #endregion

        public ObservableCollection<Kademe> Kademeler { get; set; }
        #region Yuzdelik oranlar
        public string inboluak_r { get; set; }
        public string kapboluak_r { get; set; }
        public string inboluak_s { get; set; }
        public string kapboluak_s { get; set; }
        public string inboluak_t { get; set; }
        public string kapboluak_t { get; set; }
        public string inboluak_top { get; set; }
        public string kapboluak_top { get; set; }
        public string onnsure_r { get; set; }
        public string offsure_r { get; set; }
        public string onnsure_s { get; set; }
        public string offsure_s { get; set; }
        public string onnsure_t { get; set; }
        public string offsure_t { get; set; }
        public string onnsure_top { get; set; }
        public string offsure_top { get; set; }
        #endregion
    }
}