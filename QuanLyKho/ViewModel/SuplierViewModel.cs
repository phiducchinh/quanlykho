using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class SuplierViewModel: BaseViewModel
    {
        private ObservableCollection<Suplier> _List;

        public ObservableCollection<Suplier> List
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

        
        public Suplier SelectedItem
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

        private Suplier _SelectedItem;
        private string _DisplayName;
        private string _Address;
        private string _Phone;
        private string _Email;
        private string _MoreInfo;
        private DateTime? _DateContract;

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

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

        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                var displayList = DataProvider.Ins.DB.Supliers.Where(x => x.DisplayName == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }, (p) =>
            {
                
                var suplier = new Suplier() { DisplayName = DisplayName, Address=Address,Phone=Phone, Email=Email,MoreInfo=MoreInfo, DateContract = DateContract };
                DataProvider.Ins.DB.Supliers.Add(suplier);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(suplier);
            });
        }

    }
}
