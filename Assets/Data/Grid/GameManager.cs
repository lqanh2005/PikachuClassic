
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GridManager gridManager;
    public HintManager hintManager;
    public LineRender lineRender;

    private BlockData firstSelected;
    private BlockData secondSelected;

    private void Awake()
    {
        instance = this;
    }
    public void SelectBlock(BlockData block)
    {
        if(firstSelected == null)
        {
            firstSelected = block;
            firstSelected.ChangeColor(true);
        }
        else if(secondSelected == null && firstSelected != block)
        {
            secondSelected = block;
            secondSelected.ChangeColor(true);
            CheckMath();
        }
        else if(firstSelected == block)
        {
            firstSelected.ChangeColor(false);
            firstSelected=null;
        }
    }

    private void CheckMath()
    {
        if(firstSelected.id == secondSelected.id)
        {
            List<Vector2Int> path = PathFinding.CanConnect(firstSelected, secondSelected);
            if(path.Count>0)
            {
                lineRender.DrawConnection(path);
                firstSelected.HideBlock();
                secondSelected.HideBlock();
            }
        }
        firstSelected.ChangeColor(false);
        secondSelected.ChangeColor(false) ;
        firstSelected = null;
        secondSelected = null;
    }
    public void Shuffle()
    {
        if (gridManager == null) return;
        List<BlockData> remainBlock= new List<BlockData>();
        for(int x = 1; x <= 9; x++)
        {
            for(int y = 1; y <= 16; y++)
            {
                if (gridManager.grid[x, y] != null)
                {
                    remainBlock.Add(gridManager.grid[x, y]);
                }
            }
        }
        List<int> shuffleId=remainBlock.Select(b => b.id).ToList();
        shuffleId=shuffleId.OrderBy(x=>Random.value).ToList();
        for(int i=0; i<remainBlock.Count; i++)
        {
            remainBlock[i].SetBlockData(gridManager.spriteList[shuffleId[i]], shuffleId[i], remainBlock[i].x, remainBlock[i].y);
        }
    }
    public void ShowHint()
    {
        hintManager.ShowHint();
    }
}
