﻿using SonicRealms.Core.Utils;
using SonicRealms.Level;
using UnityEngine;

namespace SonicRealms.Core.Actors
{
    /// <summary>
    /// Sonic sounds, animations, and effects on top of the breath meter.
    /// </summary>
    public class SonicBreathMeter : BreathMeter
    {
        #region Sound
        /// <summary>
        /// Points at which to play the warning sound, in seconds of air remaining.
        /// </summary>
        [Foldout("Sound")]
        [Tooltip("Points at which to play the warning sound, in seconds of air remaining.")]
        public float[] WarningPoints;

        /// <summary>
        /// Warning sound to play when the breath meter is being depleted.
        /// </summary>
        [Foldout("Sound")]
        [Tooltip("Warning sound to play when the breath meter is being depleted.")]
        public AudioClip WarningSound;

        /// <summary>
        /// Once the player has this much air left, in seconds, the drowning BGM will play.
        /// </summary>
        [Space, Foldout("Sound")]
        [Tooltip("Once the player has this much air left, in seconds, the drowning BGM will play.")]
        public float DrowningPoint;

        /// <summary>
        /// The drowning music to play.
        /// </summary>
        [Foldout("Sound")]
        [Tooltip("The drowning music to play.")]
        public AudioClip DrowningBGM;

        /// <summary>
        /// The sound to play once drowned.
        /// </summary>
        [Foldout("Sound")]
        [Tooltip("The sound to play once drowned.")]
        public AudioClip DrownSound;

        [Foldout("Sound")]
        public AudioSource DrownSource;
        #endregion

        private float _previousAir;

        public override void Reset()
        {
            base.Reset();
            DrowningPoint = 12f;
        }

        public override void Update()
        {
            base.Update();

            if (CanBreathe)
            {
                if (DrowningBGM != null)
                {
                    DrownSource.clip = null;
                    DrownSource.Stop();
                }
            }

            if (_previousAir > DrowningPoint && RemainingAir < DrowningPoint)
            {
                if (DrowningBGM)
                {
                    if (DrownSource.clip != DrowningBGM)
                    {
                        DrownSource.Play();
                        DrownSource.clip = DrowningBGM;
                    }
                }
            }

            if (WarningSound != null)
            {
                foreach (var warningPoint in WarningPoints)
                {
                    // See if we passed a warning point and play the sound
                    if (_previousAir > warningPoint && RemainingAir < warningPoint)
                        DrownSource.PlayOneShot(WarningSound);
                }
            }

            _previousAir = RemainingAir;
        }

        public override void Drown()
        {
            if (Drowned) return;

            var hedgehogHealth = Health as HedgehogHealth;
            if (hedgehogHealth != null)
            {
                if (DrownSound != null)
                    hedgehogHealth.DeathSound = DrownSound;
            }

            base.Drown();

        }
    }
}
