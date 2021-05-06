using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QL.DAO;

namespace QL
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;


            if (Login(username, password))
            {
                frmMain frmMain = new frmMain();
                this.Hide();
                frmMain.ShowDialog();

            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác","Lỗi đăng nhập",MessageBoxButtons.OK,MessageBoxIcon.Error);   
            }
        }

        bool Login(string username, string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }
    }
}
