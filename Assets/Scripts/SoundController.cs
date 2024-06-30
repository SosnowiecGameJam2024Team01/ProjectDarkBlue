using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
	Crash,
	Dio,
	Wenus,
	Drift,
	Cabbages,
	Crowd,
	Gong,
	Wheel,
	Win,
	ItemPickup,
}

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

	[SerializeField] List<AudioSource> crash;
	[SerializeField] AudioSource dio;
	[SerializeField] AudioSource wenus;
	[SerializeField] List<AudioSource> drift;
	[SerializeField] AudioSource cabbages;
	[SerializeField] List<AudioSource> crowd;
	[SerializeField] AudioSource gong;
	[SerializeField] AudioSource wheel;
	[SerializeField] AudioSource win;
	[SerializeField] AudioSource itemPickup;

	private void Awake()
	{
		if (Instance == null) Instance = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P)) PlaySound(SoundType.Gong);
	}

	public void PlaySound(SoundType type)
	{
		switch (type)
		{
			case SoundType.Crash:
				crash[Random.Range(0, crash.Count)].Play();
				break;
			case SoundType.Dio:
				dio.Play();
				break;
			case SoundType.Wenus:
				wenus.Play();
				break;
			case SoundType.Drift:
				drift[Random.Range(0, drift.Count)].Play();
				break;
			case SoundType.Cabbages:
				cabbages.Play();
				break;
			case SoundType.Crowd:
				crowd[Random.Range(0, crowd.Count)].Play();
				break;
			case SoundType.Gong:
				gong.Play();
				break;
			case SoundType.Wheel:
				wheel.Play();
				break;
			case SoundType.Win:
				win.Play();
				break;
			case SoundType.ItemPickup:
				itemPickup.Play();
				break;
		}
	}

}
