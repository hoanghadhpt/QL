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
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Environment.Exit(1);
                    }
                }
                else
                {
                    // Cancel the close
                    e.Cancel = true;
                }
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
   
    }
}
