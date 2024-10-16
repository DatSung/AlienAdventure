using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    public static AudioManagement instance { get; private set; }
    private AudioSource AudioSource;
    [SerializeField] private AudioClip[] backgroundSound;

    [SerializeField] private int mapCount;
    private int soundChangerIndex = 0;
    // Start is called before the first frame update
    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        instance = this;
    }

    public void PlaySound(AudioClip clipSource)
    {
        //AudioSource.Stop();
        AudioSource.PlayOneShot(clipSource);
    }
    public void PlayEventually(string audioAction)
    {

        //changing audio logic
        var audioCount = backgroundSound.Length;

        switch (audioAction)
        {
            case "next":
                {
                    soundChangerIndex++;
                    break;
                }
            case "previous":
                {
                    soundChangerIndex--;
                    break;
                }
        }
        //make sure no out of list
        if (soundChangerIndex < 0 || soundChangerIndex > audioCount)
        {
            soundChangerIndex = 0;
        }
        
        PlaySound(backgroundSound[soundChangerIndex]);
    }
    public void StopSound() {
        AudioSource.Stop();
    }
}
