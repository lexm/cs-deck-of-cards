using System;
using System.Collections.Generic;

namespace deckofcards
{
    class Program
    {
        public class Card
        {
            public string stringVal;
            public string suit;
            public int val;
            string[] cardNames = {"Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};
            string[] suitNames = {"Clubs", "Spades", "Hearts", "Diamonds"};
            public Card(int suitnum, int cardnum)
            {
                suit = suitNames[suitnum];
                val = cardnum + 1;
                stringVal = cardNames[cardnum];
            }

        }

        public class Deck
        {
            private static Random randy = new Random();
            public List<Card> cards = new List<Card>();
            public Deck()
            {
                for(int suitnum = 0; suitnum < 4; suitnum++)
                {
                    for(int cardnum = 0; cardnum < 13; cardnum++)
                    {
                        cards.Add(new Card(suitnum, cardnum));
                    }
                }
            }
            public Card Deal()
            {
                Card last = cards[0];
                cards.RemoveAt(0);
                return last;
            }
            public void Reset()
            {
                cards.Clear();
                for (int suitnum = 0; suitnum < 4; suitnum++)
                {
                    for (int cardnum = 0; cardnum < 13; cardnum++)
                    {
                        cards.Add(new Card(suitnum, cardnum));
                    }
                }
            }
            public void Shuffle()
            {
                int pos = cards.Count;
                while(pos > 1)
                {
                    pos--;
                    int rnd = randy.Next(pos + 1);
                    Card temp = cards[rnd];
                    cards[rnd] = cards[pos];
                    cards[pos] = temp;
                }
            }
        }

        public class Player
        {
            public string name;
            public List<Card> hand;
            public Player(string pname)
            {
                name = pname;
                hand = new List<Card>();
            }
            public Card Draw(Deck MyDeck)
            {
                Card drawnCard = MyDeck.Deal();
                hand.Add(drawnCard);
                return drawnCard;
            }
            public Card Discard(int index)
            {
                if(index < 0 || index > hand.Count) {
                    return null;
                } else {
                    Card temp = hand[index];
                    hand.RemoveAt(index);
                    return temp;
                }
            }
            public void ShowHand()
            {
                for(int i = 0; i < hand.Count; i++)
                {
                    System.Console.WriteLine("Card number {0}: {1} of {2}", (i + 1).ToString(), hand[i].stringVal, hand[i].suit);
                }
            }

        }
        static void Main(string[] args)
        {
            Deck mydeck = new Deck();
            Player player1 = new Player("Joe");
            mydeck.Shuffle();
            for(int i = 0; i < 5; i++)
            {
                player1.Draw(mydeck);
            }
            Card third = player1.Discard(2);
            System.Console.WriteLine(" Pulled card: {0} of {1}", third.stringVal, third.suit);
            System.Console.WriteLine("Rest of hand:");
            player1.ShowHand();
            while(player1.hand.Count > 0)
            {
                player1.Discard(0);
            }
            System.Console.WriteLine("Resetting...");
            mydeck.Reset();
            for (int i = 0; i < 5; i++)
            {
                player1.Draw(mydeck);
            }
            player1.ShowHand();
        }
    }
}
