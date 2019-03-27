using UnityEngine;

namespace AStar
{
    public class Node
    {
        public int positionX, positionY;
        public bool blocked;
        public float gCost;
        public float hCost;
        public float fCost;

        public Node parent;
    }
}
