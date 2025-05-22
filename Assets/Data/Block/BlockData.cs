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
    private static readonly Color selectColor = new Color(128 / 255f, 128 / 255f, 128 / 255f);
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void SetBlockData(Sprite sprite,int id, int x, int y)
    {
        this.spriteRenderer.sprite = sprite;
        this.id = id;
        this.x = x;
        this.y = y;
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
            this.spriteRenderer.color = selectColor;
        }
        else
        {
            this.spriteRenderer.color = Color.white;
        }
    }
}
