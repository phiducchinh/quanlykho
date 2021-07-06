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
    public class MainViewModel: BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        private ObservableCollection<Unit> _Unit;


        private string _LuongNhap;
        private string _LuongXuat;
        private int _TonKho;
        private TonKho _SelectedTK;
        private string _DonVi;


        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SuplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ObiectCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }

        public ObservableCollection<TonKho> TonKhoList
        {
            get
            {
                return _TonKhoList;
            }

            set
            {
                _TonKhoList = value;
                OnPropertyChanged();
            }
        }

        public string LuongNhap
        {
            get
            {
                return _LuongNhap;
            }

            set
            {
                _LuongNhap = value;
                OnPropertyChanged();
            }
        }

        public string LuongXuat
        {
            get
            {
                return _LuongXuat;
            }

            set
            {
                _LuongXuat = value;
                OnPropertyChanged();
            }
        }

        public int TonKho
        {
            get
            {
                return _TonKho;
            }

            set
            {
                _TonKho = value;
                OnPropertyChanged();
            }
        }

        public TonKho SelectedTK
        {
            get
            {
                return _SelectedTK;
            }

            set
            {
                _SelectedTK = value;
                OnPropertyChanged();
                if (SelectedTK != null)
                {
                    LoginWindow loginWindow = new LoginWindow();
                    var loginVM = loginWindow.DataContext as LoginViewModel;
                    //MessageBox.Show(loginVM.User.DisplayName.ToString());

                    var objectchon = DataProvider.Ins.DB.Objects.Where(x=>x.Id== SelectedTK.Object.Id).SingleOrDefault();
                    var objectListNhap = DataProvider.Ins.DB.InputInfoes.Where(v=>v.IdObject==objectchon.Id);
                    int sumInput = 0;
                    foreach (var item in objectListNhap)
                    {                      
                        sumInput += (int) item.Count;
                       
                    }
                    string a = SelectedTK.Object.Unit.DisplayName.ToString();
                    LuongNhap = sumInput.ToString()+ " (" + a + ")";

                   
                    var objectListXuat = DataProvider.Ins.DB.OutputInfoes.Where(v => v.IdObject == objectchon.Id);
                    int sumOutput = 0;
                    foreach (var item in objectListXuat)
                    {
                        sumOutput += (int)item.Count;

                    }
                    LuongXuat = sumOutput.ToString() + " (" + SelectedTK.Object.Unit.DisplayName + ")";
                    TonKho = sumInput - sumOutput;

                    
                }
            }
        }

        public string DonVi
        {
            get
            {
                return _DonVi;
            }

            set
            {
                _DonVi = value;
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
            }
        }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;

                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }

               
            }
            );

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UnitWindow uw = new UnitWindow(); uw.ShowDialog(); } );
            SuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow sw = new SuplierWindow(); sw.ShowDialog(); });
            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow cw = new CustomerWindow(); cw.ShowDialog(); });
            ObiectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow cw = new ObjectWindow(); cw.ShowDialog(); });
            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow cw = new UserWindow(); cw.ShowDialog(); });
            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { InputWindow cw = new InputWindow(); cw.ShowDialog(); });
            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OutputWindow cw = new OutputWindow(); cw.ShowDialog(); });

        }

        void LoadTonKhoData()
        {

            Unit = new ObservableCollection<Model.Unit>(DataProvider.Ins.DB.Units);
            TonKhoList = new ObservableCollection<TonKho>();

            int i = 1;

            var objectList = DataProvider.Ins.DB.Objects;
            foreach(var item in objectList)
            {
                var inputList = DataProvider.Ins.DB.InputInfoes.Where(p=>p.IdObject== item.Id);
                var outputList= DataProvider.Ins.DB.OutputInfoes.Where(p => p.IdObject == item.Id);

                int sumInput = 0;
                int sumOutput = 0;

                if (inputList != null && inputList.Count() >0)
                {
                    sumInput = (int)inputList.Sum(p => p.Count);
                }
                if (outputList != null && outputList.Count()>0)
                {
                    sumOutput = (int)outputList.Sum(p => p.Count);
                }

                TonKho tonkho = new TonKho();
                tonkho.STT = i;
                tonkho.Count = sumInput - sumOutput;
                tonkho.Object = item;

                TonKhoList.Add(tonkho);

                i++;
            }

            
            
            
        }


    }
}
