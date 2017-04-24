using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace LD38 {
    public class ItemRepository : SingletonBehaviour<ItemRepository> {
        [SerializeField, UsedImplicitly]
        public MapItemDefinition[] MapItems;

        private readonly Dictionary<string, Transform> _cache = new Dictionary<string, Transform>();

        public Transform GetPrefab(string itemName) {
            Transform prefab;
            if (!_cache.TryGetValue(itemName, out prefab)) {
                prefab = _cache[itemName] = MapItems.First(i => i.ItemName == itemName).Prefab;
            }
            return prefab;
        }
    }

    [Serializable]
    public class MapItemDefinition {
        public string ItemName;
        public Transform Prefab;
        public Texture2D Icon;
    }
}
