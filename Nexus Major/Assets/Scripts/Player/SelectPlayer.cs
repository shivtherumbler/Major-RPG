using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SelectPlayer : MonoBehaviour
{
    public GameObject youngPlayer;
    public GameObject finalPlayer;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public GameObject gameoverPanel;

    static bool AudioBegin = false;
    public AudioMixer audioMixer;
    public AudioMixerGroup mixerGroup;
    public Slider slide;
    public bool fight;

    private void Awake()
    {
        int randomStartTime = Random.Range(0, audioClips[0].samples - 1); //clip.samples is the lengh of the clip in samples
        audioSource.timeSamples = randomStartTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        audioSource.outputAudioMixerGroup = mixerGroup;

        float volume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        slide.value = volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(finalPlayer.GetComponent<Animator>().GetBool("Battle") == true && fight == true)
        {
            
            audioSource.clip = audioClips[1];
            audioSource.enabled = false;
            audioSource.enabled = true;
            fight = false;
            int randomStartTime = Random.Range(0, audioClips[1].samples - 1); //clip.samples is the lengh of the clip in samples
            audioSource.timeSamples = randomStartTime;
        }
        else if(finalPlayer.GetComponent<Animator>().GetBool("Battle") == false && fight == false)
        {
            audioSource.clip = audioClips[0];
            audioSource.enabled = false;
            audioSource.enabled = true;
            fight = true;
            int randomStartTime = Random.Range(0, audioClips[0].samples - 1); //clip.samples is the lengh of the clip in samples
            audioSource.timeSamples = randomStartTime;
        }

        if(gameoverPanel.activeInHierarchy)
        {
            finalPlayer.GetComponent<Player>().audioSource.PlayOneShot(gameObject.GetComponent<Player>().clips[18]);

        }

    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        //PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
