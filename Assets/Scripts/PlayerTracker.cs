using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public GameObject player;

    Vector3 temp;

    void LateUpdate()
    {
        temp.Set(player.transform.position.x, 0, 0);
        transform.position = temp;
    }
}
