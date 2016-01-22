using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SequenceManager : MonoBehaviour
{

    public static SequenceManager Instance;

    string _userSequence;

    bool _isListening;

    void Awake()
    {
        Instance = this;
        _userSequence = "";
    }

    void OnDestroy()
    {
        Instance = null;
    }

    public static string GenerateRandomKeySequence(int minLength, int maxLength)
    {
        string keyCombination = "";
        int totalLength = Utilities.R.Next(minLength, maxLength);
        for (int i = 0; i < totalLength; i++)
        {
            keyCombination += Constants.AllAvailableKeys[Utilities.R.Next(Constants.AllAvailableKeys.Length)];
        }
        return keyCombination;
    }

    public void StartListeningSequences()
    {
        if (!_isListening)
        {
            _isListening = true;
            InputManager.KeyPressed += ControlSequences;
        }
    }

    public void StopListeningSequences()
    {
        if (_isListening)
            InputManager.KeyPressed -= ControlSequences;
    }

    public void ControlSequences(KeyCode code)
    {
        _userSequence += code.ToString();
        if (PlatformSpawner.Instance.ActivePlatforms.Any(x => x.KeyCombination.Equals(_userSequence)))
        {
            if (CharacterManager.Instance.CurrentPlatform != null)
                CharacterManager.Instance.CurrentPlatform.Deactivate();
            Platform correctPlatform = PlatformSpawner.Instance.ActivePlatforms.Find(x => x.KeyCombination.Equals(_userSequence));
            correctPlatform.Activate();
            CharacterManager.Instance.MoveCharacterToTransform(correctPlatform);
        }
        if (!PlatformSpawner.Instance.ActivePlatforms.Any(x => x.KeyCombination.StartsWith(_userSequence, System.StringComparison.CurrentCulture)))
        {
            _userSequence = "";
        }
    }
}
