using UnityEngine;
using System.Collections;

public class mapGen : MonoBehaviour {

	public GameObject mapTile;
	public LayerMask mask;

	GameObject player;

	float xmin;
	float ymin;
	float xmax;
	float ymax;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		xmin = Mathf.Ceil(player.transform.position.x -35f);
		ymin = Mathf.Ceil(player.transform.position.y -35f);
		xmax = Mathf.Ceil(player.transform.position.x +35f);
		ymax = Mathf.Ceil(player.transform.position.y +35f);

		for(float xbound = xmin; xbound < xmax; xbound++)
		{			
			for(float ybound = ymin; ybound < ymax; ybound++)
			{
				if (xbound%10 == 0 && ybound%10 == 0)
				{
					if(Physics2D.OverlapCircle(new Vector2(xbound, ybound), 0.2f, mask) == null)
					{
						GameObject newMapTile = (GameObject)GameObject.Instantiate(mapTile, new Vector3(xbound, ybound, 0f), Quaternion.identity);
						newMapTile.transform.parent = transform;
					}
				}
			}
		}
	}
}
