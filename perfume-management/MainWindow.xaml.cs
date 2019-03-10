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
        public string path = @"Data Source=.\SQLEXPRESS;Initial Catalog=Perfume;Integrated Security=True";
        public string query, cmdstring;
        SqlConnection conn;
        SqlDataAdapter adapter;

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

            conn = new SqlConnection(path);

            query = @"SELECT * FROM PERFUME";
            adapter = new SqlDataAdapter(query, conn);

            DataTable table = new DataTable();
            adapter.Fill(table);
            datagrid_Items.DataContext = table.DefaultView;
            //datagrid_Items.CanUserAddRows = false;
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = datagrid_Items.SelectedItem as DataRowView;
            AddUpdate addUpdate;


            if (row != null)
            {
                Item item = new Item();
                item.id = int.Parse(row.Row.ItemArray[0].ToString());
                item.name = row.Row.ItemArray[1].ToString();
                item.volume = int.Parse(row.Row.ItemArray[2].ToString());
                item.price = int.Parse(row.Row.ItemArray[3].ToString());
                item.brand = row.Row.ItemArray[4].ToString();
                addUpdate = new AddUpdate(item);
                addUpdate.OnUpdate += AddUpdate_OnUpdate;
                row = null;
            }
            else
            {
                 addUpdate = new AddUpdate();
                addUpdate.OnUpdate += AddUpdate_OnAddNew;
            }
            
            addUpdate.ShowDialog();
        }

        private void AddUpdate_OnUpdate(Item item)
        {
            conn = new SqlConnection(path);
            conn.Open();

            cmdstring = String.Format("UPDATE PERFUME SET ITEM = '{0}', VOLUME = '{1}', PRICE = '{2}', BRAND = '{3}' WHERE ID = '{4}'", item.name, item.volume, item.price, item.brand, item.id);
            SqlCommand command = new SqlCommand(cmdstring, conn);
            command.ExecuteNonQuery();
            conn.Close();

            LoadData();
        }

        private void AddUpdate_OnAddNew(Item item)
        {

            conn = new SqlConnection(path);
            conn.Open();

            cmdstring = String.Format("INSERT INTO PERFUME VALUES('{0}','{1}','{2}','{3}')", item.name, item.volume, item.price, item.brand);
            SqlCommand command = new SqlCommand(cmdstring, conn);
            command.ExecuteNonQuery();
            conn.Close();

            LoadData();
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();

            

            DataSet dataSet = new DataSet();
            DataTable table = ((DataView)datagrid_Items.ItemsSource).ToTable();
            //table = table.GetChanges();
            //table.AcceptChanges();
            dataSet.Tables.Add(table);
            //adapter.Fill(dataSet.Tables[0]);
            //adapter.Fill(table);

            adapter.TableMappings.Add("PERFUME", "dataSet");

            //adapter.AcceptChangesDuringUpdate = true;


            //DataTable categoryTable = new DataTable();
            //adapter.Fill(categoryTable);
            dataSet.EndInit();
            //adapter.Update(dataSet, "PERFUME");
            adapter.Update(dataSet);
            //table.AcceptChanges();
            //conn.Close();
            //adapter.Update(/*categoryTable*/);
            adapter.Dispose();
            conn.Dispose();
            LoadData();
            ///


        }

        private void Button_Del_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = datagrid_Items.SelectedItem as DataRowView;
            int id = int.Parse(row.Row.ItemArray[0].ToString());


            cmdstring = String.Format("DELETE FROM PERFUME WHERE ID = '{0}'", id);
            conn.Open();
            SqlCommand command = new SqlCommand(cmdstring, conn);
            command.ExecuteNonQuery();
            conn.Close();

            LoadData();

        }

    }
}
