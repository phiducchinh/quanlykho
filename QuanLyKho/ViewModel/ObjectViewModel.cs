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
    public class ObjectViewModel:BaseViewModel
    {
        private ObservableCollection<Model.Object> _List;

        private ObservableCollection<Unit> _Unit;
        private ObservableCollection<Suplier> _Suplier;


        public ObservableCollection<Model.Object> List
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

        public Model.Object SelectedItem
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
                    BarCode = SelectedItem.BarCode;
                    QRCode = SelectedItem.QRCode;
                    SelectedSuplier = SelectedItem.Suplier;
                    SelectedUnit = SelectedItem.Unit;
                   
                }
            }
        }

        private Model.Object _SelectedItem;
        private Model.Unit _SelectedUnit;
        private Model.Suplier _SelectedSuplier;

        private string _DisplayName;
        private int _IdUnit;
        private int _IdSuplier;
        private string _QRCode;
        private string _BarCode;

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

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

        public int IdUnit
        {
            get
            {
                return _IdUnit;
            }

            set
            {
                _IdUnit = value;
                OnPropertyChanged();
            }
        }

        public string QRCode
        {
            get
            {
                return _QRCode;
            }

            set
            {
                _QRCode = value;
                OnPropertyChanged();
            }
        }

        public int IdSuplier
        {
            get
            {
                return _IdSuplier;
            }

            set
            {
                _IdSuplier = value;
                OnPropertyChanged();
            }
        }

        public string BarCode
        {
            get
            {
                return _BarCode;
            }

            set
            {
                _BarCode = value;
                OnPropertyChanged();
            }
        }
        
       
        public ObservableCollection<Unit> Unit
        {
            get
            {
                return _Unit;
            }

            set
            {
                _Unit = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Suplier> Suplier
        {
            get
            {
                return _Suplier;
            }

            set
            {
                _Suplier = value;
                OnPropertyChanged();
            }
        }

        public Unit SelectedUnit
        {
            get
            {
                return _SelectedUnit;
            }

            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
            }
        }

        public Suplier SelectedSuplier
        {
            get
            {
                return _SelectedSuplier;
            }

            set
            {
                _SelectedSuplier = value;
                OnPropertyChanged();
            }
        }

        public ObjectViewModel()
        {
            List = new ObservableCollection<Model.Object>(DataProvider.Ins.DB.Objects);
            Unit = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units);
            Suplier = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers);

            AddCommand = new RelayCommand<object>((p)=>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (SelectedSuplier == null || SelectedUnit == null)
                    return false;
                else
                    return true;
            },(p)=>
            {
                var object1 = new Model.Object() { DisplayName = DisplayName, Id = Guid.NewGuid().ToString(), IdUnit = SelectedUnit.Id, IdSuplier = SelectedSuplier.Id, BarCode = BarCode, QRCode=QRCode };
                DataProvider.Ins.DB.Objects.Add(object1);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(object1);

            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (SelectedItem==null)
                    return false;
                else
                    return true;
            }, (p) =>
            {
                var obj =DataProvider.Ins.DB.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                obj.DisplayName = DisplayName;
                obj.IdUnit = SelectedUnit.Id;
                obj.IdSuplier = SelectedSuplier.Id;
                obj.BarCode = BarCode;
                obj.QRCode = QRCode;

                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DisplayName = DisplayName;

            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (SelectedItem == null)
                    return false;
                else
                    return true;
            }, (p) =>
            {
               var obj = DataProvider.Ins.DB.Objects.SingleOrDefault(x => x.Id == SelectedItem.Id);
                
                DataProvider.Ins.DB.Objects.Remove(obj);
                List.Remove(obj);
                DataProvider.Ins.DB.SaveChanges();

            });


        }
    }
}
