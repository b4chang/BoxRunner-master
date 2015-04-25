using UnityEngine;
using System.Collections;

public class NewGroundSpawner : PoolingSpawner {
	public SpriteRenderer prevPlatform;
	public Transform nextSpawnPoint;

	// Use this for initialization
	void OnAwake () {
		Vector3 pos = prevPlatform.transform.position;
		pos.x += prevPlatform.bounds.size.x;
		this.transform.position = pos;
	}
	
	public override	bool ShouldSpawn(){
		if (prevPlatform.isVisible) {
		//	Debug.Log("yeah");
			return true;
		}
		//Debug.Log ("no");
		return false;
	}

	public override void ActivateObject(GameObject obj){
		obj.SetActive (true);
		prevPlatform = obj.GetComponent<SpriteRenderer> ();

		//obj.transform.position = nextSpawnPoint.position;
		Transform[] l = obj.GetComponentsInChildren<Transform> ();

		Vector3 anchorToPosVector = obj.transform.position - l [2].position;
		obj.transform.position = nextSpawnPoint.position + anchorToPosVector;

		nextSpawnPoint.position = l[1].position;//t.position;
	}

	public override bool IsReadyToRecycle(GameObject obj){
		if (obj.transform.position.x < cam.position.x-20 && !obj.GetComponent<SpriteRenderer> ().isVisible)
			return true;
		return false;
	}



}
