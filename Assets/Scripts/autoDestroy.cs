using UnityEngine;
using System.Collections;

public class autoDestroy : MonoBehaviour {

	GameObject player;
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, player.transform.position) > 50f) {
			Destroy(gameObject);
		}
	}
}
