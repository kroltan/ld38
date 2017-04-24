using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace LD38.UI {
    public class ResourceDisplay : MonoBehaviour {
        [Serializable]
        public class ResourceSprite {
            public string ResourceName;
            public Sprite Sprite;
        }

        public RectTransform CounterPrefab;
        public ResourceSprite[] ResourceSprites;

        [UsedImplicitly]
        private void Start() {
            Inventory.Instance.OnResourceChange.AddListener((resource, count) => {
                var child = transform.Find(resource);
                if (count == 0 && child != null) {
                    Destroy(child.gameObject);
                    return;
                }
                if (child == null) {
                    child = Instantiate(CounterPrefab, transform);
                    child.name = resource;
                    var sprite = ResourceSprites.First(rs => rs.ResourceName == resource).Sprite;
                    child.Find("Image").GetComponent<Image>().sprite = sprite;
                }
                child.Find("Text/Value").GetComponent<Text>().text = count.ToString();
            });
        }
    }
}
