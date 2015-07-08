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

	public GameObject background;
	public GameObject ground;
	public GameObject []  propTools;
	public GameObject objectHolder;
	public GameObject toolBar;
	[HideInInspector]
	public GameObject objectSelected;
	public int objectLimit;
	[HideInInspector]
	public bool canSpawnObject;

	GameObject currentTool;
	GameObject go;
	int objectCount;
	bool isToolBarHidden;
	float toolBarInitialPosition;
	
	private List<GameObject> ObjectList = new List<GameObject>();

	void Start () 
	{
		canSpawnObject = true;
		objectSelected = null;
		objectCount = 0;
		toolBarInitialPosition = toolBar.transform.position.x;
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

	// Selects the tool to use
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

		SerializeToXMLFile( "C:/", ObjectList[0], true);

		file = File.Open( Application.persistentDataPath + "/CustomLevel.dat", FileMode.OpenOrCreate );

		/*foreach ( var _component in ObjectList[0].GetComponents<Component>() )
		{
			Debug.Log( "Component = " + _component );
			if ( _component.GetType().IsSerializable )
			{
				tmp.Add( _component );
				Debug.Log( "Component actualy saved = " + _component );
			}
		}*/

		//bf.Serialize( file, tmp );
		Debug.Log( "Data path = " + Application.dataPath );
		//PrefabUtility.CreatePrefab( "C:/obj.asset", ObjectList[0] );
		file.Close();
	}

	public static bool SerializeToXMLFile(string writePath, object serializableObject, bool overWrite = true)
	{
		if(File.Exists(writePath) || overWrite == false)
			return false;
		XmlSerializer serializer = new XmlSerializer( serializableObject.GetType() );
		using( var writeFile = File.Create(writePath))
		{
			serializer.Serialize(writeFile, serializableObject);
		}
		return true;
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
