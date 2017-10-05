using System;

namespace sushi_go
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            // System.Console.WriteLine(deck.cards.Count);
            Game newGame = new Game();
            newGame.startGame();
        }
    }
}
