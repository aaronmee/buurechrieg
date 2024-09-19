using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class VectorTest
{
    

    // A Test behaves as an ordinary method
    [Test]
    public void VectorPlayerDeckPositionTest()
    // The Function tests if the vector in the first part of the brackets equals the vector in the second part of the brackets. The output is True or False
    {
        Assert.AreEqual(expected: new Vector3(x: 7, y: 0, z:0), actual: GameManager.playerDeckPosition);
    }


    [Test]

    public void VectorComputerDeckPositionTest()
    // The Function tests if the vector in the first part of the brackets equals the vector in the second part of the brackets. The output is True or False
    {
        Assert.AreEqual(new Vector3(x: -7, y: 0, z: 0), GameManager.computerDeckPosition);
    }

    [Test]

    public void VectorPlayerPilePositionTest()
    // The Function tests if the vector in the first part of the brackets equals the vector in the second part of the brackets. The output is True or False
    {
        Assert.AreEqual(new Vector3(x: 2.5f, y: 0, z: 0), GameManager.playerPilePosition);
    }

    [Test]

    public void VectorComputerPilePositionTest()
        // The Function tests if the vector in the first part of the brackets equals the vector in the second part of the brackets. The output is True or False
    {
        Assert.AreEqual(new Vector3(x: -2.5f, y: 0, z: 0), GameManager.computerPilePosition);
    }
}