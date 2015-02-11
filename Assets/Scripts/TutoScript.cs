using UnityEngine;
using System.Collections;

public class TutoScript : MonoBehaviour {
	
	public MoveHeros moveHeros;
	public Defil background;
	public Defil ground;

	bool firstClick = false;
	bool secondClick = false;
	bool thirdClick = false;

	// Use this for initialization
	void Start () 
	{
		this.GetComponent<SpriteRenderer>().enabled = false;
		StartCoroutine(tuto(2.4f));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0) && Time.timeSinceLevelLoad > 2.4f && !firstClick)
		{
			Time.timeScale = 1;
			firstClick = true;
			moveHeros.enabled = true;
			background.enabled = true;
			ground.enabled = true;
			this.GetComponent<SpriteRenderer>().enabled = false;
			StartCoroutine(tuto(2.2f));
		}
		if (Input.GetMouseButtonDown(0) && Time.timeSinceLevelLoad > 4.6f && !secondClick && firstClick)
		{
			Time.timeScale = 1;
			secondClick = true;
			moveHeros.enabled = true;
			background.enabled = true;
			ground.enabled = true;
			this.GetComponent<SpriteRenderer>().enabled = false;
			StartCoroutine(tutoPart2(.7f));
		}
		if (Input.GetMouseButtonDown(0) && !thirdClick && secondClick && firstClick & Time.timeScale == 0)
		{
			thirdClick = true;
			moveHeros.enabled = true;
			background.enabled = true;
			ground.enabled = true;
			this.GetComponent<SpriteRenderer>().enabled = false;
			Time.timeScale = 1;
		}
	}

	IEnumerator tuto(float len)
	{
		yield return new WaitForSeconds(len);
		Time.timeScale = 0;
		this.GetComponent<SpriteRenderer>().enabled = true;
		moveHeros.enabled = false;
		background.enabled = false;
		ground.enabled = false;
	}

	IEnumerator tutoPart2(float len)
	{
		yield return new WaitForSeconds(len);
		this.GetComponent<SpriteRenderer>().enabled = true;
		moveHeros.enabled = false;
		background.enabled = false;
		ground.enabled = false;
		this.transform.position += new Vector3(0, 2.5f, 0);
		Time.timeScale = 0f;
	}
}
