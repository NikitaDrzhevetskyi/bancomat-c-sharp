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
    public partial class FormCheckout : Form
    {
        const string textFormatMultiply = @"Введіть сумму кратну * та натисніть Далі ";
        const string textFormatAvailable = @"Доступні купюри: *";

        List<int> available;
        float balance;
        CardService cardService = CardService.Instance;

        public FormCheckout()
        {
            available = new List<int>() {
                100, 200, 500               // Генерація буде
            };
            InitializeComponent();
        }

        private void FormCheckout_Load(object sender, EventArgs e) {
            available.Sort();   // Найменше - перше
            string availableUahText = "";
            foreach (var item in available) {
                availableUahText += item.ToString() + ", ";
            }
            balance = cardService.GetBalanceByCardNumber(FormsService.ChosenCardNumber);
            labelAvailable.Text = textFormatAvailable.Replace("*", availableUahText);
            labelMulti.Text = textFormatMultiply.Replace("*", available[0].ToString());
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textPASS_KeyDown(object sender, KeyEventArgs e) {
            e.Handled = (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) ||
                      (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9);
            e.Handled = e.KeyCode == Keys.Back || e.Handled;
        }

        private void buttonBalance_Click(object sender, EventArgs e) {
            
             
            MessageBox.Show(
                $"Ваш баланс складає {Math.Round(balance, 2)} гривень."
                );
        }

        private void button2_Click(object sender, EventArgs e) {
            textPASS.Text = "100";
        }

        private void button3_Click(object sender, EventArgs e) {
            textPASS.Text = "500";
        }

        private void button4_Click(object sender, EventArgs e) {
            textPASS.Text = "1000";
        }

        private void buttonWithdrown_Click(object sender, EventArgs e) {
            if(textPASS.Text.Length < 2) {
                MessageBox.Show(
                    "Введіть коректну суму для виводу!"
                    );
                return;
            }
            float.TryParse(textPASS.Text, out float amount);
            if((int)amount % available[0] != 0) {
                MessageBox.Show(
                    $"Введіть коректну суму для виводу, кратну {available[0]}!"
                    );
                return;
            }
            if(balance < amount) {
                MessageBox.Show(
                    "На балансі недостатньо коштів! \nГрусть, печаль, тоска :("
                    );
                return;
            }
            cardService.WithdrawFromCard(FormsService.ChosenCardNumber, amount);
            Form6 form = new Form6();
            this.Hide();
            form.Show();
            form.Focus();
            return;
        }

        private void textPASS_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
                e.Handled = true;	//	перевіряемо чи був оброблений ввод с клавіатури
        }
    }
}
