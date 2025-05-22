using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 0;
    }

    public void DrawConnection(List<Vector2Int> path)
    {
        StartCoroutine(DrawAndHideConnection(path, 0.5f));
    }

    public void HideConnection()
    {
        lineRenderer.positionCount = 0;
    }
    public IEnumerator DrawAndHideConnection(List<Vector2Int> path, float delay)
    {
        if (path.Count < 1)
        {
            HideConnection();
            yield break;
        }

        lineRenderer.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(path[i].y, path[i].x, 0));
        }
        yield return new WaitForSeconds(delay);
        HideConnection();
    }
}


