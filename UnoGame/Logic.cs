using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoGame
{
    internal class Logic
    {
        public Logic()
        {

        }

        public void setupCards()
        {
            Card[] cards = new Card[108];

            string[] colors = { "blue", "green", "yellow", "red" };

            int i = 0;
            foreach (var color in colors)
            {
                //Setup 0s
                cards[i].color = color;
                cards[i].number = 0;
                i++;

                for(int j = 1; j < 10; i++)
                {
                    cards[i].color = color;
                    cards[i].number = j;
                    i++;
                }
            }

            foreach (var element in cards)
            {
                Console.WriteLine(element);
                
            }
            Console.ReadKey();
        }
    }
}
