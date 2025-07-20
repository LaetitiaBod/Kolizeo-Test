using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	[SerializeField] private AudioSource[] sfx;

	private void Awake()
	{
		if (instance != null)
			Destroy(instance.gameObject);
		else
			instance = this;
	}

	public void PlaySFX(int _index, bool _pitchVariations)
	{
		if (_index < sfx.Length)
		{
			if (_pitchVariations)
				sfx[_index].pitch = Random.Range(.85f, 1.15f);
			sfx[_index].Play();
		}
	}

	public void StopSFXWithTime(int _index) => StartCoroutine(DecreaseVolume(sfx[_index]));

	private IEnumerator DecreaseVolume(AudioSource _audio)
	{
		float defaultVolume = _audio.volume;

		while (_audio.volume > 0)
		{
			_audio.volume -= _audio.volume * .2f;
			yield return new WaitForSeconds(.1f);

			if (_audio.volume < .1f)
			{
				_audio.Stop();
				_audio.volume = defaultVolume;
				break;
			}
		}
	}
}
