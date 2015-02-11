using UnityEngine;
using System.Collections;

public class PicsScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.collider2D.tag == "Player")
		{
			Destroy(col.gameObject);
			GameObject.Find("_GameManager").GetComponent<GameManager>().Death();
		}
	}
}
