using System;
using System.Collections.Generic;
using Jump4YourLife.Scripts.Core.Enums;
using Jump4YourLife.Scripts.Development.Utils;
using UnityEngine;

namespace Jump4YourLife.Scripts.Development.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Serializable]
        public struct SoundEntry
        {
            public AudioLibrarySounds sound;
            public AudioClip clip;
        }
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioClip bgmClip;
        [SerializeField] private List<SoundEntry> soundEntries = new List<SoundEntry>();

        private readonly Dictionary<AudioLibrarySounds, AudioClip> _clips = new Dictionary<AudioLibrarySounds, AudioClip>();
        private const string MuteKey = "IS_MUTED";

        public bool IsMuted { get; private set; }

        public override void Awake()
        {
            base.Awake();
            if (Instance != this) return;
            foreach (SoundEntry entry in soundEntries)
            {
                _clips[entry.sound] = entry.clip;
            }
            IsMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
            ApplyMute();
        }
        private void Start()
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        public static void PlaySound(AudioLibrarySounds sound)
        {
            Instance?.Play(sound);
        }

        public void ToggleMute()
        {
            IsMuted = !IsMuted;
            ApplyMute();
            PlayerPrefs.SetInt(MuteKey, IsMuted ? 1 : 0);
            PlayerPrefs.Save();
        }
        private void Play(AudioLibrarySounds sound)
        {
            if (IsMuted) return;
            if (_clips.TryGetValue(sound, out AudioClip clip))
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        private void ApplyMute()
        {
            sfxSource.mute = IsMuted;
            bgmSource.mute = IsMuted;
        }
    }
}
