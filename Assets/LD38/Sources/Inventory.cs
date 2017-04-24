using System.Collections.Generic;
using UnityEngine.Events;

namespace LD38 {
    public class Inventory : SingletonBehaviour<Inventory> {
        public class ResourceEvent : UnityEvent<string, int> {}
        public class ResourceState {
            public int Count;
            public bool Persist;
        }

        private readonly Dictionary<string, ResourceState> _itemCounts = new Dictionary<string, ResourceState>();
        public ResourceEvent OnResourceChange = new ResourceEvent();

        public void Reset(bool force = false) {
            var toRemove = new List<string>();
            foreach (var kvp in _itemCounts) {
                if (!force && kvp.Value.Persist) {
                    continue;
                }
                toRemove.Add(kvp.Key);
                OnResourceChange.Invoke(kvp.Key, 0);
            }
            foreach (var key in toRemove) {
                _itemCounts.Remove(key);
            }
        }

        public void MarkPersistent(string item) {
            ResourceState state;
            if (!_itemCounts.TryGetValue(item, out state)) {
                state = new ResourceState();
                _itemCounts[item] = state;
            }
            state.Persist = true;
        }

        public void Collect(string item) {
            ResourceState state;
            if (!_itemCounts.TryGetValue(item, out state)) {
                state = new ResourceState();
                _itemCounts[item] = state;
            }
            state.Count++;
            OnResourceChange.Invoke(item, state.Count);
        }

        public bool Consume(string item) {
            ResourceState state;
            if (!_itemCounts.TryGetValue(item, out state) || state.Count == 0) {
                return false;
            }
            state.Count--;
            if (state.Count == 0) {
                _itemCounts.Remove(item);
            }
            OnResourceChange.Invoke(item, state.Count);
            return true;
        }
    }
}
