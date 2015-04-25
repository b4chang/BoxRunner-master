using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectPool : MonoBehaviour {
	
	public GameObject[] pooledObject;//Objects to be pooled
	public int[] poolCount;//count of each objects
	//public int poolCount;//Number of objects to be pooled
	public bool willGrow = true;//helps increase pool size if needed

	//All the objects that are in the spawn list
	public static List<GameObject> pooledObjects;
	
	
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
