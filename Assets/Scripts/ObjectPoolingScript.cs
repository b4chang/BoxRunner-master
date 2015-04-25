using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectPoolingScript : MonoBehaviour {
	
	public GameObject[] pooledObject;//Objects to be pooled
	public int[] poolCount;//count of each objects
	//public int poolCount;//Number of objects to be pooled
	public bool willGrow = true;//helps increase pool size if needed

	//Checks if y position needs to be varied
	public bool yChange = false;
	public float minY = 2.0f;
	public float maxY = -2.0f;

	//Start time and spawn variation
	public float startTime = 2.0f;
	public float spawnMin = 2.0f;
	public float spawnMax = 2.0f;

	//All the objects that are in the spawn list
	List<GameObject> pooledObjects;
	
	
	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject> ();

		for (int j=0; j < pooledObject.Length; j++) {
			for (int i=0; i < poolCount[j]; i++) {
				GameObject obj = (GameObject)Instantiate (pooledObject [j]);
				obj.SetActive (false);
				pooledObjects.Add (obj);
			}
		}

		Randomize ();

		//if No change randomness in position of spawn
		if (!yChange) {
			minY = maxY = this.transform.position.y;
		}

		Invoke ("Spawn", startTime);
	}
	
	GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++) {
			if(!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}
		
		if (willGrow) {
			GameObject obj = (GameObject)Instantiate(pooledObject[Random.Range(0, pooledObject.Length)]);
			pooledObjects.Add(obj);
			return obj;
		}
		
		return null;
	}
	
	void Spawn () {

		float randomTime = Random.Range (spawnMin, spawnMax);
		GameObject obj = this.GetPooledObject ();
		
		if (obj == null)
			return;


		Vector3 pos = this.transform.position;
		pos.y = Random.Range(minY, maxY);
		obj.transform.position = pos;
		obj.SetActive (true);

		if (!GameStateManager.Instance.Paused) {
			Invoke ("Spawn", randomTime);
		}
		
	}

	void Randomize (){
		int n = pooledObjects.Count;

		while (n > 1) {
			n--;
			int k = Random.Range(0, n + 1);
			GameObject tmp = pooledObjects[k];
			pooledObjects[k] = pooledObjects[n];
			pooledObjects[n] = tmp;
		}

	}
}
