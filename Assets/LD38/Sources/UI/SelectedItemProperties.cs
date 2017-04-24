using System;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace LD38.UI {
    public class SelectedItemProperties : MonoBehaviour {
        private MapItemHolder _holder;
        public ItemSelectionInterface Selector;

        private Text _title;
        private InputField _name, _scale, _rotation, _threshold, _target;

        private InputField GetInputForProp(string property) {
            return transform.Find($"Content/{property}Prop").GetComponentInChildren<InputField>();
        }

        [UsedImplicitly]
        private void Start() {
            _title = transform.Find("Title").GetComponentInChildren<Text>();

            _name = GetInputForProp("Name");
            _name.onValueChanged.AddListener(val => _holder.Item.Name = val);
            _scale = GetInputForProp("Scale");
            _scale.onValueChanged.AddListener(val => {
                float numeric;
                if (float.TryParse(val, out numeric)) {
                    _holder.transform.localScale = Vector3.one * numeric;
                    _holder.Item.Scale = numeric;
                }
            });
            _rotation = GetInputForProp("Rotation");
            _rotation.onValueChanged.AddListener(val => {
                float numeric;
                if (float.TryParse(val, out numeric)) {
                    var euler = _holder.transform.localRotation.eulerAngles;
                    euler.y = numeric;
                    _holder.transform.localRotation = Quaternion.Euler(euler);
                    _holder.Item.YRotation = numeric;
                }
            });
            _threshold = GetInputForProp("Threshold");
            _threshold.onValueChanged.AddListener(val => {
                int numeric;
                if (int.TryParse(val, out numeric)) {
                    _holder.Item.ActivationThreshold = numeric;
                }
            });
            _target = GetInputForProp("Target");
            _target.onValueChanged.AddListener(val => _holder.Item.ActivationTarget = val);
        }

        [UsedImplicitly]
        private void Update() {
            var notNull = Selector.LastSelected != null;
            _name.interactable = notNull;
            _scale.interactable = notNull;
            _rotation.interactable = notNull;
            _threshold.interactable = notNull;
            _target.interactable = notNull;

            if (Selector.LastSelected == _holder || !notNull) {
                return;
            }
            _holder = Selector.LastSelected;
            _title.text = _holder.Item.PrefabName;
            _name.text = _holder.Item.Name;
            _scale.text = _holder.Item.Scale.ToString(CultureInfo.CurrentCulture);
            _rotation.text = _holder.Item.YRotation.ToString(CultureInfo.CurrentCulture);
            _threshold.text = _holder.Item.ActivationThreshold.ToString();
            _target.text = _holder.Item.ActivationTarget;
        }
    }
}
