//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKho.Model
{
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public partial class OutputInfo:BaseViewModel
    {
        public string Id { get; set; }

        public string IdObject
        {
            get
            {
                return _IdObject;
            }

            set
            {
                _IdObject = value;
                OnPropertyChanged();
            }
        }

        public string IdInputInfo
        {
            get
            {
                return _IdInputInfo;
            }

            set
            {
                _IdInputInfo = value;
                OnPropertyChanged();
            }
        }

        public string IdOutputInfo
        {
            get
            {
                return _IdOutputInfo;
            }

            set
            {
                _IdOutputInfo = value;
                OnPropertyChanged();
            }
        }

        public int IdCustomer
        {
            get
            {
                return _IdCustomer;
            }

            set
            {
                _IdCustomer = value;
                OnPropertyChanged();
            }
        }

        public int? Count
        {
            get
            {
                return _Count;
            }

            set
            {
                _Count = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }

        public Customer Customer
        {
            get
            {
                return _Customer;
            }

            set
            {
                _Customer = value;
                OnPropertyChanged();
            }
        }

        public InputInfo InputInfo
        {
            get
            {
                return _InputInfo;
            }

            set
            {
                _InputInfo = value;
                OnPropertyChanged();

            }
        }

        public Object Object
        {
            get
            {
                return _Object;
            }

            set
            {
                _Object = value;
                OnPropertyChanged();

            }
        }

        public Output Output
        {
            get
            {
                return _Output;
            }

            set
            {
                _Output = value;
                OnPropertyChanged();

            }
        }

        private string _IdObject ;
        private string _IdInputInfo ;
        private string _IdOutputInfo ;
        private int _IdCustomer ;
        private Nullable<int> _Count ;
        private string _Status ;
    
        private  Customer _Customer ;
        private  InputInfo _InputInfo ;
        private  Object _Object ;
        private  Output _Output ;
    }
}
