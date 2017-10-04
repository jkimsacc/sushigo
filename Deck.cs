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
            string[] types = new string[11] {"[Tempura", "[Sashimi", "[Dumpling", "[2 Maki rolls", "[3 Maki rolls", "[1 Maki rolls", "[Salmon Nigiri", "[Squid Nigiri", "[Egg Nigiri", "[Pudding", "[Chopsticks"};
            string[] descriptionList = new string[11] {"*2 = 5pt]", "*3 = 10pt]", "1 3 6 10 15]", "Most 6/3]", "Most 6/3]", "Most 6/3]", "2]", "3]", "1]", "Most 6 / Least -6]", "Swap for 2]"};
            int[] numOfCards = new int[11] {14, 14, 14, 12, 8, 6, 10, 5, 5, 10, 4};
            for ( int i = 0 ; i < types.Length; i++){
                for ( int j = 0 ; j < numOfCards[j]; j++){
                    cards.Add(new Card(types[i], descriptionList[i]));
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
    }
}