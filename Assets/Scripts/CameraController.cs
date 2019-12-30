using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerTracker;

    Vector3 gameViewPosition = new Vector3(-22.5f, 45.11f, -8.2f);

    void LateUpdate()
    {
        transform.position = gameViewPosition + playerTracker.transform.position;
    }
}
