using UnityEngine;

public class answerButton : MonoBehaviour {

    // boo to indicate which answer box this is (true = left)
    public bool answerValue;
    public GameObject dialogueManager;
    
    // works only if in question mode
    void OnMouseUp() { // AsButton() {
        if (GameState.inQuestion) {
            dialogueManager.GetComponent<dialogueManager>().close();
            // Get index of event corresponding to the answer chosen
            int temp;
            if (answerValue)
                temp = EventManager.activeEvent.paramB;
            else
                temp = EventManager.activeEvent.paramC;
            // activate that event
            GameState.eventObject.GetComponent<EventList>().triggerAt(temp);
        }
    }
}