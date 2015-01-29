using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	
	public void Play()
	{
		Application.LoadLevel(1);
	}

	public void Options()
	{

	}

	public void Quit()
	{
		Application.Quit();
	}
}
