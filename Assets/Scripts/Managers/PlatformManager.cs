using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    // Use this for initialization
    public void Initialize()
    {
        PlatformSpawner.Instance.SpawnPlatforms(Camera.main.transform.FindChild("StartPos").position);
    }
	
}
