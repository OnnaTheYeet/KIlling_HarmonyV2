using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class SpriteChange
{
    public Actor actor;
    public int expression;
    public int onScreenImageID;
}

[Serializable]
public class BackgroundChange
{
    public Sprite sprite;
    public int onScreenImageID;
}

[Serializable]
public class DialogueLine
{
    public string line;
    public Actor actor;
    public List<SpriteChange> spriteChanges;
    public List<BackgroundChange> backgroundChanges;

    public TMP_FontAsset fontAsset;
    public int fontSize = 24; 
    public FontStyle fontStyle = FontStyle.Normal;
    public Color fontColor = Color.white;
    public Sprite dialogBoxBackground;
}


[CreateAssetMenu(menuName = "Dialogue/DialogueContainer")]
public class DialogueContainer : ScriptableObject
{
    public List<DialogueLine> lines;

    public bool playOnlyOnce = false;

    [NonSerialized] public bool hasPlayed = false;

    public Sprite dialogBoxBackground;
    public TMP_FontAsset fontAsset;
    public int fontSize = 24;
    public FontStyle fontStyle = FontStyle.Normal;
    public Color fontColor = Color.white;
}
