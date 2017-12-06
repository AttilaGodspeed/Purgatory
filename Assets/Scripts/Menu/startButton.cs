using UnityEngine;

public class startButton : MonoBehaviour {

    void Start() {
        // initialize/whipe taglist
        GameState.tagList = "";
    }

    // on click behaviour, loads Purgatory
    void OnMouseUpAsButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
