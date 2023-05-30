using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using WindowsFormsApp1.Services;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApp1.Models;
using Newtonsoft.Json;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1 {
    public partial class FormStart : Form {
        //  Location 300:0, Size 220:140
        Rectangle startRectangle = new Rectangle(300, 0, 220, 140);
        Point startDragging;
        static CardService cardService;
        static Random rnd;
        static PictureBox currentChoose;
        public static int cardTypes;
        
        public FormStart() {
            InitializeComponent();
            rnd = new Random((int)(DateTime.Now.Ticks));
            var dirPath = AppSettings.jsonDirPath;
            var fileName = AppSettings.jsonFileName;
            int startIndex = 0;
            PictureBox cardBox;
            cardService = CardService.Instance;
            cardService.LoadCards(dirPath + fileName);
            for (int i = 0; i < cardService.Count(); i++) {
                Rectangle current = startRectangle;
                current.X += (i * (500 / cardService.Count()));
                cardService.LocateCard(i, current);
                cardService.LoadCardImageFromFormatPath(i, AppSettings.cardImgFormat);
                cardBox = cardService.GetPicture(i);
                cardBox.MouseEnter += CardBox_MouseEnter;
                cardBox.MouseLeave += CardBox_MouseLeave;
                cardBox.MouseDown += CardBox_MouseDown;
                cardBox.MouseUp += CardBox_MouseUp;
                cardBox.MouseMove += CardBox_MouseMove;
                this.Controls.Add(cardBox);
                cardBox.BringToFront();
                //startIndex += startIndex == 0 ? Controls.GetChildIndex(cardBox) : 1;
                //this.Controls.SetChildIndex(cardBox, startIndex);
            }

        }
        private void FormStart_Load(object sender, EventArgs e)
        {
        }

        private void CardBox_MouseUp(object sender, MouseEventArgs e) {
            if (currentChoose.Bounds.IntersectsWith(Controls["cardInput"].Bounds)) {
                FormsService.ChosenCardNumber = cardService.GetCardByPicture(currentChoose).CardNumber;
                MoveToLogin();
            }
        }

        private void CardBox_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                startDragging = new Point(e.X, e.Y);
        }

        private void CardBox_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left)
                return;
            currentChoose.Top = currentChoose.Top + (e.Y - startDragging.Y);
            currentChoose.Left = currentChoose.Left + (e.X - startDragging.X);
        }

        public void MoveToLogin()
        {
            this.Visible = false;
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            formLogin.TopLevel = true;
        }

        private void CardBox_MouseLeave(object sender, EventArgs e) {
            PictureBox left = sender as PictureBox;
            var shifted = left.Location;
            shifted.Y -= 30;
            left.Location = shifted;
        }

        private void CardBox_MouseEnter(object sender, EventArgs e) {
            currentChoose = sender as PictureBox;
            var shifted = currentChoose.Location;
            shifted.Y += 30;
            currentChoose.Location = shifted;
        }

        private void no_cardButton_Click(object sender, EventArgs e) {
            FormNoCard form = new FormNoCard();
            form.Show();
            this.Visible = false;
            form.Focus();
        }

        private void button8_Click(object sender, EventArgs e) {
            List<Card> cards = new List<Card>();
            var dirPath = AppSettings.jsonDirPath;
            var fileName = AppSettings.jsonFileName;
            var typesCount = Directory.GetFiles("./Cards/").Length;
            cards.Add(new Card(364, rnd.Next(1, typesCount))    {CardNumber = "1234567890", PhoneNumber="+380637012889", Password = "1234" });
            cards.Add(new Card(100000, rnd.Next(1, typesCount)) { CardNumber = "6578493021", PhoneNumber = "+380660192339", Password = "1234" });
            cards.Add(new Card(3000, rnd.Next(1, typesCount))   { CardNumber = "0192837465", PhoneNumber = "+380972134977", Password = "1234" });
            cards.Add(new Card(5610, rnd.Next(1, typesCount))   { CardNumber = "1029384756", PhoneNumber = "+380123456789", Password = "1234" });
            var jsonData = JsonConvert.SerializeObject(cards, Formatting.Indented);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            File.WriteAllText(dirPath + fileName, jsonData, Encoding.UTF8);
        }
        // Створює базовий json файл, для подальшої роботи програми. 
        // Виконується "адміністратором" при першому заупуску програми, для "доступу" до бд з картками

        private void controlLeftPanel_Paint(object sender, PaintEventArgs e) {

        }

        //private void pictureBox3_Click(object sender, EventArgs e)
        //{
        //    currentObject = sender;
        //}

    }
}
