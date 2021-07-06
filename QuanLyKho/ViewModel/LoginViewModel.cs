using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _UserName;
        private string _Password;

        public User User { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public string UserName
        {
            get
            {
                return _UserName;
            }

            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }

            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });


        }

        void Login(Window p)
        {
            if (p == null)
                return;

            var acc= DataProvider.Ins.DB.Users.Where(x => x.UserName == UserName && x.Password==Password).Count();

            User = DataProvider.Ins.DB.Users.Where(y => y.UserName == UserName).SingleOrDefault();

            if (acc > 0)
            {
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("sai tài khoản hoặc mật khẩu");
            }
           

        }
    }
}