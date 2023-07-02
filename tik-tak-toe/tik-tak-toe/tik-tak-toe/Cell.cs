using System;
using System.Collections.Generic;
using System.Text;

namespace tik_tak_toe
{
   public class Cell
    {
        public bool is_occupied=false;
        public int x, SubX;
        public int y, SubY;
        public char player='E';// 'E' stands for Empty meaning not played
        public string[] Path = new string[2];// Holds the exact path of the cell as it was inputted : NW.CE for example
        Parser.Tokenizer tokenizer=new Parser.Tokenizer();
        public Cell(int x, int y,int subx, int suby, string[] path)
        {
            this.x = x;
            this.y = y;
            this.SubX = subx;
            this.SubY = suby;
            this.Path = path;
        }
        public Cell(int x, int y, int subx, int suby)
        {
            this.x = x;
            this.y = y;
            this.SubX = subx;
            this.SubY = suby;
        }
        public Cell() { }

        public void Occupy(char player)
        {
            if (!this.is_occupied)
            {
                this.is_occupied = true;
                this.player = player;
            }
            else
            {
                Console.WriteLine("Cell is occupied already : " + this.Path[0]+"."+this.Path[1]);
                Environment.Exit(1);
            }
        }

    }
}
