using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FloorGeneratorTests {

    public FloorGenerator GetTestFloorGenerator(string seed)
    {
        GameObject fm = GameObject.Instantiate(new GameObject("FileManager"));
        fm.AddComponent<FileManager>();
        Variables.floorSeed = seed;
        Variables.newGame = true;
        FloorGenerator fg = fm.AddComponent<FloorGenerator>();
        return fg;
    }

    public void CleanScene()
    {
        foreach (GameObject o in UnityEngine.Object.FindObjectsOfType<GameObject>()) {
            GameObject.Destroy(o);
        }
    }

    // Floor Generation Tests
    [UnityTest]
    public IEnumerator GenerateFloorSeedTest()
    {
        FloorGenerator fg = GetTestFloorGenerator("test");
        Floor f1 = fg.GetCurrFloor();
        Assert.AreEqual("1test",f1.seed);
        yield return new WaitForSeconds(0f);
        CleanScene();
    }

    [UnityTest]
    public IEnumerator GenerateFloorShortSeedTest()
    {
        FloorGenerator fg = GetTestFloorGenerator("f");
        Floor f1 = fg.GetCurrFloor();  
        Assert.AreEqual("1f1f1f",f1.seed);
        yield return new WaitForSeconds(0f);
        CleanScene();
    }

    [UnityTest]
    public IEnumerator GenerateFloorNumRoomsTest()
    {
        FloorGenerator fg = GetTestFloorGenerator("test");
        Floor f1 = fg.GetCurrFloor();
        Assert.AreEqual(10,f1.rooms.Length);
        yield return new WaitForSeconds(0f);
        CleanScene();
    }

    [UnityTest]
    public IEnumerator GenerateFloorLayoutSizeTest()
    {
        FloorGenerator fg = GetTestFloorGenerator("test");
        Floor f1 = fg.GetCurrFloor();
        Assert.That(f1.floorLayout.Length, Is.GreaterThan(9));
        yield return new WaitForSeconds(0f);
        CleanScene();
    }

    [UnityTest]
    public IEnumerator GenerateFloorLayoutValuesTest()
    {
        FloorGenerator fg = GetTestFloorGenerator("test");
        Floor f1 = fg.GetCurrFloor();
        int[,] layout = f1.floorLayout;
        bool correctLayout = true;
        for(int i = 0; i<layout.GetLength(0);i++)
        {
            for(int j = 0; j<layout.GetLength(1);j++)
            {
                if(layout[i,j]<0 || layout[i,j]>10)
                {
                    correctLayout = false;
                    break;
                }
            }
        }
        Assert.That(correctLayout);
        yield return new WaitForSeconds(0f);
        CleanScene();
    }

    // GetDirectionsFromExitString Tests
    [UnityTest]
    public IEnumerator GetDirFromExitStringOutputTest()
    {
        FloorGenerator fg = GetTestFloorGenerator("test");
        string[] test = {"north","east","west","south"};
        Assert.AreEqual(fg.GetDirectionsFromExitString("news"), test);
        yield return new WaitForSeconds(0f);
        CleanScene();
    }

    [UnityTest]
    public IEnumerator GetDirFromExitStringInvalidInputTest()
    {
        FloorGenerator fg = GetTestFloorGenerator("test");
        string[] test = {"north","east","west","south",null,null,null,"east",null};
        Assert.AreEqual(fg.GetDirectionsFromExitString("newspaper"), test);
        yield return new WaitForSeconds(0f);
        CleanScene();
    }

    // GetRoomExits Tests
    [UnityTest]
    public IEnumerator GetRoomExitsOutputTest()
    {
        FloorGenerator fg = GetTestFloorGenerator("test");
        int[,] layout = {{1,2},{3,0}};
        string[,] test = {{"es","w"},{"n","0"}};
        Assert.AreEqual(fg.GetRoomExits(layout), test);
        yield return new WaitForSeconds(0f);
        CleanScene();
    }
}
