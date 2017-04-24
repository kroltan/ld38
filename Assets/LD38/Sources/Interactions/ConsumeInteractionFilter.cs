using UnityEngine;

namespace LD38.Interactions {
    public class ConsumeInteractionFilter : MonoBehaviour, IInteractionFilter {
        public string Resource;

        public bool CanInteract() {
            return Inventory.Instance.Consume(Resource);
        }
    }
}
