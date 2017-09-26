using UnityEngine;
using System.Collections;

public class lootSpawner : MonoBehaviour {

	public GameObject[] loot;
	public GameObject lootHolder;


	GameObject player;
	public float elapsedTime = 0f;
	float waitTime = 1f;

	void Start () 
	{
		player = GameObject.Find ("player").gameObject;
	}
	

	void Update () 
	{
		elapsedTime += Time.deltaTime;
		waitTime -= Time.deltaTime;
		if (waitTime < 0) {
			SpawnLoot();
			float value = Random.Range(5f, 10f)-elapsedTime*(1f/30f);
			waitTime = Mathf.Clamp(value, 2f, 10f);
		}
	}

	void SpawnLoot()
	{
		int number = Random.Range (0, 3);

		bool canSpawn = false;
		while (canSpawn == false)
		{
			Vector2 spawnCirkle = Random.insideUnitCircle * 35;
			if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), spawnCirkle) > 25f)
			{
				canSpawn = true;
				Vector3 spawnPos = new Vector3 (spawnCirkle.x, spawnCirkle.y, 0f);
				Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));

				GameObject theLoot = (GameObject)GameObject.Instantiate (loot[number], spawnPos, rot);
				theLoot.transform.parent = lootHolder.transform;
			}
		}
	}
}
