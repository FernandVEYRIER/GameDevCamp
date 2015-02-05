using UnityEngine;
using System.Collections;

public class MoveHeros : MonoBehaviour {

	public float	speed = 4;
	void Update ()
	{
		transform.Translate (Vector2.right * Time.deltaTime * speed);
	}
}
