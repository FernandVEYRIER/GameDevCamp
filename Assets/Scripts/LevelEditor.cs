using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour {

	public GameObject background;
	public GameObject ground;
	public GameObject []  propTools;
	GameObject currentTool;

	bool canSpawnObject;

	GameObject go;

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
				go = (GameObject) Instantiate( currentTool, Camera.main.ScreenToWorldPoint( Input.mousePosition ), Quaternion.identity );

				// We reset the Z axis of the obect othewise GameObject will be invisible
				go.transform.position += new Vector3( 0, 0, Mathf.Abs( Camera.main.transform.position.z ) );
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
