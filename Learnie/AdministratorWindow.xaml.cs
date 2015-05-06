using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ServiceProxy;
using ServiceProxy.LearnieService;

namespace Learnie
{
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        private List<User> _usersList;
        private readonly ProxyService _proxy;
        public AdministratorWindow(User administrator)
        {
            InitializeComponent();
            Show();
            _proxy = new ProxyService();
        
            #region UI initialization & Data binding
            UpdateUserTree();

            RoleBox.Items.Add("Студент");
            RoleBox.Items.Add("Вчитель");
            RoleBox.Items.Add("Адміністратор");

            StatusBox.Items.Add("Неактивний");
            StatusBox.Items.Add("Активний");
           
            #endregion
        }

        private void UsersTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ClearUserData();
            var username = UsersTreeView.SelectedValue.ToString();
            User selectedUser = _usersList.Find(user => user.Username == username);
            if (selectedUser != null)
            {
                LoginBox.Text = selectedUser.Username;
                PasswordBox.Password = selectedUser.Password;
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

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length != 0 && PasswordBox.Password.Length != 0
                && ProgressBox.Text.Length != 0)
            {
                if (!_usersList.Exists(user => user.Username == LoginBox.Text))
                {
                    
                    User newUser = new User()
                    {
                        Username = LoginBox.Text,
                        Password = PasswordBox.Password,
                        Role = RoleBox.SelectedIndex,
                        Status = StatusBox.SelectedIndex,
                        Progress = Int32.Parse(ProgressBox.Text)
                    };
                  
                    _proxy.AddUser(newUser);

                    UpdateUserTree();
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

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length != 0)
            {
                if (!_proxy.DeleteUser(LoginBox.Text))
                {
                    ErrorMessage.Text = "Користувача з таким іменем не існує!";
                }
                else
                {
                    UpdateUserTree();
                    ClearUserData();
                }
            }
            else
            {
                ErrorMessage.Text = "Введіть ім'я користувача!";
            }
        }

        /// <summary>
        /// Change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text.Length != 0 && PasswordBox.Password.Length != 0 &&
                ProgressBox.Text.Length != 0)
            {
                User changedUser = new User()
                {
                    Username = LoginBox.Text,
                    Password = PasswordBox.Password,
                    Role = RoleBox.SelectedIndex,
                    Status = StatusBox.SelectedIndex,
                    Progress = Int32.Parse(ProgressBox.Text)
                };
                if (_proxy.DeleteUser(changedUser.Username))
                {
                    _proxy.AddUser(changedUser);
                    UpdateUserTree();
                    ClearUserData();
                }
                else
                {
                    ErrorMessage.Text = "Користувача з таким іменем не існує!";
                }
            }
            else
            {
                ErrorMessage.Text = "Заповність всі поля!";
            }
        }

        private void UpdateUserTree()
        {
            _usersList = _proxy.GetUsers().ToList();
            UsersTreeView.Items.Clear();
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

            _usersList.ForEach(user =>
            {
                switch (user.Role)
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
        }

        private void ClearUserData()
        {
            LoginBox.Clear();
            PasswordBox.Clear();
            RoleBox.SelectedIndex = 0;
            StatusBox.SelectedIndex = 1;
            ProgressBox.Clear();
            ErrorMessage.Text.Remove(0);
        }
    }
}
