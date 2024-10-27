using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundsButton;
    [SerializeField] private Color _soundOnColor;
    [SerializeField] private Color _soundOffColor;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundsVolumeKey = "SoundsVolume";

    private float _musicVolume;
    private float _soundsVolume;

    private float _offVolume = -80;

    private bool _isMusicOff;
    private bool _isSoundsOff;

    private void Awake()
    {
        _audioMixer.GetFloat(MusicVolumeKey, out _musicVolume);
        _audioMixer.GetFloat(SoundsVolumeKey, out _soundsVolume);
    }

    private void Calculate(float volume, Button button , string volumeKey, float soundVolume)
    {
        if (volume > _offVolume)
        {
            _audioMixer.SetFloat(volumeKey, _offVolume);
            button.GetComponent<Image>().color = _soundOffColor;
        }
        else
        {
            _audioMixer.SetFloat(volumeKey, soundVolume);
            button.GetComponent<Image>().color = _soundOnColor;
        }
    }

    public void ToggleMusic()
    {
        _audioMixer.GetFloat(MusicVolumeKey, out float _volume);
        Calculate(_volume, _musicButton, MusicVolumeKey, _musicVolume);
    }

    public void ToggleSounds()
    {
        _audioMixer.GetFloat(SoundsVolumeKey, out float _volume);
        Calculate(_volume, _soundsButton, SoundsVolumeKey , _soundsVolume);
    }
}
