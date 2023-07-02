using System;
using System.Collections.Generic;
using System.Text;

namespace tik_tak_toe
{
    class BigGrid
    {
        public SmallGrid[,] grids = new SmallGrid[3, 3];
        public BigGrid()
        
        {

            //create an empty grid of smaller grids
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grids[i, j] = new SmallGrid(i,j);
                }
            }
        }
        public void Occupy(int x, int y, int subx, int suby, char player)
        {

            grids[x, y].Occupy(subx, suby, player);

        }


    }
}
