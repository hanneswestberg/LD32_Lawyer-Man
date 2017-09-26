using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameMan : MonoBehaviour {

	playerActions playAct;
	GUIManager guiboss;

	GameObject player;
	public Transform mobHolder;
	public Transform lootHolder;
	public Text score;
	public Text sueText;
	public Button startButt;
	public AudioClip[] audi;
	bool showRestart = false;
	float currentScore = 0f;
	int numberOfSues = 0;

	void Start () 
	{
		player = GameObject.Find ("player");
		playAct = player.GetComponent<playerActions> ();
		guiboss = transform.GetChild(0).GetComponent<GUIManager> ();
		Time.timeScale = 0f;
	}

	public void StartGame()
	{
		transform.GetChild(3).GetComponent<AudioSource>().clip = audi [0];
		transform.GetChild(3).GetComponent<AudioSource>().Play ();
		Time.timeScale = 1f;
		startButt.GetComponent<Button>().enabled = false;
		startButt.GetComponent<Image>().enabled = false;
		sueText.GetComponent<Text>().enabled = true;
		StartCoroutine (TextWait ());
	}

	public void PauseGame()
	{
		Time.timeScale = 0f;
		startButt.GetComponent<Button>().enabled = true;
		startButt.GetComponent<Image>().enabled = true;
	}

	public void ToggleSound()
	{
		if (Camera.main.GetComponent<AudioListener> ().enabled == true) {
			Camera.main.GetComponent<AudioListener> ().enabled = false;
		} else {
			Camera.main.GetComponent<AudioListener> ().enabled = true;
		
		}
	}

	public void ToggleMusic()
	{
		if (gameObject.GetComponent<AudioSource> ().enabled == true) {
			gameObject.GetComponent<AudioSource> ().enabled = false;
		} else {
			gameObject.GetComponent<AudioSource> ().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playAct.IsAlive == false && showRestart == false) {
			guiboss.ToggleRestartMess();
			showRestart = true;
		}
		score.text = "Score: " + currentScore;
	}

	public void RestartGame()
	{
		foreach (Transform mob in mobHolder) Destroy(mob.gameObject);
		foreach (Transform loot in lootHolder) Destroy(loot.gameObject);
		guiboss.ToggleRestartMess();
		showRestart = false;
		player.transform.position = Vector3.zero;
		player.transform.rotation = Quaternion.identity;
		playAct.IsAlive = true;
		player.transform.GetChild(0).GetComponent<Animator> ().SetTrigger ("GameRestart");
		for (int i = 0; i < 4; i++)
			playAct.numberOfPapers[i] = 8;
		currentScore = 0;
		numberOfSues = 0;
		PauseGame ();
		transform.GetChild (1).GetComponent<mobSpawner> ().elapsedTime = 0f;
		transform.GetChild (2).GetComponent<lootSpawner> ().elapsedTime = 0f;
	}

	public void AddScore()
	{
		numberOfSues++;
		currentScore += 100 * numberOfSues;
	}

	IEnumerator TextWait()
	{
		yield return new WaitForSeconds (3f);
		sueText.GetComponent<Text>().enabled = false;
	}
}
