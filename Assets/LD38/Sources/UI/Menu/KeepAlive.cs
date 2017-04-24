using JetBrains.Annotations;
using UnityEngine;

namespace LD38.UI.Menu {
    public class KeepAlive : MonoBehaviour {
        [UsedImplicitly]
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}
