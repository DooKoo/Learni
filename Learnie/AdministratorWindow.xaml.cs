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
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        private List<User> _usersList;
        public AdministratorWindow(User administrator)
        {
            InitializeComponent();
            Show();
           
            ServiceClient client = new ServiceClient();
            _usersList = client.GetUsers().ToList();
            client.Close();

            #region UI initialization & Data binding

            var studentsViewItems = new TreeViewItem()
            {
                Header = "Студенти"
            };

            var teachersViewItems = new TreeViewItem()
            {
                Header = "Вчителі"
            };

            var adminsViewItems = new TreeViewItem()
            {
                Header = "Адміністратори"
            };

            _usersList.ForEach((user) =>
            {
                switch(user.Role)
                {
                    case 0:
                        studentsViewItems.Items.Add(user.Username);
                        break;
                    case 1:
                        teachersViewItems.Items.Add(user.Username);
                        break;
                    case 2:
                        adminsViewItems.Items.Add(user.Username);
                        break;
                }
            });
            UsersTreeView.Items.Add(studentsViewItems);
            UsersTreeView.Items.Add(teachersViewItems);
            UsersTreeView.Items.Add(adminsViewItems);

            RoleBox.Items.Add("Студент");
            RoleBox.Items.Add("Вчитель");
            RoleBox.Items.Add("Адміністратор");

            StatusBox.Items.Add("Неактивний");
            StatusBox.Items.Add("Активний");
           
            #endregion
        }

        private void UsersTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var username = UsersTreeView.SelectedValue.ToString();
            User selectedUser = _usersList.Find(user => user.Username == username);
            if (selectedUser != null)
            {
                LoginBox.Text = selectedUser.Username;
                RoleBox.SelectedIndex = selectedUser.Role;
                StatusBox.SelectedIndex = selectedUser.Status;
                ProgressBox.Text = selectedUser.Progress.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length != 0 && PasswordBox.Password.Length != 0
                && ProgressBox.Text.Length != 0)
            {
                if (!_usersList.Exists(user => user.Username == LoginBox.Text))
                {
                    ServiceClient client = new ServiceClient();
                    client.AddUser(new User()
                    {
                        Username = LoginBox.Text,
                        Password = PasswordBox.Password,
                        Role = RoleBox.SelectedIndex,
                        Status = StatusBox.SelectedIndex,
                        Progress = Int32.Parse(ProgressBox.Text)
                    });
                    client.Close();
                }
                else
                {
                    ErrorMessage.Text = "Такий користувач вже існує!";
                }
            }
            else
            {
                ErrorMessage.Text = "Заповніть всі поля!";
            }
        }
    }
}
