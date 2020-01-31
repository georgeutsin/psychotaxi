using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicRoadObject : MonoBehaviour
{
    public static float obstacleSpeed = 0.025f;
    public static float rotationSpeed = 90f;
    public GameStateScriptableObject gs;

    protected Rigidbody rb;
    UnityAction pauseListener;
    UnityAction resumeListener;
    Vector3 savedVelocity;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(obstacleSpeed, 0f, 0f);

        pauseListener = new UnityAction(PauseEvent);
        EventManager.StartListening("GamePaused", pauseListener);

        resumeListener = new UnityAction(ResumeEvent);
        EventManager.StartListening("GameResumed", resumeListener);
    }

    public virtual void Update()
    {
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
    }

    void PauseEvent()
    {
        savedVelocity = rb.velocity;
        rb.velocity = Vector3.zero;
    }

    void ResumeEvent()
    {
        rb.velocity = savedVelocity;
    }
}
