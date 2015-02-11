using UnityEngine;
using System.Collections;

public class AddCoin : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.collider2D.tag == "Player")
		{
			GameObject.Find("_GameManager").GetComponent<GameManager>().coins++;
			Destroy(this.gameObject);
		}
	}
}
