using UnityEngine;
using System.Collections;

public class mobSpawner : MonoBehaviour {

	GameObject player;
	GameObject mobHolder;
	public GameObject[] mobs;

	float waitTime = 1f;
	public float elapsedTime = 0f;
	Vector3 spawnPos;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("player").gameObject;
		mobHolder = GameObject.Find ("Mobholder").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		elapsedTime += Time.deltaTime;
		waitTime -= Time.deltaTime;
		if (waitTime < 0 && player.GetComponent<playerActions>().IsAlive == true) {
			SpawnMob ();
			float value = Random.Range(5f, 9f)-elapsedTime*(1f/40f);
			waitTime = Mathf.Clamp(value, 3f, 10f);
		}
	}

	void SpawnMob()
	{
		bool canSpawn = false;
		while (canSpawn == false)
		{
			Vector2 spawnCirkle = Random.insideUnitCircle * 35;
			if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), spawnCirkle) > 25f)
			{
				canSpawn = true;
				int number = Random.Range (0, 4);
				spawnPos = new Vector3 (spawnCirkle.x, spawnCirkle.y, 0f);
				GameObject enemy = (GameObject)GameObject.Instantiate (mobs[number], spawnPos, Quaternion.identity);
				enemy.transform.parent = mobHolder.transform;
			}
		}
	}
	
}
