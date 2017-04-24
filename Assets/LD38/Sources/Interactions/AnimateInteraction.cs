using JetBrains.Annotations;
using UnityEngine;

namespace LD38.Interactions {
    [RequireComponent(typeof(Animator))]
    public class AnimateInteraction : MonoBehaviour {
        private Animator _animator;

        [UsedImplicitly]
        private void Start() {
            _animator = GetComponent<Animator>();
        }

        [UsedImplicitly]
        private void Interact() {
            _animator.SetTrigger("Interact");
        }

        [UsedImplicitly]
        private void InteractComplete() {
            _animator.SetTrigger("InteractComplete");
        }
    }
}
