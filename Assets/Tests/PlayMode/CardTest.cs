using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CardTest
{
    
    [UnityTest]
    public IEnumerator CardTestWithEnumeratorPasses()
    {
        var gameObject = new GameObject();
        var card = gameObject.AddComponent<Card>();
        yield return new WaitForSeconds(card.FlipAnimationDuration);
    }
}
