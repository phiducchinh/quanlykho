﻿using QuanLyKho.Model;
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