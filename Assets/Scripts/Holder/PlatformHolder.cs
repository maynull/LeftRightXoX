using UnityEngine;
using System.Collections.Generic;

public class PlatformHolder : MonoBehaviour
{
    public static PlatformHolder Instance { get; private set; }

    public List<Platform> AvailablePlatforms;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }
}
