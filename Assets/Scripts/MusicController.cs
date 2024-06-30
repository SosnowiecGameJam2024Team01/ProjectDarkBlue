using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Music
{
	MainMenu,
	MainMenuLoop,
	raceLoop,
	raceLast,
	raceLastLoop
}

public enum Scene
{
	Menu,
	Game
}

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

	[SerializeField] AudioSource mainMenu;
	[SerializeField] AudioSource mainMenuLoop;
	[SerializeField] AudioSource raceLoop;
	[SerializeField] AudioSource raceLast;
	[SerializeField] AudioSource raceLastLoop;

	[SerializeField] float maxVolume;
	AudioSource current;
	AudioSource next;

	[SerializeField] bool change;
	[SerializeField] float blendTime;
	float start;

	Scene currentScene;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	
		DontDestroyOnLoad(gameObject);
		
	}

	private void Update()
	{
		if (!change) {
			if (current.clip.length - current.time <= blendTime)
			{
				LoopOrNext();
			}
			return; 
		}
		if (current == null)
		{
			current = next;
			current.volume = maxVolume;
			change = false;
		}
		else
		{

			if (Time.time < start + blendTime)
			{
				float t = (Time.time - start) / blendTime;
				current.volume = Mathf.Lerp(maxVolume, 0, t);
				next.volume = Mathf.Lerp(0, maxVolume, t);
			}
			else
			{
				current.Stop();
				current = next;
				change = false;
			}
		}

 
	}

	private void LoopOrNext()
	{
		if(!current.loop)
		{
			if(currentScene == Scene.Menu)
			{
				SwitchTracks(Music.MainMenuLoop);
			}
			else if (currentScene == Scene.Game)
			{
				SwitchTracks(Music.raceLastLoop);
			}
		} 
	}

	public void ChangeScene(Scene scene)
	{
		currentScene = scene;
		switch (scene)
		{
			case Scene.Menu:
				SwitchTracks(Music.MainMenu);
				break;
			case Scene.Game:
				SwitchTracks(Music.raceLoop);
				break;
		}
	}

	public void SwitchTracks(Music music)
	{
		switch (music)
		{
			case Music.MainMenu:
				next = mainMenu;
				break;
			case Music.MainMenuLoop:
				next = mainMenuLoop;
				break;
			case Music.raceLoop:
				next = raceLoop;
				break; 
			case Music.raceLast:
				next = raceLast;
				break;
			case Music.raceLastLoop:
				next = raceLastLoop;
				break;
		}
		start = Time.time;
		next.volume = 0;
		next.Play();
		change = true;
	}


}
