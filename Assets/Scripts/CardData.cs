using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Data", order = 1)]
public class CardData : ScriptableObject
{
    public string cardName;
    public int value;
    public Sprite frontCardImage;
    public Sprite backCardImage;
    public bool owner;
}