using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	//this contains the menu clip
	public AudioClip menuClip;

	//and this all the other clips
	public AudioClip [] audioClip;

	[RequireComponent(typeof (AudioSource))]
	void Start () 
	{
		audio.loop = 1;
		if (Application.loadedLevel == 0)
			audio.Play();
		else
			StartCoroutine(playSongs());
	}
	
	IEnumerator playSongs()
	{
		foreach(AudioClip clip in audioClip)
		{
			audio.clip = clip;
			audio.Play();
			yield return new WaitForSeconds(audio.clip.length);
		}
	}
}
