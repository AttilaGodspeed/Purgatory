using UnityEngine;

// Holds information for an event, decoded by EventManager
public class Event : ScriptableObject {
    public string description, opCode;
    public bool advance;
    public int paramA, paramB, paramC;
    public string stringParam;
}
