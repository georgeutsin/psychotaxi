﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : DynamicRoadObject
{
    ExplosionPool explosionPool;

    override public void Start()
    {
        base.Start();
        GameObject explosionPoolGameObject = GameObject.FindWithTag("ExplosionPool"); // WARNING: SLOW!!
        explosionPool = explosionPoolGameObject.GetComponent<ExplosionPool>();
    }

    override public void Update()
    {
        base.Update();
    }

    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBounds")
        {
            gameObject.SetActive(false);
            return;
        }

        if (other.tag == "ExplosiveBounds")
        {
            gameObject.SetActive(false);
            if (explosionPool != null)
            {
                ParticleSystem ps = explosionPool.Next();
                ps.transform.position = transform.position;
                ps.Play();
            }
        }
    }

}
