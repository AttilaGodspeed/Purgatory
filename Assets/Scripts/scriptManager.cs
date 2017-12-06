using UnityEngine;

public class scriptManager : MonoBehaviour {

    public TextAsset textFile;
    public string[] scriptArray;
    private string[] eventLine;

	// initialize list of event scripts, uses Awake to execute before any Start functions
	void Awake () {
        print("Script manager loading " + textFile.name + "...");
        if (textFile != null) {
            scriptArray = textFile.text.Split('\n');
            //print("Loading " + Application.dataPath + "/Events/" + textFile.name + ".txt");
            //int temp = System.IO.File.ReadAllLines(Application.dataPath + "/Events/" + textFile.name + ".txt").Length;
            //print(textFile.name + ".txt has " + temp + " lines.");
            //scriptArray = new string[temp];
            //scriptArray = System.IO.File.ReadAllLines(Application.dataPath + "/Events/" + textFile.name + ".txt");
        }
        print("Script manager loaded " + scriptArray.Length + " Events.");
    }
	
    public Event getEvent(int line) {
        eventLine = scriptArray[line].Split('#');

        Event tempEvent = Event.CreateInstance<Event>();//new Event();
        int tempInt;

        tempEvent.description = eventLine[0];
        tempEvent.opCode = eventLine[1];

        if (eventLine[2] == "true")
            tempEvent.advance = true;
        else
            tempEvent.advance = false;

        int.TryParse(eventLine[3], out tempInt);
        tempEvent.paramA = tempInt;

        int.TryParse(eventLine[4], out tempInt);
        tempEvent.paramB = tempInt;

        int.TryParse(eventLine[5], out tempInt);
        tempEvent.paramC = tempInt;

        tempEvent.stringParam = eventLine[6];

        return tempEvent;
    }
}
