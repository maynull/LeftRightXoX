using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;


public class CharacterManager : MonoBehaviour
{

    public static CharacterManager Instance;

    public Platform CurrentPlatform;

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public void StartGame()
    {
        List<KeyValuePair<int, Platform>> lowestLanes = PlatformSpawner.Platforms.FindAll(x => x.Key == PlatformSpawner.Platforms.ElementAt(0).Key);
        foreach (KeyValuePair<int, Platform> kvp in lowestLanes)
        {
            kvp.Value.SetCandidate();
        }
    }

    public void MoveCharacterToTransform(Platform platform)
    {
        transform.DOMove(platform.transform.position, 0.5f).OnComplete(new TweenCallback(() =>
                {
                    transform.parent = platform.transform;
                }));
        CurrentPlatform = platform;
    }



}
