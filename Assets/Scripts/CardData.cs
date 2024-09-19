using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card Data", order = 1)]
// Enables the creation of scribtable objects in the Unity editor.
public class CardData : ScriptableObject
{
    // Defines the attributes of the scribtable objects.
    // This allows us to give our Cards properties which
    // we can utilise within the code of other scripts.
    public string cardName;
    public int value;
    public Sprite frontCardImage;
    public Sprite backCardImage;
    public bool owner;
    public bool isTribut = false;
}