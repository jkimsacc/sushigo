using System;
using System.Collections.Generic;

namespace sushi_go{
    public class Player{
        public string playerName;
        public List<Card> Hand;
        public List<Card> Keep;
        public int score;
        public int maki;
        public int pudding;

        public Player(string name){
            playerName = name;
            Hand = new List<Card>();
            Keep = new List<Card>();
            maki = 0;
            pudding = 0;
            score = 0;
        }
        public void showHand(){
            System.Console.WriteLine($"{playerName}\'s cards are:");
            for(int i = 0; i < Hand.Count; i ++){
                System.Console.WriteLine($"{i}: {Hand[i]}");
            }
        }

        public void newKeep() {
            Console.WriteLine($"{playerName} chose {Keep[Keep.Count-1]}");
        }

        public void chooseCard(){
            System.Console.WriteLine("Choose card #");
            string gNum = Console.ReadLine();
            int number = 0;
            bool result = Int32.TryParse(gNum, out number);
            while(!result || number > Hand.Count) {
                System.Console.WriteLine("Choose a card by typing a number.");
                gNum = Console.ReadLine();
                result = Int32.TryParse(gNum, out number);
            }
            number = 0;
            Card pick = Hand[number];
            Hand.RemoveAt(number);
            Keep.Add(pick);
        }


    }
}