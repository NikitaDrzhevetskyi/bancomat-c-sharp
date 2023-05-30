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
	public partial class FormLogin : Form
	{
		int countTries = 0;

		public FormLogin()
		{
			InitializeComponent();
			this.textPASS.AutoSize = false;
			this.textPASS.Size = new Size(this.textPASS.Width, 44); // присвоюємо ширину text box
		}

		private void button8_Click(object sender, EventArgs e)
		{
			Form Form3 = new FormCheckout();
			Form3.Show();
			Hide();
		}



		private void logButton_Click(object sender, EventArgs e)
		{
			CardService cardService = CardService.Instance;
			string      password    = textPASS.Text;
			if (password.Length != 4)
			{
				MessageBox.Show(
					"Введіть 4 цифри пінкода!",
					"Помилка в введені паролю",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning,
					MessageBoxDefaultButton.Button2
				);
				textPASS.Text = "";
				return;
			}

			countTries++;
			if (!cardService.CheckPassword(FormsService.ChosenCardNumber, password))
			{
				if (countTries < 3)
					MessageBox.Show(
						$"Пінкод неправильний!\nСпроб залишилось: {3 - countTries}",
						"Помилка в введені паролю",
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation,
						MessageBoxDefaultButton.Button2
					);
				else
				{
					var res = MessageBox.Show(
						"Пінкод неправильний! \nСпроб залишилось: 0. \nТікайте, поліцію викликано, а карту заблоковано!",
						"Помилка в введені паролю",
						MessageBoxButtons.OK,
						MessageBoxIcon.Exclamation,
						MessageBoxDefaultButton.Button2
					);
					if (res == DialogResult.OK)
						Application.Exit();
				}

				textPASS.Text = "";
				return;
			}

			this.Hide();
			FormCheckout formCheckout = new FormCheckout();
			formCheckout.Show();
		}
		


		private void textPASS_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;
			if (!char.IsDigit(number)&& number != 8)
				e.Handled = true;	//	перевіряемо чи був оброблений ввод с клавіатури
		}

	}
}