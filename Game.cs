using System;
using System.Collections.Generic;

namespace sushi_go{
    public class Game{
        public int round;
        public List<Player> players;
        public Deck newDeck;

        public Game(){
            round = 1;
            players = new List<Player>();
            newDeck = new Deck();
        }
        public void startGame(){
            getPlayers();
            while(round < 4){
                System.Console.WriteLine($"Round {round}!");
                makeHands();
                Turn();
            }
            calcPudding();
            checkWinner();
            System.Console.WriteLine("Play again? (type yes)");
            string again = Console.ReadLine();
            if(again == "yes") {
                round = 1;
                startGame();
                players.Clear();
                newDeck = new Deck();
            }
        }

        public void getPlayers(){
            System.Console.WriteLine("Enter number of players (2-5)");
            string gNum = Console.ReadLine();
            int number = 0;
            bool result = Int32.TryParse(gNum, out number);
            while(!result || number < 2 || number > 5) {
                System.Console.WriteLine("I SAID 2 TO 5!!!!!");
                gNum = Console.ReadLine();
                result = Int32.TryParse(gNum, out number);
            }
            for(int i = 0; i < number; i++) {
                System.Console.WriteLine($"Enter Player {i+1} name (min 2 char)");
                string pName = Console.ReadLine();
                while (pName.Length < 2){
                    System.Console.WriteLine($"I SAID MIN 2!!!!");
                    pName = Console.ReadLine();
                }
                Player p = new Player(pName);
                players.Add(p);
            }
        }
        public void makeHands(){
            if (players.Count == 2){
                foreach( Player player in players){
                    for(int i = 0; i < 10; i++){
                        player.Hand.Add(newDeck.Deal());
                    }
                }
            }
            else if (players.Count == 3){
                foreach( Player player in players){
                    for(int i = 0; i < 9; i++){
                        player.Hand.Add(newDeck.Deal());
                    }
                }
            }
            else if (players.Count == 4){
                foreach( Player player in players){
                    for(int i = 0; i < 8; i++){
                        player.Hand.Add(newDeck.Deal());
                    }
                }
            }
            else if (players.Count == 5){
                foreach( Player player in players){
                    for(int i = 0; i < 7; i++){
                        player.Hand.Add(newDeck.Deal());
                    }
                }
            }
        }
        public void Turn() {
            while(players[0].Hand.Count != 0){
                for(int i = 0; i < players.Count; i++){
                    System.Console.WriteLine($"Player {i + 1}'s turn.");
                    if(players[i].Keep.Exists(x => x.cardName == "Chopsticks")){
                        Card temp = players[i].Keep[players[i].Keep.Count-1];
                        players[i].Keep.Remove(temp);
                        players[i].showHand();
                        players[i].chooseCard();
                        players[i].Hand.Add(temp);
                    }
                    players[i].showHand();
                    players[i].chooseCard();
                }
                foreach(Player player in players){
                    player.newKeep();
                }
                for(int k = 0; k < players.Count; k++){
                    List<Card> temp = players[k].Hand;
                    players[k].Hand = players[(k+1) % players.Count].Hand;
                    players[(k+1) % players.Count].Hand = temp;
                }
            }
            foreach(Player player in players){
                calcScore(player);
            }
            calcMaki();
            foreach(Player player in players){
                player.Keep.Clear();
            }
            
            round += 1;
            
        }
        public int calcScore(Player player){
            int tempura = 0;
            int sashimi = 0;
            int dumpling = 0;
            int maki = 0;
            int salmon = 0;
            int squid = 0;
            int egg = 0;
            foreach(Card card in player.Keep){
                if(card.cardName == "Tempura"){
                    tempura += 1;
                }
                else if(card.cardName == "Sashimi"){
                    sashimi += 1;
                }
                else if(card.cardName == "Dumpling"){
                    dumpling += 1;
                }
                else if(card.cardName == "2 Maki rolls"){
                    maki += 2;
                }
                else if(card.cardName == "3 Maki rolls"){
                    maki += 3;
                }
                else if(card.cardName == "1 Maki rolls"){
                    maki += 1;
                }
                else if(card.cardName == "Salmon Nigiri"){
                    salmon += 1;
                }
                else if(card.cardName == "Squid Nigiri"){
                    squid += 1;
                }
                else if(card.cardName == "Egg Nigiri"){
                    egg += 1;
                }
                else if(card.cardName == "Pudding"){
                    player.pudding += 1;
                }
            }
            player.score += tempura/2 * 5;
            player.score += sashimi/3 * 10;
            player.score += salmon * 2;
            player.score += squid * 3;
            player.score += egg;
            player.maki = maki;
            if(dumpling == 1){
                player.score += 1;
            }
            else if(dumpling == 2){
                player.score += 3;
            }
            else if(dumpling == 3){
                player.score += 6;
            }
            else if(dumpling == 4){
                player.score += 10;
            }
            else if(dumpling == 5){
                player.score += 15;
            }
            return player.score;
        }
        public void calcMaki(){
            List<Player> temp = new List<Player>();
            List<Player> temp2 = new List<Player>();
            List<Player> most = new List<Player>();
            List<Player> second = new List<Player>();
            int max = 0;
            int secondMax = 0;
            foreach(Player player in players){
                temp.Add(player);
            }
            foreach(Player player in temp){
                if( max < player.maki){
                    max = player.maki;
                }
            }
            foreach(Player player in temp){
                if(player.maki == max){
                    most.Add(player);
                }
                else{
                    temp2.Add(player);
                }
            }
            foreach(Player player in most){
                player.score += 6 / most.Count;
            }
            foreach(Player player in temp2){
                if( secondMax < player.maki){
                    secondMax = player.maki;
                }
            }
            foreach(Player player in temp2){
                if(player.maki == secondMax){
                    second.Add(player);
                    temp.Remove(player);
                }
            }
            foreach(Player player in second){
                player.score += 3 / most.Count;
            }
            foreach(Player player in players){
                player.maki = 0;
                System.Console.WriteLine($"{player.playerName} scored {player.score}!!!!");
            }
        }
        public void calcPudding(){
            List<Player> temp = new List<Player>();
            List<Player> temp2 = new List<Player>();
            List<Player> most = new List<Player>();
            List<Player> least = new List<Player>();
            int max = players[0].pudding;
            int last = players[0].pudding;
            foreach(Player player in players){
                temp.Add(player);
            }
            foreach(Player player in temp){
                if( max < player.pudding){
                    max = player.pudding;
                }
            }
            foreach(Player player in temp){
                if(player.pudding == max){
                    most.Add(player);
                }
                else{
                    temp2.Add(player);
                }
            }
            foreach(Player player in most){
                player.score += 6 / most.Count;
                System.Console.WriteLine($"{player.playerName} got the most pudding! {6/ most.Count} added!!!!");
            }
            foreach(Player player in temp2){
                if( last < player.pudding){
                    last = player.pudding;
                }
            }
            foreach(Player player in temp2){
                if(player.pudding == last){
                    least.Add(player);
                    temp.Remove(player);
                }
            }
            foreach(Player player in least){
                player.score -= 6 / most.Count;
                System.Console.WriteLine($"{player.playerName} got the least pudding! {6/ most.Count} deducted!!!!");
            }
        }
        public void checkWinner() {
            int winningScore = 0;
            List<Player> winners = new List<Player>();
            foreach(Player player in players){
                if( winningScore < player.score){
                    winningScore = player.score;
                }
            }
            foreach(Player player in players){
                if(player.score == winningScore){
                    winners.Add(player);
                }
            }
            if(winners.Count == 1) {
                System.Console.WriteLine($"{winners[0].playerName} won the game with {winners[0].score} points.");
            }else {
                System.Console.WriteLine($"These players tied for first place with {winningScore} ponts!");
                foreach(Player player in winners) {
                    System.Console.Write($"{player.playerName} | ");
                }
            }
        }
    }    
}