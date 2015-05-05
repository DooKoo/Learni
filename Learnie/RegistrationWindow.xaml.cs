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
using System.Windows.Shapes;
using Learnie.ServiceReference;

namespace Learnie
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationButton_OnClick(object sender, RoutedEventArgs e)
        {
            ServiceClient client = new ServiceClient();
            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                ErrorMessage.Text = "Паролі не співпадають";
                return;
            }

            bool result = client.AddUser(new User()
            {
                Password = PasswordBox.Password,
                Username = LoginBox.Text,
                Progress = 0,
                Status = 1,
                Role = 0
            });

            if (!result)
            {
                ErrorMessage.Text = "Користувач з таким іменем вже існує";
            }
            client.Close();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
