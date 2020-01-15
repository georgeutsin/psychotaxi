using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    public ParticleSystem explosion;
    protected List<ParticleSystem> pool;
    public GameObject parent;

    protected const int size = 20;

    void Start()
    {
        pool = new List<ParticleSystem>(size);
        for (int i = 0; i < size; i++)
        {
            ParticleSystem ps = Instantiate(explosion, transform.position, Quaternion.identity, parent.transform) as ParticleSystem;
            pool.Add(ps);
        }
    }

    public ParticleSystem Next()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].isPlaying)
            {
                return pool[i];
            }
        }

        ParticleSystem ps = Instantiate(explosion, transform.position, Quaternion.identity, parent.transform) as ParticleSystem;
        pool.Add(ps);
        return ps;
    }
}
