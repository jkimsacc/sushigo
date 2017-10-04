using System;

namespace sushi_go
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck newDeck = new Deck();
            for(int i = 0; i <  newDeck.cards.Count; i++){
                System.Console.WriteLine(newDeck.cards[i]);
            }
        }
    }
}
