using System;
using System.Collections.Generic;
using System.Text;

namespace tik_tak_toe
{
    class GameManager
    {
        public BigGrid BigGrid = new BigGrid();
        Parser.Tokenizer tokenizer = new Parser.Tokenizer();
        List<Cell> cells = new List<Cell>();
        List<SmallGrid> smallGrids = new List<SmallGrid>();
        public char winner;
        public int Xwinnings = 0;
        public int Owinnings = 0;
        public void RecordMoves(string moves)
        {
            int i = 0;
            int j = 0;
            tokenizer.SeparateMoves(moves);//seperate the moves entered from the users
            tokenizer.Tokenize();//Assign these moves to a Cell
            this.cells = tokenizer.cells;//gets the cells inside the tokenizer

            //the following logic is to occupy each cell from tokenizer once for player x and the other for player y
            if (tokenizer.moves.Count % 2 != 0)
            {
                j = 1;
            }
            while (i < tokenizer.cells.Count)
            {
                Cell cell = tokenizer.cells[i];
                if ((i + j) % 2 == 0)
                {
                    BigGrid.Occupy(cell.x, cell.y, cell.SubX, cell.SubY, 'O');//occupies the empty cells with a player x
                    this.cells[i].player = 'O';
                }
                else
                {
                    BigGrid.Occupy(cell.x, cell.y, cell.SubX, cell.SubY, 'X');//occupies the empty cells with a player y
                    this.cells[i].player = 'X';
                }
                CheckWinnerCells();
                i++;
            }
        }

        public void CheckWinnerCells()
        {
            foreach (var grid in BigGrid.grids)
            {
                foreach (var cell in grid.grid)
                {
                    bool winner = false;
                    if ((cell.player == 'X' || cell.player == 'O'))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (grid.grid[cell.SubX, i].player != cell.player)
                                break;
                            if (i == 2)
                            {
                                winner = true;

                            }
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            if (grid.grid[i, cell.SubY].player != cell.player)
                                break;
                            if (i == 2)
                            {
                                winner = true;
                            }
                        }
                        if (cell.SubX == cell.SubY)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (grid.grid[i, i].player != cell.player)
                                {
                                    break;
                                }
                                if (i == 2)
                                {
                                    winner = true;
                                }
                            }
                        }
                        if (cell.SubX + cell.SubY == 2)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (grid.grid[i, 2 - i].player != cell.player)
                                {
                                    break;
                                }
                                if (i == 2)
                                {
                                    winner = true;
                                }
                            }
                        }
                    }
                    if (winner)
                    {
                        grid.player = cell.player;
                        grid.won = true;

                    }
                }
            }
        }

        public void CheckWinnerGrids()
        {
            
            char player = ' ';
            bool winner = false;
            foreach (var smallGrid in BigGrid.grids)
            {
                winner = false;
                if ((smallGrid.player == 'X' || smallGrid.player == 'O'))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (BigGrid.grids[smallGrid.x, i].player != smallGrid.player)
                            break;
                        if (i == 2)
                        {
                            winner = true;
                            player = smallGrid.player;
                        }
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        if (BigGrid.grids[i, smallGrid.y].player != smallGrid.player)
                            break;
                        if (i == 2)
                        {
                            winner = true;
                            player = smallGrid.player;
                        }
                    }
                    if (smallGrid.x == smallGrid.y)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (BigGrid.grids[i, i].player != smallGrid.player)
                            {
                                break;
                            }
                            if (i == 2)
                            {
                                winner = true;
                                player = smallGrid.player;

                            }
                        }
                    }
                    if (smallGrid.x + smallGrid.y == 2)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (BigGrid.grids[i, 2 - i].player != smallGrid.player)
                            {
                                break;
                            }
                            if (i == 2)
                            {
                                winner = true;
                                player = smallGrid.player;
                                
                            }
                        }
                    }

                }
                if (winner)
                {
                    smallGrids.Add(smallGrid);
                    this.winner = player;

                }
            }
        }

        public void CheckWinnerFinal()
        {
            List<string> winningGrids = new List<string>();

            CheckWinnerCells();
            CheckWinnerGrids();
            this.CountWinnings();
            foreach (var cell in this.cells)
            {
                //display the first line : the winning grids
                if (cell.player == this.winner && BigGrid.grids[cell.x, cell.y].won)
                    if (!winningGrids.Contains(cell.Path[0]))//this is to prevent repeating the grids
                    {                       
                        winningGrids.Add(cell.Path[0]);
                        Console.Write(cell.Path[0]+" ");
                    }
                        
            }
            Console.WriteLine("");

            //display the winning moves
            foreach (var cell in this.cells) {
                if (cell.player == this.winner&& BigGrid.grids[cell.x, cell.y].won)
                    Console.Write(cell.Path[0]+"."+cell.Path[1]+", ");
            }

            //display the number of winnings for each player
            if (this.winner == 'X')
            {
                Console.WriteLine("");
                Console.Write(1 + "." + Xwinnings + ", ");
                Console.Write(0 + "." + Owinnings);
            }
            else if(this.winner == 'O')
            {
                Console.WriteLine("");
                Console.Write(0 + "." + Xwinnings + ", ");
                Console.Write(1 + "." + Owinnings);

            }
        }

        void CountWinnings()
        {
            foreach (var grid in BigGrid.grids)
            {
                if (grid.player == 'X')
                    Xwinnings++;
                if (grid.player == 'O')
                    Owinnings++;

            }

        }
        
    }
}
