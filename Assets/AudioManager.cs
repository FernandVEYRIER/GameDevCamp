using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class AudioManager : MonoBehaviour {

	//this contains the menu clip
	public AudioClip menuClip;

	//and this all the other clips
	public AudioClip [] audioClip;

	void Start () 
	{
		audio.loop = true;
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
