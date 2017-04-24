using System;
using JetBrains.Annotations;
using UnityEngine;

namespace LD38.Interactions {
    public class NextLevelInteraction : MonoBehaviour {
        private bool _loading;

        [UsedImplicitly]
        private void InteractComplete() {
            if (!_loading) {
                _loading = true;
                LevelServerConnector.Instance.InteractiveLoadRandom();
            }
            Inventory.Instance.Collect("score");
            Inventory.Instance.MarkPersistent("score");
        }
    }
}
