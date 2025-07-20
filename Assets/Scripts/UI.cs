using System.Collections;
using UnityEngine;

public class UI : MonoBehaviour
{
	[Header("Transitions animations")]
	[SerializeField] private AnimationClip[] transitionsAnims;
	[SerializeField] private GameObject transitionScreen;

	private Animator transitionAnimator;

	[Header("Fade screen animations")]
	[SerializeField] private GameObject fadeScreen;
	public GameObject loadingIcon;

	private Animator fadeAnimator;
	private Animator loadingIconAnimator;
	private float fadeDuration = 1f;

	[Header("UI Elements")]
	[SerializeField] private GameObject[] uiElements;
	public GameObject menuUI;
	public GameObject inGameUI;
	public string currentScene { get; set; }

	private void Awake()
	{
		transitionAnimator = transitionScreen.GetComponent<Animator>();
		fadeAnimator = fadeScreen.GetComponent<Animator>();
		loadingIconAnimator = loadingIcon.GetComponent<Animator>();
	}

	private void Start()
	{
		SwitchTo(menuUI);
	}

	public void SwitchTo(GameObject _uiToEnable)
	{
		foreach (GameObject ui in uiElements)
		{
			ui.SetActive(false);
		}

		if (_uiToEnable != null)
			_uiToEnable.SetActive(true);
	}

	/**
	 *	Function used as OnClick event in menu and inGame buttons
	 */
	public void GoToNextScene(string _sceneName)
	{
		currentScene = _sceneName;
		AudioManager.instance.PlaySFX(0, false);
		StartCoroutine(FadeInScreenCo(_sceneName));
	}

	public IEnumerator RandomTransitionCo(string _sceneName)
	{
		transitionScreen.SetActive(true);

		int randomIndex = Random.Range(0, (transitionsAnims.Length - 1));
		AnimationClip myAnim = transitionsAnims[randomIndex];
		transitionAnimator.SetTrigger(myAnim.name);

		yield return new WaitForSeconds(myAnim.length);
	}

	public IEnumerator FadeInScreenCo(string _sceneName)
	{
		fadeScreen.SetActive(true);

		yield return new WaitForSeconds(fadeDuration / 2);

		int random = Random.Range(0, 3);
		if (random == 0)
		{
			loadingIcon.SetActive(true);
			yield return new WaitForSeconds(3);
		}

		StartCoroutine(RandomTransitionCo(_sceneName));
	}

	public IEnumerator FadeOutScreenCo()
	{
		AudioManager.instance.StopSFXWithTime(0);

		fadeAnimator.SetTrigger("fadeOut");

		yield return new WaitForSeconds(fadeDuration);

		fadeScreen.SetActive(false);
	}

	public IEnumerator FadeOutIconCo()
	{
		loadingIconAnimator.SetTrigger("fadeOut");

		yield return new WaitForSeconds(fadeDuration);

		loadingIcon.SetActive(false);
	}
}
