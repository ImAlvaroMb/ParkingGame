using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Grid unityGrid;
    public Dictionary<Vector2Int, BaseTile> Grid = new Dictionary<Vector2Int, BaseTile>();
    public BaseTile ConstructionPrefab;

    // add new tile

    public void AddNewTile(Vector3 mousePos, BaseTile prefab)
    {
        Vector3Int cellPos = unityGrid.WorldToCell(mousePos);
        Vector2Int pos2D = new Vector2Int(cellPos.x, cellPos.y);

        if (Grid.ContainsKey(pos2D)) return;

        Vector3 worldPos = unityGrid.CellToWorld(cellPos);
        BaseTile newTile = Instantiate(prefab, worldPos, Quaternion.identity);
        newTile.Initialize(pos2D);
        Grid.Add(pos2D, newTile);
    }

    public void ReplaceTile(Vector2Int pos, BaseTile newPrefab, float constructionDuration)
    {
        if(Grid.ContainsKey(pos)) Destroy(Grid[pos].gameObject);
        BaseTile construction = Instantiate(ConstructionPrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
        construction.IsUnderConstruction = true;
        Grid[pos] = construction;
        UpdateNeighbors(pos);
        TimerSystem.Instance.CreateTimer(constructionDuration, onTimerDecreaseComplete: () =>
        {
            Destroy(construction.gameObject);
            BaseTile finalTile = Instantiate(newPrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity);
            finalTile.Initialize(pos);
            Grid[pos] = finalTile;

            UpdateNeighbors(pos);
        }, onTimerDecreaseUpdate: (progress) =>
        {
            // update any visual elements
        });
    }

    private void UpdateNeighbors(Vector2Int pos)
    {
        Vector2Int[] dirs = { Vector2Int.zero, Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        foreach (var d in dirs)
        {
            if (Grid.TryGetValue(pos + d, out BaseTile tile))
                tile.RefreshConnections(Grid);
        }
    }
}
