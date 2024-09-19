using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

//all comments are from chatgpt
public class VectorTest
{
    // Purpose: This test checks whether the player's deck position is correctly set in the GameManager.
    // Arguments: No arguments are passed to this function.
    // Output: If the actual GameManager.playerDeckPosition is equal to the expected Vector3(7, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void VectorPlayerDeckPositionTest()
    {
        Assert.AreEqual(expected: new Vector3(x: 7, y: 0, z: 0), actual: GameManager.playerDeckPosition);
    }

    // Purpose: This test checks whether the computer's deck position is correctly set in the GameManager.
    // Arguments: No arguments are passed to this function.
    // Output: If the actual GameManager.computerDeckPosition is equal to the expected Vector3(-7, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void VectorComputerDeckPositionTest()
    {
        Assert.AreEqual(new Vector3(x: -7, y: 0, z: 0), GameManager.computerDeckPosition);
    }

    // Purpose: This test checks whether the player's pile position is correctly set in the GameManager.
    // Arguments: No arguments are passed to this function.
    // Output: If the actual GameManager.playerPilePosition is equal to the expected Vector3(2.5, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void VectorPlayerPilePositionTest()
    {
        Assert.AreEqual(new Vector3(x: 2.5f, y: 0, z: 0), GameManager.playerPilePosition);
    }

    // Purpose: This test checks whether the computer's pile position is correctly set in the GameManager.
    // Arguments: No arguments are passed to this function.
    // Output: If the actual GameManager.computerPilePosition is equal to the expected Vector3(-2.5, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void VectorComputerPilePositionTest()
    {
        Assert.AreEqual(new Vector3(x: -2.5f, y: 0, z: 0), GameManager.computerPilePosition);
    }

    // Purpose: This test checks whether the zero vector in the Card class is correctly defined.
    // Arguments: No arguments are passed to this function.
    // Output: If the Card.zeroVector is equal to the expected Vector3(0, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void zeroVectorTest()
    {
        Assert.AreEqual(new Vector3(x: 0, y: 0, z: 0), Card.zeroVector);
    }

    // Purpose: This test checks whether the player deck position in the Card class is correctly set.
    // Arguments: No arguments are passed to this function.
    // Output: If the Card.pDeckPosition is equal to the expected Vector3(7, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void pDeckPositionTest()
    {
        Assert.AreEqual(new Vector3(x: 7, y: 0, z: 0), Card.pDeckPosition);
    }

    // Purpose: This test checks whether the computer deck position in the Card class is correctly set.
    // Arguments: No arguments are passed to this function.
    // Output: If the Card.cDeckPosition is equal to the expected Vector3(-7, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void cDeckPositionTest()
    {
        Assert.AreEqual(new Vector3(x: -7, y: 0, z: 0), Card.cDeckPosition);
    }

    // Purpose: This test checks whether the player pile position in the Card class is correctly set.
    // Arguments: No arguments are passed to this function.
    // Output: If the Card.pPilePosition is equal to the expected Vector3(2.5, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void pPilePositionTest()
    {
        Assert.AreEqual(new Vector3(x: 2.5f, y: 0, z: 0), Card.pPilePosition);
    }

    // Purpose: This test checks whether the computer pile position in the Card class is correctly set.
    // Arguments: No arguments are passed to this function.
    // Output: If the Card.cPilePosition is equal to the expected Vector3(-2.5, 0, 0), the test passes. Otherwise, it fails.
    [Test]
    public void cPilePositionTest()
    {
        Assert.AreEqual(new Vector3(x: -2.5f, y: 0, z: 0), Card.cPilePosition);
    }
}
