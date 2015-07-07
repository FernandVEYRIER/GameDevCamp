using UnityEngine;
using System.Collections;

/*
 * This script is attached to object when using the level editor to handle modifications
 */
public class ObjectEditor : MonoBehaviour {

	private LevelEditor editor;

	void Awake()
	{
		editor = GameObject.FindGameObjectWithTag( "GameManager" ).GetComponent<LevelEditor>();
	}

	void OnMouseDown()
	{
		editor.objectSelected = this.gameObject;
		editor.canSpawnObject = false;
	}

	void OnMouseUp()
	{
		editor.objectSelected = null;
		editor.canSpawnObject = true;
	}

	void OnMouseDrag()
	{
		// If we are not in moving mode, don't move object
		if ( editor.canSpawnObject )
			return;
		this.transform.position = Camera.main.ScreenToWorldPoint( Input.mousePosition ) + new Vector3( 0, 0, -Camera.main.transform.position.z );
	}
}
