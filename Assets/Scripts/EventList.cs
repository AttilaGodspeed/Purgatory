using UnityEngine;

// holds and triggers a sequence of events
public class EventList : MonoBehaviour {
    public GameObject scriptManager;
    public int scriptLineStart, scriptLineEnd;
    public Event[] eventList;
    public int currentEvent;
    // list of game bjects that can be called using a triggerLink or triggerLinkAt Event
    public GameObject[] links;

    // if not null, jump to this object and trigger events there instead
    //public GameObject handOff;

    // load events if given a script manager
    void Start() {
        print(gameObject.name + " event list loading...");
        if (scriptManager != null) {
            eventList = new Event[scriptLineEnd - scriptLineStart + 1];
            for (int i = 0; i < eventList.Length; i++) {
                eventList[i] = scriptManager.GetComponent<scriptManager>().getEvent(scriptLineStart + i);
                //print("Loaded event " + i + " of " + (eventList.Length - 1) + ": " + eventList[i].description);
            }
            print(gameObject.name + " event list loaded.");
        }
    }
    

    public void trigger() {
       // if (handOff != null) {
       //     print("Handing off from " + gameObject.name + " to " + handOff.name);
       //     handOff.GetComponent<EventList>().trigger();
       // //}
       // else {
            int temp = currentEvent;
            if (eventList[currentEvent].advance)
                currentEvent++;
            print("Triggering event " + currentEvent + " for " + gameObject.name);
            EventManager.manageEvent(eventList[temp]);
       // }
    }

    public void triggerAt(int index) {
        //if (handOff != null) {
        //    print("Handing off from " + gameObject.name + " to " + handOff.name);
        //    handOff.GetComponent<EventList>().triggerAt(index);
        //}
        //else {
        if (eventList[index].advance)
            currentEvent = index + 1;
        else
            currentEvent = index;
        print("Triggering event " + index + " for " + gameObject.name);
        EventManager.manageEvent(eventList[index]);
        //}
    }

    public void triggerLink(int index) {
        GameState.eventObject = links[index];
        links[index].GetComponent<EventList>().trigger();
    }

    public void triggerLinkAt(int index, int eventNumber) {
        GameState.eventObject = links[index];
        links[index].GetComponent<EventList>().triggerAt(eventNumber);
    }

    /*
    // overrides the hand-off and then calls trigger
    public void overrideAndTrigger() {
        print("Making handOff null for " + gameObject.name);
        handOff = null;
        if (handOff == null)
            print("HandOff is null.");
        trigger();
    }
    */
}
