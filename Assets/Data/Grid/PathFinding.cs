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
    public static List<Vector2Int> CanConnect(BlockData startBlock, BlockData targetBlock)
    {
        // Kiểm tra null và id
        if (startBlock == null || targetBlock == null || startBlock.id != targetBlock.id)
            return new List<Vector2Int>();

        Vector2Int startPos = new Vector2Int(startBlock.x, startBlock.y);
        Vector2Int targetPos = new Vector2Int(targetBlock.x, targetBlock.y);
        if (!IsValidPosition(startPos) || !IsValidPosition(targetPos))
            return new List<Vector2Int>();
        if (startPos == targetPos)
            return new List<Vector2Int> { startPos };

        return BFS(startPos, targetPos);
    }


    private static List<Vector2Int> BFS(Vector2Int start, Vector2Int target)
    {
        Queue<(Vector2Int pos, Vector2Int dir, int turns, List<Vector2Int> path)> queue =
            new Queue<(Vector2Int, Vector2Int, int, List<Vector2Int>)>();
        bool[,] visited = new bool[rows, cols];
        queue.Enqueue((start, Vector2Int.zero, 0, new List<Vector2Int> { start }));
        visited[start.x, start.y] = true;

        while (queue.Count > 0)
        {
            var (pos, dir, turns, path) = queue.Dequeue();
            if (pos == target)
            {
                return path;
            }
            if (turns > 2) continue;

            foreach (var newDir in directions)
            {
                Vector2Int nextPos = pos + newDir;
                int newTurns = turns;
                if (dir != Vector2Int.zero && newDir != dir)
                {
                    newTurns = turns + 1;
                }

                if (IsValid(nextPos, target) && !visited[nextPos.x, nextPos.y])
                {
                    visited[nextPos.x, nextPos.y] = true;
                    var newPath = new List<Vector2Int>(path) { nextPos };
                    queue.Enqueue((nextPos, newDir, newTurns, newPath));
                }
            }
        }

        return new List<Vector2Int>();
    }


    private static bool IsValid(Vector2Int pos, Vector2Int target)
    {
        if (pos.x < 0 || pos.x >= rows || pos.y < 0 || pos.y >= cols)
            return false;
        if (pos == target) return true;
        return grid[pos.x, pos.y] == null;
    }
    private static bool IsValidPosition(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < rows && pos.y >= 0 && pos.y < cols;
    }
    public static (BlockData, BlockData)? FindHint()
    {
        if (grid == null) return null;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                BlockData currentBlock = grid[i, j];
                if (currentBlock == null) continue;
                for (int x = 0; x < rows; x++)
                {
                    for (int y = 0; y < cols; y++)
                    {
                        if (x == i && y == j) continue;

                        BlockData targetBlock = grid[x, y];
                        if (targetBlock == null || targetBlock.id != currentBlock.id) continue;
                        List<Vector2Int> path = CanConnect(currentBlock, targetBlock);
                        if (path.Count > 0)
                        {
                            return (currentBlock, targetBlock);
                        }
                    }
                }
            }
        }

        return null; 
    }
    
}
