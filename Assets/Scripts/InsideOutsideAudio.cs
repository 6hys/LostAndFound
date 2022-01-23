using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideOutsideAudio : MonoBehaviour
{
    public AudioSource insideAudio;
    public AudioSource outsideAudio;

    public AudioSource insideFootsteps;
    public AudioSource outsideFootsteps;

    public AudioSource TVStatic;

    public PlayerController playerController;

    public bool isInside = false;
    public bool onFirstFloor = false;

    private float outsideVol;
    private float insideVol;
    private float TVVol;

    private float fadeTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        outsideVol = outsideAudio.volume;
        insideVol = insideAudio.volume; 
        TVVol = TVStatic.volume;
    }

    // Update is called once per frame
    void Update()
    {
        // Character is moving
        if (playerController.isGrounded && playerController.isMovementEnabled && playerController.isMoving)
        {
            if (isInside && insideFootsteps.isPlaying == false)
            {
                insideFootsteps.Play();
            }
            else if (!isInside && outsideFootsteps.isPlaying == false)
            {
                outsideFootsteps.Play();
            }
        }
        else
        {
            if(isInside && insideFootsteps.isPlaying)
            {
                insideFootsteps.Stop();
            }
            else if(!isInside && outsideFootsteps.isPlaying)
            {
                outsideFootsteps.Stop();
            }
        }

        // Character is on the second floor.

        onFirstFloor = (playerController.gameObject.transform.position.y >= 7f);
        
        if(onFirstFloor && TVStatic.isPlaying)
        {
            StartCoroutine(FadeOut(TVStatic));
        }
        else if(isInside && !onFirstFloor && TVStatic.isPlaying == false)
        {
            StartCoroutine(FadeIn(TVStatic, TVVol));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(FadeIn(insideAudio, insideVol));
            StartCoroutine(FadeOut(outsideAudio));
        }

        isInside = true;
        if(outsideFootsteps.isPlaying)
        {
            outsideFootsteps.Stop();
        }

        StartCoroutine(FadeIn(TVStatic, TVVol));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeIn(outsideAudio, outsideVol));
            StartCoroutine(FadeOut(insideAudio));
        }

        isInside = false;
        if (insideFootsteps.isPlaying)
        {
            insideFootsteps.Stop();
        }

        StartCoroutine(FadeOut(TVStatic));
    }

    // Audio fading info from here: https://forum.unity.com/threads/fade-out-audio-source.335031/

    public IEnumerator FadeOut(AudioSource audio)
    {
        float startVolume = audio.volume;

        while(audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audio.Stop();
        audio.volume = startVolume;
    }

    IEnumerator FadeIn(AudioSource audio, float volume)
    {
        audio.volume = 0f;
        audio.Play();

        while(audio.volume < volume)
        {
            audio.volume += volume * Time.deltaTime / fadeTime;

            yield return null;
        }
    }
}
