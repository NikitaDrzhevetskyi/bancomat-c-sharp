using System;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace WindowsFormsApp1
{
	public partial class Form6 : Form
	{
		public Form6()
		{
			InitializeComponent();
		}

		private Point AnimationEndPoint;
		private int animationLen = 150, step = 2;
		Timer animationTimer;


		private void Form6_Load(object sender, EventArgs e)
		{
			animationTimer = new Timer(100);
			animationTimer.SynchronizingObject = this;	
			animationTimer.Elapsed += AnimationTick; // функция, которая срабатывает каждые 100мс
			animationTimer.Start();
			// Указываем где должна будет остановиться панелька. По Х так же, а по У будет больше(ниже)
			AnimationEndPoint = new Point(panelMoney.Location.X, panelMoney.Location.Y + animationLen); 
		}

		private void AnimationTick(object sender, ElapsedEventArgs e)
		{
			var prevPoint = panelMoney.Location; 
			panelMoney.Location = new Point(prevPoint.X, prevPoint.Y + step); // Перемещаем панель с деньгами на {step} ниже 
			
			if(panelMoney.Location.Y >= AnimationEndPoint.Y) { // Если по У ушло ниже/на уровне конечной позиции - останов очка
				(sender as Timer).Stop();
				switch (MessageBox.Show("Ваш сеанс закінчено, бажаєте здійснити ще одну операцію?", "Сеанс закінчено", 
						MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly,
					false)) // MessageBox на выходе возвращает enum DialogResult, кнопочку, которую тыкнул пользователь
				{			// Соответственно, если он нажал да, хочу ещё снять деньги - то переадресовываем его на старт
					case DialogResult.Yes:
						FormStart formStart = new FormStart();
						this.Visible = false;
						formStart.Show();
						formStart.Focus();
						break;
					default:				// Ну а если он нажал нет, или закрыл окно - то выходим из программы и усё
						Application.Exit();
						break;
				}
			}
		}
	}
}