using QL.DAO;
using QL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QL.DTO.Menu;

namespace QL
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            loadTables(); // load tables
            loadCategory(); // load category
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProfile frmProfile = new frmProfile();
            frmProfile.Show();
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAccount frmAccount = new frmAccount();
            frmAccount.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logout == false)
            {
                logout = true;
                Application.Exit();
            }
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            logout = true;
            Application.Restart();
        }

        bool logout = false;

        private void loadTables()
        {
            List<Table> tables = DAO.TableDAO.Instance.GetListTables();

            foreach (Table table in tables)
            {
                Button btn = new Button() { Width = TableDAO.tableWith, Height = TableDAO.tableHeigh };
                btn.Text = table.Name;
                btn.FlatStyle = FlatStyle.Flat;

                // set event click

                btn.Click += Btn_Click;
                btn.Tag = table;

                // end


                if (table.Status == "Trống")
                {
                    btn.BackColor = Color.AliceBlue;
                }
                else 
                {
                    btn.BackColor = Color.PaleGreen;
                }

                flTable.Controls.Add(btn);
            }
        }
        // tạo event click cho btn
        private void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lvFood.Tag = (sender as Button).Tag;
            showBill(tableID);
        }

     

        private void showBill(int id)
        {
            // clear listview

            lvFood.Items.Clear();

            // load client
            Client client = MenuDAO.Instance.GetClientInMenuByTableID(id);
            txtClientPhone.Text = client.Phone;
            txtClientAddress.Text = client.Address;
            txtClientComment.Text = client.Comment;

            //Load menu
            List<Menu> menu = MenuDAO.Instance.GetListMenuByTable(id,Convert.ToInt32(txtDiscount.Text));
            float totalPrice = 0;
            foreach (Menu item in menu)
            {
                ListViewItem viewItem = new ListViewItem(item.FoodName.ToString());
                viewItem.SubItems.Add(item.Count.ToString("0,0"));
                //viewItem.SubItems.Add(item.Discount.ToString());
                viewItem.SubItems.Add(item.Price.ToString("0,0"));
                viewItem.SubItems.Add(item.TotalPrice.ToString("0,0"));
                totalPrice += item.TotalPrice;
                lvFood.Items.Add(viewItem);
                
            }

            txtTotalPrice.Text = totalPrice.ToString("0,0");


        }

        private void loadCategory()
        {
            List<Category> categories = new List<Category>();
            categories = CategoryDAO.Instance.GetCategories();
            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "name";

        }

        private void loadFoodByCategoryID(int id)
        {
            
            List<Food> foods = new List<Food>();
            foods = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = foods;
            cbFood.DisplayMember = "name";
        }


        private void btnFindClientByPhone_Click(object sender, EventArgs e)
        {
            Client client = ClientDAO.Instance.GetClientByPhone(txtClientPhone.Text.Trim());
            txtClientPhone.Text = client.Phone;
            txtClientAddress.Text = client.Address.Trim();
            txtClientComment.Text = client.Comment;

        }

        private void btnAddNewClient_Click(object sender, EventArgs e)
        {
            ClientDAO.Instance.AddNewClient(txtClientPhone.Text.Trim(), txtClientAddress.Text, txtClientComment.Text);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbFood.Text = "";
            int id = 0;
            ComboBox comboBox = sender as ComboBox;

            if (comboBox.SelectedItem == null)
                return;

            Category selected = comboBox.SelectedItem as Category;
            id = selected.ID;


            loadFoodByCategoryID(id);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Table table = lvFood.Tag as Table; // lay table hien tai
            int clientID = ClientDAO.Instance.GetClientIdByPhone(txtClientPhone.Text);
            int idFood = (cbFood.SelectedItem as Food).ID;
            int count = (int)numericUpDown1.Value;


            int idBill = BillDAO.Instance.GetUncheckBillIdByTableId(table.ID);
            if (idBill == -1)  // Bill chua ton tai
            {
                BillDAO.Instance.InsertBill(table.ID, clientID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxBillID(), idFood, count,clientID,idBill);
            }
            else // Bill da ton tai thì insert vào
            {

                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count, clientID, idBill);
            }


            showBill(table.ID);
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            Table table = lvFood.Tag as Table;
            int idBill = BillDAO.Instance.GetUncheckBillIdByTableId(table.ID);

            if (idBill == -1)
            { }
            else
            {
                if (MessageBox.Show("Bạn có muốn thanh toán cho " + table.Name + " không ", "Thống báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill);
                }
            }

            showBill(table.ID);

        }
    }
}
