using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public Vector2Int GridPos;
    public bool IsUnderConstruction;
    public List<Node> InternalNodes = new List<Node>();

    public virtual void Initialize(Vector2Int pos)
    {
        GridPos = pos;
        foreach (Node node in InternalNodes) node.ParentTile = this;
    }

    public abstract void RefreshConnections(Dictionary<Vector2Int, BaseTile> grid);

    public virtual bool CanEnter() => !IsUnderConstruction;
}
