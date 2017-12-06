using UnityEngine;

public class GameState {
    // Which scene you are in.
    public static string scene;
    // is movement locked, player colliding with anything, or dialogue/question mode engaged?
    public static bool moveLocked, playerCollision, inDialogue, inQuestion;
    // the player's GameObject
    public static GameObject player;
    // what the player last collided with
    public static GameObject collision;
    // the object who's events are being activated
    public static GameObject eventObject;
    

    // your good and bad points
    public static int goodPoints, badPoints;

    // string of combined tags objects can use to track occurances of things
    public static string tagList;
}