using System;
using UnityEngine;

namespace LD38 {
    [Serializable]
    public class Map {
        public string Name;
        public string Author;
        public MapItem[] Items;
    }

    [Serializable]
    public class MapItem {
        public string Name;
        public string PrefabName;
        public Vector3 Position;
        public float Scale = 1;
        public float YRotation;
        
        public int ActivationThreshold = 1;
        public string ActivationTarget;
    }
}
