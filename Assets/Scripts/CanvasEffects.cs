using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEffects : MonoBehaviour
{
    public static CanvasEffects Instance;

	[SerializeField] Animator canvasAnim;

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
				canvasAnim.SetTrigger("BirdA");
				break;
			case PlayerType.Venus:
				canvasAnim.SetTrigger("BirdB"); 
				break;
		}
	}
}
