using LogicitApp.Stuff.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LogicitApp.ViewModels
{
    public class LoginViewModel
    {
        public SimpleCommand LoginCommand { get; set; }

        public string LoginString { get; set; }
        public string PasswordString { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new SimpleCommand(LoginHandler);
        }

        private void LoginHandler(object? parameter)
        {
            var sec = parameter as PasswordBox;
            
          
            var JUST_BREAKPOINT = string.Empty; 
        }
    }
}
