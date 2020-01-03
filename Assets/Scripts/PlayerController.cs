using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float[] lanePosns = { 3f, 0f, -3f }; // todo: make this a scriptable object
    float jumpSpeed = 40f; // todo: make this a scriptable object
    float vehicleHeight = 2.0f; // todo: make this a scriptable object
    float minSpeed = 40f; // todo: make this a scriptable object
    float maxSpeed = 80f; // todo: make this a scriptable object
    float lateralSpeed = 40f; // todo: make this a scriptable object
    float maxAcceleration = 15f; // todo: make this a scriptable object

    int posIdx = 1;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            Jump();
        }

        if (Input.GetKeyDown("left"))
        {
            Left();
        }

        if (Input.GetKeyDown("right"))
        {
            Right();
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.x < maxSpeed)
        {
            rb.AddForce(new Vector3(maxAcceleration, 0, 0));
        }

        float step = lateralSpeed * Time.deltaTime;
        Vector3 target = new Vector3(transform.position.x, transform.position.y, lanePosns[posIdx]);
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void Left()
    {
        if (posIdx > 0) posIdx -= 1;
    }

    public void Right()
    {
        if (posIdx < lanePosns.Length - 1) posIdx += 1;
    }

    public void Jump()
    {
        if (rb.transform.position.y < vehicleHeight) // single jump
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
    }
}
