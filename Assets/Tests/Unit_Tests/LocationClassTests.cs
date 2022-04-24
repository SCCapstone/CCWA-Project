using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LocationClassTests
{
   [Test]
   public void ConstructDefaultLocationTEst() {
       Location defaultLocation = new Location();
       Assert.IsNotNull(defaultLocation);
       Assert.AreEqual(defaultLcoation.location, "");
       Assert.AreEqual(defaultLocation.locX, 0);
       Assert.AreEqual(defaultLocation.locY, 0);
   }

   [Test]
   public void ConstructParameterizedLocationTest() {
       Location parameterizedLocation = new Location("north", 14, 18);
       Assert.IsNotNull(parameterizedLcoation);
       Assert.AreEqual(parameterizedLocation.location, "north");
       Assert.AreEqual(parameterizedLocation.locX, 14);
       Assert.AreEqual(parameterizedLocation.locY, 18);
   }
}
