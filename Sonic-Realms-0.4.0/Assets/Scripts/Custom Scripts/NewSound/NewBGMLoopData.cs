﻿using UnityEngine;

    /// <summary>
    /// Contains data that can be sent to SoundManager.PlayBGM(). Allows looping audio from
    /// Animation Events and SendMessage calls.
    /// </summary>
    public class NewBGMLoopData : MonoBehaviour
    {
        /// <summary>
        /// The audio clip to play.
        /// </summary>
        [HideInInspector]
        [Tooltip("The audio clip to play.")]
        public AudioClip Clip;

        /// <summary>
        /// Audio clip volume.
        /// </summary>
        [Tooltip("Audio clip volume.")]
        public float Volume;

        /// <summary>
        /// Time to start at after looping, in seconds.
        /// </summary>
        [Tooltip("Time to start at after looping, in seconds.")]
        public float LoopStart;

        /// <summary>
        /// The time at which to loop, in seconds.
        /// </summary>
        [Tooltip("The time at which to loop, in seconds.")]
        public float LoopEnd;

        void Update()
        {
        if(GetComponent<AudioSource>().time > LoopEnd)
        {
            GetComponent<AudioSource>().time = LoopStart;
        }
        GetComponent<AudioSource>().volume = Volume;
        }
}

