
using System;
using System.ComponentModel;

namespace MySisEvo.Classes
{
    public class Data : INotifyPropertyChanged
    {
        string _Enlem;
        string _Boylam;
        string _Hiz;
        string _Adres;
        string _Kontak;
        
        public string Enlem {
            get
            {
                return _Enlem;
            }
            set
            {
                if (_Enlem != value)
                {
                    _Enlem = value;
                    OnPropertyChanged("Enlem");
                }
            }
        }

        
        public string Boylam
        {
            get
            {
                return _Boylam;
            }
            set
            {
                if (_Boylam != value)
                {
                    _Boylam = value;
                    OnPropertyChanged("Boylam");
                }
            }
        }
        public string Hiz
        {
            get
            {
                return _Hiz;
            }
            set
            {
                if (_Hiz != value)
                {
                    _Hiz = value;
                    OnPropertyChanged("Hiz");
                }
            }
        }

        public string Adres
        {
            get
            {
                return _Adres;
            }
            set
            {
                if (_Adres!= value)
                {
                    _Adres = value;
                    OnPropertyChanged("Adres");
                }
            }
        }

        public string Kontak
        {
            get
            {
                return _Kontak;
            }
            set
            {
                if (_Kontak != value)
                {
                    _Kontak = value;
                    OnPropertyChanged("Kontak");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}