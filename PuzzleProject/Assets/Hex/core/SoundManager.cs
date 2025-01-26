using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip m_fileName;
    }

    [System.Serializable]
    public class SoundGroup
    {
        public string m_id;
        public List<Sound> m_sounds;

        [Header("Volume between 0 and 1")]
        public float m_volume = 1f;

        Queue<Sound> m_queue;

        public void Init()
        {
            m_queue = new Queue<Sound>();
            foreach (Sound sound in m_sounds)
            {
                m_queue.Enqueue(sound);
            }
        }

        public Sound GetSound()
        {
            m_queue.TryDequeue(out Sound sound);
            m_queue.Enqueue(sound);
            return sound;
        }
    }

    public List<SoundGroup> m_soundGroups;

    AudioSource[] m_audioSources;

    new public void Awake()
    {
        base.Awake();
        m_audioSources = GetComponents<AudioSource>();

        foreach (SoundGroup group in m_soundGroups)
            group.Init();
    }

    public void PlaySound(string id)
    {
        SoundGroup group = m_soundGroups.Find(a => a.m_id == id);
        AudioSource source = GetAudioSource();
        Sound sound = group.GetSound();
        source.clip = sound.m_fileName;
        source.volume = group.m_volume;
        source.Play();
    }

    AudioSource GetAudioSource()
    {
        foreach(AudioSource audio in m_audioSources)
        {
            if (audio.isPlaying == false)
            {
                return audio;
            }
        }

        return m_audioSources[Random.Range(0, m_audioSources.Length)];
    }

    public void PlayInteruptingSound(string id)
    {
        SoundGroup group = m_soundGroups.Find(a => a.m_id == id);
        AudioSource source = GetAudioSource();
        Sound sound = group.GetSound();
        source.clip = sound.m_fileName;
        source.volume = group.m_volume;
        MusicManager.Instance().PauseMusic(source.clip.length);
        source.Play();
    }
}
