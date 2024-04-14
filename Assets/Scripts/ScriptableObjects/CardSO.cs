using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardSO : ScriptableObject
{
    public string CardName;
    public PatternSO Pattern;
    public Sprite CardImage;
}
