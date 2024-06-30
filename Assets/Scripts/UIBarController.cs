using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarController : MonoBehaviour
{
    public MovementController controller;


	[SerializeField] List<Sprite> normal;
	[SerializeField] List<Sprite> highlight;
	[SerializeField] List<Image> images;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = controller.abilityBar / (float)controller.maxAbilityBar;
        int active = Mathf.FloorToInt((controller.abilityBar / (float)controller.maxAbilityBar + Mathf.Epsilon) * 3) - 1;

		for (int i = 0; i < images.Count; i++)
        {
            if(active == i) images[i].sprite = highlight[i];
            else images[i].sprite = normal[i];
		}
    }
}
