using UnityEngine;

namespace LD38 {
    public class MapItemHolder : MonoBehaviour {
        public MapItem Item;

        public void Rotate(float deltaAngle) {
            Item.YRotation += deltaAngle;
            transform.localRotation = Quaternion.AngleAxis(deltaAngle, transform.up) * transform.localRotation;
        }

        public void Translate(Vector3 deltaPosition) {
            Item.Position += deltaPosition;
            transform.position += deltaPosition;
        }
    }
}
