using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApp1.Models {
    class Card {
        public string CardNumber { get; set; }
        public string PhoneNumber { get; set; }
        public float Balance { get; set; }
        public string Password { get; set; }
        public int CardType;
        
        [JsonIgnore] // Щоб всередині json не були ці picturebox'и, інформацію про них же нам не треба зберігати, тільки про карту
        public PictureBox cardBox; // PictureBox для відображення на формі, всередині класу Card, щоб було по-ООПшному,
                                   // і бо так зрозуміліше, аніж якось потім ці picturebox до конкретних карт підв'язувати по якомусь полю 
                                   
        public Card() : this(0, null, 0) { }
        public Card(float balance, int cardType) : this(balance, null, cardType) { }

        public Card(float balance, PictureBox pictureBox, int cardType = 1) {
            this.Balance = balance;
            cardBox = pictureBox;
            this.CardType = cardType;
        }

        public void Withdraw(float amount) {
            if (amount > Balance)
                throw new ArgumentOutOfRangeException("Не достатньо коштів!");
            Balance -= amount;
        }

    }
}
