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
        }
    }
}
