using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace QuasarGames
{

    [Serializable]
    public class AudioTrack
    {
        public AudioManager audioManager;

        public string name;
        public int index = 0;
        public SoundPriority priority;

        public AudioClip clip;
        public AudioSource source;
        public Action<AudioTrack> callback;

        [Range(0f, 1f)]
        public float volume = 1f;
        public float pitch = 1f;
        public bool loop; // loop doesnt work yet
        public bool isPaused = false;

        /// returns true if the sound has finished playing
        /// will always be false for looping sounds
        public bool finished
        {
            get
            {
                if (source == null || clip == null)
                    return true;

                return !loop && (!source.isPlaying && !isPaused);
            }
        }

        /// returns true if the sound is currently playing,
        /// false if it is paused or finished
        /// can be set to true or false to play/pause the sound
        /// will register the sound before playing
        public bool playing
        {
            get
            {
                return (source.clip != null && source.isPlaying);
            }
            set
            {
                PlayOrPause(value);
            }
        }

        public void SetClip(string newName)
        {
            name = newName;
            clip = (AudioClip)Resources.Load("Media/Audio/Sounds/" + name, typeof(AudioClip));
            if (clip == null)
            {
                throw new Exception("Couldn't find AudioClip with name '" + name + "'. Are you sure the file is in a folder named 'Resources'?");
            }
            else
            {
                source.clip = clip;
                source.volume = volume;
                source.pitch = pitch;
            }
        }

        public void UpdateTrack()
        {
            if (finished)
                Finish();
        }

        /// Try to avoid calling this directly
        /// Use the Sound.playing property instead
        public void PlayOrPause(bool play)
        {
            if (play && !source.isPlaying)
            {
                source.Play();
                isPaused = false;
            }
            else
            {
                source.Pause();
                isPaused = true;
            }
        }

        /// performs necessary actions when a sound finishes
        public void Finish()
        {
            PlayOrPause(false);

            if (callback != null)
                callback(this);

            clip = null;
            source.clip = null;

            index = 0;

            if (priority == SoundPriority.HIGH)
            {
                audioManager.RemoveTrack(this);
            }

        }

        /// Reset the sound to its beginning
        public void Reset()
        {
            source.time = 0f;
        }
    }
}