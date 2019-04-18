using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class GridGenerator : MonoBehaviour
    {

        /*
         * have this grid as a personal grid
         * scan world from 0,0 check area and check for floor
         */

        public Node[,] grid;// make static?
        public int sizeX;
        public int sizeY;

        // Start is called before the first frame update
        void Awake()
        {
            grid = new Node[sizeX,sizeY];
            

            BuildGrid();
        }

        // Update is called once per frame
        void Update()
        {
            /*
             * starting at ownercreate it
             * moving out in each dirrection check (check if node exists at specified point)
             */
            
            if (Input.GetKeyDown(KeyCode.A)) BuildGrid();
        }

        void BuildGrid()
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    grid[i, j] = new Node();
                    grid[i,j].position = new Vector2(i,j);
                    if (Physics.CheckBox(new Vector3(i, 0,j), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
                    {
                        grid[i, j].blocked = true;
                        
                    }
                    else
                    {
                        grid[i, j].blocked = false;
                    }
                }
            }
            
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            for (int x = 0;x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (grid[x,y].blocked)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(grid[x,y].position.x,-0.45f, grid[x,y].position.y), Vector3.one );
                    }
                }
            }
        }
    }
}
