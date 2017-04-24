using JetBrains.Annotations;
using UnityEngine;

namespace LD38.Interactions {
    public class ActiveStateInteraction : MonoBehaviour {
        public GameObject Target;
        public bool State;

        [UsedImplicitly]
        private void OnEnable() {
            Target.SetActive(!State);
        }

        [UsedImplicitly]
        private void InteractComplete() {
            Target.SetActive(State);
        }
    }
}
