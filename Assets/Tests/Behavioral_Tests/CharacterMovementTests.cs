using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CharacterMovementTests {

    [UnityTest]
    public IEnumerator PlayerMovementTest()
    {
        GameObject test = GameObject.Instantiate(new GameObject());
        PlayerMovement testMove = test.AddComponent<PlayerMovement>();
        Rigidbody2D rb = test.AddComponent<Rigidbody2D>();

        // Assign rb to player
        testMove.rb = rb;

        // Obtain the initial position for the player
        Vector2 originalPosition = testMove.rb.position;

        // Facilitate a "key press" by moving the player upwards.
        // There is not a clear way to send a keystroke in the Unity API
        // So, setting the movement to up is the closest we can get
        testMove.movement = Vector2.up;

        // Wait for change
        yield return new WaitForSeconds(0.2f);

        // Obtain the final position for the character
        Vector2 finalPosition = testMove.rb.position;

        // Ensure that the original and final positions are not equal
        Assert.AreNotEqual(originalPosition, finalPosition, "The character has moved from " + originalPosition + " to " + finalPosition);
    }

    [UnityTest]
    public IEnumerator PlayerMovementTestDown() {
        GameObject test = GameObject.Instantiate(new GameObject());
        PlayerMovement testMove = test.AddComponent<PlayerMovement>();
        Rigidbody2D rb = test.AddComponent<Rigidbody2D>();

        // Assign rb to player
        testMove.rb = rb;

        // Obtain the initial position for the player
        Vector2 originalPosition = testMove.rb.position;

        // Facilitate a "key press" by moving the player upwards.
        // There is not a clear way to send a keystroke in the Unity API
        // So, setting the movement to down is the closest we can get
        testMove.movement = Vector2.down;

        // Wait for change
        yield return new WaitForSeconds(0.2f);

        // Obtain the final position for the character
        Vector2 finalPosition = testMove.rb.position;

        // Ensure that the original and final positions are not equal
        Assert.AreNotEqual(originalPosition, finalPosition, "The character has moved from " + originalPosition + " to " + finalPosition);
    }

    [UnityTest]
    public IEnumerator PlayerMovementTestLeft() {
        GameObject test = GameObject.Instantiate(new GameObject());
        PlayerMovement testMove = test.AddComponent<PlayerMovement>();
        Rigidbody2D rb = test.AddComponent<Rigidbody2D>();

        // Assign rb to player
        testMove.rb = rb;

        // Obtain the initial position for the player
        Vector2 originalPosition = testMove.rb.position;

        // Facilitate a "key press" by moving the player upwards.
        // There is not a clear way to send a keystroke in the Unity API
        // So, setting the movement to left is the closest we can get
        testMove.movement = Vector2.left;

        // Wait for change
        yield return new WaitForSeconds(0.2f);

        // Obtain the final position for the character
        Vector2 finalPosition = testMove.rb.position;

        // Ensure that the original and final positions are not equal
        Assert.AreNotEqual(originalPosition, finalPosition, "The character has moved from " + originalPosition + " to " + finalPosition);
    }

    [UnityTest]
    public IEnumerator PlayerMovementTestRight() {
        GameObject test = GameObject.Instantiate(new GameObject());
        PlayerMovement testMove = test.AddComponent<PlayerMovement>();
        Rigidbody2D rb = test.AddComponent<Rigidbody2D>();

        // Assign rb to player
        testMove.rb = rb;

        // Obtain the initial position for the player
        Vector2 originalPosition = testMove.rb.position;

        // Facilitate a "key press" by moving the player upwards.
        // There is not a clear way to send a keystroke in the Unity API
        // So, setting the movement to right is the closest we can get
        testMove.movement = Vector2.right;

        // Wait for change
        yield return new WaitForSeconds(0.2f);

        // Obtain the final position for the character
        Vector2 finalPosition = testMove.rb.position;

        // Ensure that the original and final positions are not equal
        Assert.AreNotEqual(originalPosition, finalPosition, "The character has moved from " + originalPosition + " to " + finalPosition);
    }
}
