using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    enum IsPlaying { None, Source1, Source2 }

    public enum Track
    {
        Menu,
        Game,
        _count
    }
    [SerializeField, Universal.Attributes.EnumArray(typeof(Track))] List<AudioClip> tracks;
    [SerializeField] float fadeDuration;
    [SerializeField] AnimationCurve fadeInCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] AnimationCurve fadeOutCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    [SerializeField] AudioSource s1, s2;
    IsPlaying currentSource;
    IsPlaying targetSource;
    [Header("Runtime")]
    [SerializeField] Track cTrack = Track._count;
    float timer;
    
    public static MusicManager inst { get; private set; }
    
    void Awake()
    {
        if (inst != null)
        {
            Destroy(this);
            return;
        }
        
        DontDestroyOnLoad(this);
        inst = this;
        timer = fadeDuration;
    }
    void OnDestroy()
    {
        if(inst == this)
            inst = null;
    }
    void Update()
    {
        if(timer >= fadeDuration) return;

        timer += Time.unscaledDeltaTime;
        float tIn = fadeInCurve.Evaluate(timer / fadeDuration);
        float tOut = fadeOutCurve.Evaluate(timer / fadeDuration);
        
        switch (currentSource)
        {
            case IsPlaying.Source1:
                s1.volume = Mathf.Lerp(0, 1, tOut);
                break;
            case IsPlaying.Source2:
                s2.volume = Mathf.Lerp(0, 1, tOut);
                break;
        }

        switch (targetSource)
        {
            case IsPlaying.Source1:
                s1.volume = Mathf.Lerp(0, 1, tIn);
                break;
            case IsPlaying.Source2:
                s2.volume = Mathf.Lerp(0, 1, tIn);
                break;
        }

        if (timer >= fadeDuration)
        {
            switch (currentSource)
            {
                case IsPlaying.Source1:
                    s1.Stop();
                    break;
                case IsPlaying.Source2:
                    s2.Stop();
                    break;
            }
            currentSource = targetSource;
        }
    }
    public void Play()
    {
        if(cTrack == Track._count) return;
        if(targetSource == IsPlaying.None) 
            targetSource = IsPlaying.Source1;
        switch (targetSource)
        {
            case IsPlaying.Source1:
                s1.volume = 0;
                s1.Play();
                break;
            case IsPlaying.Source2:
                s2.volume = 0;
                s2.Play();
                break;
        }
        timer = 0;
    }
    public void Stop()
    {
        if(currentSource == IsPlaying.None) return;
        targetSource = IsPlaying.None;
        timer = 0;
    }
    public void PlayTrack(Track track)
    {
        if(track == cTrack) return;
        cTrack = track;
        switch (currentSource)
        {
            case IsPlaying.None:
            case IsPlaying.Source2:
                targetSource = IsPlaying.Source1;
                s1.clip = tracks[(int)track];
                break;
            case IsPlaying.Source1:
                targetSource = IsPlaying.Source2;
                s2.clip = tracks[(int)track];
                break;
        }

        Play();
    }
}