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
        [SerializeField] private int sizeX;
        [SerializeField] private int sizeZ;

        // Start is called before the first frame update
        void Start()
        {
            grid = new Node[sizeX,sizeZ];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeZ; j++)
                {
                    grid[i, j].positionX = i;
                    grid[i, j].positionY = j;
                    grid[i, j].blocked = false;
                }
            }

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
                for (int j = 0; j < sizeZ; j++)
                {
                    if (Physics.CheckBox(new Vector3((transform.position.x - sizeX/2) + i, 0, (transform.position.z - sizeZ/2) + j), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
                    {
                        grid[i, j].blocked = true;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeZ; j++)
                {
                    if (grid[i,j] != null || grid[i,j].blocked)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3((transform.position.x - sizeX/2) + i,0,(transform.position.z - sizeZ/2) + j), Vector3.one);
                    }
                }
            }
        }
    }
}
