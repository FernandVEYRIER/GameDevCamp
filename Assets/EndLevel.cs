using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class EndLevel : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.collider2D.tag == "Player")
			GameObject.Find("_GameManager").GetComponent<GameManager>().Victory();
	}
}
