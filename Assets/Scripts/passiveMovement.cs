using UnityEngine;
using System.Collections;

// passivly updates attached animator proporties as its moved around
public class passiveMovement : MonoBehaviour {

    private Vector3 lastPosition;
    private float dx, dy;

	// Use this for initialization
	void Start () {
        lastPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // calculate change in position since last frame
        dx = gameObject.transform.position.x - lastPosition.x;
        dy = gameObject.transform.position.y - lastPosition.y;

        var animator = gameObject.GetComponent<Animator>();

        // if there is movement along x axis, default animator to l/r movement
        if (dx != 0) {
            animator.SetFloat("xSpeed", (dx));
            animator.SetFloat("ySpeed", 0);
        }
        // otherwise, use the y-axis animations
        else {
            animator.SetFloat("xSpeed", 0);
            animator.SetFloat("ySpeed", (dy));
        }

        // update lastPosition
        lastPosition = gameObject.transform.position;
    }
}
