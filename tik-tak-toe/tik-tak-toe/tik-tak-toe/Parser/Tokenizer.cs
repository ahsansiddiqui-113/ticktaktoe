using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tik_tak_toe.Parser
{
    public class Tokenizer
    {
        public List<string> moves = new List<string>();
        public List<Cell> cells = new List<Cell>();

        Dictionary<char, int> CordsX = new Dictionary<char, int> { { 'S', 0 }, { 'C', 1 }, { 'N', 2 } };//{ { 'S', 0 }, { 'C', 1 }, { 'N', 2 } };
        Dictionary<char, int> CordsY = new Dictionary<char, int> { { 'W', 0 }, { 'C', 1 }, { 'E', 2 } };
        Dictionary<char, char> CordsXR = new Dictionary<char, char> { {'0', 'S' }, { '1','C' }, { '2','N' } };//{ { 'S', 0 }, { 'C', 1 }, { 'N', 2 } };
        Dictionary<char, char> CordsYR = new Dictionary<char, char> { { '0','W' }, { '1','C'}, { '2','E' } };
        public void SeparateMoves(string moves)
        {
            string mover = "";
            //check if a character is not a , and if it's a comma it will record it as a new move
            foreach (var i in moves)
            {
                if(i == ',')
                {
                    this.moves.Add(mover);//adds the move to list
                    mover = "";
                }
                else
                {
                    mover += i;
                    
                }
                
            }
            if (mover.Length == 5)
            {
                this.moves.Add(mover);//adds the move to list
            }
        }
        public void Tokenize()
        {
            foreach (var move in this.moves)
            {
                //take each move and make a cell and adds it to the list
                int dCordX, dCordY, CordX, CordY;
                string[] subMoves = move.Split(".");
                Cell cell = new Cell();
                if (subMoves.Count() == 2)
                {
                    try
                    {
                        var bigMove = subMoves[0];
                        CordX = CordsX[bigMove[0]];
                        CordY = CordsY[bigMove[1]];
                        var smallMove = subMoves[1];
                        dCordX = CordsX[smallMove[0]];
                        dCordY = CordsY[smallMove[1]];
                        cell = new Cell(CordX, CordY, dCordX, dCordY, subMoves);
                        this.cells.Add(cell);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid Input : " + move);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input :" + move);
                }
            }
        }

        public char[,] Deparse(string OuterPath, string InnerPath)
        {
            //a quick utility to switch from coordinate based to N-S-E-W based location
            char[,] path = new char[2, 2];

                path[0,0] = CordsXR[OuterPath[0]]; //for x of the grid
                path[0,1] = CordsYR[OuterPath[1]]; //for y of the grid

                path[1,0] = CordsXR[InnerPath[0]]; //for x of the cell
                path[1,1] = CordsYR[InnerPath[1]]; //for y of the cell
            return path;
        }

        public char[] Deparse(string OuterPath)
        {
            //a quick utility to switch from coordinate based to N-S-E-W based location
            char[] path = new char[2];

            path[0] = CordsXR[OuterPath[0]]; //for x of the grid
            path[1] = CordsYR[OuterPath[1]]; //for y of the grid

                return path;
        }
    }
}
