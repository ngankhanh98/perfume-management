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
            LoadData();
        }

        private void LoadData()
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=Perfume;Integrated Security=True";
            SqlConnection conn = new SqlConnection(path);

            string query = @"SELECT * FROM PERFUME";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

            DataTable table = new DataTable();
            adapter.Fill(table);
            datagrid_Items.DataContext = table.DefaultView;
            datagrid_Items.CanUserAddRows = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUpdate addUpdate = new AddUpdate();
            addUpdate.OnUpdate += AddUpdate_OnUpdate;
            addUpdate.ShowDialog();
        }

        private void AddUpdate_OnUpdate(Item item)
        {
            string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=Perfume;Integrated Security=True";
            SqlConnection conn = new SqlConnection(path);
            conn.Open();

            string cmdstring = String.Format("INSERT INTO PERFUME VALUES('{0}','{1}','{2}','{3}')",item.name, item.volume, item.price, item.brand);
            SqlCommand command = new SqlCommand(cmdstring, conn);
            command.ExecuteNonQuery();
            conn.Close();

            LoadData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
