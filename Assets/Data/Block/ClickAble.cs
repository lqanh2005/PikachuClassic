using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAble : MonoBehaviour
{
    public BlockData blockData;
    private void OnMouseDown()
    {
        if(blockData != null)
        {
            GameManager.instance.SelectBlock(blockData);
        }
    }

    
}
