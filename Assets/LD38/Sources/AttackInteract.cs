using JetBrains.Annotations;
using UnityEngine;

namespace LD38 {
    public class AttackInteract : MonoBehaviour {
        public Collider Hitbox;

        [UsedImplicitly]
        private void StartHitbox() {
            Hitbox.enabled = true;
        }

        [UsedImplicitly]
        private void StopHitbox() {
            Hitbox.enabled = false;
        }
    }
}
