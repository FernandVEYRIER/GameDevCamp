using UnityEngine;
using System.Collections;

public class fourbe : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter()
	{
		rigidbody2D.isKinematic = false;
	}
}
