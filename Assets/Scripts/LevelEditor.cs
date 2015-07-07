using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

	public GameObject background;
	public GameObject ground;
	public GameObject []  propTools;
	public GameObject objectHolder;
	public int objectLimit;

	GameObject currentTool;

	int objectCount;

	bool canSpawnObject;

	GameObject go;

	void Start () 
	{
		canSpawnObject = true;
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
				++objectCount;
			}
		}
	}

	public void OnMouseDrag()
	{
		canSpawnObject = false;
		if ( currentTool != null )
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
		Debug.Log( "Save level" );
	}

	public void CleanLevel()
	{
		GameObject [] allObjects = GameObject.FindGameObjectsWithTag( "EditorOnly" );

		foreach ( GameObject go in allObjects )
		{
			Destroy( go );
		}
	}
}
