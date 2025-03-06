using System.Collections.Generic;
using UnityEngine;

namespace _scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        
        public Sound[] musicSounds, sfxSounds;
        public AudioSource musicSource, sfxSource;

        private Dictionary<string, Sound> musicDict;
        private Dictionary<string, Sound> sfxDict;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            InitializeDictionaries();
        }

        private void InitializeDictionaries()
        {
            musicDict = new Dictionary<string, Sound>();
            
            foreach (var sound in musicSounds)
            {
                musicDict.TryAdd(sound.soundName, sound);
            }

            sfxDict = new Dictionary<string, Sound>();

            foreach (var sound in sfxSounds)
            {
                sfxDict.TryAdd(sound.soundName, sound);
            }
        }

        private void Start()
        {
            PlayMusic("SoundMenu");
        }

        public void PlayMusic(string musicName)
        {
            if (musicDict.TryGetValue(musicName, out var sound))
            {
                musicSource.clip = sound.clip;
                musicSource.Play();
            }
            else
            {
                Debug.LogWarning($"Music sound '{musicName}' not found!");
            }
        }

        public void PlaySfx(string sfxName)
        {
            if (sfxDict.TryGetValue(sfxName, out var sound))
            {
                sfxSource.PlayOneShot(sound.clip);
            }
            else
            {
                Debug.LogWarning($"SFX sound '{sfxName}' not found!");
            }
        }
    }
}