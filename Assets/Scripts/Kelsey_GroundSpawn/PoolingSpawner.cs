using UnityEngine;
using System.Collections;

/*A spawner that implements object pooling for efficiency's sake.
 * This class can be extended to be more object specific/exhbit different behavior.
 * Functions that could/should be overwritten are denoted by <<//OVERWRITE FOR DIFFRENT BEHAVIORS>>
 */

public abstract class PoolingSpawner : MonoBehaviour {
	public GameObject[] pool;  //keeps track of all the objects in the pool
	private GameObject[] inPlay;  //list of which objects are 
	private GameObject[] inWait;  //
	private int inPlaySize;//track number of filled elements in inPlay
	private int inWaitSize;//track number of filled elements in inWait

	public int spawningFrequency = 100;//likelihood of spawning

	public Transform cam; //the main cam's position
	public float camHorizOffset = 12; // how far to the right of the camera position to spawn stuff.  Probably a better way to calculate this
	public float vertMax =3;//highest point we can spawn at (in respect to camera center)
	public float vertMin=-3;//lowest point we can spawn at (in respect to camera center)


	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
		inPlay = new GameObject[pool.Length];
		inPlaySize = 0;
		inWait = (GameObject[])pool.Clone ();
		inWaitSize = pool.Length;
	}

	//****Checks that the condition is met for the object to be "destroyed" 
	//and returned to the pool.  OVERWRITE FOR DIFFERENT BEHAVIOR
	abstract public bool IsReadyToRecycle(GameObject obj);

	//****Setup the object being spawned (eg turn on renderer, change position)
	//OVERWRITE FOR DIFFERENT BEHAVIOR
	abstract public void ActivateObject (GameObject obj);

	//****Decommission the object being recycled 
	//OVERWRITE FOR DIEFFERENT BEHAVIORS
	void DeactivateObject(GameObject obj){
		obj.SetActive (false);
	}

	//The logic that dictates whether or not it's time to spawn
	//OVERWRITE FOR DIFFRENT BEHAVIORS
	public abstract bool ShouldSpawn ();

	// Checks for recycling and checks if it should spawn
	void Update () {
		//check in-play objects to see if they're ready to recycle
		for (int i = 0; i< inPlaySize; i++) 
			if(IsReadyToRecycle(inPlay[i]))
				Recycle(i);
		if (ShouldSpawn ())
			Spawn ();
	}

	//Recycle the object at inPlay[index] by sending back to inWait
	void Recycle(int index){
		DeactivateObject (inPlay [index]);
		inWait [inWaitSize] = inPlay [index];
		inWaitSize++;
		//shift all the elements
		for (int i = index + 1; i < inPlaySize; i++)
			inPlay[i-1] = inPlay[i];
		inPlaySize--;
	}
		
	//Spawns a gameObject if one is available
	void Spawn(){
		//if there's one in waiting pool, ready to spawn
		if (inWaitSize > 0) {
			inPlay[inPlaySize] = inWait[inWaitSize-1];
			ActivateObject(inPlay[inPlaySize]);
			inPlaySize++;
			inWaitSize--;
		}
	}
}
