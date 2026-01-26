using System.Collections.Generic;
using UnityEngine;

public class RoadTile : BaseTile
{
    public List<Node> LaneA; //entry on nord exit on south
    public List<Node> LaneB; //entry on south exit on nord

    public override void RefreshConnections(Dictionary<Vector2Int, BaseTile> grid)
    {
        foreach(Node n in InternalNodes) n.Connections.Clear();


    }

    private void CheckAndConntect(Dictionary<Vector2Int, BaseTile> grid, Vector2Int dir, Node extitNode)
    {
        if(grid.TryGetValue(GridPos + dir, out BaseTile neighbour))
        {
            // filtrate and connect nodes
        }
    }
}
