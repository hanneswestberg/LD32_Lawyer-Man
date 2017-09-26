using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

	public GUIManager guiboss;
	public LayerMask whatIsCursorCollider;
	public float maxSpeed = 1f;
	public bool isAlive = true;

	private Vector3 cursorPosition;
	private Vector3 playerPosition;
	private Vector3 dir;

	Quaternion headRotation;
	float headDegree;

	float moveVer = 0f;
	float moveHor = 0f;
	Vector2 moveNorm;

	Rigidbody2D rig;
	Animator anim;

	void Start () {
		rig = GetComponent<Rigidbody2D> ();
		anim = transform.GetChild(0).GetComponent<Animator> ();
	}

	void Update(){
		isAlive = GetComponent<playerActions> ().IsAlive;
		playerPosition = gameObject.transform.position;

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray,out hit, whatIsCursorCollider))
			cursorPosition = hit.point;

		dir = (cursorPosition - playerPosition);

		headDegree = Vector2.Angle(Vector2.up, dir.normalized);
		
		if (dir.normalized.x > 0)
			headDegree = -headDegree;

		headRotation = Quaternion.Euler (0, 0, headDegree);

		// Animation
		anim.SetFloat ("Movement", rig.velocity.magnitude);
		anim.SetBool ("IsAlive", isAlive);
	}

	void FixedUpdate () {
		if (isAlive && guiboss.paperIsShowing == false) {
			moveHor = Input.GetAxis ("Horizontal");
			moveVer = Input.GetAxis ("Vertical");
			moveNorm = new Vector2 (moveHor, moveVer);
			if (Mathf.Abs (moveHor) == 1f && Mathf.Abs (moveVer) == 1f) {
				moveNorm = new Vector2 (moveHor, moveVer).normalized;				
			}
			gameObject.transform.rotation = Quaternion.Lerp (transform.rotation, headRotation, Time.deltaTime * 6f);
			rig.velocity = Vector2.Lerp(rig.velocity, new Vector2 (moveNorm.x * maxSpeed, moveNorm.y * maxSpeed), 0.3f);
			
		} else if (guiboss.paperIsShowing == true) {
			rig.velocity = Vector2.Lerp(rig.velocity, Vector2.zero, 0.02f);
		} else {
			moveHor = 0;
			moveVer = 0;
			moveNorm = new Vector2(moveHor, moveVer);
			rig.velocity = Vector2.Lerp(rig.velocity, new Vector2 (moveNorm.x * maxSpeed, moveNorm.y * maxSpeed), 0.3f);
		}
	}

	public void DeathMov(Vector3 dir)
	{
		moveHor = dir.x;
		moveVer = dir.y;
		headDegree = Vector2.Angle(Vector2.up, dir.normalized);
		
		if (dir.normalized.x > 0)
			headDegree = -headDegree;
		
		headRotation = Quaternion.Euler (0, 0, headDegree);
		gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, headRotation, Time.deltaTime * 6f);
	}
}
