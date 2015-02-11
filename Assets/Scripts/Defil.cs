using UnityEngine;
using System.Collections;

public class Defil : MonoBehaviour {

	public float	speed;
	// Update is called once per frame
	void Update ()
	{
		gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset.x + speed * 0.001f,
		                                                                                 gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset.y );
	}
}
