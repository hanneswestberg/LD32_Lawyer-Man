using UnityEngine;
using System.Collections;

public class paperLoot : MonoBehaviour {

	GameObject player;
	public int paperCol;
	public AudioClip aClip;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("player");
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject == player) {
			player.GetComponent<playerActions>().numberOfPapers[paperCol]++;
			player.GetComponent<AudioSource>().clip = aClip;
			player.GetComponent<AudioSource>().Play();
			Destroy(gameObject);
		}
	}
}
