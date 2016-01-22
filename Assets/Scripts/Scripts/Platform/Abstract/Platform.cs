using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public abstract class Platform : MonoBehaviour
{
    public Color Color;

    public string KeyCombination { get; set; }

    public bool IsActive;

    public float DurationCoeff = 0.5f;

    tk2dTextMesh _combinationText;

    SpriteRenderer _sprite;

    Tweener _moveTween;


    void Awake()
    {
        _combinationText = transform.FindChild("KeyCombinationText").GetComponent<tk2dTextMesh>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void Initialize()
    {
        _sprite.color = Color;
        do
        {
            KeyCombination = SequenceManager.GenerateRandomKeySequence(1, 5);
        }
        while(PlatformSpawner.Instance.CompareKeySequencesWithOtherPlatforms(KeyCombination));

        SetKeyCombinationText(KeyCombination);
    }

    public virtual void Activate()
    {
        RevealKeyCombination(0);
        float maxDist = PlatformSpawner.Instance.MaxDistanceY * 2;
        _moveTween = transform.DOMoveY(transform.position.y + maxDist, DurationCoeff * maxDist)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Yoyo);
        PlatformSpawner.Instance.RemoveCandidatePlatform(this);
    }

    public virtual void SetCandidate()
    {
        RevealKeyCombination(1);
        PlatformSpawner.Instance.AddCandidatePlatform(this);
    }

    public virtual void Deactivate()
    {
        _moveTween.Kill();
    }

    public void SetKeyCombinationText(string keyComb)
    {
/*        string sequence = "";
        foreach (KeyCode key in keyComb)
        {
            sequence += key.ToString();
        }*/
        _combinationText.text = keyComb;
    }

    public void RevealKeyCombination(int endValue)
    {
        DOTween.ToAlpha(() => _combinationText.color, x => _combinationText.color = x, endValue, 0.7f);
    }
}
