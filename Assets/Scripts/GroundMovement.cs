using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {

	private Vector3 cameraPos;
	public int cameraDist;//distance away from center of camera
	public int offset;//distance away from camera to spawn
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cameraPos = Camera.main.transform.position;

		if (this.transform.position.x <= (cameraPos.x - cameraDist)) {
			this.transform.position = new Vector3(cameraPos.x + offset, this.transform.position.y, this.transform.position.z);
		}
	}
}
