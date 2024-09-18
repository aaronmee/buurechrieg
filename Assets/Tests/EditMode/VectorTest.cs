using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class VectorTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void MixCardstestSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.AreEqual(expected: new Vector3(x: 7, y: 0, z:0), actual: GameManager.playerDeckPosition);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
}
