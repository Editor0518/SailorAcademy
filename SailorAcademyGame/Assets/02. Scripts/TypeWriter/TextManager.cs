using UnityEngine;
using TMPro;
using System;
using UnityEditor;

public class TextManager : MonoBehaviour
{
    public Color32[] baseColor;
    public TextTransform typingTransform;
    public TextTransform impactTypingTransform;
    public float typingSpeed;
    public float typingDelay;
    public float typingRestDelay;

    [Space(25)]

    public float shakePower;
    public float shakeDelay;
    public float wavePower;
    public float waveGap;
    public float waveSpeed;
    public float rotateAngle;
    public float rotateRandomA;
    public float rotateRandomB;

    public string[] scriptTags;
    public TagReference[] changeTags;
}

[Serializable]
public struct TextTransform
{
    public Vector3 position;
    public Vector3 Scale;
    public Color32[] color;
}

[Serializable]
public struct TagReference
{
    public string oldTag;
    public string newText;
}