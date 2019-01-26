using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

namespace QuasarGames
{

    public enum SoundPriority
    {
        LOW,
        MEDIUM,
        HIGH
    }

    [System.Serializable]
    public class SoundItem
    {
        public string name;

        [Range(0f, 1f)]
        public float volume = 1f;
        public AudioClip clip;
    }

    public class AudioManager : MonoBehaviour
    {

        public static AudioManager Instance;

        public AudioMixer mainMixer;

        public AudioSource musicSource;

        [Space(20)]
        [Header("SAMPLES")]
        public bool samplingEnabled = false;

        private float[] musicSamples = new float[64];
        public float[] normSamples = new float[6];
        public float sampleSpeedUp = 1f;
        public float sampleSpeedDown = 1f;

        [Space(20)]
        [Header("MUSIC")]
        public AudioClip[] musicTracks;
        public bool randomTrack = false;
        public int lastTrackID = -1;

        [Space(20)]
        [Header("MUTE")]
        public bool muteMusic;
        public bool muteSFX;


        // SOUNDS PRIORITY
        [Space(20), Header("Low and Med priority tracks")]
        [SerializeField]
        private GameObject containerLow = null;
        [SerializeField]
        private int maxTracksLow = 8;
        [SerializeField]
        private List<AudioTrack> tracksLow = new List<AudioTrack>();
        private int indexL = 0;

        [SerializeField]
        private GameObject containerMed = null;
        [SerializeField]
        private int maxTracksMed = 4;
        [SerializeField]
        private List<AudioTrack> tracksMed = new List<AudioTrack>();
        private int indexM = 0;

        [Space(20), Header("High priority tracks are created in need")]
        [SerializeField]
        private GameObject containerHigh = null;
        private List<AudioTrack> tracksHigh = new List<AudioTrack>();

        void Awake()
        {
            Instance = this;
        }

        // Use this for initialization
        void Start()
        {
            // AudioSources containers

            // LOW
            containerLow = new GameObject("LowPriority");
            containerLow.transform.parent = this.transform;

            for (int i = 0; i < maxTracksLow; i++)
            {
                AudioSource source = containerLow.AddComponent<AudioSource>();

                source.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Master/SFX")[0];

                AudioTrack newTrack = new AudioTrack();

                newTrack.audioManager = this;
                newTrack.source = source;

                newTrack.priority = SoundPriority.LOW;

                tracksLow.Add(newTrack);
            }

            // MEDIUM
            containerMed = new GameObject("MedPriority");
            containerMed.transform.parent = this.transform;

            for (int i = 0; i < maxTracksMed; i++)
            {
                AudioSource source = containerMed.AddComponent<AudioSource>();

                source.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Master/SFX")[0];

                AudioTrack newTrack = new AudioTrack();

                newTrack.audioManager = this;
                newTrack.source = source;

                newTrack.priority = SoundPriority.MEDIUM;

                tracksMed.Add(newTrack);
            }

            // HIGH
            containerHigh = new GameObject("HighPriority");
            containerHigh.transform.parent = this.transform;


            switch (PrefsCenter.MuteMusic)
            {
                case "ON":
                    muteMusic = true;
                    break;
                case "OFF":
                    muteMusic = false;
                    break;
                default:
                    muteMusic = false;
                    PrefsCenter.MuteMusic = "OFF";
                    break;
            }

            switch (PrefsCenter.MuteSFX)
            {
                case "ON":
                    muteSFX = true;
                    break;
                case "OFF":
                    muteSFX = false;
                    break;
                default:
                    muteSFX = false;
                    PrefsCenter.MuteSFX = "OFF";
                    break;
            }

            PlayMusic();

        }

        void Update()
        {
            // MUSIC
            if (!muteMusic && !musicSource.isPlaying)
                PlayMusic();

            if (samplingEnabled)
            {
                musicSource.GetSpectrumData(musicSamples, 0, FFTWindow.Rectangular);
                // 64 = 2 - 4 - 8 - 16 - 32 - 64
                // 0-1
                // 2-3
                // 4-7
                // 8-15
                // 16-31
                // 32-63

                float[] tempSamples = new float[normSamples.Length];

                int j = 0;
                for (int i = 0; i < musicSamples.Length; i++)
                {
                    if (i > Mathf.Pow(2, j + 1) - 1)
                    {
                        j++;
                    }

                    tempSamples[j] += musicSamples[i];
                }

                float deltaTime = Time.deltaTime;
                for (int i = 0; i < normSamples.Length; i++)
                {
                    if (tempSamples[i] > normSamples[i])
                    {
                        normSamples[i] += Mathf.Clamp(tempSamples[i] - normSamples[i], 0, sampleSpeedUp * deltaTime);
                    }
                    else
                    {
                        normSamples[i] += Mathf.Clamp(tempSamples[i] - normSamples[i], -sampleSpeedDown * deltaTime, 0);
                    }
                }
            }

            if (!muteSFX)
            {
                int i = 0;

                // HIGH
                tracksHigh.ForEach(track => track.UpdateTrack());


                // LOW
                bool nothingPlaying = true;
                for (i = 0; i < tracksLow.Count; i++)
                {
                    tracksLow[i].UpdateTrack();

                    if (tracksLow[i].playing)
                    {
                        nothingPlaying = false;
                    }
                }

                if (nothingPlaying)
                {
                    indexL = 0;
                    for (i = 0; i < tracksLow.Count; i++)
                    {
                        tracksLow[i].index = 0;
                    }

                }


                // MEDIUM
                nothingPlaying = true;
                for (i = 0; i < tracksMed.Count; i++)
                {
                    tracksMed[i].UpdateTrack();

                    if (tracksMed[i].playing)
                    {
                        nothingPlaying = false;
                    }
                }

                if (nothingPlaying)
                {
                    indexM = 0;
                    for (i = 0; i < tracksMed.Count; i++)
                    {
                        tracksMed[i].index = 0;
                    }

                }
            }
        }


        // PRIVATE

        /// Creates a new sound, registers it, and gives it the properties specified
        private AudioTrack NewSound(string soundName, SoundPriority priority = SoundPriority.LOW, float volume = 1f, float pitch = 1f, bool loop = false, Action<AudioTrack> callback = null)
        {
            int i = 0;

            if (priority == SoundPriority.HIGH)
            {
                AudioSource sourceH = containerHigh.AddComponent<AudioSource>();
                sourceH.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Master/SFX")[0];

                AudioTrack trackH = new AudioTrack();

                trackH.audioManager = this;

                trackH.priority = SoundPriority.HIGH;

                trackH.loop = loop;
                trackH.source = sourceH;
                trackH.source.loop = loop;

                trackH.volume = volume;
                trackH.pitch = pitch;
                trackH.SetClip(soundName);

                trackH.callback = callback;

                tracksHigh.Add(trackH);

                return trackH;
            }
            else if (priority == SoundPriority.MEDIUM)
            {
                AudioTrack trackM = new AudioTrack();

                AudioTrack trackWithLowestIndex = tracksMed[0];
                int minIndex = tracksMed[0].index;

                bool freeTrackFound = false;
                for (i = 0; i < tracksMed.Count; i++)
                {
                    if (tracksMed[i].clip == null)
                    {
                        trackM = tracksMed[i];

                        indexM++;
                        trackM.index = indexM;

                        freeTrackFound = true;
                        break;
                    }

                    if (tracksMed[i].index < minIndex)
                    {
                        minIndex = tracksMed[i].index;
                        trackWithLowestIndex = tracksMed[i];
                    }
                }

                if (!freeTrackFound)
                {
                    trackM = trackWithLowestIndex;
                }


                trackM.priority = SoundPriority.MEDIUM;

                trackM.loop = loop;
                trackM.source.loop = loop;

                trackM.volume = volume;
                trackM.pitch = pitch;
                trackM.SetClip(soundName);


                trackM.callback = callback;

                return trackM;
            }
            else
            {
                AudioTrack trackL = new AudioTrack();

                AudioTrack trackWithLowestIndex = tracksLow[0];
                int minIndex = tracksLow[0].index;

                bool freeTrackFound = false;
                for (i = 0; i < tracksLow.Count; i++)
                {
                    if (tracksLow[i].clip == null)
                    {
                        trackL = tracksLow[i];

                        indexL++;
                        trackL.index = indexL;

                        freeTrackFound = true;
                        break;
                    }

                    if (tracksLow[i].index < minIndex)
                    {
                        minIndex = tracksLow[i].index;
                        trackWithLowestIndex = tracksLow[i];
                    }
                }

                if (!freeTrackFound)
                {
                    trackL = trackWithLowestIndex;
                }

                trackL.priority = SoundPriority.LOW;

                trackL.loop = loop;
                trackL.source.loop = loop;

                trackL.volume = volume;
                trackL.pitch = pitch;
                trackL.SetClip(soundName);

                trackL.callback = callback;

                return trackL;

            }


        }


        // PUBLIC INTERFACE

        public void ToggleMusic()
        {

            muteMusic = !muteMusic;

            if (muteMusic && musicSource.isPlaying)
            {
                StopMusic();
            }

            if (muteMusic)
                PrefsCenter.MuteMusic = "ON";
            else
                PrefsCenter.MuteMusic = "OFF";

            PlayMusic();
        }

        public void ToggleSFX()
        {
            muteSFX = !muteSFX;

            if (muteSFX)
                PrefsCenter.MuteSFX = "ON";
            else
                PrefsCenter.MuteSFX = "OFF";
        }


        // Creates a new sound, registers it, gives it the properties specified, and starts playing it
        public void PlaySoundDefault(string soundName)
        {
            PlaySound(soundName);
        }

        public void PlaySound(string soundName, SoundPriority priority = SoundPriority.LOW, float volume = 1f, float pitch = 1f, bool loop = false, Action<AudioTrack> callback = null)
        {
            if (!muteSFX)
            {
                AudioTrack track = NewSound(soundName, priority, volume, pitch, loop, callback);
                track.playing = true;
            }
        }

        public bool StopSound(string soundName, SoundPriority priority = SoundPriority.LOW, Action<AudioTrack> callback = null)
        {
            bool soundStopped = false;

            return soundStopped;
        }

        public void RemoveTrack(AudioTrack rTrack)
        {
            if (rTrack.priority == SoundPriority.HIGH)
            {
                Destroy(rTrack.source);
                rTrack.source = null;
                tracksHigh.Remove(rTrack);
            }
        }


        public void PlayMusic()
        {

            if (!muteMusic && !musicSource.isPlaying)
            {
                int nextTrackID;

                if (randomTrack)
                {
                    nextTrackID = UnityEngine.Random.Range(0, musicTracks.Length - 1);

                    if (nextTrackID >= lastTrackID)
                    {
                        nextTrackID++;
                    }
                }
                else
                {
                    nextTrackID = lastTrackID + 1;
                }

                if (nextTrackID >= musicTracks.Length)
                {
                    nextTrackID = 0;
                }

                musicSource.clip = musicTracks[nextTrackID];
                musicSource.Play();
                lastTrackID = nextTrackID;
            }

        }

        public void StopMusic()
        {
            musicSource.Stop();
        }


        public void SetMasterVolume(float newVolume = 1f)
        {
            mainMixer.SetFloat("MasterVolume", Mathf.Log(Mathf.Clamp(newVolume, 0.001f, 1f)) * 20);

            //  TODO 
            // For Snapshots

            //if (newVolume == 1f)
            //{
            //    mainMixer.ClearFloat("MasterVolume");
            //}
        }

        // GET

    }
}