using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MusicManager : SingletonMonoBehaviour<MusicManager>
{
    [System.Serializable]
    public class Music
    {
        public AudioClip m_fileName;
    }

    [System.Serializable]
    public class Album
    {
        public string m_id;
        public List<Music> m_music;
        Queue<Music> m_queue;

        public void Init()
        {
            m_queue = new Queue<Music>();
            foreach(Music music in m_music)
            {
                m_queue.Enqueue(music);
            }
        }

        public Music GetMusicToPlay()
        {
            m_queue.TryDequeue(out Music music);
            m_queue.Enqueue(music);
            return music;
        }
    }

    public List<Album> m_albums;

    AudioSource m_audioSource;
    bool m_isPaused = false;

    new public void Awake()
    {
        base.Awake();
        TryGetComponent(out m_audioSource);
        foreach (Album album in m_albums)
            album.Init();
    }

    public void PlayAlbum(string id)
    {
        Album album = m_albums.Find(a => a.m_id == id);
        StartCoroutine(PlayAlbum(album));
    }

    IEnumerator PlayAlbum(Album album)
    {
        while (true)
        {
            if (m_audioSource.isPlaying)
                yield return null;

            if (m_isPaused)
                yield return null;

            m_audioSource.clip = album.GetMusicToPlay().m_fileName;
            m_audioSource.Play();
            yield return new WaitForSecondsRealtime(m_audioSource.clip.length);
        }
    }

    public void PauseMusic(float time)
    {
        StartCoroutine(PauseMusicFor(time));
    }

    IEnumerator PauseMusicFor(float time)
    {
        float volume = Mathf.Max(m_audioSource.volume, 0);
        float timer = 0;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            m_audioSource.volume = Mathf.Lerp(volume, 0, 2 * timer);
            yield return null;
        }

        m_audioSource.Pause();
        m_isPaused = true;
        yield return new WaitForSecondsRealtime(time);
        m_audioSource.UnPause();
        m_isPaused = false;

        timer = 0;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            m_audioSource.volume = Mathf.Lerp(0, volume, 2 * timer);
            yield return null;
        }
    }
}
