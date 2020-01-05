using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveUtil
{
    public static float NormalizedSample(AnimationCurve c, float value, float minX, float maxX, float minY, float maxY) {
        float v = (value - minX) / (maxX - minX);
        return c.Evaluate(v) * (maxY-minY) + minY;
    }

    public static float MultiplierSample(AnimationCurve c, float value, float maxMultiplier = 2f)
    {
        return c.Evaluate(value) * (maxMultiplier - 1f) + 1f;
    }

    public static AnimationCurve InvertCurve(AnimationCurve c)
    {
        AnimationCurve inverse = new AnimationCurve();
        int precision = 1000;
        for (int i = 0; i < precision; i++)
        {
            float time = (float)i / precision;
            float value = c.Evaluate(time);
            Debug.Log(time + " - " + value);
            Keyframe inverseKey = new Keyframe(value, time);
            inverse.AddKey(inverseKey);
        }
        return inverse;
    }


}
