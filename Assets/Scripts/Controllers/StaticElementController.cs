using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticElementController
{
    float segmentLength;
    float internalStart = 0f;
    float internalEnd = 0f;
    List<GameObject> segments;
    ObjectPool pool;

    public StaticElementController(GameObject segment, Transform parent)
    {
        Mesh mesh = segment.GetComponent<MeshFilter>().mesh;
        Vector3 scale = segment.transform.lossyScale;
        Vector3 objectSize = Vector3.Scale(scale, mesh.bounds.size);
        segmentLength = objectSize.x * 0.99f;
        pool = new ObjectPool(segment, parent, 20);
        segments = new List<GameObject>();
    }

    public StaticElementController(GameObject segment, Transform parent, float segmentLength, int poolSize)
    {
        this.segmentLength = segmentLength;
        pool = new ObjectPool(segment, parent, poolSize);
        segments = new List<GameObject>();
    }

    public void RenderFrom(float start, float end)
    {
        AddSegmentsBetween(start, end);
        RemoveSegmentsOutside(start, end);
    }

    void AddSegmentsBetween(float start, float end)
    {
        while (internalStart > start)
        {
            GameObject o = pool.Next();
            internalStart -= segmentLength;
            o.transform.position = new Vector3(internalStart, 0f, 0f);
            segments.Insert(0, o);
        }

        while (internalEnd < end)
        {
            GameObject o = pool.Next();
            o.transform.position = new Vector3(internalEnd, 0f, 0f);
            internalEnd += segmentLength;
            segments.Add(o);
        }
    }

    void RemoveSegmentsOutside(float start, float end)
    {
        while(internalStart + segmentLength < start)
        {
            pool.Return(segments[0]);
            segments.RemoveAt(0);
            internalStart += segmentLength;
        }

        while (internalEnd - segmentLength > end)
        {
            pool.Return(segments[segments.Count - 1]);
            segments.RemoveAt(segments.Count - 1);
            internalEnd -= segmentLength;
        }
    }
}
