using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameLogicTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void GameLogic_Initialization_SetsObjectivesCorrectly()
    {
        // Use the AAA
        GameLogic gameLogic;

        gameLogic = new GameLogic(5);

        Assert.AreEqual(5, gameLogic.ObjectivesToWin);
        Assert.AreEqual(0, gameLogic.ObjectivesCompleted);
        Assert.IsFalse(gameLogic.IsVictoryConditionMet);
    }

}
