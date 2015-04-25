using UnityEngine;
using System.Collections;

public class ScoreManagerScript : MonoBehaviour {

	public Sprite[] numSprites;

	public SpriteRenderer Units, Tens, Hundreds;

	private int previousScore = -1;

	public static int Score = 0;

	void Start () 
	{
		Debug.Log ("score start");
		Tens.enabled = false;
		Hundreds.enabled = false;
	}
	
	void Update () 
	{

		if(previousScore != Score){
			if(Score < 10){
				Units.sprite = numSprites[Score];
			}
			else if(Score >= 10 && Score < 100){
				Tens.enabled = true;

				Units.transform.localPosition = new Vector2(0.5f, 0.0f);
				Tens.transform.localPosition = new Vector2(-0.5f, 0.0f);	

				Tens.sprite = numSprites[Score / 10];
				Units.sprite = numSprites[Score % 10];
			}
			else if(Score >= 100){
				Tens.enabled = true;
				Hundreds.enabled = true;

				Units.transform.localPosition = new Vector2(1.0f, 0.0f);
				Tens.transform.localPosition = new Vector2(0.0f, 0.0f);
				Hundreds.transform.localPosition = new Vector2(-1.0f, 0.0f);
				
				Hundreds.sprite = numSprites[Score / 100];
				int rest = Score % 100;
				Tens.sprite = numSprites[rest / 10];
				Units.sprite = numSprites[rest % 10];
				
			}
		}

	}
}
