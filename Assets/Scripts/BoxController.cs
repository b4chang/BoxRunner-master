using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour {

	//Force of jump
	public Vector2 jumpForce = new Vector2(0, 150);

	public LayerMask ground_layers; //reference to the layer mask that has the ground
	private Transform groundCheck; //point used to check if player grounded

	private Rigidbody2D thisBody;//rigidbody of player

	private bool doubleJump = true;//double jump

	public float moveSpeed = 3.0f;//Movespeed of player...can change publicly...this should be accessed to change speed of game.

	private int score = 0;
	private int startTime;
	
	void Awake (){
		thisBody = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		groundCheck = transform.Find("GroundCheck");
	}
	
	// Update is called once per frame
	void Update () {

		if (!GameStateManager.Instance.Paused) {

			ScoreManagerScript.Score = score;

			score = (int)(Time.timeSinceLevelLoad);

			MoveOnXAxis ();

			if (IsGrounded () && Input.GetButtonDown ("Jump")) {
				doubleJump = true;
				Jump ();
			} else if (!IsGrounded () && Input.GetButtonDown ("Jump") && doubleJump) {
				doubleJump = false;
				Jump ();
			}
		}
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Obstacle") //pipeblank is an empty gameobject with a collider between the two pipes
		{
			score = 0;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void Jump(){
		thisBody.velocity = Vector2.zero;//linear velocity = 0
		thisBody.AddForce(jumpForce);//adds upward force
	}

	void MoveOnXAxis()
	{
		Vector2 Xvelocity = new Vector2 (0, 0);
		transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
		if (thisBody.velocity.x > 0.0) {
			thisBody.velocity = Xvelocity;
		}
	}

	bool IsGrounded(){

		return Physics2D.Linecast(transform.position, groundCheck.position, ground_layers);
	}
}
