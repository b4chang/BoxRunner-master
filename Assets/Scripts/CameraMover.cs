using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	//Transform of player
	public Transform player;

	//X offset for player so player can see obstacles from a distance
	public float Xoffset = 3.5f;
	public float Yoffset = 2.1f;

	//keeping the original z position
	private float cameraZ;


	// Use this for initialization
	void Start () {
		cameraZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.position.x + Xoffset, 
		                                  player.position.y+ Yoffset, cameraZ);

	}
}
