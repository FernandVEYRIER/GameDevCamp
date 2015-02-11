using UnityEngine;
using System.Collections;

public class TutoScript : MonoBehaviour {
	
	public MoveHeros moveHeros;
	public Defil background;
	public Defil ground;
	// Use this for initialization
	void Start () 
	{
		this.GetComponent<SpriteRenderer>().enabled = false;
		StartCoroutine(tuto());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("toto");
		}
	}

	IEnumerator tuto()
	{
		yield return new WaitForSeconds(2.2f);
		this.GetComponent<SpriteRenderer>().enabled = true;
		moveHeros.enabled = false;
		background.enabled = false;
		ground.enabled = false;
	}	
}
