using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject blockPrefabs;
    public Transform holder;
    public static int rows = 11;
    public static int cols = 18;
    public float spacing = 1f;

    public List<Sprite> spriteList = new List<Sprite>();
    public BlockData[,] grid = new BlockData[rows, cols];
    
    private void Start()
    {
        GenerateGrid();
        PathFinding.Init(grid);
    }

    private void GenerateGrid()
    {
        for(int i=0; i<rows; i++)
        {
            for(int j=0; j<cols; j++)
            {
                grid[i, j] = null;
            }
        }
        List<int> listIds = ShuffleId();
        int idx = 0;
        for(int x=1; x<=rows-2; x++)
        {
            for(int y=1; y<=cols-2; y++)
            {
                Vector2 pos = new Vector2(y, x);
                GameObject block = Instantiate(blockPrefabs, pos, Quaternion.identity, holder);
                BlockData blockData = block.GetComponent<BlockData>();

                blockData.SetBlockData(spriteList[listIds[idx]], listIds[idx], x,y);
                grid[x, y] = blockData;
                idx++;
            }
        }
    }

    private List<int> ShuffleId()
    {
        int evenCnt= ((cols - 2) * (rows - 2)) / spriteList.Count;
        List<int> indices = new List<int>();
        for (int i = 0; i < spriteList.Count; i++)
        {
            for (int j = 0; j < evenCnt; j++) 
            { 
                indices.Add(i); 
            }
        }
        for (int i = indices.Count - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);
            (indices[i], indices[randIndex]) = (indices[randIndex], indices[i]); // Swap
        }
        return indices;
    }
}
