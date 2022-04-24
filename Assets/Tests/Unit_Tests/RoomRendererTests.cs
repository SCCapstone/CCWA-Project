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
        // Tilemaps and Tilebases must be set in the
        // unity editor. They will always be null until 
        // the game is played.
        Assert.IsNotNull(rr.currentRoom);
        Assert.IsNotNull(rr.currFloor);
        Assert.IsNotNull(rr.roomGenerator);
        Assert.IsNotNull(rr.playerX);
        Assert.IsNotNull(rr.playerY);
    }

}
