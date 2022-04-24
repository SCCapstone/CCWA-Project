using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoomRendererTests
{

    static RoomGenerator rg = new RoomGenerator(true);
    static RoomRenderer rr = new RoomRenderer();
    static string[] testDirections = new string[]{"north"};
    static Room testRoom = rg.GenerateRoom("test",1,1,false,testDirections);

    // A Test behaves as an ordinary method
    [Test]
    public void SetCurrentRoomTest() {
        rr.setCurrentRoom(testRoom);
        Assert.AreEqual(testRoom, rr.currentRoom);
    }

    [Test]
    public void RenderRoomTest() {
        Assert.IsNotNull(rr.floorMap);
        Assert.IsNotNull(rr.wallMap);
        Assert.IsNotNull(rr.itemMap);
        Assert.IsNotNull(rr.floorTile);
        Assert.IsNotNull(rr.wallTile);
        Assert.IsNotNull(rr.exitTile);
        Assert.IsNotNull(rr.lockedExitTile);
        assert.IsNotNull(rr.currFloor);
    }

}
