using JetBrains.Annotations;
using UnityEngine;

namespace LD38.Interactions {
    public class PlayerSpawn : MonoBehaviour {
        [UsedImplicitly]
        private void OnEnable() {
            PlayerController.Instance.transform.position = transform.position;
        }
    }
}
