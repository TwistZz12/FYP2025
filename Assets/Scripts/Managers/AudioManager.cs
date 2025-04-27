using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private float sfxMinimumDistance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;


    public bool playBgm;
    private int bgmIndex;
    private bool isCrossFading;

    private bool canPlaySFX;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;

        Invoke("AllowSFX", 1f);
    }

    private void Update()
    {
        if (isCrossFading) return;

        if (!playBgm)
            StopAllBGM();
        else
        {
            if (!bgm[bgmIndex].isPlaying)
                PlayBGM(bgmIndex);
        }
    }



    public void PlaySFX(int _sfxIndex, Transform _source)
    {
        //if (sfx[_sfxIndex].isPlaying)
        //    return;

        if (canPlaySFX == false)
            return;


        if (_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > sfxMinimumDistance)
            return;


        if (_sfxIndex < sfx.Length)
        {

            sfx[_sfxIndex].pitch = Random.Range(.85f, 1.1f);
            sfx[_sfxIndex].Play();
        }
    }

    public void StopSFX(int _index) => sfx[_index].Stop();

    public void StopSFXWithTime(int _index) => StartCoroutine(DecreaseVolume(sfx[_index]));

    private IEnumerator DecreaseVolume(AudioSource _audio)
    {
        float defaultVolume = _audio.volume;

        while (_audio.volume > .1f)
        {
            _audio.volume -= _audio.volume * .2f;
            yield return new WaitForSeconds(.6f);

            if (_audio.volume <= .1f)
            {
                _audio.Stop();
                _audio.volume = defaultVolume;
                break;
            }
        }

    }

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;

        StopAllBGM();
        bgm[bgmIndex].Play();
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    private void AllowSFX() => canPlaySFX = true;

    public IEnumerator FadeOutCurrentBGM(float duration)
    {
        AudioSource src = bgm[bgmIndex];
        float startVol = src.volume;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            src.volume = Mathf.Lerp(startVol, 0, t / duration);
            yield return null;
        }
        src.Stop();
        src.volume = startVol;
    }

    public IEnumerator FadeInBGM(int newIndex, float duration)
    {
        AudioSource src = bgm[newIndex];
        float targetVol = 1f;
        src.volume = 0;
        src.Play();
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            src.volume = Mathf.Lerp(0, targetVol, t / duration);
            yield return null;
        }
    }

    public void CrossFadeBGM(int newIndex, float fadeOutTime, float fadeInTime)
    {
        if (isCrossFading) return;
        isCrossFading = true;
        StartCoroutine(CrossFadeCoroutine(newIndex, fadeOutTime, fadeInTime));
    }

    private IEnumerator CrossFadeCoroutine(int newIndex, float fadeOutTime, float fadeInTime)
    {
        yield return FadeOutCurrentBGM(fadeOutTime);
        yield return FadeInBGM(newIndex, fadeInTime);
        bgmIndex = newIndex;
        isCrossFading = false;
    }
}
