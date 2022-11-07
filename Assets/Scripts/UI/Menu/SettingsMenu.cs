using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider _volumeLevel;
    [HideInInspector]
    public AudioManager _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _volumeLevel.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume) // Daha sonra ses sayisi artarsa audio mixer ile  ses kis .
    {
        _audioManager._sounds[0].source.volume = volume;
    }


}