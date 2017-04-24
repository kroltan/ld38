using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace LD38.UI {
    public class StateChanger : MonoBehaviour {
        public GameObject[] PlaymodeObjects;
        public GameObject[] EditmodeObjects;
        public bool Playing;

        private bool _oldPlaying;
        private Text _text;

        private void ApplyChange() {
            _text.text = Playing ? "Edit" : "Play";
            foreach (var go in EditmodeObjects) {
                go.SetActive(!Playing);
            }
            foreach (var go in PlaymodeObjects) {
                go.SetActive(Playing);
            }

            MapLoader.Instance.Reload();
            if (!Playing) {
                Inventory.Instance.Reset(true);
            }
        }

        [UsedImplicitly]
        private void Start() {
            _text = GetComponentInChildren<Text>();
            ApplyChange();
        }

        [UsedImplicitly]
        private void Update() {
            if (Playing == _oldPlaying) {
                return;
            }
            ApplyChange();
            _oldPlaying = Playing;
        }

        public void ToggleState() {
            Playing = !Playing;
        }
    }
}
