using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

	public Transform	origin;
	public float		strenght = 2;
	private bool		isgrounded = false;
	private Transform	parent;
	private Vector2		vel;
	private bool		activate = false;
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
		//Debug.DrawLine (new Vector2(transform.position.x, transform.position.y - collider2D.bounds.extents.y), new Vector2(transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.1f), Color.green);
		if (Physics2D.Linecast (new Vector2 (transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.01f), new Vector2 (transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.1f)).collider != null)
			tag = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.01f), new Vector2(transform.position.x, transform.position.y - collider2D.bounds.extents.y - 0.1f)).collider.tag;
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

	}
}
