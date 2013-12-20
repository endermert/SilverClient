using System;
using System.Collections.Generic;
using System.Linq;

namespace MySisEvo.Classes
{
    public class SayacData
    {
        public int SYC_SER1NO { get; set; }
        public string SYC_T { get; set; }
        public string SYC_T1 { get; set; }
        public string SYC_T2 { get; set; }
        public string SYC_T3 { get; set; }
        public string SYC_1ND { get; set; }
        public string SYC_KAP { get; set; }
        public string SYC_HAM { get; set; } // ham verileri tutmak için 
        public Boolean SYC_DURUM { get; set; } // degerlerin yeni olup olmadığını kontrol eder,, timerlar arsında geçişin kontrolu için
        
        public SayacData()
        {
            SYC_DURUM = new Boolean();
            SYC_DURUM = false;
        }
    }
    
    
}
