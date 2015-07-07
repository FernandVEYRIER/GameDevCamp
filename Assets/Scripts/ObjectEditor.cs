using UnityEngine;
using System.Collections;

/*
 * This script is attached to object when using the level editor to handle modifications
 */
public class ObjectEditor : MonoBehaviour {

	void OnMouseDown()
	{
		Debug.Log("mabit");
	}

	void OnMouseDrag()
	{
		this.transform.position = Camera.main.ScreenToWorldPoint( Input.mousePosition ) + new Vector3( 0, 0, -Camera.main.transform.position.z );
	}
}
