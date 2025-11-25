using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(AudioSource))]

public class BeatDetector : MonoBehaviour
{
    public static BeatDetector Instance
    {
        get;
        private set;
    }
    public static float Loudness
    {
        get;
        private set;
    }

    [Header("Analysis Settings")]
    public int sampleRate = 1024;
    public float beatThreshold = 1.35f;
    public int historyLength = 43;

    [Header("Events")]
    public UnityEvent OnBeat;

    AudioSource source;
    public Slider slider;
    private float[] samples;
    private float[] history;
    int historyPos;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        samples = new float[sampleRate];
        history = new float[historyLength];
        historyPos = 0;
    }
    private void Update()
    {

        if (!source.isPlaying) return;

        source.pitch = slider.value;

        source.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        float energy = 0;
        for(int i = 0; i<sampleRate; i++)
        {
            energy += samples[i] * samples[i];
        }
        Loudness = energy;

        float avgEnergy = 0;
        for(int i = 0;i<historyLength; i++)
        {
            avgEnergy += history[i];
        }
        avgEnergy /= historyLength;

        if(energy> beatThreshold *avgEnergy)
        {
            OnBeat.Invoke();
        }

        history[historyPos] = energy;
        historyPos = (historyPos + 1)%historyLength;
    }
}
