using UnityEngine;
using System.Collections;

public class mobAI : MonoBehaviour {

	// References
	GameObject player;
	Rigidbody2D rig;
	string myName;
	bool isAlive = true;
	playerActions playerAct;

	//Variables
	public int enemyCol = 0;
	public AudioClip[] audi;
	AudioSource audioS;
	bool playerIsNear = false;
	bool chase = false;
	public float maxSpeed = 3f;
	private Vector3 dir;
	Vector3 playerPosition;
	Quaternion headRotation;
	float headDegree;
	bool walkingAway = false;
	bool standing = false;
	bool waiting = false;
	Animator anim;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("player");
		rig = gameObject.GetComponent<Rigidbody2D> ();
		playerAct = player.GetComponent<playerActions> ();
		myName = transform.parent.GetChild (0).GetComponent<mobName> ().myName;
		anim = transform.GetChild(1).GetComponent<Animator> ();
		audioS = gameObject.GetComponent<AudioSource> ();
		maxSpeed = Mathf.Clamp (Random.Range (2f, 3f) + GameObject.Find ("Mob spawner").GetComponent<mobSpawner> ().elapsedTime * (1f/50f), 2f, 8f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		isAlive = player.GetComponent<playerActions> ().IsAlive;
		// 5 different stages!
		// prio: 1 - player is dead, walk away, call WalkAway
		// 2 - player has given out the right paper, call WalkAway
		// 3 - player has given out the wrong paper, call RipPaper
		// 4 - attack the player, enemy is very close, call Attack
		// 5 - chase the player, call Chase

		if (isAlive == true){
			chase = false;
			if (playerIsNear == true){ // Player is near
				if(playerAct.playerHasPaper == true ){ // Player has a paper, we need to check if it's the correct one
					if(playerAct.playerString.ToLower() == myName.ToLower() && playerAct.paperColor == enemyCol){// The paper was correct, we walk away
						int number = Random.Range(1,5);
						audioS.clip = audi [number];
						audioS.Play ();
						WalkAway(true);
						playerAct.playerHasPaper = false;
						standing = true;
						anim.SetTrigger ("ReadPaper");
					}
					else{//The paper was not correct, we rip it
						int number = Random.Range(1,5);
						audioS.clip = audi [number];
						audioS.Play ();
						RipPaper();
						standing = true;
						playerAct.playerHasPaper = false;
						anim.SetTrigger ("ReadPaper");
					}
				}
				else if (standing == false){// Player is not holding a paper, we can attack
					Attack();
					standing = true;
					player.GetComponent<playerActions>().IsAlive = false;
				}
			}
			else if (walkingAway == false && standing == false){ // The player is not near, we need to chase him
				chase = true;
			}
		}
		else if (walkingAway == false && waiting == false){ // Player is dead
			WalkAway(false);
		}

		if (walkingAway == true && Vector3.Distance (transform.position, player.transform.position) > 50f) {
			Destroy(transform.parent.gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			playerIsNear = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			playerIsNear = false;
		}
	}

	void WalkAway(bool gotSued){
		StartCoroutine (WaitFunc (2f, gotSued));
	}

	void RipPaper(){
		StartCoroutine (RipFunc (2f));
	}

	void Attack()
	{
		audioS.clip = audi [9];
		audioS.Play ();
		player.GetComponent<playerMovement> ().DeathMov (dir);
		anim.SetTrigger ("Attack");

	}

	void FixedUpdate(){
		if (chase == true) {
			playerPosition = player.transform.position;
			dir = (playerPosition - gameObject.transform.position);
			headDegree = Vector2.Angle (Vector2.up, dir.normalized);
			if (dir.normalized.x > 0)
				headDegree = -headDegree;
			
			headRotation = Quaternion.Euler (0, 0, headDegree);
			rig.velocity = Vector2.Lerp (rig.velocity, dir.normalized * maxSpeed, 0.3f);
			gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, headRotation, Time.deltaTime * 6f);
		} 
		else if (walkingAway == true) {
			playerPosition = player.transform.position;
			dir = (gameObject.transform.position - playerPosition);
			headDegree = Vector2.Angle (Vector2.up, dir.normalized);
			if (dir.normalized.x > 0)
				headDegree = -headDegree;
			headRotation = Quaternion.Euler (0, 0, headDegree);;
			rig.velocity = Vector2.Lerp (rig.velocity, dir.normalized * maxSpeed, 0.3f);
			gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, headRotation, Time.deltaTime * 6f);
		}
		else {
			rig.velocity = Vector2.Lerp (rig.velocity, Vector2.zero, 0.3f);
		}
		// Animation
		anim.SetFloat ("Movement", rig.velocity.magnitude);
	}

	IEnumerator WaitFunc(float waitTime, bool gotSued)
	{
		chase = false;
		waiting = true;
		yield return new WaitForSeconds (waitTime);
		walkingAway = true;
		maxSpeed = 6f;
		standing = false;
		if (gotSued == true){
			GameObject.Find("_GameManager").GetComponent<gameMan>().AddScore();
			int number = Random.Range(6,9);
			audioS.clip = audi [number];
			audioS.Play ();
		}
	}

	IEnumerator RipFunc(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		anim.SetTrigger ("RipPaper");
		audioS.clip = audi [0];
		audioS.Play ();
		yield return new WaitForSeconds (0.2f);
		standing = false;
	}
}
