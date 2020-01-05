using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public GameObject player;
    Rigidbody rb;
    Vector3 temp;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        temp.Set(player.transform.position.x, 0, 0);
        transform.position = temp;
    }

    public float GetVelocity()
    {
        return rb.velocity.x;
    }
}
