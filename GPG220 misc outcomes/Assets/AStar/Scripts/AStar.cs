using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class AStar : MonoBehaviour
    {
        private List<Node> closed;

        public Vector2 end;

        private Node lowest;
        private Node[,] map;
        private List<Node> open;
        private List<Node> path;
        private Vector2 size;

        public Vector2 start;
        public Node newLowest;
        public bool completed;

        // Start is called before the first frame update
        private void Start()
        {
            open = new List<Node>();
            closed = new List<Node>();
            path = new List<Node>();
            map = GetComponent<GridGenerator>().grid;
            size = new Vector2(GetComponent<GridGenerator>().sizeX, GetComponent<GridGenerator>().sizeY);
            
        }

        private void SetUp()
        {
            completed = false;
            open = new List<Node>();
            closed = new List<Node>();
            path = new List<Node>();
            start = new Vector2((int)Random.Range(0, size.x), (int)Random.Range(0, size.y));
            end = new Vector2((int)Random.Range(0, size.x), (int)Random.Range(0, size.y));
            //new Vector2((int)Random.Range(0, size.x), (int)Random.Range(0, size.y));
            foreach (var maps in map)
            {
                maps.hCost = Vector2.Distance(maps.position, end);
                maps.fCost = size.x * 3;
            }

            open.Add(map[(int) start.x, (int) start.y]);
            lowest = map[(int) start.x, (int) start.y];
            map[(int) start.x, (int) start.y].gCost = 0;
            map[(int) start.x, (int) start.y].fCost = 0;
            NewPaths();
        }

        // Update is called once per frame
        private void Update()
        {
            if (completed)
            {
                completion();
            }
            else
            {
                if (Input.GetMouseButtonDown(1)) CyclePath();
                if (Input.GetMouseButton(2)) CyclePath();
                if (Input.GetMouseButton(2)) CyclePath();
                if (Input.GetMouseButton(2)) CyclePath();
                if (Input.GetMouseButton(2)) CyclePath();
                if (Input.GetMouseButton(2)) CyclePath();
            }

            if (Input.GetMouseButtonDown(0)) SetUp();
        }

        private void CyclePath()
        {
            if (completed)
            {
                completion();
                return;
            }
            newLowest = new Node();
            newLowest.fCost = 0;
            foreach (var testNode in open)
            {
                var temp = testNode.gCost + testNode.hCost;
                if (temp < testNode.fCost)
                {
                    testNode.fCost = temp;
                    //testNode.parent = lowest;
                }

                if (newLowest.fCost > testNode.fCost || newLowest.fCost == 0)
                {
                    newLowest = testNode;
                }
            }
            lowest = newLowest;
            if (lowest.position == end)
            {
                completed = true;
            }
            else
            {
                NewPaths();
            }
        }

        
        private void NewPaths()
        {
            //Debug.Log("new paths");
            for (var i = (int) lowest.position.x - 1; i < lowest.position.x + 2; i++)
            {
                for (var j = (int) lowest.position.y - 1; j < lowest.position.y + 2; j++)
                {
                    if (i < 0 || i >= size.x || j < 0 || j >= size.y) continue;
                    if (map[i,j] == lowest) continue;
                    if (open.Contains(map[i, j]))
                    {
                        if (map[i, j].gCost >= lowest.gCost + Vector2.Distance(map[i,j].position, lowest.position))
                        {
                            map[i, j].gCost = lowest.gCost + Vector2.Distance(map[i,j].position, lowest.position);
                            map[i,j].parent = lowest;
                        }
                    }
                    else if (closed.Contains(map[i,j]))
                    {
                        if (map[i, j].gCost  >= lowest.gCost + Vector2.Distance(map[i,j].position, lowest.position))
                        {
                            lowest.gCost = lowest.gCost + Vector2.Distance(map[i,j].position, lowest.position);
                            lowest.parent = map[i, j];
                            closed.Remove(map[i, j]);
                            open.Add(map[i,j]);
                        } 
                    }
                    else if (!map[i, j].blocked && !closed.Contains(map[i,j]))
                    {
                        map[i, j].parent = lowest;
                        map[i, j].gCost = lowest.gCost + Vector2.Distance(map[i,j].position, lowest.position);
                        open.Add(map[i, j]);
                    }
                }
            }
            closed.Add(map[(int)lowest.position.x,(int)lowest.position.y]);
            open.Remove(map[(int)lowest.position.x,(int)lowest.position.y]);
        }

        void completion()
        {
            if (lowest == null) return;
            Node temp = lowest;
            
                if (closed.Contains(map[(int) temp.position.x, (int) temp.position.y]))
                    closed.Remove(map[(int) temp.position.x, (int) temp.position.y]);
                if (open.Contains(map[(int)temp.position.x,(int)temp.position.y]))open.Remove(map[(int) temp.position.x, (int) temp.position.y]);
                path.Add(map[(int)temp.position.x,(int)temp.position.y]);
                lowest = temp.parent;
        }
        private void CheckBestOpen()
        {
            for (var i = 0; i > open.Count; i++)
                if (open[i].fCost < lowest.fCost)
                    lowest = open[i];
            //CheckOpenGrid();
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            Gizmos.color = Color.green;
            foreach (var VARIABLE in closed)
            {
                Gizmos.DrawCube(new Vector3(VARIABLE.position.x, -0.45f, VARIABLE.position.y),
                    Vector3.one);
            }
            Gizmos.color = Color.blue;
            foreach (var VARIABLE in open)
            {
                Gizmos.DrawCube(new Vector3(VARIABLE.position.x, -0.45f, VARIABLE.position.y),
                    Vector3.one);
            }
            Gizmos.color = Color.cyan;
            foreach (var VARIABLE in path)
            {
                Gizmos.DrawCube(new Vector3(VARIABLE.position.x, -0.45f, VARIABLE.position.y),
                    Vector3.one);
            }
            Gizmos.color = Color.black;
            Gizmos.DrawCube(new Vector3(start.x, -0.45f, start.y),
                Vector3.one);
            Gizmos.DrawCube(new Vector3(end.x, -0.45f, end.y),
                Vector3.one);
        }

//        void CheckOpenGrid()
//        {
//            for (int i = (int)lowest.position.x - 1; i > lowest.position.x + 1; i++)
//            {
//                for (int j = (int)lowest.position.y - 1; j > lowest.position.y + 1; i++)
//                {
//                    if (map[i, j] != target)
//                    {
//                        float temp = 0;
//                        if (i == lowest.position.x && j == lowest.position.y)
//                        {
//
//                        }
//                        else if (i != lowest.position.x && j != lowest.position.y)
//                        {
//                            temp = 1.5f + lowest.gCost + GridGenerator.grid[i, j].hCost;
//                        }
//                        else
//                        {
//                            temp = 1 + lowest.gCost + GridGenerator.grid[i, j].hCost;
//                        }
//
//                        if (GridGenerator.grid[i, j].fCost == 0 || GridGenerator.grid[i, j].fCost > temp)
//                        {
//                            map[i, j].fCost = temp;
//                            map[i, j].gCost = 1 + lowest.gCost;
//                            map[i, j].parent = lowest;
//                        }
//                    }
//                }
//            }
//        }
    }
}