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

namespace QL
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            loadTables(); // load tables
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
            showBill(tableID);
        }

     

        private void showBill(int id)
        {
            // clear listview

            lvFood.Items.Clear();


            //


            List<BillInfo> listBillInfo = BillInfoDAO.Instance.GetListBillInfo(BillDAO.Instance.GetUncheckBillIdByTableId(id));

            // tao list view item cho moi bill info.

            foreach (BillInfo item in listBillInfo)
            {
                ListViewItem listViewItem = new ListViewItem(item.IdFood.ToString()); // first column item
                listViewItem.SubItems.Add(item.Count.ToString()); // sub item of first column

                // add to list view
                lvFood.Items.Add(listViewItem);

            }

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
    }
}
