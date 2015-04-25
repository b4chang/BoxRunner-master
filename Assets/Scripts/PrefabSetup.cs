using UnityEngine;
using System.Collections;

public class PrefabSetup : MonoBehaviour {

	//List of sprites
	public Sprite[] spriteList;

	//destroy time after being spawned
	public float destroyTime = 3.0f;

	//gets the prefab spriteRenderer
	private SpriteRenderer objectRenderer;

	//if this prefab has many sprites that it can load
	public bool manySprites = false;

	//if object position needs to be greater thatn spawner position if the texture size is bigger
	public float yChange = 0;





	void Awake(){
		if(manySprites)
			objectRenderer = GetComponent<SpriteRenderer> ();
	}

	// This is called when gameobject is set active
	void OnEnable() {
		if(manySprites)
			objectRenderer.sprite = spriteList [Random.Range (0, spriteList.Length)];

		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + yChange);

		Invoke ("Destroy", destroyTime);
	}
	
	// Sets gameobject to inactive
	void Destroy () {
		gameObject.SetActive (false);
	}

	void OnDisable(){
		CancelInvoke ();
	}

}
