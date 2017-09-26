using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GameObject[] papers;
	public GameObject[] big_papers;
	public InputField[] myTexts;
	public AudioClip[] aud;
	public Text[] paperCount;
	public bool paperIsShowing = false;
	public GameObject stample;
	public Text restartMessage;

	public float slowMoAmount = 0.2f;

	playerActions playerInv;
	string savedString = "";
	int selectedPaper;

	// Use this for initialization
	void Start () 
	{
		playerInv = GameObject.Find ("player").GetComponent<playerActions>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		int i = 0;
		foreach (Text count in paperCount) {
			count.text = playerInv.numberOfPapers[i].ToString();
			i++;
		}

		if (myTexts [selectedPaper].text != savedString && myTexts [selectedPaper].text != "") {
			savedString = myTexts [selectedPaper].text;
			int number = Random.Range(1, 6);
			gameObject.GetComponent<AudioSource> ().clip = aud [number];
			gameObject.GetComponent<AudioSource> ().Play ();
		}
		if (playerInv.IsAlive == false && paperIsShowing == true) {
			HideBigPaper(selectedPaper);
		}
	}

	public void ShowBigPaper(int indx)// 0 = red....
	{
		selectedPaper = indx;
		myTexts[indx].enabled = true;
		big_papers[indx].GetComponent<Image>().enabled = true;
		if (paperIsShowing == false) {
			myTexts [indx].text = "";
			myTexts [indx].ActivateInputField ();
		}
		Time.timeScale = slowMoAmount;
		Time.fixedDeltaTime = 0.02f*slowMoAmount;
		paperIsShowing = true;
	}

	public string HideBigPaper(int indx)
	{
		if (paperIsShowing == true) {
			paperIsShowing = false;
			myTexts [indx].DeactivateInputField ();
			if(myTexts[indx].text != "")
			{
				StartCoroutine(stampleAnim(indx));
			}
			else{
				stample.GetComponent<Image>().enabled = false;
				myTexts[indx].enabled = false;
				myTexts [indx].text = "";
				big_papers[indx].GetComponent<Image>().enabled = false;
				Time.timeScale = 1f;
				Time.fixedDeltaTime = 0.02f;
			}
			return myTexts [indx].text.ToString();
		}
		return "";
	}

	public void SelectedPaper(int indx)// 0 = red....
	{
		foreach (GameObject paper in papers) {
			if(paper == papers[indx])
			{
				papers [indx].GetComponent<RectTransform> ().localScale = Vector3.Lerp(papers [indx].GetComponent<RectTransform> ().localScale, Vector3.one, 0.2f);
				if (selectedPaper != indx)
				{
					selectedPaper = indx;
					gameObject.GetComponent<AudioSource> ().clip = aud [6];
					gameObject.GetComponent<AudioSource> ().Play ();
				}
			}
			else{
				paper.GetComponent<RectTransform> ().localScale = Vector3.Lerp(paper.GetComponent<RectTransform> ().localScale, new Vector3 (0.7f, 0.7f, 0.7f), 0.2f);
			}
		}

	}

	public void ToggleRestartMess()
	{
		if(restartMessage.enabled == false)
			restartMessage.enabled = true;
		else
			restartMessage.enabled = false;
	}

	IEnumerator stampleAnim(int indx)
	{
		gameObject.GetComponent<AudioSource> ().clip = aud [0];
		gameObject.GetComponent<AudioSource> ().Play ();
		stample.GetComponent<Image>().enabled = true;
		stample.GetComponent<Animator>().SetTrigger ("trigg");
		yield return new WaitForSeconds (0.06f);
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f;
		stample.GetComponent<Image>().enabled = false;
		myTexts[indx].enabled = false;
		myTexts [indx].text = "";
		big_papers[indx].GetComponent<Image>().enabled = false;
	}
}
