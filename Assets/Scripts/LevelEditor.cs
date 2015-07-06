using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

	public GameObject []  propTools;
	GameObject currentTool;

	bool canSpawnObject;

	void Start () 
	{
		canSpawnObject = true;
	}
	
	void LateUpdate () 
	{
		if ( currentTool != null )
		{
			if ( Input.GetMouseButtonDown( 0 ) && canSpawnObject )
			{
				GameObject go = (GameObject) Instantiate( currentTool, Camera.main.ScreenToWorldPoint( Input.mousePosition ), Quaternion.identity );

				go.transform.position += new Vector3( 0, 0, Mathf.Abs( Camera.main.transform.position.z ) );
			}
		}
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
}
