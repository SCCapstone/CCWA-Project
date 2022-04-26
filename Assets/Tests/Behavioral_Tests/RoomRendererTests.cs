using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoomRendererTests
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator RoomRendererTestsWithEnumeratorPasses()
    {
        RoomRenderer rr = GameObject.FindWithTag("Floor Map").GetComponent<RoomRenderer>();
        Assert.IsNotNull(rr.floorMap);
        Assert.IsNotNull(rr.wallMap);
        Assert.IsNotNull(rr.itemMap);
        Assert.IsNotNull(rr.floorTile);
        Assert.IsNotNull(rr.wallTile);
        Assert.IsNotNull(rr.exitTile);
        Assert.IsNotNull(rr.lockedExitTile);
        Assert.IsNotNull(rr.attackUp);
        Assert.IsNotNull(rr.defenseUp);
        Assert.IsNotNull(rr.health);
        Assert.IsNotNull(rr.key);
        Assert.IsNotNull(rr.chest);
        Assert.IsNotNull(rr.enemy);
        Assert.IsNotNull(rr.boss);
        Assert.IsNotNull(rr.player);
        Assert.IsNotNull(rr.roomGenerator);
        yield return null;
    }
}
