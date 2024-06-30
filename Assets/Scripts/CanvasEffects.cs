using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEffects : MonoBehaviour
{
    public static CanvasEffects Instance;

	[SerializeField] Animator canvasAnim;

	[SerializeField] float birdCD;

	[SerializeField] AudioSource one;
	[SerializeField] AudioSource two;

	float turnOff;
	bool sounds;
	float birbACD = 0;
	float birbBCD = 0;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}

	private void Update()
	{
		if(sounds && turnOff < Time.time)
		{
			one.Pause();
			two.Pause();
			sounds = false;
		}
	}

	public void ShowBird(PlayerType type)
	{
		switch (type)
		{
			case PlayerType.Dionysus:
				if (birbACD > Time.time) return;
				canvasAnim.SetTrigger("BirdA");
				turnOff = Time.time + birdCD;
				if(!sounds)
				{
					sounds = true;
					one.Play();
					two.Play();
				}
				birbACD = Time.time + birdCD;
				break;
			case PlayerType.Venus:
				if (birbBCD > Time.time) return;
				canvasAnim.SetTrigger("BirdB");
				turnOff = Time.time + birdCD;
				if (!sounds)
				{
					sounds = true;
					one.Play();
					two.Play();
				}
				birbBCD = Time.time + birdCD;
				break;
		}
	}
}
