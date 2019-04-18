using System;
using UnityEngine;

namespace AStar
{
    
    public class Node
    {
        public Vector2 position;
        public bool blocked;
        public float gCost;
        public float hCost;
        public float fCost;

        public Node parent;
    }
}
