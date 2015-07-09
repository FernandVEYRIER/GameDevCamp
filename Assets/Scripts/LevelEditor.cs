using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Runtime.InteropServices;
using UnityEditor;
using System.Xml.Serialization;

public class LevelEditor : MonoBehaviour {

	// Décor
	public GameObject background;
	public GameObject ground;
	// Barre d'outils
	public GameObject []  propTools;
	// Object de référence parent des objects instanciés
	public GameObject objectHolder;
	public GameObject toolBar;
	[HideInInspector]
	public GameObject objectSelected;
	public int objectLimit;
	[HideInInspector]
	public bool canSpawnObject;
	[HideInInspector]
	public byte currentMode;

	GameObject currentTool;
	GameObject go;
	int objectCount;
	bool isToolBarHidden;
	float toolBarInitialPosition;

	private List<GameObject> buttonList = new List<GameObject>();
	private List<GameObject> ObjectList = new List<GameObject>();

	public enum EditingState : byte
	{
		MODE_SPAWN,
		MODE_SELECT,
		MODE_MOVE
	}

	void Start () 
	{
		canSpawnObject = true;
		objectSelected = null;
		objectCount = 0;
		toolBarInitialPosition = toolBar.transform.position.x;
		currentMode = (byte) EditingState.MODE_MOVE;

		for ( int i = 0; i < toolBar.transform.childCount; i++ )
		{
			buttonList.Add( toolBar.transform.GetChild( i ).gameObject );
		}
		Debug.Log( buttonList.Count );
	}
	
	void LateUpdate () 
	{
		if ( currentMode == (byte) EditingState.MODE_SPAWN )
		{
			if ( Input.GetMouseButtonDown( 0 ) && currentTool != null && objectCount < objectLimit && canSpawnObject )
			{
				SpawnProp();
			}
		}
	}

	public void SpawnProp()
	{
		go = (GameObject) Instantiate( currentTool, Camera.main.ScreenToWorldPoint( Input.mousePosition ), Quaternion.identity );
		
		// We reset the Z axis of the obect othewise GameObject will be invisible
		go.transform.position += new Vector3( 0, 0, Mathf.Abs( Camera.main.transform.position.z ) );
		go.transform.SetParent( objectHolder.transform );
		go.tag = "EditorOnly";
		go.AddComponent<ObjectEditor>();
		ObjectList.Add( go );
		++objectCount;
	}

	// Hides or shows the toolbar
	public void MoveToolBar()
	{
		if ( isToolBarHidden )
		{
			isToolBarHidden = false;
			toolBar.transform.position = new Vector3( toolBarInitialPosition, toolBar.transform.position.y, 0 );
		}
		else
		{
			isToolBarHidden = true;
			toolBar.transform.position = new Vector3( Screen.width, toolBar.transform.position.y, 0 );
		}
	}

	// Called when scrolling the map
	public void OnMouseDrag()
	{
		canSpawnObject = false;

		if ( currentMode != (byte) EditingState.MODE_MOVE )
		{
			return;
		}
		background.GetComponent<MeshRenderer>().material.mainTextureOffset -= new Vector2( Input.GetAxis( "Mouse X" ) * 0.01f, 0 );
		ground.GetComponent<MeshRenderer>().material.mainTextureOffset -= new Vector2( Input.GetAxis( "Mouse X" ) * 0.01f, 0 );
		objectHolder.transform.position += new Vector3( Input.GetAxis( "Mouse X" ) * 0.2f, 0, 0 );
    }

	// Avoid object from spaning when selecting another one
	public void SetSpawnObject( bool state )
	{
		canSpawnObject = state;
	}

	// Selects the tool to use
	public void OnToolClick( int index )
	{
		if ( index < propTools.Length )
		{
			currentTool = propTools[ index ];
			// If we are using a tool then we are spawning objects
			if ( currentTool != null )
			{
				currentMode = (byte) EditingState.MODE_SPAWN;
			}
			// else we are in moving mode
			else if ( currentTool == null )
			{
				currentMode = (byte) EditingState.MODE_MOVE;
			}
		}
		else if ( index == 5 )
		{
			currentMode = (byte) EditingState.MODE_SELECT;
		}
		else
		{
			Debug.LogWarning( "Missing prop tool." );
		}
	}

	// Removes all the objets of the level
	public void CleanLevel()
	{
		GameObject [] allObjects = GameObject.FindGameObjectsWithTag( "EditorOnly" );

		foreach ( GameObject go in allObjects )
		{
			Destroy( go );
		}
		ObjectList.Clear();
	}
}
