using System;
using System.Collections.Generic;
using System.Text;

namespace tik_tak_toe
{
    public class SmallGrid
    {
        public Cell[, ] grid=new Cell[3,3];
        public int x, y;
        public char player='E';// 'E' stands for Empty meaning not played
        public bool won;
        public char[] path = new char[2];// Holds the exact path of the grid as it was inputted : NW.CE for example
        Parser.Tokenizer tokenizer = new Parser.Tokenizer();
        public SmallGrid()
        {

        }
        public SmallGrid(int x, int y)
        {
            this.x = x;
            this.y = y;
            
            //create an empty gird of cells
            for(int i =0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    grid[i,j]= new Cell(this.x, this.y, i,j);
                }
            }
            this.path = tokenizer.Deparse(this.x + "" + this.y);
        }
        public bool is_occupy(int x, int y)
        {
            return grid[x, y].is_occupied;
        }
        public void Occupy(int subx, int suby, char player)
        {
            if(!this.won)
                grid[subx, suby].Occupy(player);

            else
            {
                Console.WriteLine("Grid is already won");
                Environment.Exit(1);
            }
            
        }
    }
}
