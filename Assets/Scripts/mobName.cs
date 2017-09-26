using UnityEngine;
using System.Collections;

public class mobName : MonoBehaviour {

	GameObject gameMan;

	public string myName = "";

	void Start() 
	{
		gameMan = GameObject.Find ("_GameManager");
		myName = gameMan.GetComponent<nameLoader> ().getName ().firstName + " " + gameMan.GetComponent<nameLoader> ().getName ().lastName;
		gameObject.GetComponent<TextMesh> ().text = myName;
	}

	void FixedUpdate() 
	{  
		transform.position = transform.parent.GetChild (1).transform.position;
	}
}
