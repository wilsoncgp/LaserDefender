using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	void Awake ()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			print ("Duplicate music player self-destructing");
		}
		else
		{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}
	}
	
	void OnLevelWasLoaded(int level)
	{
		if(!ReferenceEquals(music, null))
		{
			music.Stop();
		
			switch(level)
			{
				case 0:
					music.clip = startClip;
					break;
				case 1:
					music.clip = gameClip;
					break;
				case 2:
					music.clip = endClip;
					break;
			}
			
			music.loop = true;
			music.Play();
		}
	}
}
