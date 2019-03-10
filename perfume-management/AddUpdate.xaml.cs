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

namespace perfume_management
{
    /// <summary>
    /// Interaction logic for AddUpdate.xaml
    /// </summary>
    public partial class AddUpdate : Window
    {
        public delegate void DataChange(Item item);
        public event DataChange OnUpdate = null;
        public int id = 0;
        public AddUpdate()
        {
            InitializeComponent();
        }

        public AddUpdate(Item item)
        {
            InitializeComponent();
            id = item.id;
            txt_Name.Text = item.name;
            txt_Volume.Text = item.volume.ToString();
            txt_Price.Text = item.price.ToString();
            txt_Brand.Text = item.brand;
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            Item newitem = GetItemInfor();

            if (OnUpdate != null)
                OnUpdate(newitem);
        }

        private Item GetItemInfor()
        {
            Item item = new Item();
            item.id = id;
            item.name = txt_Name.Text;
            item.volume = int.Parse(txt_Volume.Text);
            item.price = int.Parse(txt_Price.Text);
            item.brand = txt_Brand.Text;

            return item;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
