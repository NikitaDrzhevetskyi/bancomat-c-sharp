using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1
{
    public partial class FormNoCard : Form
    {
        public FormNoCard()
        {
            InitializeComponent();
        }
private void textPASS_KeyPress(object sender, KeyPressEventArgs e)
        {
        char number = e.KeyChar;
            if (!char.IsDigit(number)&& number != 8)
                e.Handled = true;//перевіряемо чи був оброблений ввод с клавіатури 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormStart f1 = new FormStart();
            f1.Show();
            Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string phonenum = "+38" + new string(
                                            (from s in maskedTextBoxPhone.Text.ToCharArray()
                                             where !("()- ".Contains(s)) // Вибираємо всі символу, окрім (, ), - і пробілу
                                             select s).ToArray()); // ToArray - щоб зліпити в кучу
            // MessageBox.Show(phonenum);
            try
            {
                FormsService.ChosenCardNumber = CardService.Instance.GetNumberByPhone(phonenum);
                FormNoCardConfirm f5 = new FormNoCardConfirm();
                f5.Show();
                this.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка при пошуку за телефоном");
            }
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number)&& number != 8)
                e.Handled = true;//перевіряемо чи був оброблений ввод с клавіатури
        }
    }
}
