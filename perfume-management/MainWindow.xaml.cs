using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace perfume_management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=Perfume;Integrated Security=True";
            SqlConnection conn = new SqlConnection(path);

            string query = @"SELECT * FROM PERFUME";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

            DataTable table = new DataTable();
            adapter.Fill(table);
            datagrid_Items.DataContext = table.DefaultView;
        }
    }
}
