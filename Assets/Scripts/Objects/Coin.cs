using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : DynamicRoadObject
{
    override public void Start()
    {
        base.Start();
    }

    override public void Update()
    {
        base.Update();
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
}
