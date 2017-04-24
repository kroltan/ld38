using System.Linq;
using JetBrains.Annotations;
using LD38.Interactions;
using UnityEngine;

namespace LD38 {
    public class ItemInteractor : MonoBehaviour {
        public ItemInteractor Target;
        public bool UserInteractible;
        public int Threshold;

        public bool CanPlayerInteract => UserInteractible || Threshold < 0;

        private int _count;

        [UsedImplicitly]
        private void Start() {
            if (Threshold == 0) {
                Complete();
            }
        }

        public void Interact() {
            if (!GetComponents<IInteractionFilter>().All(f => f.CanInteract())) {
                return;
            }
            _count++;
            Complete();
        }

        private void Complete() {
            if (_count != Threshold && !CanPlayerInteract) return;
            SendMessage("InteractComplete", SendMessageOptions.DontRequireReceiver);
            if (Target != null && Target.GetComponents<IInteractionFilter>().All(f => f.CanInteract())) {
                Target.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
