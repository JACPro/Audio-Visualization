using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    AudioSource audioSource;
    public static float[] samples = new float[512];
    public static float[] bands = new float[8];
    public static float[] bandBuffer = new float[8];
    public static float[] bufferDecrease = new float[8];


    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void Update()
    {
        GetSpectrumAudioSource();
        MakeFqBands();
        BandBuffer();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    private void MakeFqBands()
    {
        /*
        - 0 -   2       86Hz
        - 1 -   4       87-258Hz
        - 2 -   8       259-602Hz
        - 3 -   16      603-1290Hz
        - 4 -   32      1291-2666Hz
        - 5 -   64      2667-5418Hz
        - 6 -   128     5419-10922Hz
        - 7 -   256     10923-21930Hz
        Total: 510
        */

        int count = 0;

        //for each band we're grouping into
        for (int i = 0; i < 8; i++) {
            float avg = 0;            
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            //if it's the last band, add 2 to get the final 2 elements in the samples array
            if (i == 7) {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++) {
                avg += samples[count] * (count + 1);
                count++;
            }

            avg /= count;

            bands[i] = avg * 10;
        }
    }

    private void BandBuffer()
    {
        for (int i = 0; i < 8; i++) {
            if (bands[i] > bandBuffer[i]) {
                bandBuffer[i] = bands[i];
                bufferDecrease[i] = 0.005f;
            } else if (bands[i] < bandBuffer[i]) {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }

        }
    }
}
