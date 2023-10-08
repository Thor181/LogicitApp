using LogicitApp.Data.DataLogic;
using LogicitApp.Shared;
using LogicitApp.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LogicitApp.ViewModels
{
    public class LoginViewModel
    {
        public SimpleCommand LoginCommand { get; set; }

        public string LoginString { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new SimpleCommand(LoginHandler);
        }

        private void LoginHandler(object? parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                var pwd = passwordBox.Password;

                if (string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(LoginString))
                {
                    MessageBox.Show("Логин и пароль должны быть введены");
                    return;
                }

                var passwordHash = Password.Hash(pwd);
                using var userLogic = new UserLogic();

                var result = userLogic.IsValidUser(LoginString, passwordHash);
                if (!result.Success)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MainWindow.ChangeView(Views.Shared.AvailableViews.MainView);
            }
        }
    }
}
