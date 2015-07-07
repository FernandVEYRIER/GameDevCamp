using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Runtime.InteropServices;

public class LevelEditor : MonoBehaviour {

	public GameObject background;
	public GameObject ground;
	public GameObject []  propTools;
	public GameObject objectHolder;
	public GameObject objectSelected;
	public int objectLimit;
	public bool canSpawnObject;

	GameObject currentTool;
	GameObject go;
	int objectCount;

	private List<GameObject> ObjectList = new List<GameObject>();

	void Start () 
	{
		canSpawnObject = true;
		objectSelected = null;
		objectCount = 0;
	}
	
	void LateUpdate () 
	{
		if ( currentTool != null )
		{
			if ( Input.GetMouseButtonDown( 0 ) && canSpawnObject && objectCount < objectLimit )
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
		}
	}

	public void OnMouseDrag()
	{
		canSpawnObject = false;

		if ( currentTool != null || objectSelected != null )
			return;
		background.GetComponent<MeshRenderer>().material.mainTextureOffset -= new Vector2( Input.GetAxis( "Mouse X" ) * 0.01f, 0 );
		ground.GetComponent<MeshRenderer>().material.mainTextureOffset -= new Vector2( Input.GetAxis( "Mouse X" ) * 0.01f, 0 );
		objectHolder.transform.position += new Vector3( Input.GetAxis( "Mouse X" ) * 0.2f, 0, 0 );
    }

	// Avoid object from spaning when selecting another one
	public void SetSpawnObject( bool state )
	{
		canSpawnObject = state;
	}

	public void OnToolClick( int index )
	{
		if ( index < propTools.Length )
		{
			currentTool = propTools[ index ];
		}
		else
		{
			Debug.LogWarning( "Missing prop tool." );
		}
	}

	public void SaveLevel()
	{
		Debug.Log( "Save level. Object count : " + ObjectList.Count );
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;

		List<UnityEngine.Object> tmp = new List<UnityEngine.Object>();

		file = File.Open( Application.persistentDataPath + "/CustomLevel.dat", FileMode.OpenOrCreate );

		foreach ( var component in ObjectList[0].GetComponents<Component>() )
		{
			Debug.Log( "Component = " + component );
			tmp.Add( component );
		}

		bf.Serialize( file, tmp );
		file.Close();
	}

	public void LoadLevel()
	{
		List<GameObject> ObjectListFile;

		Debug.Log( "Loading level." );
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;

		if ( File.Exists( Application.persistentDataPath + "/CustomLevel.dat" ) )
		{
			file = File.OpenRead( Application.persistentDataPath + "/CustomLevel.dat" );
		}
		else
		{
			Debug.LogError( "File doesn't exist !" );
			return;
		}

		ObjectListFile = (List<GameObject>) bf.Deserialize( file );

		foreach ( GameObject go in ObjectListFile) 
		{
			Instantiate( go );
		}
		file.Close();
	}

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
