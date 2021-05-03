using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_работа__1
{
    public partial class AuthorizationForm : Form
    {
        HelloForm parentForm;
        bool isAuth = false; // Авторизовался ли пользователь
        public AuthorizationForm(HelloForm helloForm)
        {
            parentForm = helloForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (loginTextBox.Text == "admin" && passwordTextBox.Text == "admin")
            {
                isAuth = true;
                parentForm.Show();
                parentForm.start();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Неправильные логин или пароль!",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                loginTextBox.Text = "";
                passwordTextBox.Text = "";
                loginTextBox.Focus();
            }
        }

        private void loginTextBox_Enter(object sender, EventArgs e)
        {
            passwordTextBox.Focus();
        }

        private void loginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passwordTextBox.Focus();
            }
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void AuthorizationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isAuth)
            {
                parentForm.Show();
            }
        }
    }
}
