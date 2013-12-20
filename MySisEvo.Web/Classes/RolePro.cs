using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace MySisEvo.Web.Classes
{
    public class RolePro
    {
        
        public int UInt16(string value)
        {
            if (int.Parse(value) <= 32767)
                return Convert.ToInt16(value);
            else
                return Convert.ToInt16(int.Parse(value) - 65536);

            //if (int.Parse(value) <= 65535)
            //    return Convert.ToUInt16(value);
            //else
            //    return Convert.ToUInt16(int.Parse(value) - 65536);
        }


        public String [] DegerAyikla(string value)
        {
            String[] values=value.Split('$');
            int boyut=int.Parse(values[2]);
            String[] returns=new String[boyut/2];
            int sayac = 0;
            for (int i = 0; i < boyut/2; i++)
            {
                returns[i] = (int.Parse(values[4 + sayac]) + int.Parse(values[3 + sayac]) * 256).ToString();
                sayac = sayac + 2;

            }
                return returns;
        }

        public String[] DegerAyikla2(string value)
        {
            String[] values = value.Split('$');
            int boyut = int.Parse(values[2]);
            String[] returns = new String[boyut / 2];
            int sayac = 0;
            for (int i = 0; i < boyut / 2; i++)
            {
                if (i == 44 || i == 46 || i == 48 || i == 50 || i == 52 || i == 54 || i == 56 || i == 58 || i == 60)
                {
                    returns[i] = (uint.Parse(values[6 + sayac]) + (uint.Parse(values[5 + sayac]) * 256) + (uint.Parse(values[4 + sayac]) * 256 * 256) + (uint.Parse(values[3 + sayac]) * 256 * 256 * 256)).ToString();
                    sayac = sayac + 4;
                    i++;
                }
                else
                {
                    returns[i] = (int.Parse(values[4 + sayac]) + int.Parse(values[3 + sayac]) * 256).ToString();
                    sayac = sayac + 2;
                }

            }
            return returns;
        }

        public Role getAniDegerler(String value)
        {
            String[] values = DegerAyikla2(value);
            Role role = new Role();
            role.Kademeler=new System.Collections.ObjectModel.ObservableCollection<Kademe>();
            #region Cihaz çalışma bilgileri
            if ((int.Parse(values[1]) & 1)!= 0) role.i_mod = "Manuel"; else role.i_mod = "Otomatik";
            if ((int.Parse(values[1]) & 2)!= 0) role.i_ogrmod = "Tüm Kademeler Öğrenildi"; else role.i_ogrmod = "Öğrenme Modunda";
            if ((int.Parse(values[1]) & 4)!= 0) role.i_hiz = "Hızlı"; else role.i_hiz = "Normal";
            if ((int.Parse(values[1]) & 8)!= 0) role.i_kademeKoru = "Aktif"; else role.i_kademeKoru = "Pasif";
            if ((int.Parse(values[1]) & 16)!= 0) role.i_jenarator = "Girişte sinüs var"; else role.i_jenarator = "Algılanıyor";
            if ((int.Parse(values[1]) & 32)!= 0) role.i_tanfi = "TanFi 2 devrede"; else role.i_tanfi = "TanFi 1 devrede";
            if ((int.Parse(values[1]) & 64)!= 0) role.i_L1ReaktifYon = "İndüktif"; else role.i_L1ReaktifYon = "Kapasitif";
            if ((int.Parse(values[1]) & 128)!= 0) role.i_L2ReaktifYon = "İndüktif"; else role.i_L2ReaktifYon = "Kapasitif";
            if ((int.Parse(values[1]) & 256)!= 0) role.i_L3ReaktifYon = "İndüktif"; else role.i_L3ReaktifYon = "Kapasitif";
            if ((int.Parse(values[1]) & 512)!= 0) role.i_ELReaktifYon = "İndüktif"; else role.i_ELReaktifYon = "Kapasitif";
            if ((int.Parse(values[1]) & 1204) == 0) role.i_L1DogruYon = "Visible"; else role.i_L1DogruYon = "Collapsed";
            if ((int.Parse(values[1]) & 2048) == 0) role.i_L2DogruYon = "Visible"; else role.i_L2DogruYon = "Collapsed";
            if ((int.Parse(values[1]) & 4096) == 0) role.i_L3DogruYon = "Visible"; else role.i_L3DogruYon = "Collapsed";
            if ((int.Parse(values[1]) & 8192)!= 0) role.i_L1Ogr = "Evet"; else role.i_L1Ogr = "Hayır";
            if ((int.Parse(values[1]) & 16384)!= 0) role.i_L2Ogr = "Evet"; else role.i_L2Ogr = "Hayır";
            if ((int.Parse(values[1]) & 32768)!= 0) role.i_L3Ogr = "Evet"; else role.i_L3Ogr = "Hayır";
            #endregion
            #region Ani Degerler-1000
            role.akimtrafosu = int.Parse(values[0]);
            role.v1 = (int.Parse(values[2]) / 10.0).ToString("000.0");
            role.v2 = (int.Parse(values[3]) / 10.0).ToString("000.0");
            role.v3 = (int.Parse(values[4]) / 10.0).ToString("000.0");
            
            role.akm1 = (int.Parse(values[5]) / 1000.0 * role.akimtrafosu).ToString("000.00");
            role.akm2 = (int.Parse(values[6]) / 1000.0 * role.akimtrafosu).ToString("000.00");
            role.akm3 = (int.Parse(values[7]) / 1000.0 * role.akimtrafosu).ToString("000.00");

            role.kw1 = (int.Parse(values[8]) / 1000.0 * role.akimtrafosu).ToString("000.00");
            role.kw2 = (int.Parse(values[9]) / 1000.0 * role.akimtrafosu).ToString("000.00");
            role.kw3 = (int.Parse(values[10]) / 1000.0 * role.akimtrafosu).ToString("000.00");

            role.kr1 = (UInt16(values[11]) / 1000.0 * role.akimtrafosu).ToString("000.00");
            role.kr2 = (UInt16(values[12]) / 1000.0 * role.akimtrafosu).ToString("000.00");
            role.kr3 = (UInt16(values[13]) / 1000.0 * role.akimtrafosu).ToString("000.00");

            role.kVA1 = (int.Parse(values[14]) / 1000.0 * role.akimtrafosu).ToString("000.00");
            role.kVA2 = (int.Parse(values[15]) / 1000.0 * role.akimtrafosu).ToString("000.00");
            role.kVA3 = (int.Parse(values[16]) / 1000.0 * role.akimtrafosu).ToString("000.00");

            role.Cos1 = (UInt16(values[17]) / 1000.0).ToString("000.0");
            role.Cos2 = (UInt16(values[18]) / 1000.0).ToString("000.0");
            role.Cos3 = (UInt16(values[19]) / 1000.0).ToString("000.0");
            role.CosE = (UInt16(values[20]) / 1000.0).ToString("000.0");
            role.CosFi = UInt16(values[20]);
            if (role.CosFi < 0)
            {
                role.CosFi = -1 * role.CosFi;
                role.VMeter = role.CosFi - 1000;
            }
            else
                role.VMeter = 1000-role.CosFi;

            role.topAktif = (int.Parse(values[25]) / 1000.0 * role.akimtrafosu).ToString("00000.00");
            role.er = (int.Parse(values[26]) / 1000.0).ToString("000.0");
            role.erl = (int.Parse(values[27]) / 1000.0).ToString("000.0");
            role.erc = (UInt16(values[28]) / 1000.0).ToString("000.0");
            role.topGorunen = (int.Parse(values[29]) / 1000.0 * role.akimtrafosu).ToString("00000.00");
            role.frekans = int.Parse(values[30]) / 10.0;
            role.sicaklik = UInt16(values[31]).ToString() + "°";
            #endregion
            #region 1. Faz için alarmlar
            if ((int.Parse(values[32]) & 1)!= 0) role.A_L1OVolt = true; else role.A_L1OVolt = false;
            if ((int.Parse(values[32]) & 2)!= 0) role.A_L1UVolt = true; else role.A_L1UVolt = false;
            if ((int.Parse(values[32]) & 4)!= 0) role.A_L2OCurrent = true; else role.A_L1OCurrent = false;
            if ((int.Parse(values[32]) & 8)!= 0) role.A_L2OComp = true; else role.A_L1OComp = false;
            if ((int.Parse(values[32]) & 16)!= 0) role.A_L1UComp = true; else role.A_L1UComp = false;
            if ((int.Parse(values[32]) & 32)!= 0) role.A_L1MissP = true; else role.A_L1MissP = false;
            if ((int.Parse(values[32]) & 64)!= 0) role.A_L1OTHDC = true; else role.A_L1OTHDC = false;
            if ((int.Parse(values[32]) & 128)!= 0) role.A_L1OHDC = true; else role.A_L1OHDC = false;
            if ((int.Parse(values[32]) & 256)!= 0) role.A_L1OTHDV = true; else role.A_L1OTHDV = false;
            if ((int.Parse(values[32]) & 512)!= 0) role.A_L1OHDV = true; else role.A_L1OHDV = false;
            #endregion
            #region 2. Faz için alarmlar
            if ((int.Parse(values[33]) & 1)!= 0) role.A_L2OVolt = true; else role.A_L2OVolt = false;
            if ((int.Parse(values[33]) & 2)!= 0) role.A_L2UVolt = true; else role.A_L2UVolt = false;
            if ((int.Parse(values[33]) & 4)!= 0) role.A_L2OCurrent = true; else role.A_L2OCurrent = false;
            if ((int.Parse(values[33]) & 8)!= 0) role.A_L2OComp = true; else role.A_L2OComp = false;
            if ((int.Parse(values[33]) & 16)!= 0) role.A_L2UComp = true; else role.A_L2UComp = false;
            if ((int.Parse(values[33]) & 32)!= 0) role.A_L2MissP = true; else role.A_L2MissP = false;
            if ((int.Parse(values[33]) & 64)!= 0) role.A_L2OTHDC = true; else role.A_L2OTHDC = false;
            if ((int.Parse(values[33]) & 128)!= 0) role.A_L2OHDC = true; else role.A_L2OHDC = false;
            if ((int.Parse(values[33]) & 256)!= 0) role.A_L2OTHDV = true; else role.A_L2OTHDV = false;
            if ((int.Parse(values[33]) & 512)!= 0) role.A_L2OHDV = true; else role.A_L2OHDV = false;
            #endregion
            #region 3. Faz için alarmlar
            if ((int.Parse(values[34]) & 1)!= 0) role.A_L3OVolt = true; else role.A_L3OVolt = false;
            if ((int.Parse(values[34]) & 2)!= 0) role.A_L3UVolt = true; else role.A_L3UVolt = false;
            if ((int.Parse(values[34]) & 4)!= 0) role.A_L3OCurrent = true; else role.A_L3OCurrent = false;
            if ((int.Parse(values[34]) & 8)!= 0) role.A_L3OComp = true; else role.A_L3OComp = false;
            if ((int.Parse(values[34]) & 16)!= 0) role.A_L3UComp = true; else role.A_L3UComp = false;
            if ((int.Parse(values[34]) & 32)!= 0) role.A_L3MissP = true; else role.A_L3MissP = false;
            if ((int.Parse(values[34]) & 64)!= 0) role.A_L3OTHDC = true; else role.A_L3OTHDC = false;
            if ((int.Parse(values[34]) & 128)!= 0) role.A_L3OHDC = true; else role.A_L3OHDC = false;
            if ((int.Parse(values[34]) & 256)!= 0) role.A_L3OTHDV = true; else role.A_L3OTHDV = false;
            if ((int.Parse(values[34]) & 512)!= 0) role.A_L3OHDV = true; else role.A_L3OHDV = false;
            #endregion
            #region Genel hatalar
            if ((int.Parse(values[35]) & 1)!= 0) role.GA_MissL1Err = true; else role.GA_MissL1Err = false;
            if ((int.Parse(values[35]) & 2)!= 0) role.GA_MissL2Err = true; else role.GA_MissL2Err = false;
            if ((int.Parse(values[35]) & 4)!= 0) role.GA_MissL3Err = true; else role.GA_MissL3Err = false;
            if ((int.Parse(values[35]) & 8)!= 0) role.GA_OCompErr = true; else role.GA_OCompErr = false;
            if ((int.Parse(values[35]) & 16)!= 0) role.GA_UCompErr = true; else role.GA_UCompErr = false;
            if ((int.Parse(values[35]) & 32)!= 0) role.GA_SysErr = true; else role.GA_SysErr = false;
            if ((int.Parse(values[35]) & 64)!= 0) role.GA_OHeatErr = true; else role.GA_OHeatErr = false;
            if ((int.Parse(values[35]) & 128)!= 0) role.GA_FazConErr = true; else role.GA_FazConErr = false;
            if ((int.Parse(values[35]) & 256)!= 0) role.GA_KdmChgErr = true; else role.GA_KdmChgErr = false;
            if ((int.Parse(values[35]) & 512)!= 0) role.GA_KdmSfrErr = true; else role.GA_KdmSfrErr = false;
            if ((int.Parse(values[35]) & 1024)!= 0) role.GA_YetkiErr = true; else role.GA_YetkiErr = false;
            #endregion
            #region Kademe Koruma Sebepleri
            if ((int.Parse(values[36]) & 1)!= 0) role.K_sicaklik = true; else role.K_sicaklik = false;
            if ((int.Parse(values[36]) & 2)!= 0) role.K_dVolt = true; else role.K_dVolt= false;
            if ((int.Parse(values[36]) & 4)!= 0) role.K_vHarm = true; else role.K_vHarm = false;
            if ((int.Parse(values[36]) & 8)!= 0) role.K_vBagHata = true; else role.K_vBagHata = false;
            if ((int.Parse(values[36]) & 16)!= 0) role.K_akimYok = true; else role.K_akimYok = false;
            if ((int.Parse(values[36]) & 32)!= 0) role.K_fabrika = true; else role.K_fabrika = false;
            #endregion
            #region Faz Bağlantı ve akım yok uyarıları
            if ((int.Parse(values[37]) & 1) != 0) role.F_A1var = true; else role.F_A1var = false;
            if ((int.Parse(values[37]) & 2) != 0) role.F_A2var = true; else role.F_A2var = false;
            if ((int.Parse(values[37]) & 4) != 0) role.F_A3var = true; else role.F_A3var = false;
            if ((int.Parse(values[37]) & 8) != 0) role.F_F1var = true; else role.F_F1var = false;
            if ((int.Parse(values[37]) & 16) != 0) role.F_F2var = true; else role.F_F2var = false;
            if ((int.Parse(values[37]) & 32) != 0) role.F_F3var = true; else role.F_F3var = false;
            if ((int.Parse(values[37]) & 64) != 0) role.F_KayipF = true; else role.F_KayipF = false;
            if ((int.Parse(values[37]) & 128) != 0) role.F_SiraF = true; else role.F_SiraF = false;
            if ((int.Parse(values[37]) & 256) != 0) role.F_F12kisa = true; else role.F_F12kisa = false;
            if ((int.Parse(values[37]) & 512) != 0) role.F_F23kisa = true; else role.F_F23kisa = false;
            if ((int.Parse(values[37]) & 1024) != 0) role.F_F13kisa = true; else role.F_F13kisa = false;
            if ((int.Parse(values[37]) & 2048) != 0) role.F_NyeF = true; else role.F_NyeF = false;
            #endregion
            #region Kademeler detayları
            for (byte i = 0; i < 18; i++)
            { 
                Kademe kdm=new Kademe();
                uint byt = Convert.ToUInt32(Math.Pow(2, i));
                if ((uint.Parse(values[44]) & byt) != 0) kdm.Devrede = true; else kdm.Devrede = false;
                if ((uint.Parse(values[46]) & byt) != 0) kdm.Ogrenildi = true; else kdm.Ogrenildi = false;
                if ((uint.Parse(values[48]) & byt) != 0) kdm.Faz1Ind = true; else kdm.Faz1Ind = false;
                if ((uint.Parse(values[50]) & byt) != 0) kdm.Faz2Ind = true; else kdm.Faz2Ind = false;
                if ((uint.Parse(values[52]) & byt) != 0) kdm.Faz3Ind = true; else kdm.Faz3Ind = false;
                if ((uint.Parse(values[54]) & byt) != 0) kdm.Faz1Kon = true; else kdm.Faz1Kon = false;
                if ((uint.Parse(values[56]) & byt) != 0) kdm.Faz2Kon = true; else kdm.Faz2Kon = false;
                if ((uint.Parse(values[58]) & byt) != 0) kdm.Faz3Kon = true; else kdm.Faz3Kon = false;
                if ((uint.Parse(values[60]) & byt) != 0) kdm.ZamanDolmus = true; else kdm.ZamanDolmus = false;
                role.Kademeler.Add(kdm);
            }
            #endregion
            #region Yuzdelik oranlar
            role.inboluak_r = "% " + int.Parse(values[62]).ToString("00.0") ;
            role.kapboluak_r = "% " + int.Parse(values[63]).ToString("00.0");
            role.inboluak_s = "% " + int.Parse(values[64]).ToString("00.0");
            role.kapboluak_s = "% " + int.Parse(values[65]).ToString("00.0");
            role.inboluak_t = "% " + int.Parse(values[66]).ToString("00.0");
            role.kapboluak_t = "% " + int.Parse(values[67]).ToString("00.0");
            role.inboluak_top = "% " + int.Parse(values[68]).ToString("00.0");
            role.kapboluak_top = "% " + int.Parse(values[69]).ToString("00.0");
            role.onnsure_r = (int.Parse(values[82]) * 0.2).ToString("##0.0");
            role.offsure_r = (int.Parse(values[83]) * 0.2).ToString("##0.0");
            role.onnsure_s = (int.Parse(values[84]) * 0.2).ToString("##0.0");
            role.offsure_s = (int.Parse(values[85]) * 0.2).ToString("##0.0");
            role.onnsure_t = (int.Parse(values[86]) * 0.2).ToString("##0.0");
            role.offsure_t = (int.Parse(values[87]) * 0.2).ToString("##0.0");
            role.onnsure_top = (int.Parse(values[88]) * 0.2).ToString("##0.0");
            role.offsure_top = (int.Parse(values[89]) * 0.2).ToString("##0.0");
            #endregion

            return role;
        }
    }
}