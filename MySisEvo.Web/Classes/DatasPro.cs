using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Data;
using MySisEvo.Web.Classes;

namespace MySisEvo.Web.Classes
{
    public class DatasPro
    {
        //DataSet Eds;
        Datas data1 = null;
        ObservableCollection<Datas> DataList1 = null;

        public Datas getData(int serino)
        {
            data1 = null;
            //string sorgu = "SELECT TOP 1 * FROM ATS_DATA WHERE ARC_SER1NO=" + serino.ToString() +" ORDER BY DT_TAR1H DESC;";
            //Eds = SqlHelper.DataBasePro(sorgu);
            //data1 = getData(Eds);
            return data1;
        }

        public ObservableCollection<Datas> getData(List<string> serinolar)
        {
            data1 = null;
            DataList1 = new ObservableCollection<Datas>();
            //for (int i = 0; i < serinolar.Count; i++)
            //{
            //    string sorgu = "SELECT TOP 1 * FROM ATS_DATA WHERE ARC_SER1NO=" + serinolar[i] + " ORDER BY DT_TAR1H DESC;";
            //    Eds = SqlHelper.DataBasePro(sorgu);
            //    data1 = getData(Eds);
            //    DataList1.Add(data1);
            //}
            DataList1 = null;
            return DataList1;
        }

        public ObservableCollection<Datas> getData(int serino, DateTime basTar, DateTime bitTar)
        {
            DataList1 = new ObservableCollection<Datas>();
            DataList1 = null;
            //string sorgu = "SELECT * FROM ATS_DATA WHERE (ARC_SER1NO=" + serino.ToString() + ") AND (DT_TAR1H BETWEEN '" + String.Format("{0:MM.dd.yy HH:mm:ss}", basTar) + "' AND '" + String.Format("{0:MM.dd.yy HH:mm:ss}", bitTar) + "') ORDER BY DT_TAR1H;";
            //Eds = SqlHelper.DataBasePro(sorgu);
            //DataList1 = getDataList(Eds);
            return DataList1;
        }

        public Datas getData(DataSet ds)
        {
            if (ds.Tables.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    data1 = new Datas();
                    data1.DT_1D=int.Parse(ds.Tables[0].Rows[0]["DT_1D"].ToString());
                    data1.ARC_SER1NO = int.Parse(ds.Tables[0].Rows[0]["ARC_SER1NO"].ToString());
                    data1.DT_ENLEM = ds.Tables[0].Rows[0]["DT_ENLEM"].ToString();
                    data1.DT_BOYLAM = ds.Tables[0].Rows[0]["DT_BOYLAM"].ToString();
                    data1.DT_ENLEM_HAM = ds.Tables[0].Rows[0]["DT_ENLEM_HAM"].ToString();
                    data1.DT_BOYLAM_HAM = ds.Tables[0].Rows[0]["DT_BOYLAM_HAM"].ToString();
                    data1.DT_H1Z = ds.Tables[0].Rows[0]["DT_H1Z"].ToString();
                    data1.DT_TAR1H = DateTime.Parse(ds.Tables[0].Rows[0]["DT_TAR1H"].ToString());
                    data1.DT_ADRES = ds.Tables[0].Rows[i]["DT_ADRES"].ToString();
                    data1.DT_DURAKLAMA = Boolean.Parse(ds.Tables[0].Rows[i]["DT_DURAKLAMA"].ToString());
                    string bos = ds.Tables[0].Rows[0]["DT_KONTAK"].ToString();
                    if (bos == "1")
                        data1.DT_KONTAK = "Kapalı";
                    if (bos == "0")
                        data1.DT_KONTAK = "Açık";
                }
            }
            return data1;
        }

        public ObservableCollection<Datas> getDataList(DataSet ds)
        {
           ObservableCollection<Datas> dataList1 = new ObservableCollection<Datas>();

            if (ds.Tables.Count != 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    data1 = new Datas();
                   
                    data1.DT_1D = int.Parse(ds.Tables[0].Rows[i]["DT_1D"].ToString());
                    data1.ARC_SER1NO = int.Parse(ds.Tables[0].Rows[i]["ARC_SER1NO"].ToString());
                    data1.DT_ENLEM = ds.Tables[0].Rows[i]["DT_ENLEM"].ToString();
                    data1.DT_BOYLAM = ds.Tables[0].Rows[i]["DT_BOYLAM"].ToString();
                    data1.DT_ENLEM_HAM = ds.Tables[0].Rows[i]["DT_ENLEM_HAM"].ToString();
                    data1.DT_BOYLAM_HAM = ds.Tables[0].Rows[i]["DT_BOYLAM_HAM"].ToString();
                    data1.DT_H1Z = ds.Tables[0].Rows[i]["DT_H1Z"].ToString();
                    data1.DT_TAR1H = DateTime.Parse(ds.Tables[0].Rows[i]["DT_TAR1H"].ToString());
                    data1.DT_ADRES = ds.Tables[0].Rows[i]["DT_ADRES"].ToString();
                    string bos = ds.Tables[0].Rows[i]["DT_KONTAK"].ToString();
                    if (bos == "1")
                        data1.DT_KONTAK = "Kapalı";
                    if (bos == "0")
                        data1.DT_KONTAK = "Açık";

                    dataList1.Add(data1);

                }
            }
            return dataList1;
        }

    }
}