using JetBrains.Annotations;
using UnityEngine;

namespace LD38 {
    public class PlayerInteractor : MonoBehaviour {
        [UsedImplicitly]
        private void OnTriggerEnter(Collider other) {
            var item = other.GetComponent<ItemInteractor>();
            if (item != null && item.CanPlayerInteract) {
                other.SendMessage("Interact");
            }
        }
    }
}
