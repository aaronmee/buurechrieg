using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManagerTest
{
    
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator cardPlayerTest()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        Card card = new Card();
        card.cardData.owner = true;
        List<Card> cards = new List<Card>();
        cards.Add(card);
        Assert.AreEqual(card.cardData.owner, true, GameManager.definePlayerAttribute(cards));
    }
}
