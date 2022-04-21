using UnityEngine;
//using Lofelt.NiceVibrations;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; protected set; }
    [HideInInspector] public bool isHeavyVibration;

    void Awake()
    {
        Instance = this;
    }

    public void Vibrate()
    {
        var freq = 0.4f;
        if (isHeavyVibration)
        {
            isHeavyVibration = false;
            freq = 0.9f;
        }
        //HapticPatterns.PlayEmphasis(freq, 0.0f);
    }
}