using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoGame
{
    public class Card
    {
        string color = "";
        string symbol = "";


        public Card(string color, string symbol)
        {
            this.color = color;
            this.symbol = symbol;
        }


        public string GetColor()
        {
            return this.color;
        }

        public void SetColor(string color)
        {
            this.color = color;
        }


        public string GetSymbol()
        {
            return this.symbol;
        }

        public void SetSymbol(string symbol)
        {
            this.symbol = symbol;
        }
    }
}