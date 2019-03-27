using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class AStar : MonoBehaviour
    {
        private Node[] closed;
        private Node[] open;

        private Node lowest;
        private Node target;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void CheckBestOpen()
        {
            for (int i = 0; i > open.Length; i++)
            {
                if (open[i].fCost < lowest.fCost) lowest = open[i];
            }
            //CheckOpenGrid();
        }
/*
        void CheckOpenGrid()
        {
            for (int i = lowest.positionX - 1; i > lowest.positionX + 1; i++)
            {
                for (int j = lowest.positionY - 1; j > lowest.positionY + 1; i++)
                {
                    if (GridGenerator.grid[i, j] != target)
                    {
                        float temp = 0;
                        if (i == lowest.positionX && j == lowest.positionY)
                        {
                            
                        }
                        else if (i != lowest.positionX && j != lowest.positionY)
                        {
                             temp = 1.4f + lowest.gCost + GridGenerator.grid[i, j].hCost;
                        }
                        else
                        {
                             temp = 1 + lowest.gCost + GridGenerator.grid[i, j].hCost;
                        }
                        if (GridGenerator.grid[i, j].fCost == 0 || GridGenerator.grid[i, j].fCost > temp)
                        {
                            GridGenerator.grid[i, j].fCost = temp;
                            GridGenerator.grid[i, j].gCost = 1 + lowest.gCost;
                            GridGenerator.grid[i, j].parent = lowest;
                        }
                    }
                }
            }
        }
        */
    }
}
