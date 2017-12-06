using UnityEngine;
using System.Collections;

public class destroyOnLoad : MonoBehaviour {

    public string tagString;

	// if the object's name is present in the tagList, destroy this object
	void Start () {
	    if (GameState.tagList.Contains(tagString))
            Destroy(gameObject);
    }
}
