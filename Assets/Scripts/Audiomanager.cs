using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class Audiomanager : MonoBehaviour
{
	public Sound[] musicSounds, sfxSounds;
	public AudioSource musicSource, sfxSource;

	public static Audiomanager instance;
    
    void Awake()
    {

		if(instance == null)
			instance = this;
		else 
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
    }
	void Start()
	{
		PlayMusic("Combat-Theme_Africa");
	}

	public void PlayMusic (string name)
	{
		Sound s = Array.Find(musicSounds, sound => sound.name == name);
		if(s == null)
		{
			Debug.Log("Sounds not found");
		}
		else
		{
			musicSource.clip = s.clip;
			musicSource.Play();
			//musicSource.volume = s.volume;
		}
	}
	public void PlaySFX (string name)
	{
		Sound s = Array.Find(sfxSounds, sound => sound.name == name);
		if(s == null)
		{
			Debug.Log("Sounds not found");
		}
		else
		{
			sfxSource.PlayOneShot(s.clip);
		}
	}
	public void Stop(string name)
	{
		Sound s = Array.Find(musicSounds, x => x.name == name);
		if(s == null)
			return;
		musicSource.Stop();
	}
}
