using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private float hintDuration = 3f;
    [SerializeField] private Color hintColor = Color.black;

    public void ShowHint()
    {
        var hint = PathFinding.FindHint();
        if (hint != null)
        {
            StartCoroutine(BlinkBlocks(hint.Value.Item1, hint.Value.Item2));
        }
    }

    private IEnumerator BlinkBlocks(BlockData block1, BlockData block2)
    {
        float elapsed = 0;
        while (elapsed < hintDuration)
        {
            // Bật/tắt highlight
            bool highlight = (elapsed % 0.5f) < 0.25f;
            SetBlockHighlight(block1, highlight ? hintColor : Color.white);
            SetBlockHighlight(block2, highlight ? hintColor : Color.white);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset về màu ban đầu
        SetBlockHighlight(block1, Color.white);
        SetBlockHighlight(block2, Color.white);
    }

    private void SetBlockHighlight(BlockData block, Color color)
    {
        block.GetComponent<SpriteRenderer>().color = color;
    }
}
