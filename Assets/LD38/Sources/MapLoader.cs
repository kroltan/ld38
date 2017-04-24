using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace LD38 {
    public class MapLoader : SingletonBehaviour<MapLoader> {
        public string MapName;
        public string MapAuthor;

        public UnityEvent OnLoad;

        [UsedImplicitly]
        private void Start() {
            StartCoroutine(LevelServerConnector.Instance.LoadRandom());
        }

        private void Unload() {
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
        }

        public void Reload() {
            FromMinicode(ToMinicode());
        }

        public void ToClipboard() {
            GUIUtility.systemCopyBuffer = ToMinicode();
        }

        public void FromClipboard() {
            FromMinicode(GUIUtility.systemCopyBuffer);
        }

        public string ToMinicode() {
            var items = new List<MapItem>(transform.childCount);
            items.AddRange(transform
                .Cast<Transform>()
                .Select(child => {
                    var item = child.GetComponent<MapItemHolder>().Item;
                    var interactor = child.GetComponent<ItemInteractor>();
                    if (interactor?.Target != null) {
                        item.ActivationTarget = interactor.Target.GetComponent<MapItemHolder>().Item.Name;
                    }
                    return item;
                }));

            var map = new Map {
                Author = MapAuthor,
                Name = MapName,
                Items = items.ToArray()
            };

            var memory = new MemoryStream();
            var stream = new GZipStream(memory, CompressionLevel.Optimal);
            using (var writer = new StreamWriter(stream)) {
                writer.Write(JsonUtility.ToJson(map));
                stream.Flush();
            }
            stream.Dispose();
            return Convert.ToBase64String(memory.ToArray());
        }

        public void FromMinicode(string minicode) {
            var bytes = Convert.FromBase64String(minicode);
            var stream = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress);
            using (var reader = new StreamReader(stream)) {
                FromJson(reader.ReadToEnd());
                stream.Flush();
            }
            stream.Dispose();
        }

        public void FromJson(string json) {
            FromData(JsonUtility.FromJson<Map>(json));
        }

        public void FromData(Map map) {
            Unload();
            MapName = map.Name;
            MapAuthor = map.Author;

            foreach (var item in map.Items) {
                CreateItem(item);
            }

            foreach (Transform t in transform) {
                BindItem(t.gameObject);
            }

            Inventory.Instance.Reset();
            OnLoad.Invoke();
        }

        public GameObject CreateItem(MapItem item) {
            var go = Instantiate(
                    ItemRepository.Instance.GetPrefab(item.PrefabName),
                    item.Position,
                    Quaternion.AngleAxis(item.YRotation, transform.up), transform
                ).gameObject;
            go.transform.localScale *= item.Scale;
            go.name = string.IsNullOrWhiteSpace(item.Name) ? Guid.NewGuid().ToString() : item.Name;
            go.AddComponent<MapItemHolder>().Item = item;
            return go;
        }

        public void BindItem(GameObject go) {
            var item = go.GetComponent<MapItemHolder>().Item;
            var interactor = go.GetComponent<ItemInteractor>();
            if (interactor == null) {
                return;
            }
            interactor.Threshold = item.ActivationThreshold;
            interactor.Target = transform.Find(item.ActivationTarget).GetComponent<ItemInteractor>();
        }
    }
}
