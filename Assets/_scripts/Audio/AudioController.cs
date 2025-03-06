using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

namespace _scripts.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        private void Start()
        {
            if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume"))
            {
                LoadVolume();
            }
            else
            {
                SetMusicVolume();
                SetSfxVolume();
            }
        }

        private void LoadVolume()
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            SetMusicVolume();
            SetSfxVolume();
        }
        
        private void SetMusicVolume()
        {
            float volume = musicSlider.value;
            audioMixer.SetFloat("Music", MathF.Log10(volume) * 20);
            PlayerPrefs.SetFloat("musicVolume", volume);

        }
        private void SetSfxVolume()
        {
            Debug.Log("Sonó un efecto :')");
            float volume = sfxSlider.value;
            audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("sfxVolume", volume);
        }

    }
}