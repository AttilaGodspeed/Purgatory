using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour {

    public GameObject dialogueBox;
    public Text dialogueText, answer1Text, answer2Text;

    public TextAsset textFile;
    public string[] textArray;

    // trackers for the dialogue text
    private int currentLine, endLine;

    // Load text file and close UI, send reference to EventManager
    void Start () {
        if (textFile != null)
            textArray = textFile.text.Split('\n');
        close();
        EventManager.dialogueManager = gameObject;
    }
    
    // advance or close out a dialogue
    public void advance() {
        // advance the dialogue
        if (currentLine < endLine) {
            currentLine++;
            displayText();
        }
        // close it out if done
        else {
            close();
            // Activate the next event in the list if paramC > 0
            if (EventManager.activeEvent.paramC > 0)
                GameState.eventObject.GetComponent<EventList>().trigger();
        }
    }

    // close out all dialogue UI
    public void close() {
        GameState.inDialogue = false;
        GameState.inQuestion = false;
        GameState.moveLocked = false;
        dialogueBox.SetActive(false);
        dialogueText.text = "";
        answer1Text.text = "";
        answer2Text.text = "";
    }

    // open dialogue box and lock player movement
    private void openLock() {
        dialogueBox.SetActive(true);
        GameState.moveLocked = true;
    }

    // display current line from file into text box
    private void displayText() {
        if (textFile != null && currentLine < textArray.Length)
            dialogueText.text = textArray[currentLine];
        else
            dialogueText.text = "No text loaded or out of bounds.";
    }
    // display answers for a question, now with error checking!
    private void displayAnswers() {
        if (textFile != null && currentLine < (textArray.Length + 2)) {
            answer1Text.text = textArray[currentLine + 1];
            answer2Text.text = textArray[currentLine + 2];
        }
        else {
            answer1Text.text = "No text loaded";
            answer2Text.text = "or out of bounds.";
        }

    }

    public void openDialogue() {
        currentLine = EventManager.activeEvent.paramA;
        endLine = EventManager.activeEvent.paramB;
        GameState.inDialogue = true;
        openLock();
        displayText();
    }

    public void openQuestion() {
        currentLine = EventManager.activeEvent.paramA;
        GameState.inQuestion = true;
        openLock();
        displayText();
        displayAnswers();
    }
}
