using UnityEngine;
using System.Collections;

public class ScieScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		Destroy(col.gameObject);
		GameObject.Find("_GameManager").GetComponent<GameManager>().Death();
	}
}
