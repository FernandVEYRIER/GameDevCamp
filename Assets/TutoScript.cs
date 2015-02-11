using UnityEngine;
using System.Collections;

public class TutoScript : MonoBehaviour {
	
	public MoveHeros moveHeros;
	public Defil background;
	public Defil ground;

	bool firstClick = false;
	// Use this for initialization
	void Start () 
	{
		this.GetComponent<SpriteRenderer>().enabled = false;
		StartCoroutine(tuto());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0) && Time.timeSinceLevelLoad > 2.4f && !firstClick)
		{
			firstClick = true;
			moveHeros.enabled = true;
			background.enabled = true;
			ground.enabled = true;
			this.GetComponent<SpriteRenderer>().enabled = false;
			//StartCoroutine(totoPart2());
		}
	}

	IEnumerator tuto()
	{
		yield return new WaitForSeconds(2.4f);
		this.GetComponent<SpriteRenderer>().enabled = true;
		moveHeros.enabled = false;
		background.enabled = false;
		ground.enabled = false;
	}

	IEnumerator totoPart2()
	{
		yield return new WaitForSeconds(2.2f);
	}
}
