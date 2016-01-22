using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public static event Action<KeyCode> KeyPressed;
    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }
	
    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode key in Constants.AllAvailableKeys)
        {
            if (Input.GetKeyDown(key))
            {
                if (KeyPressed != null)
                    KeyPressed(key);
            }
        }
    }
}
