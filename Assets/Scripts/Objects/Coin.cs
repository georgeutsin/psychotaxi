using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : DynamicRoadObject
{
    override public void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
}
