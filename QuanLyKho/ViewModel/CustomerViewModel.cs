using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKho.Model;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class CustomerViewModel: BaseViewModel
    {
        private ObservableCollection<Customer> _List;

        public ObservableCollection<Customer> List
        {
            get
            {
                return _List;
            }

            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }

        public Customer SelectedItem
        {
            get
            {
                return _SelectedItem;
            }

            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Address = SelectedItem.Address;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    MoreInfo = SelectedItem.MoreInfo;
                    DateContract = SelectedItem.DateContract;
                }
            }
        }

        private Customer _SelectedItem;
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }


        private string _DisplayName;
        private string _Address;
        private string _Phone;
        private string _Email;
        private string _MoreInfo;
        private DateTime? _DateContract;

        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }

            set
            {
                _DisplayName = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get
            {
                return _Address;

            }

            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get
            {
                return _Phone;
            }

            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return _Email;
            }

            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }

        public string MoreInfo
        {
            get
            {
                return _MoreInfo;
            }

            set
            {
                _MoreInfo = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DateContract
        {
            get
            {
                return _DateContract;
            }

            set
            {
                _DateContract = value;
                OnPropertyChanged();
            }
        }

        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);
            AddCommand= new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                return true;
            }, (p) =>
            {
                var customer = new Customer() { DisplayName = DisplayName,Address= Address,Phone= Phone,Email= Email,MoreInfo=MoreInfo,DateContract=DateContract};
                DataProvider.Ins.DB.Customers.Add(customer);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(customer);

            });

        }

    }
}
