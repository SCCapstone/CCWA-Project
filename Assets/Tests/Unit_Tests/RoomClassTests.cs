using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoomClassTests
{
    [Test]
    public void ConstructDefaultRoomTest() {
        Room defaultRoom = new Room();
        Assert.AreEqual(defaultRoom.width, 0);
        Assert.AreEqual(defaultRoom.height, 0);
        Assert.IsNotNull(defaultRoom.map);
        Assert.AreEqual(defaultRoom.seed, "none");
    }

    [Test]
    public void ConstructParameterizedRoomTest() {
        int size = 25;
        Room parameterizedRoom = new Room(size,size,new int[size,size],"test",
        new Location[2], new Location[2], new Location[2], new Location(), 2, 2);
        Assert.AreEqual(parameterizedRoom.width, 25);
        Assert.AreEqual(parameterizedRoom.height, 25);
        Assert.AreEqual(parameterizedRoom.map.Length, 625); //25*25=625
        Assert.AreEqual(parameterizedRoom.seed, "test");
        Assert.IsNotNull(parameterizedRoom.exitLocations);
        Assert.IsNotNull(parameterizedRoom.itemLocations);
        Assert.IsNotNull(parameterizedRoom.enemyLocations);
        Assert.AreEqual(parameterizedRoom.exitLocations.Length, 2);
        Assert.AreEqual(parameterizedRoom.itemLocations.Length, 2);
        Assert.AreEqual(parameterizedRoom.enemyLocations.Length, 2);
        Assert.AreEqual(parameterizedRoom.numItems, 2);
    }
}
