using UnityEngine;

public class EventManager {
    // the dialogue manger should set this to it's home GameObject when it's initialised
    public static GameObject dialogueManager;

    // the event currently being triggered
    public static Event activeEvent;

    public static void manageEvent(Event _activeEvent) {
        activeEvent = _activeEvent;
        switch (activeEvent.opCode.ToLower()) {

            // add the stringParam as a tag to the taglist
            case "addtag":
                // paramA = not used
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                // stringParam = tag to add to tagList
                GameState.tagList = GameState.tagList + " " + activeEvent.stringParam;
                MonoBehaviour.print("Current Tags: " + GameState.tagList);
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // prints stringParam to the console
            case "console":
                // paramA = not used
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                // stringParam = printed to the console
                MonoBehaviour.print(activeEvent.stringParam);
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // opens up a dialogue
            case "dialogue":
                // paramA = start line of dialogue
                // paramB = end line of dialogue
                // paramC = take action after dialogue? ( value > 0 = true )
                dialogueManager.GetComponent<dialogueManager>().openDialogue();
                break;

            // disables collision on the event object
            case "disablecollision":
                // paramA = not used
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                GameState.eventObject.GetComponent<BoxCollider2D>().enabled = false;
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // change the currentEvent tracker in the object's eventList
            case "goto":
                // paramA = which event in list to go to next
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                GameState.eventObject.GetComponent<EventList>().currentEvent = activeEvent.paramA;
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;
            
            // change currentEvent if stringParam present in tags list, go to next otherwise
            // advance should always be true for this command.
            case "gotoontag":
                // paramA = which event in list to go if tag found
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                // stringParam = tag to search for
                if (GameState.tagList.IndexOf(activeEvent.stringParam) > -1)
                    GameState.eventObject.GetComponent<EventList>().currentEvent = activeEvent.paramA;
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // like gotoOnTag, but branches if the tag is NOT present
            case "gotonotontag":
                if (GameState.tagList.IndexOf(activeEvent.stringParam) <= -1)
                    GameState.eventObject.GetComponent<EventList>().currentEvent = activeEvent.paramA;
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break; 

            // branch based on moral points
            case "judge":
                // paramA = event to go to if judged good
                // paramB = event to go to if judged bad
                // paramC = not used (yet)
                if(GameState.badPoints > GameState.goodPoints)
                    GameState.eventObject.GetComponent<EventList>().currentEvent = activeEvent.paramB;
                else
                    GameState.eventObject.GetComponent<EventList>().currentEvent = activeEvent.paramA;
                GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // load another scene by it's index
            case "loadscene":
                // paramA = index of scene to load
                // paramB = not used
                // paramC = not used
                //UnityEngine.SceneManagement.SceneManager.LoadScene(activeEvent.stringParam);
                UnityEngine.SceneManagement.SceneManager.LoadScene(activeEvent.paramA);
                break;

            // adds to moral point totals
            case "moralpoints":
                // paramA = good moral points to add
                // paramB = bad moral points to add
                // paramC = take another action? ( value > 0 = true )
                GameState.goodPoints += activeEvent.paramA;
                GameState.badPoints += activeEvent.paramB;
                MonoBehaviour.print("Added " + (activeEvent.paramA - activeEvent.paramB) + " points.");
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // play audio clip attached to the event object
            case "playaudio":
                // paramA = not used
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                GameState.eventObject.GetComponent<AudioSource>().Play();
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // removes a tag from the taglist
            case "removetag":
                // paramA = not used
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                // stringParam = tag to remove
                GameState.tagList.Replace(activeEvent.stringParam, "");
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // activates an object's movement behaviour
            case "startmove":
                // paramA = not used
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                GameState.eventObject.GetComponent<moveManager>().active = true;
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // deactivates/freezes an object's movment behaviour
            case "stopmove":
                // paramA = not used
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                GameState.eventObject.GetComponent<moveManager>().stop();
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // do nothing, will not call an additional action (but advance will still have an effect)
            case "stop":
                // params = not used
                break;

            // teleports the event object to a new position
            case "teleport":
                // paramA = x-coordinate
                // paramB = y-coordinate
                // paramC = take another action? ( value > 0 = true )
                GameState.eventObject.transform.position = new Vector3(activeEvent.paramA, activeEvent.paramB, 0);
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;
            
            // teleports the player object to a new position
            case "teleportplayer":
                // paramA = x-coordinate
                // paramB = y-coordinate
                // paramC = take another action? ( value > 0 = true )
                GameState.player.transform.position = new Vector3(activeEvent.paramA, activeEvent.paramB, 0);
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // Toggles the linked object's sprite renderer
            case "togglelinkvisible":
                // paramA = index of object in links array
                // paramB = not used
                // paramC = take another action? ( value > 0 = true )
                GameObject temp = GameState.eventObject.GetComponent<EventList>().links[activeEvent.paramA];
                temp.GetComponent<SpriteRenderer>().enabled = !temp.GetComponent<SpriteRenderer>().enabled;
                if (activeEvent.paramC > 0)
                    GameState.eventObject.GetComponent<EventList>().trigger();
                break;

            // Triggers another object contained in the even object's links list, is a behaviour hand-off
            case "triggerlink":
                // paramA = index of object in links array
                // paramB = not used
                // paramC = not used
                GameState.eventObject.GetComponent<EventList>().triggerLink(activeEvent.paramA);
                break;
            
            // Triggers specific Event on another object containind in "links," is a behaviour hand-off
            case "triggerlinkat":
                // paramA = index of object in links array
                // paramB = index of event to trigger in target EventList
                // paramC = not used
                GameState.eventObject.GetComponent<EventList>().triggerLinkAt(activeEvent.paramA, activeEvent.paramB);
                break;

            // opens a question dialogue
            case "question":
                // paramA = question dialogue start line
                // paramB = index of event for left answer
                // paramC = index of event for right answer
                dialogueManager.GetComponent<dialogueManager>().openQuestion();
                break;
        }
    }
}
