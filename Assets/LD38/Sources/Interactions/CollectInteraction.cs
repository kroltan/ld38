using JetBrains.Annotations;
using UnityEngine;

namespace LD38.Interactions {
    public class CollectInteraction : MonoBehaviour {
        public string Resource;
        public bool Persist;

        [UsedImplicitly]
        private void InteractComplete() {
            Inventory.Instance.Collect(Resource);
            if (Persist) {
                Inventory.Instance.MarkPersistent(Resource);
            }
        }
    }
}
