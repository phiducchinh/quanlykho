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
     public class OutputViewModel:BaseViewModel
    {
        private ObservableCollection<Output> _ListOutput;
        private ObservableCollection<OutputInfo> _ListOutputInfo;
        private ObservableCollection<InputInfo> _InputInfoList;
        private ObservableCollection<Model.Object> _ObjectList;
        private ObservableCollection<Customer> _Customer;

        private Output _SelectedItemOutput;
        private OutputInfo _SelectedItemOutputInfo;
        private Model.Object _SelectedObject;
        private Customer _SelectedCustomer;
        private InputInfo _SelectedInputInfoList;


        public ICommand AddCommandOutput { get; set; }
        public ICommand EditCommandOutput { get; set; }
        public ICommand DeleteCommandOutput { get; set; }

        public ICommand ResetCommand { get; set; }

        public ICommand AddCommandOutputInfo { get; set; }
        public ICommand EditCommandOutputInfo { get; set; }
        public ICommand DeleteCommandOutputInfo { get; set; }

        
        private DateTime? _DateOutput;
        private int? _Count;
        private double? _PriceOutput;
        private string _Status;

        public ObservableCollection<Output> ListOutput
        {
            get
            {
                return _ListOutput;
            }

            set
            {
                _ListOutput = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OutputInfo> ListOutputInfo
        {
            get
            {
                return _ListOutputInfo;
            }

            set
            {
                _ListOutputInfo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Model.Object> ObjectList
        {
            get
            {
                return _ObjectList;
            }

            set
            {
                _ObjectList = value;
                OnPropertyChanged();

            }
        }

        public ObservableCollection<Customer> Customer
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

        public Output SelectedItemOutput
        {
            get
            {
                return _SelectedItemOutput;
            }

            set
            {
                _SelectedItemOutput = value;
                OnPropertyChanged();
                if (SelectedItemOutput != null)
                {
                    DateOutput = SelectedItemOutput.DateOutput;
                    ListOutputInfo = new ObservableCollection<OutputInfo>(DataProvider.Ins.DB.OutputInfoes.Where(p => p.IdOutputInfo ==SelectedItemOutput.Id));
                   
                }
            }
        }

        public OutputInfo SelectedItemOutputInfo
        {
            get
            {
                return _SelectedItemOutputInfo;
            }

            set
            {
                _SelectedItemOutputInfo = value;
                OnPropertyChanged();
                if(SelectedItemOutputInfo!= null)
                {
                    InputInfoList = new ObservableCollection<InputInfo>(DataProvider.Ins.DB.InputInfoes.Where(x => x.IdObject == SelectedItemOutputInfo.IdObject));
                    SelectedObject = SelectedItemOutputInfo.Object;
                    SelectedCustomer = SelectedItemOutputInfo.Customer;
                    SelectedInputInfoList = SelectedItemOutputInfo.InputInfo;
                    Count = SelectedItemOutputInfo.Count;
                    Status = SelectedItemOutputInfo.Status;
                }
            }
        }

        public Model.Object SelectedObject
        {
            get
            {
                return _SelectedObject;
            }

            set
            {
                _SelectedObject = value;
                OnPropertyChanged();
            }
        }

        public Customer SelectedCustomer
        {
            get
            {
                return _SelectedCustomer;
            }

            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DateOutput
        {
            get
            {
                return _DateOutput;
            }

            set
            {
                _DateOutput = value;
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

        public double? PriceOutput
        {
            get
            {
                return _PriceOutput;
            }

            set
            {
                _PriceOutput = value;
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

        public ObservableCollection<InputInfo> InputInfoList
        {
            get
            {
                return _InputInfoList;
            }

            set
            {
                _InputInfoList = value;
                OnPropertyChanged();
            }
        }

        public InputInfo SelectedInputInfoList
        {
            get
            {
                return _SelectedInputInfoList;
            }

            set
            {
                _SelectedInputInfoList = value;
                OnPropertyChanged();
            }
        }

        public OutputViewModel()
        {
            ListOutput = new ObservableCollection<Output>(DataProvider.Ins.DB.Outputs);
            ObjectList = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects);
            Customer = new ObservableCollection<Model.Customer>(DataProvider.Ins.DB.Customers);
            /*
            if(SelectedItemOutputInfo !=null ) 
                InputInfoList = new ObservableCollection<InputInfo>(DataProvider.Ins.DB.InputInfoes.Where(x => x.IdObject == SelectedItemOutputInfo.IdObject));
            */
            ResetCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                SelectedObject = ObjectList[0];
                SelectedCustomer = Customer[0];
                DateOutput = DateTime.Now;
                Count = null;
                PriceOutput = null;
                Status = null;

            });

            AddCommandOutput = new RelayCommand<object>((p) =>
            {
                if (DateOutput == null)
                    return false;

                else
                    return true;
            }, (p) =>
            {
                var Output = new Model.Output() { Id = Guid.NewGuid().ToString(), DateOutput = DateOutput };
                DataProvider.Ins.DB.Outputs.Add(Output);
                DataProvider.Ins.DB.SaveChanges();

                ListOutput.Add(Output);

            });

            AddCommandOutputInfo = new RelayCommand<object>((p) =>
            {
                if (SelectedObject == null||SelectedCustomer==null)
                    return false;

                else
                    return true;
            }, (p) =>
            {
                var Outputinfo = new OutputInfo() { Id = Guid.NewGuid().ToString(), IdObject = SelectedObject.Id, IdInputInfo=SelectedInputInfoList.Id, IdOutputInfo=SelectedItemOutput.Id, IdCustomer = SelectedCustomer.Id, Count = Count, Status = Status };
                DataProvider.Ins.DB.OutputInfoes.Add(Outputinfo);
                DataProvider.Ins.DB.SaveChanges();

                ListOutputInfo.Add(Outputinfo);

            });
        }
    }
}
