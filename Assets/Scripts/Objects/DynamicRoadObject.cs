using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRoadObject : MonoBehaviour
{
    public static float obstacleSpeed = 0.025f;
    public static float rotationSpeed = 90f;
    public GameStateScriptableObject gs;

    public bool pauseState = true;

    protected Rigidbody rb;

    public Vector3 obsVelocity;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        obsVelocity = new Vector3(obstacleSpeed, 0f, 0f);
        rb.velocity = obsVelocity;
    }

    public virtual void Update()
    {
        if (gs.isPaused)
        {
            if (pauseState)
            {
                obsVelocity = rb.velocity;
                rb.velocity = Vector3.zero;
                pauseState = false;
            }

        } else 
        {
            if (!pauseState)
            {
                rb.velocity = obsVelocity;
                pauseState = true;
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBounds") 
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public static void ResetObject(GameObject o)
    {
        o.transform.rotation = Quaternion.identity;
        o.transform.position = Vector3.zero;
        Rigidbody rb = o.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(obstacleSpeed, 0f, 0f);
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        DynamicRoadObject obj = o.GetComponent<DynamicRoadObject>();
        obj.obsVelocity = new Vector3(obstacleSpeed, 0f, 0f);
    }
}
