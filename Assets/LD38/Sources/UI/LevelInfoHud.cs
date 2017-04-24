using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace LD38.UI {
    public class LevelInfoHud : MonoBehaviour {
        private Text _name;
        private Text _author;

        [UsedImplicitly]
        private void Start() {
            _name = transform.Find("Name").GetComponent<Text>();
            _author = transform.Find("Author").GetComponent<Text>();
        }

        [UsedImplicitly]
        private void Update() {
            _name.text = MapLoader.Instance.MapName;
            _author.text = $"by <i>{MapLoader.Instance.MapAuthor}</i>";
        }
    }
}
