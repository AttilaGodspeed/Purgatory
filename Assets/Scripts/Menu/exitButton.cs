using UnityEngine;
using System.Collections;

public class exitButton : MonoBehaviour {
    // on click behaviour, exits the game
    void OnMouseUpAsButton() {
        Application.Quit();
    }
}
