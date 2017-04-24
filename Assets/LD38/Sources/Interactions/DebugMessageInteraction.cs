using JetBrains.Annotations;
using UnityEngine;

namespace LD38.Interactions {
    public class DebugMessageInteraction : MonoBehaviour {
        public string Message;

        [UsedImplicitly]
        private void InteractComplete() {
            Debug.Log(Message);
        }
    }
}
