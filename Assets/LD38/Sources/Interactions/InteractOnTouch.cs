using JetBrains.Annotations;
using UnityEngine;

namespace LD38.Interactions {
    public class InteractOnTouch : MonoBehaviour {
        [UsedImplicitly]
        private void OnTriggerEnter(Collider other) {
            var collider = GetComponent<Collider>();
            if (!collider.enabled || other.GetComponent<PlayerController>() == null) {
                return;
            }

            SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            collider.enabled = false;
        }
    }
}
