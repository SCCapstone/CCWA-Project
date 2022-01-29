using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoomGeneratorTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void GenerateRoomTest()
    {
        // Use the Assert class to test conditions
        RoomGenerator rg = new RoomGenerator(true);
        Room room  = rg.GenerateRoom("test",1,1,false,new string[1]{"north"});

        Assert.AreEqual("test",room.seed);
        Assert.AreEqual(1,room.numEnemies);
        Assert.AreEqual(1,room.enemyLocations.Length);
        Assert.AreEqual(1,room.numItems);
        Assert.AreEqual(1,room.itemLocations.Length);
        Assert.AreEqual(1,room.exitLocations.Length);
        
    }
}
