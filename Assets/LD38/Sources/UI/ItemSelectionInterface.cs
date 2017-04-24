using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace LD38.UI {
    public class ItemSelectionInterface : MonoBehaviour {
        public RectTransform ItemTemplate;
        public float DragFactor = 0.03f;

        [NonSerialized]
        public MapItemHolder LastSelected;

        private MapItemDefinition _selected;
        private Vector2 _lastMousePos;
        private MapItemHolder _dragging;

        private MapItemHolder GetObjectUnderCursor() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) {
                return null;
            }

            var holder = hit.transform.GetComponentInParent<MapItemHolder>();
            return holder;
        }

        [UsedImplicitly]
        private void Start() {
            var repository = ItemRepository.Instance;
            foreach (var definition in repository.MapItems) {
                var uiItem = Instantiate(ItemTemplate, transform);
                uiItem.GetComponent<Image>().sprite = Sprite.Create(
                    definition.Icon,
                    new Rect(
                        Vector2.zero,
                        new Vector2(definition.Icon.width, definition.Icon.height)
                    ),
                    Vector2.zero
                );
                uiItem.GetComponent<Button>().onClick.AddListener(() => _selected = definition);
            }
        }

        [UsedImplicitly]
        private void Update() {
            HandleRotation();
            HandleItemCreation();
            HandleDragging();
            HandleItemDeletion();

            if (_dragging != null) {
                LastSelected = _dragging;
            }
            _lastMousePos = Input.mousePosition;
        }

        private void HandleRotation() {
            if (!Input.GetKeyDown(KeyCode.R)) {
                return;
            }
            var holder = GetObjectUnderCursor();
            if (holder != null) {
                holder.Rotate(45);
            }
        }

        private void HandleItemCreation() {
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                _selected = null;
            }
            if (!Input.GetMouseButtonDown(0) || _selected == null) {
                return;
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) {
                return;
            }

            var go = MapLoader.Instance.CreateItem(new MapItem {
                PrefabName = _selected.ItemName
            });
            go.transform.position = hit.point;
            _dragging = go.GetComponent<MapItemHolder>();
            _dragging.Item.Position = go.transform.position;

            MapLoader.Instance.BindItem(go);

            if (!Input.GetKey(KeyCode.LeftShift)) {
                _selected = null;
            }
        }

        private void HandleDragging() {
            if (Input.GetMouseButtonDown(0) && _selected == null) {
                _dragging = GetObjectUnderCursor();
            }
            if (_dragging != null) {
                var delta = Input.mousePosition - (Vector3)_lastMousePos;
                if (!Input.GetKey(KeyCode.LeftControl)) {
                    delta.z = delta.y;
                    delta.y = 0;
                }
                var rotation = Quaternion.AngleAxis(
                    Camera.main.transform.rotation.eulerAngles.y,
                    Vector3.up
                );
                _dragging.Translate(rotation * delta * DragFactor);
            }
            if (Input.GetMouseButtonUp(0)) {
                _dragging = null;
            }
        }

        private void HandleItemDeletion() {
            if (!Input.GetMouseButtonDown(1)) {
                return;
            }

            var holder = GetObjectUnderCursor();
            if (holder == null) {
                return;
            }

            Destroy(holder.gameObject);
        }
    }
}
