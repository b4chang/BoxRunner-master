using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectSpawn : MonoBehaviour {
	
	//Checks if y position needs to be varied
	public bool yChange = false;
	public float minY = 2.0f;
	public float maxY = -2.0f;
	
	//Start time and spawn variation
	public float startTime = 2.0f;
	public float spawnMin = 2.0f;
	public float spawnMax = 2.0f;
	
	
	// Use this for initialization
	void Start () 
	{
		//if No change randomness in position of spawn
		if (!yChange) {
			minY = maxY = this.transform.position.y;
		}

		int number = Random.Range (0, 100);
		if (number >= 40)
			Spawn ();
	}
	
	GameObject GetPooledObject()
	{
		for (int i = 0; i < ObjectPool.pooledObjects.Count; i++) {
			if(!ObjectPool.pooledObjects[i].activeInHierarchy)
			{
				return ObjectPool.pooledObjects[i];
			}
		}
		
		return null;
	}
	
	void Spawn () {

		GameObject obj = this.GetPooledObject ();
		
		if (obj == null)
			return;

		Vector3 pos = this.transform.position;
		pos.y = Random.Range(minY, maxY);
		obj.transform.position = pos;
		obj.SetActive (true);

	}

}
