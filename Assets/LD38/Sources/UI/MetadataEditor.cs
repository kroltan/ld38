using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace LD38.UI {
    public class MetadataEditor : MonoBehaviour {
        [UsedImplicitly]
        private void Start() {
            var title = transform.Find("Title").GetComponent<InputField>();
            var author = transform.Find("Author").GetComponent<InputField>();

            title.onValueChanged.AddListener(val => MapLoader.Instance.MapName = val);
            author.onValueChanged.AddListener(val => MapLoader.Instance.MapAuthor = val);
            MapLoader.Instance.OnLoad.AddListener(() => {
                title.text = MapLoader.Instance.MapName;
                author.text = MapLoader.Instance.MapAuthor;
            });
        }
    }
}
