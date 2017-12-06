using UnityEngine;

public class moveManager : MonoBehaviour {

    public bool active, loop;
    // movespeed is very fast, 0.05 is slow and gradual
    public float moveSpeed;
    public Vector3[] waypoints;

    private int currentWaypoint;
    private Vector3 temp;
    private float dx, dy;

	// Use this for initialization
	void Start () {
        currentWaypoint = 0;
	}

    // stops all movement, freezes in place
    public void stop() {
        active = false;
        // if there's an animator, tell it to stop
        var animator = gameObject.GetComponent<Animator>();
        if (animator != null) {
            animator.SetFloat("xSpeed", 0);
            animator.SetFloat("ySpeed", 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // only do stuff if active
        if (active) {
            // is the object at the destination?
            // move to it if not
            if (transform.position != waypoints[currentWaypoint]) {
                temp = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint], moveSpeed);

                // if there is an attached animator figure out which values to send to it
                var animator = gameObject.GetComponent<Animator>();
                if (animator != null) {
                    dx = temp.x - transform.position.x;
                    dy = temp.y - transform.position.y;
                    // if there is movement along x axis, default animator to l/r movement
                    if(dx != 0) {
                        animator.SetFloat("xSpeed", (dx));
                        animator.SetFloat("ySpeed", 0);
                    }
                    // otherwise, use the y-axis animations
                    else {
                        animator.SetFloat("xSpeed", 0);
                        animator.SetFloat("ySpeed", (dy));
                    }
                }
                transform.position = temp;
            }
            // else update destination if you can
            else {
                if (currentWaypoint < (waypoints.Length - 1)) {
                    currentWaypoint++;
                }
                else if (loop)
                    currentWaypoint = 0;
                // deactivate movement (and reset counter?) if at last waypoint and not looping
                else {
                    stop();
                    //currentWaypoint = 0;
                }
            }
        }
    }
}
