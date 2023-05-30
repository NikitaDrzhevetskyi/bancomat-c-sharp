using System;
using System.Collections.Generic;
using WindowsFormsApp1.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1.Services {
    class CardService {
        private List<Card> _cards;

        private static CardService cardService;
        private CardService() {
            _cards = new List<Card>();
        }
        public static CardService Instance { // Сінглтон для того, щоб можна було працювати з одним об'єктом впродовж всієї програми 
            get {
                return cardService ?? (cardService = new CardService());
            }
        }
        public void LoadCards(string filePath) {
            if (File.Exists(filePath)) {
                string jsonData = File.ReadAllText(filePath);
                _cards = JsonConvert.DeserializeObject<List<Card>>(jsonData);
            }
        }

        public void SaveCards(string filePath)
        {
            string jsonDataText = JsonConvert.SerializeObject(_cards, Formatting.Indented); // Formatting.Indented для гарного вигляду json, а не в строчку
            File.WriteAllText(filePath, jsonDataText); 
        }
        
        
        public void LocateCard(int index, Rectangle rect) {
            Card card;
            card = _cards[index];
            if (card.cardBox == null)
                card.cardBox = new PictureBox();
            card.cardBox.Location = rect.Location;
            card.cardBox.Size = rect.Size;
        }
        public void LoadCardImageFromFormatPath(int index, string formatPath) {
            Card card = _cards[index];
            PictureBox pict = card.cardBox;
            pict.ImageLocation = formatPath.Replace("*", card.CardType.ToString());
        }
        public PictureBox GetPicture(int index) {
            PictureBox cardImg = _cards[index].cardBox;
            return cardImg;
        }
        public Card GetCardByPicture(PictureBox box) {
            foreach (Card card in _cards)
                if (box == card.cardBox)
                    return card;

            return null;
        }
        public bool CheckPassword(string cardNumber, string password) {
            foreach (var card in _cards)
                if (card.CardNumber == cardNumber)
                    return card.Password == password;

            return false;
        }
        public float GetBalanceByCardNumber(string cardNumber) {
            foreach (var card in _cards)
                if (card.CardNumber == cardNumber)
                    return card.Balance;

            return -1;
        }

        public string GetNumberByPhone(string phone)
        {
            foreach (var card in _cards)
                if (card.PhoneNumber == phone)
                    return card.CardNumber;
            throw new ArgumentException("Номер телефону не зареєстрований!");
        }
        public void WithdrawFromCard(string cardNumber, float amount) {
            foreach (var card in _cards) 
                if (card.CardNumber == cardNumber) {
                    card.Withdraw(amount);
                    return;
                }
            return;
        }

        public int Count() => _cards.Count;
    }
}
