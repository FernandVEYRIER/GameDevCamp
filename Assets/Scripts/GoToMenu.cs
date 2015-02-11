using UnityEngine;
using System.Collections;

public class GoToMenu : MonoBehaviour {

	public void OnMenuClick()
	{
		Application.LoadLevel(0);
	}
}
