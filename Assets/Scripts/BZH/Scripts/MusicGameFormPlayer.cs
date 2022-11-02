using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicGameFormPlayer : MonoBehaviour
{
	[SerializeField, DisplayName("音频")]
	private AudioClip realClip;

	[SerializeField, DisplayName("BPM")]
	private float bpm;

	[SerializeField, DisplayName("分子")]
	private sbyte molecule;

	[SerializeField, DisplayName("分母")]
	private sbyte deno;

	[SerializeField, DisplayName("播放延迟(毫秒)")]
	private float playDelay;

	[SerializeField, DisplayName("开始时播放")]
	private bool playOnStart;

	private float durationPerBeat;

	private AudioSource source;

	private Timer timer;

	public MusicGameFormPlayer()
	{
		realClip = null;
		bpm = 100.0f;
		playDelay = 0.0f;
	}

	private void Awake()
	{
		durationPerBeat = 60.0f / bpm;
	}

	// Start is called before the first frame update
    void Start()
    {
	    
	    source = GetComponent<AudioSource>();
	    if (playOnStart)
	    {
		    Invoke("Play", 3.0f);
	    }
    }

    // Update is called once per frame
    void Update()
    {
	    //Displayer.text = $"{Beat}\n{Section}\n{Section * molecule}\n{BeatInSection}";
    }

    private float playDelayInSeconds
    {
	    get => playDelay / 1000.0f;
    }

    public float DurationPerBeat
    {
	    get => durationPerBeat;
    }

    public float Beat
    {
	    get
	    {
		    float playTime = source.time - playDelayInSeconds;
		    return (playTime / durationPerBeat) + 1.0f;
	    }
    }

    public int Section
    {
	    get => (int)((Beat - 1.0f) / molecule) + 1;
    }

    public float BeatInSection
    {
	    get
	    {
		    /*float beat = Beat;
		    int section = (int)((Beat - 1.0f) / molecule) + 1;
		    return (int)(beat) % (section * molecule);*/
		    float beat = Beat;
		    int section = (int)((beat - 1.0f) / molecule);
		    return beat - section * molecule;
	    }
    }

    public float ToPlayTime(float beat)
    {
	    return (beat - 1) * durationPerBeat;
    }

    public float ToPlayTime(int section, float beatInSection)
    {
	    return ToPlayTime((section - 1) * molecule + beatInSection);
    }

    public void Play()
    {
	    source.clip = realClip;
	    if (playDelay > 0.0f)
	    {
		    source.Play();
		    source.time = playDelayInSeconds;
	    }
	    else
	    {
		    source.PlayDelayed(playDelayInSeconds);
	    }
	    //TimerManager.GetTimerManager().SetTimer(AddBeat, durationPerBeat, 0.0f, 32L);
    }

    public void RevertTo(float beat)
    {
	    source.time = playDelayInSeconds + ToPlayTime(beat);
    }

    public TMP_Text Displayer;
}
