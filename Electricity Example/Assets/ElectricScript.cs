using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class ElectricScript : MonoBehaviour
{
    public Vector2 origin;
    public Vector2 target;

    public int vertexCount = 20;
    public float ArcScale = 1f;
    public float min = 0.5f;
    public float max = 1.3f;

    LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetVertexCount(vertexCount);
        line.enabled = true;

        //line.SortingLayer can be used
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, origin);
        float distance = Vector2.Distance(transform.position, target);
        for (int i = 1; i < vertexCount; i++)
        {
            float x = Mathf.Lerp(origin.x, distance, (float)i / (float)(vertexCount - 1));
            Vector2 pos = new Vector2(x, ArcScale * Mathf.Sin(i + Time.time * Random.Range(min, max)));
            line.SetPosition(i, pos);
        }
        line.SetPosition(vertexCount - 1, target);
    }
}