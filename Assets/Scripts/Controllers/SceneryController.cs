using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryController
{
    float segmentLength;
    float internalStart = 0f;
    float internalEnd = 0f;
    List<GameObject> segments;
    List<ObjectPool> pools;

    public SceneryController(GameObject[] scenes, Transform parent, float segmentLength)
    {
        this.segmentLength = segmentLength;
        pools = new List<ObjectPool>();
        foreach (GameObject scene in scenes)
        {
            pools.Add(new ObjectPool(scene, parent, 2));
        }
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
            GameObject o = pools[GetPoolIdx(internalStart)].Next();
            internalStart -= segmentLength;
            o.transform.position = new Vector3(internalStart, 0f, 0f);
            segments.Insert(0, o);
        }

        while (internalEnd < end)
        {
            GameObject o = pools[GetPoolIdx(internalEnd)].Next();
            o.transform.position = new Vector3(internalEnd, 0f, 0f);
            internalEnd += segmentLength;
            segments.Add(o);
        }
    }

    void RemoveSegmentsOutside(float start, float end)
    {
        while(internalStart + segmentLength < start)
        {
            pools[GetPoolIdx(internalStart + segmentLength)].Return(segments[0]);
            segments.RemoveAt(0);
            internalStart += segmentLength;
        }

        while (internalEnd - segmentLength > end)
        {
            pools[GetPoolIdx(internalEnd  - segmentLength)].Return(segments[segments.Count - 1]);
            segments.RemoveAt(segments.Count - 1);
            internalEnd -= segmentLength;
        }
    }

    int GetPoolIdx(float locn)
    {
        int c = pools.Count;
        int p = (int) (locn / (segmentLength * 10.0f));
        return p % c;
    }
}
