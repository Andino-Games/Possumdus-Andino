using UnityEngine;

namespace _scripts.Audio
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Sound", order = 0)]
    public class Sound : ScriptableObject
    {
        public string soundName;
        public AudioClip clip;
    }
}