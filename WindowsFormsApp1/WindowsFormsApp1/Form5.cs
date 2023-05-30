using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormNoCardConfirm : Form
    {
        private Random rnd;
        public FormNoCardConfirm()
        {
            rnd = new Random((int)DateTime.Now.Ticks);
            InitializeComponent();
        }

        private void FormNoCardConfirm_Load(object sender, EventArgs e)
        {
            codeBox.Text = GetNDigitRandNumber(6);
        }

        private string GetNDigitRandNumber(int n)
        {
                if(n == 0) return "";
                return rnd.Next(0, 10).ToString() + GetNDigitRandNumber(n-1);
        }
        private void button4_Click(object sender, EventArgs e)
        {

            FormNoCard f1 = new FormNoCard();
            f1.Show();
            Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textPASS.Text != codeBox.Text)
            {
               MessageBox.Show("Неправильний код для підтвердження, повторити спробу?");
               codeBox.Text = GetNDigitRandNumber(6);
               textPASS.Text = "";
               return; 
            }    
            FormCheckout f1 = new FormCheckout();
            this.Visible = false;
            f1.Show();
            f1.Focus();
        }

        private void textPASS_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number)&& number != 8)
                e.Handled = true;//перевіряемо чи був оброблений ввод с клавіатури
        }
    }
}
