using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
	bool loading = false;
	[SerializeField] GameObject panel;
	private void Update()
	{
		if(Input.anyKeyDown || Input.GetMouseButtonDown(0))
		{
			LoadGame();
		}
	}

	void LoadGame()
	{
		if (loading) return;
		loading = true;
		panel.SetActive(true);
		SceneManager.LoadSceneAsync("ChariotScene");
	}
}
