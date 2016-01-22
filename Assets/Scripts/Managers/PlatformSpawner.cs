using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlatformSpawner : MonoBehaviour
{

    public static PlatformSpawner Instance { get; private set; }

    public int TresholdToSpawnNewPlatforms;

    public float MinDistanceY;

    public float MaxDistanceY;

    public static List<KeyValuePair<int, Platform>> Platforms;

    public List<Platform> ActivePlatforms;

    int _laneCount;

    void Awake()
    {
        Instance = this;
        Platforms = new List<KeyValuePair<int, Platform>>();
    }

    void OnDestroy()
    {
        Instance = null;
        Platforms = null;
    }

    void Start()
    {
        _laneCount = 0;
    }

    public void SpawnPlatforms(Vector3 startPos, int amount = 10)
    {
        for (int i = 0; i < amount; i++)
        {
            startPos.y = SpawnPlatform(startPos, Utilities.R.Next(2, 6));
            _laneCount++;
        }
    }

    public float SpawnPlatform(Vector3 startPos, int lane = 5)
    {
        Vector3 topRight = CameraBounds.Instance.GetTopRight();
        Vector3 bottomLeft = CameraBounds.Instance.GetBottomLeft();

        float laneTreshold = (bottomLeft.x - topRight.x - 0.5f) / lane;
        float nextPlatformPos = startPos.y + Utilities.NextFloat(MinDistanceY, MaxDistanceY);

        for (int i = 0; i < lane; i++)
        {

            float xPos;
            if (i % 2 == 0)
                xPos = startPos.x + (laneTreshold * Mathf.CeilToInt(i / 2.0f));
            else
                xPos = startPos.x - (laneTreshold * Mathf.CeilToInt(i / 2.0f));
            
            GameObject gObj = Object.Instantiate(PlatformHolder.Instance.AvailablePlatforms[Utilities.R.Next(PlatformHolder.Instance.AvailablePlatforms.Count)].gameObject, 
                                  new Vector3(xPos, nextPlatformPos, startPos.z), 
                                  Quaternion.identity) as GameObject;

            Platform nextPlatform = gObj.GetComponent<Platform>();
            nextPlatform.Initialize();
            nextPlatform.transform.parent = PlatformHolder.Instance.transform;
            Platforms.Add(new KeyValuePair<int, Platform>(_laneCount, nextPlatform));
        }
        return nextPlatformPos;
    }

    public bool CompareKeySequencesWithOtherPlatforms(string keycomb)
    {
        return Platforms.Any(x => keycomb.Equals(x.Value.KeyCombination));
    }

    public void AddCandidatePlatform(Platform candidatePlatform)
    {
        ActivePlatforms.Add(candidatePlatform);
        if (ActivePlatforms.Count > 1)
        {
            SequenceManager.Instance.StartListeningSequences();
        }
    }

    public void RemoveCandidatePlatform(Platform platformToBeRemoved)
    {
        ActivePlatforms.Remove(platformToBeRemoved);
        if (ActivePlatforms.Count == 0)
        {
            SequenceManager.Instance.StopListeningSequences();
        }
    }

        

}
