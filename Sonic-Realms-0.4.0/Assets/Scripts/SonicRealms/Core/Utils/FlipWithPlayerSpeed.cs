using SonicRealms.Core.Actors;
using UnityEngine;

namespace SonicRealms.Core.Utils
{
    /// <summary>
    /// Flips the object based on which way the player is facing.
    /// </summary>
    public class FlipWithPlayerSpeed : MonoBehaviour
    {
        private bool _lastFacingForward;
        public HedgehogController Player;

        public void Reset()
        {
            Player = GetComponentInParent<HedgehogController>();
        }

        public void Start()
        {
            _lastFacingForward = Player.GroundVelocity >= 0f;
            Flip();
        }

        public void OnEnable()
        {
            if (Player != null) Flip();
        }

        public void FixedUpdate()
        {
            if (_lastFacingForward == (Player.GroundVelocity >= 0f))
                return;

            Flip();
        }

        public void Flip()
        {
            var t = transform;

            if(Mathf.Sign(t.localScale.x) != ((Player.GroundVelocity >= 0f) ? 1f : -1f))
                transform.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);

            _lastFacingForward = (Player.GroundVelocity >= 0f);
        }
    }
}
