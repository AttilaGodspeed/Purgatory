using UnityEngine;

// on Tags:
// Game objects should have the "Interactable" tag if the player activates their Events with the interact button.
// They should have the "Collidable" tag if the player activates their Events when they collide with them.

public class playerControl : MonoBehaviour {
    // the manager object for the dialogue manager
    public GameObject gameManager;

    // Controls the speed at which the player moves
    public float playerSpeed;

    private float xAxis, yAxis;

    // Use this for initialization
    void Start () {
        // set behaviors for GameState
        GameState.playerCollision = false;
        GameState.scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        // link self to GameState
        GameState.player = gameObject;
        // initialize taglist if it needs it
        //if (GameState.tagList == null)
        //    GameState.tagList = "";
    }
	
	// Update is called once per frame
	void Update () {
        // Set speed to move player by keypress, if movement not locked by game
        xAxis = 0;
        yAxis = 0;
        if (!GameState.moveLocked) {            
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                xAxis = -playerSpeed;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                xAxis = playerSpeed;
            
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                yAxis = -playerSpeed;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                yAxis = playerSpeed;

            GetComponent<Rigidbody2D>().velocity = new Vector2(xAxis, yAxis);
        }
        GetComponent<Animator>().SetFloat("xSpeed", xAxis);
        GetComponent<Animator>().SetFloat("ySpeed", yAxis);

        // activate event on object if interactable, colliding with it, and space or enter is pressed
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return)) {
            if (GameState.inDialogue)
                gameManager.GetComponent<dialogueManager>().advance();
            else if (!GameState.inQuestion) {
                if (GameState.playerCollision && GameState.collision.tag == "Interactable") {
                    GameState.eventObject = GameState.collision;
                    GameState.collision.GetComponent<EventList>().trigger();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        GameState.playerCollision = true;
        GameState.collision = col.gameObject;
        if (col.gameObject.tag == "Collidable") {
            GameState.eventObject = col.gameObject;
            col.gameObject.GetComponent<EventList>().trigger();
        }
    }
    void OnCollisionExit2D(Collision2D col) {
        GameState.playerCollision = false;
    }
}
