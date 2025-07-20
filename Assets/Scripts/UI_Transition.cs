using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Transition : MonoBehaviour
{
	private UI ui;

	private void Awake()
	{
		ui = FindFirstObjectByType<UI>();
	}

	/**
	 *	Function used as event in animations of Transition
	 */
	public void IconFadeOut()
	{
		StartCoroutine(ui.FadeOutIconCo());
	}

	/**
	 *	Function used as event in animations of Transition
	 */
	public void StartFadeOut()
	{
		StartCoroutine(ui.FadeOutScreenCo());

		if (ui.currentScene == "MainScene")
		{
			ui.SwitchTo(ui.menuUI);
			SceneManager.UnloadSceneAsync("SecondScene");
		}
		else if (ui.currentScene == "SecondScene")
		{
			ui.SwitchTo(ui.inGameUI);
			SceneManager.LoadSceneAsync(ui.currentScene, LoadSceneMode.Additive);
		}
	}
}
