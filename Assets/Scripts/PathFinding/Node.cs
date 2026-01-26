using Enums;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public NodeType Type;
    public BaseTile ParentTile;
    public List<Node> Connections = new List<Node>();

    public bool IsOccupied { get => _isOccupied; }
    private bool _isOccupied;
    public Car CurrentCar { get => _currentCar; }
    private Car _currentCar;

    public float gScore;
    public float hScore;

    public float FinalScore() => gScore + hScore;

    public void SetIsOccupied(bool value) => _isOccupied = value;

    public void OnDrawGizmos()
    {
        Gizmos.color = _isOccupied ? Color.red : Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
        foreach (var next in Connections)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }
}
