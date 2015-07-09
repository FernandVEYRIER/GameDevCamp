using UnityEngine;
using System.Collections;
using System;

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
		// If we are not in select mode, cannot select this object OR an object is already selected
		if ( editor.currentMode != (byte) LevelEditor.EditingState.MODE_SELECT || editor.objectSelected != null )
			return;

		// We set this one as the new object selected
		editor.objectSelected = this.gameObject;

		editor.canSpawnObject = false;
		SetSelectedColor( true );
	}

	void OnMouseUp()
	{
		editor.canSpawnObject = true;
	}

	void OnMouseDrag()
	{
		if ( editor.currentMode != (byte) LevelEditor.EditingState.MODE_SELECT || editor.objectSelected != null )
			return;

		this.transform.position = Camera.main.ScreenToWorldPoint( Input.mousePosition ) + new Vector3( 0, 0, -Camera.main.transform.position.z );
	}

	public void SetSelectedColor( bool isSelected )
	{
		if ( isSelected )
		{
			this.GetComponent<SpriteRenderer>().color = Color.red;
		}
		else
		{
			this.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
}
