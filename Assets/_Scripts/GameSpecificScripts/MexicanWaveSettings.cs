using UnityEngine;

[CreateAssetMenu(menuName = "Gate Charger / Mexican Wave Data", fileName = "Mexican Wave Data")]
public class MexicanWaveSettings : ScriptableObject
{
    [SerializeField, Space(5f)] float startTweenCompletionTime = .1f;
    [SerializeField, Space(5f)] float endTweenCompletionTime = .1f;
    [SerializeField, Space(5f)] float delayBetweenPerBall = .1f;
    [SerializeField, Space(5f)] float tweenDamper = 2f;
    [SerializeField, Space(5f)] bool inverseAnimation = true;
    [SerializeField, Range(0f, 5f), Space(5f)] float nextTweenDelay = 1f;
    [SerializeField] bool playOneByOne = false;

    public float StartTweenCompletionTime { get => startTweenCompletionTime; set => startTweenCompletionTime = value; }
    public float EndTweenCompletionTime { get => endTweenCompletionTime; set => endTweenCompletionTime = value; }
    public float DelayBetweenPerBall { get => delayBetweenPerBall; set => delayBetweenPerBall = value; }
    public float TweenDamper { get => tweenDamper; set => tweenDamper = value; }
    public bool InverseAnimation { get => inverseAnimation; set => inverseAnimation = value; }
    public float NextTweenDelay { get => nextTweenDelay; set => nextTweenDelay = value; }
    public bool PlayOneByOne { get => playOneByOne; set => playOneByOne = value; }
}
