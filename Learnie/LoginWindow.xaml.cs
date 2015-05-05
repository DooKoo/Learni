using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Learnie.ServiceReference;

namespace Learnie
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceClient client = new ServiceClient();
            User authUser = client.Authorize(LoginBox.Text, PasswordBox.Password);
            if (authUser != null)
            {
                switch (authUser.Role)
                {
                    case 0:
                        new StudentWindow(authUser);
                        Close();
                        break;
                    case 1:
                        new TeacherWindow(authUser);
                        Close();
                        break;
                    case 2:
                        new AdministratorWindow(authUser);
                        Close();
                        break;
                }
            }
            else
            {
                ErrorMessage.Text = "Невірний логін чи пароль";
            }
        }

        private void RegistrationButton_OnClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
