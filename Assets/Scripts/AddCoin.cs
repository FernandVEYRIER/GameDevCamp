using UnityEngine;
using System.Collections;

public class AddCoin : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		GameObject.Find("_GameManager").GetComponent<GameManager>().coins++;
	}
}
