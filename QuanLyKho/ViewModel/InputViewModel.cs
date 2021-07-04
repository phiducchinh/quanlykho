using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class InputViewModel:BaseViewModel
    {
        private ObservableCollection<Input> _ListInput;
        private ObservableCollection<InputInfo> _ListInputInfo;
        private ObservableCollection<Model.Object> _ObjectList;


        public ObservableCollection<Input> ListInput
        {
            get
            {
                return _ListInput;
            }

            set
            {
                _ListInput = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<InputInfo> ListInputInfo
        {
            get
            {
                return _ListInputInfo;
            }

            set
            {
                _ListInputInfo = value;
                OnPropertyChanged();
            }
        }

       

        public Input SelectedItemInput
        {
            get
            {
                return _SelectedItemInput;
            }

            set
            {
                _SelectedItemInput = value;
                OnPropertyChanged();
                if (SelectedItemInput != null)
                {
                    DateInput = SelectedItemInput.DateInput;
                    ListInputInfo = new ObservableCollection<InputInfo>(DataProvider.Ins.DB.InputInfoes.Where(p => p.IdInput == SelectedItemInput.Id));

                    /*
                    ListInputInfo = new ObservableCollection<InputInfo1>();
                    var inputinfo = DataProvider.Ins.DB.InputInfoes.Where(p => p.IdInput == SelectedItemInput.Id);
                    foreach (var item in inputinfo)
                    {
                        InputInfo1 i = new InputInfo1();
                        i.InputInfo = item;
                        Model.Object obj1 = DataProvider.Ins.DB.Objects.Where(p => p.Id == item.IdObject).SingleOrDefault();
                        Unit u = DataProvider.Ins.DB.Units.Where(p => p.Id == obj1.IdUnit).SingleOrDefault();
                        i.unit = u.DisplayName;
                        ListInputInfo.Add(i);
                    }
                    */
                }
            }
        }

        public InputInfo SelectedItemInputInfo
        {
            get
            {
                return _SelectedItemInputInfo;
            }

            set
            {
                _SelectedItemInputInfo = value;
                OnPropertyChanged();
                if (SelectedItemInputInfo != null)
                {
                    
                    Count = SelectedItemInputInfo.Count;
                    PriceInput = SelectedItemInputInfo.InputPrice;
                    PriceOutput = SelectedItemInputInfo.OutputPrice;
                    Status = SelectedItemInputInfo.Status;
                    SelectedObject = SelectedItemInputInfo.Object;
                    //List<Model.Object> obj = new List<Model.Object>(DataProvider.Ins.DB.Objects.Where(p => p.Id == SelectedItemInputInfo.IdObject));
                    /*
                    Model.Object obj = DataProvider.Ins.DB.Objects.Where(p => p.Id == SelectedItemInputInfo.IdObject).SingleOrDefault();
                    ObjectDisplayName = obj.DisplayName;
                    */


                }
            }
        }

        private Input _SelectedItemInput;
        private InputInfo _SelectedItemInputInfo;
        private Model.Object _SelectedObject;
        

        public ICommand AddCommandInput { get; set; }
        public ICommand EditCommandInput { get; set; }
        public ICommand DeleteCommandInput { get; set; }

        public ICommand ResetCommand { get; set; }


        public ICommand AddCommandInputInfo { get; set; }
        public ICommand EditCommandInputInfo { get; set; } 
        public ICommand DeleteCommandInputInfo { get; set; }

        public string ObjectDisplayName
        {
            get
            {
                return _ObjectDisplayName;
            }

            set
            {
                _ObjectDisplayName = value;
                OnPropertyChanged();
            }
        }

        public DateTime? DateInput
        {
            get
            {
                return _DateInput;
            }

            set
            {
                _DateInput = value;
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

        public double? PriceInput
        {
            get
            {
                return _PriceInput;
            }

            set
            {
                _PriceInput = value;
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

        private string _ObjectDisplayName;
        private DateTime? _DateInput;
        private int? _Count;
        private double? _PriceInput;
        private double? _PriceOutput;
        private string _Status;
        

        public InputViewModel()
        {
            ListInput = new ObservableCollection<Input>(DataProvider.Ins.DB.Inputs);
            ObjectList = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects);

            /*
            ListInputInfo = new ObservableCollection<InputInfo>(DataProvider.Ins.DB.InputInfoes.Where(p=>p.IdInput==SelectedItemInput.Id));
            
            */

            ResetCommand = new RelayCommand<object>((p) =>
            {   
                return true;
            }, (p) =>
            {
                SelectedObject = ObjectList[0];
                DateInput = DateTime.Now;
                Count = null;
                PriceInput = null;
                PriceOutput = null;
                Status = null;

            });

            AddCommandInput = new RelayCommand<object>((p) =>
            {
                if (DateInput==null)
                    return false;
                
                else
                    return true;
            }, (p) =>
            {
                var input = new Input() { Id=Guid.NewGuid().ToString(), DateInput=DateInput};
                DataProvider.Ins.DB.Inputs.Add(input);
                DataProvider.Ins.DB.SaveChanges();

                ListInput.Add(input);

            });

            EditCommandInput = new RelayCommand<object>((p) =>
            {
            if (DateInput == null || SelectedItemInput == null)
                    return false;

                else
                    return true;
            }, (p) =>
            {
                var input = DataProvider.Ins.DB.Inputs.Where(x => x.Id == SelectedItemInput.Id).SingleOrDefault();
                input.DateInput = DateInput;
                DataProvider.Ins.DB.SaveChanges();
                SelectedItemInput.Id = input.Id;

            });

            AddCommandInputInfo = new RelayCommand<object>((p) =>
            {
            if (SelectedObject == null)
                    return false;

                else
                    return true;
            }, (p) =>
            {
                var inputinfo = new InputInfo() { Id = Guid.NewGuid().ToString(), IdObject=SelectedObject.Id,IdInput=SelectedItemInput.Id,Count=Count,InputPrice= PriceInput,OutputPrice=PriceOutput,Status=Status};
                DataProvider.Ins.DB.InputInfoes.Add(inputinfo);
                DataProvider.Ins.DB.SaveChanges();

                ListInputInfo.Add(inputinfo);

            });

            EditCommandInputInfo = new RelayCommand<object>((p) =>
            {
                var inputinfoc = DataProvider.Ins.DB.InputInfoes.Where(x => x.Id == SelectedItemInputInfo.Id).SingleOrDefault();
                if (SelectedObject.Id != inputinfoc.IdObject)
                    return false;
                if (SelectedItemInputInfo==null || SelectedObject==null)
                    return false;

                else
                    return true;


            }, (p) =>
            {
                var inputinfo = DataProvider.Ins.DB.InputInfoes.Where(x => x.Id == SelectedItemInputInfo.Id).SingleOrDefault();
                inputinfo.Count = Count;
                inputinfo.InputPrice = PriceInput;
                inputinfo.OutputPrice = PriceOutput;
                DataProvider.Ins.DB.SaveChanges();
                

            });

            DeleteCommandInputInfo = new RelayCommand<object>((p) =>
            {
                if (SelectedItemInputInfo == null)
                    return false;
                else
                    return true;
            }, (p) =>
            {
                var inputinfo = DataProvider.Ins.DB.InputInfoes.SingleOrDefault(x => x.Id == SelectedItemInputInfo.Id);

                DataProvider.Ins.DB.InputInfoes.Remove(inputinfo);
                ListInputInfo.Remove(inputinfo);
                DataProvider.Ins.DB.SaveChanges();

            });

            DeleteCommandInput = new RelayCommand<object>((p) =>
            {
                if (SelectedItemInput == null)
                    return false;
                else
                    return true;
            }, (p) =>
            {
                List<InputInfo> inputinfo = new List<InputInfo>(DataProvider.Ins.DB.InputInfoes.Where(x => x.IdInput == SelectedItemInput.Id));
                foreach(InputInfo item in inputinfo)
                {
                    DataProvider.Ins.DB.InputInfoes.Remove(item);
                    
                }
                var input = DataProvider.Ins.DB.Inputs.SingleOrDefault(x => x.Id == SelectedItemInput.Id);

                DataProvider.Ins.DB.Inputs.Remove(input);
                ListInput.Remove(input);
                DataProvider.Ins.DB.SaveChanges();

            });
        }

    }
}
