using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private float speed = 100.0f;
    [SerializeField] private float accurance = 0.01f;
    [SerializeField] private Button[] buttons;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    private void InteractableInButtons(bool mode)
    {
        for(int i = 0; i < buttons.Length; ++i)
            buttons[i].interactable = mode;
    }
    public void Fade(GameObject canvas)
    {
        InteractableInButtons(false);
        if(canvas.TryGetComponent<CanvasGroup>(out CanvasGroup cg))
            StartCoroutine(Fading(cg, cg.alpha));
    }
    public IEnumerator Fading(CanvasGroup canvas, float mode)
    {
        while(canvas.alpha != (mode == 0 ? 1 : 0))
        {
            if (!canvas.gameObject.activeInHierarchy) canvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(1 / speed);
            canvas.alpha += (mode == 0 ? accurance : -accurance);
        }
        canvas.gameObject.SetActive((mode == 1) ? false : true);
        InteractableInButtons(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
