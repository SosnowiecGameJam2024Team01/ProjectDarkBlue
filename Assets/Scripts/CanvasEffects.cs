using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEffects : MonoBehaviour
{
    public static CanvasEffects Instance;

	[SerializeField] Animator canvasAnim;

	[SerializeField] float birdCD;

	float birbACD = 0;
	float birbBCD = 0;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else Destroy(gameObject);
	}


	public void ShowBird(PlayerType type)
	{
		switch (type)
		{
			case PlayerType.Dionysus:
				if (birbACD > Time.time) return;
				canvasAnim.SetTrigger("BirdA");
				birbACD = Time.time + birdCD;
				break;
			case PlayerType.Venus:
				if (birbBCD > Time.time) return;
				canvasAnim.SetTrigger("BirdB");
				birbBCD = Time.time + birdCD;
				break;
		}
	}
}
