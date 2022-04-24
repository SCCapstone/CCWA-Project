using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RoomGeneratorTests
{

    RoomGenerator rg = new RoomGenerator(true);

    // A Test behaves as an ordinary method
    [Test]
    public void GenerateRoomTest()
    {
        // Use the Assert class to test conditions
        // RoomGenerator rg = new RoomGenerator(true);
        Room room  = rg.GenerateRoom("test",1,1,false,new string[1]{"north"});

        Assert.AreEqual("test",room.seed);
        Assert.AreEqual(1,room.numEnemies);
        Assert.AreEqual(1,room.enemyLocations.Length);
        Assert.AreEqual(1,room.numItems);
        Assert.AreEqual(1,room.itemLocations.Length);
        Assert.AreEqual(1,room.exitLocations.Length);
        
    }

    [Test]
    public void GenerateRoomMapTest() {
        int[,] testMap = rg.GenerateRoomMap("test");
        Assert.IsNotNull(testMap);
        Assert.AreEqual(testMap.Length, 625); //25*25=625
    }

    [Test]
    public void GenerateRoomWithPerlinNoiseTest() {
        int[,] testMap = rg.GenerateRoomWithPerlinNoise("test");
        Assert.IsNotNull(testMap);
        Assert.AreEqual(testMap.Length, 625); // 25*25=625
    }

    [Test]
    public void ClearSpawnsTest() {
        int[,] testMap = rg.GenerateRoomWithPerlinNoise("test");
        int[,] clearedSpawnsMap = rg.ClearSpawns(testMap);
        Assert.IsNotNull(testMap);
        Assert.IsNotNull(clearedSpawnsMap);
        Assert.AreNotEqual(testMap, clearedSpawnsMap);
    }

    [Test]
    public void CreateRoomBorderTest() {
        int[,] testMap = rg.GenerateRoomWithPerlinNoise("test");
        int [,] borderedMap = rg.CreateRoomBorder(testMap);
        Assert.IsNotNull(testMap);
        Assert.IsNotNull(borderedMap);
        Assert.AreNotEqual(testMap, borderedMap);
    }

    [Test]
    public void RoomGeneratorConstructorTest() {
        RoomGenerator roomFromConstructor = new RoomGenerator(true);
        Assert.IsNotNull(roomFromConstructor);
        Assert.AreEqual(roomFromConstructor.useSeed, true);
    }

    [Test]
    public void GenerateEnemySpawnsTest() {
        int numEnemies = 5;
        Location[] testEnemyLocations = rg.GenerateEnemySpawns(numEnemies);
        Assert.IsNotNull(testEnemyLocations);
        Assert.AreEqual(testEnemyLocations.Length, numEnemies);
    }

    [Test]
    public void GenerateLootSpawnsTest() {
        int numItems = 3;
        Location[] testLootLocations = rg.GenerateEnemySpawns(numItems);
        Assert.IsNotNull(testLootLocations);
        Assert.AreEqual(testLootLocations.Length, numItems);
    }

    [Test]
    public void GenerateSingleExitSpawnTest() {
        string testDirection = "north";
        int[,] testMap = rg.GenerateRoomWithPerlinNoise("test");
        Location testLocation = rg.GenerateExitSpawn(testDirection, testMap);
        Assert.IsNotNull(testMap);
        Assert.IsNotNull(testLocation);
        Assert.AreEqual(testLocation.location, testDirection);
    }

    [Test]
    public void GenerateMultipleExitsTest() {
        int[,] testMap = rg.GenerateRoomWithPerlinNoise("test");
        string[] testDirections = new string[]{"north", "east"};
        int numOfLocations = 2;
        Location[] multipleExits = 
            rg.GenerateMultipleExits(numOfLocations, testDirections, testMap);
        Assert.IsNotNull(testMap);
        Assert.IsNotNull(testDirections);
        Assert.AreEqual(numOfLocations, testDirections.Length);
        Assert.AreEqual(multipleExits.Length, numOfLocations);
    }
}
