using System;
using System.Collections.Generic;

namespace sushi_go{
    public class Deck{
        public List<Card> cards;
        public Deck() {
            Reset();
        }
        public Deck Reset() {
            cards = new List<Card>();
            string[] types = new string[11] {"Tempura", "Sashimi", "Dumpling", "2 Maki rolls", "3 Maki rolls", "1 Maki rolls", "Salmon Nigiri", "Squid Nigiri", "Egg Nigiri", "Pudding", "Chopsticks"};
            string[] descriptionList = new string[11] {"*2 = 5pt", "*3 = 10pt", "1 3 6 10 15", "Most 6/3", "Most 6/3", "Most 6/3", "2", "3", "1", "Most 6 / Least -6", "Swap for 2"};
            int[] numOfCards = new int[11] {14, 14, 14, 14, 10, 8, 10, 5, 5, 10, 4};
            int i = 0;
            while ( i < types.Length){
                i++;
                for ( int k = 0 ; k < numOfCards[i-1]; k++){
                    // System.Console.WriteLine($"i: {i}: j: {k} num[j]: {numOfCards[k]}");
                    cards.Add(new Card(types[i-1], descriptionList[i-1]));
                }
            }
            Shuffle(); 
            return this;
        }

        public Deck Shuffle() {
            Random rand = new Random();
            for(int idx = cards.Count -1; idx > 0; idx--){
                int randIdx = rand.Next(idx);
                Card temp = cards[randIdx];
                cards[randIdx] = cards[idx];
                cards[idx] = temp;
            }
            return this;
        }
        public Card Deal() {
            if(cards.Count > 0){
                Card temp = cards[0];
                cards.RemoveAt(0);
                return temp;
            }
            System.Console.WriteLine("Out of cards");
            return null;
        }
    }
}