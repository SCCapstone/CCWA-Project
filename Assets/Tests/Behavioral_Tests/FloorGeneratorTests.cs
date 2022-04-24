using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FloorGeneratorTests {

    // A Test behaves as an ordinary method
    [UnityTest]
    public IEnumerator GenerateFloorTest()
    {
        // Use the Assert class to test conditions
        GameObject fm = GameObject.Instantiate(new GameObject("FileManager"));
        fm.AddComponent<FileManager>();
        Variables.floorSeed = "test";
        Variables.newGame = true;
        FloorGenerator fg = fm.AddComponent<FloorGenerator>();
        yield return new WaitForSeconds(0.2f);
        Floor f1 = fg.GetCurrFloor();
        
        Assert.AreEqual("1test",f1.seed);
    }
}
