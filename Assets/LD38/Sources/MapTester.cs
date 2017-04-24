using JetBrains.Annotations;
using UnityEngine;

namespace LD38 {
    public class MapTester : MonoBehaviour {
        [SerializeField, UsedImplicitly] private Map _map;

        [UsedImplicitly]
        private void Awake() {
            MapLoader.Instance.FromData(_map);
        }
    }
}
