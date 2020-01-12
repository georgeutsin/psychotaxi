using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController
{
    float roadLength;
    float endPosn = -2f;
    CircularObjectPool pool;

    public RoadController(GameObject segment, Transform parent)
    {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        Vector3 scale = segment.transform.lossyScale;
        Vector3 objectSize = Vector3.Scale(scale, mesh.bounds.size);
        roadLength = objectSize.x * 0.99f;
        pool = new CircularObjectPool(segment, parent, 20);
    }

    public void RenderUntil(float targetPosn)
    {
        while (endPosn < targetPosn)
        {
            GameObject o = pool.Next();
            o.transform.position = new Vector3(endPosn, 0f, 0f);
            endPosn += roadLength;
        }
    }

    public void FreeUntil(float targetPosn)
    {
        while (pool.First().transform.position.x < targetPosn)
        {
            pool.ReturnFirst();
        }
    }
}
