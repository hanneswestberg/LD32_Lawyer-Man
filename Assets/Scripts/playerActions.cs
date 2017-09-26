using UnityEngine;
using System.Collections;

public class playerActions : MonoBehaviour {

	public GUIManager guiboss;
	public bool playerHasPaper = false;
	public int paperColor = 0;
	public string playerString = "";
	public bool IsAlive = true;
	Animator anim;

	int selectedPaper = 0;

	public int[] numberOfPapers;
	
	// Use this for initialization
	void Start () 
	{
		anim = transform.GetChild(0).GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Jump") && IsAlive)
		{
			if (numberOfPapers[selectedPaper] > 0)
			    guiboss.ShowBigPaper(selectedPaper);
		}
		else if(Input.GetButtonDown("Submit") && IsAlive)
		{
			playerString = guiboss.HideBigPaper(selectedPaper);
			if (playerString != "")
			{
				playerHasPaper = true;
				paperColor = selectedPaper;
				numberOfPapers[selectedPaper]--;
			}
		}
		else if(Input.GetButtonDown("Cancel"))
		{
			guiboss.HideBigPaper(selectedPaper);
		}

		if (Input.GetKeyDown (KeyCode.Alpha1) && guiboss.paperIsShowing == false) {
			selectedPaper = 0;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha2) && guiboss.paperIsShowing == false) {
			selectedPaper = 1;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha3) && guiboss.paperIsShowing == false) {
			selectedPaper = 2;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4) && guiboss.paperIsShowing == false) {
			selectedPaper = 3;
		}

		if (Input.GetButtonDown("Jump") && IsAlive == false)
		{
			guiboss.transform.parent.GetComponent<gameMan>().RestartGame();
		}

		// Animation
		anim.SetBool ("HoldingPaper", playerHasPaper);
		guiboss.SelectedPaper (selectedPaper);
		
	}
}
