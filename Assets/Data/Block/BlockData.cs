using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockData : MonoBehaviour
{
    public int id;
    public int x;
    public int y;
    public SpriteRenderer spriteRenderer;
    public GridManager gridManager;

    public void SetBlockData(Sprite sprite,int id, int x, int y)
    {
        this.spriteRenderer.sprite = sprite;
        this.id = id;
        this.x = x;
        this.y = y;
        gridManager=FindObjectOfType<GridManager>();
    }
    public void HideBlock()
    {
        gridManager.grid[x, y] = null;
        this.gameObject.SetActive(false);
    }
    public void ChangeColor(bool isSelected)
    {
       
        if (isSelected)
        {
            this.spriteRenderer.color = new Color(128 / 255f, 128 / 255f, 128 / 255f);
        }
        else 
        {
            this.spriteRenderer.color = new Color(1f, 1f, 1f);
        }
    }
}
