using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public AudioSource audiosource = null;
    public static AudioManager instance = null;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        Debug.Assert(audiosource != null, "mission audio source on audio manager");
    }

    public void SwitchAudio(int indx)
    {
        audiosource.clip = audioClips[indx];
    }
}
