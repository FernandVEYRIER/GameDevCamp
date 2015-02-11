using UnityEngine;
using System.Collections;

public class MoveDust : MonoBehaviour {

	public float	max_y;
	public float	min_y;
	public float	speed;
	private bool	up = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y >= max_y)
			up = false;
		if (transform.position.y <= min_y)
			up = true;
		if (up)
			transform.Translate (Vector3.up * speed *Time.deltaTime);
		else
			transform.Translate (-Vector3.up * speed *Time.deltaTime);
	}
}
