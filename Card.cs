using System;

namespace sushi_go{
    public class Card{
        public string cardName;
        public string description;
        public Card(string name, string desc){
        cardName = name;
        description = desc;
        }
        public override string ToString(){
            return $"{cardName} : {description}";
        }
    }
}