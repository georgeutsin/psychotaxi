using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRoadObject : MonoBehaviour
{
    float obstacleSpeed = 0.025f;
    Rigidbody rb;

    Vector3 obsVelocity;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        obsVelocity = new Vector3(obstacleSpeed, 0f, 0f);
        rb.velocity = obsVelocity;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBounds") 
        {
            gameObject.SetActive(false);
            ResetObject();
        }
    }

    void ResetObject()
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        rb.velocity = obsVelocity;
        rb.angularVelocity = Vector3.zero;
    }
}
