using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public static class PathFinding
{
    private static int rows = 11;
    private static int cols = 18;
    private static BlockData[,] grid;
    private static Vector2Int[] directions = {Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left};
    public static void Init(BlockData[,] blockGrid)
    {
        grid = blockGrid;
    }
    public static bool CanConnect(BlockData startBlock, BlockData targetBlock)
    {
        if (startBlock == null || targetBlock == null || startBlock.id != targetBlock.id) return false;
        Vector2Int startPos = new Vector2Int(startBlock.x, startBlock.y);
        Vector2Int targetPos= new Vector2Int(targetBlock.x, targetBlock.y);
        if (!IsValidPosition(startPos) || !IsValidPosition(targetPos)) return false;
        return BFS(startPos, targetPos);
    }

    private static bool BFS(Vector2Int start, Vector2Int target)
    {
        List<(Vector2Int pos, Vector2Int dir, int turns, int cost)> queue = new List<(Vector2Int, Vector2Int, int, int)>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        foreach (Vector2Int dir in directions)
        {
            Vector2Int nextPos = start + dir;
            if (IsValid(nextPos, target))
            {
                queue.Add((nextPos, dir, 0, 0));
            }
        }

        while (queue.Count > 0)
        {
            queue.Sort((a, b) => a.cost.CompareTo(b.cost));
            var (pos, dir, turns, cost) = queue[0];
            queue.RemoveAt(0);

            if (turns > 2) continue;
            if (pos == target) return true;
            if (visited.Contains(pos)) continue;
            visited.Add(pos);

            foreach (var newDir in directions)
            {
                Vector2Int nextPos = pos + newDir;
                int newTurns = (newDir == dir) ? turns : turns + 1;
                int newCost = newTurns;

                if (IsValid(nextPos, target))
                {
                    queue.Add((nextPos, newDir, newTurns, newCost));
                }
            }
        }
        return false;
    }

    private static bool IsValid(Vector2Int pos, Vector2Int target)
    {
        if (pos.x < 0 || pos.x >= rows || pos.y < 0 || pos.y >= cols)
            return false;
        if (grid[pos.x, pos.y] == null || pos == target) return true;

        return false;
    }
    private static bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x <= rows && pos.y >= 0 && pos.y <= cols;
    }

}
