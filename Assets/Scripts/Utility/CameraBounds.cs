using UnityEngine;
using System.Collections;

public class CameraBounds : MonoBehaviour
{
    public static CameraBounds Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public Vector3 GetBottomLeft()
    {
        Camera camera = GetComponent<Camera>();
        return camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
    }

    public Vector3 GetTopRight()
    {
        Camera camera = GetComponent<Camera>();
        return camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }
}
