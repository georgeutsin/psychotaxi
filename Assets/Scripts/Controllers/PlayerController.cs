using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RenderConfigScriptableObject renderConfig;
    public PlayerConfigScriptableObject playerConfig;
    public LevelDifficultyScriptableObject difficulty;

    int posIdx = 1;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = playerConfig.mass; // todo: think about mass lifecycle
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
        int level = difficulty.GetLevelFromDistance(rb.transform.position.x);
        //Debug.Log(rb.velocity.x);
        if (rb.velocity.x < difficulty.GetMaxSpeed(level))
        {
            rb.AddForce(new Vector3(playerConfig.maxAcceleration, 0, 0));
        }

        float step = playerConfig.lateralSpeed * Time.deltaTime;
        Vector3 target = new Vector3(transform.position.x, transform.position.y, renderConfig.lanePosns[posIdx]);
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void Left()
    {
        if (posIdx > 0) posIdx -= 1;
    }

    public void Right()
    {
        if (posIdx < renderConfig.lanePosns.Length - 1) posIdx += 1;
    }

    public void Jump()
    {
        if (rb.transform.position.y < playerConfig.vehicleHeight) // single jump
        {
            rb.velocity = new Vector3(rb.velocity.x, playerConfig.jumpSpeed, rb.velocity.z);
        }
    }
}
