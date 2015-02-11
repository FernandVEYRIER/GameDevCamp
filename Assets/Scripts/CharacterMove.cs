using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

	public Transform	origin;
	public float		strenght = 2;
	private bool		isgrounded = false;
	private Transform	parent;
	private Vector3		vec;
	void Start()
	{
		transform.position = new Vector2 (origin.position.x, transform.position.y);
	}
	void OnMouseDown()
	{
		if (isgrounded)
		{
			parent = transform.parent;
			rigidbody2D.AddForce(Vector2.up * strenght);
		}
	}
	// Je pas réussi à faire autrement qu'avec un raycast si je faisais avec les normals de contact ça me faisait de la merde...
	void Update()
	{
		string tag = null;
		//Pour voir cette putin de ligne de merde :
		Debug.DrawLine (new Vector2(transform.position.x, transform.position.y - collider2D.bounds.extents.y), new Vector2(transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.4f), Color.green);
		if (Physics2D.Linecast (new Vector2 (transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.01f), new Vector2 (transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.4f)).collider != null)
			tag = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.01f), new Vector2(transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.4f)).collider.tag;
		if (tag == "Terrain" || tag == "Player")
		{
			isgrounded = true;
			if (tag == "Terrain")
				transform.parent = null;
		}
		else
		{
			isgrounded = false;
			transform.parent = parent;
		}
		//Pour voir l'état de isgrounded :
		//print(isgrounded);
		transform.position = Vector3.SmoothDamp (transform.position, new Vector3 (origin.position.x, transform.position.y, transform.position.z), ref vec, 0.1f);
		if (transform.position.x <= origin.position.x - 0.5f || transform.position.x >= origin.position.x + 0.5f || transform.position.x >= 5.4f)
			GameObject.Find("_GameManager").GetComponent<GameManager>().Death();
	}
}
