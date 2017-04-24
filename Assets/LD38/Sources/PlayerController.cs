using JetBrains.Annotations;
using UnityEngine;

namespace LD38 {
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerController : SingletonBehaviour<PlayerController> {
        public float WalkSpeed = 1;
        public float RunSpeed = 1.5f;
        public float RunMouseDistance = 3;

        private CharacterController _character;
        private Animator _animator;

        [UsedImplicitly]
        protected override void Awake() {
            base.Awake();
            _character = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        [UsedImplicitly]
        private void Update() {
            CalculateMovement();
            TryAttack();
        }

        private void TryAttack() {
            var actionLayer = _animator.GetCurrentAnimatorStateInfo(_animator.GetLayerIndex("Actions"));
            if (Input.GetMouseButtonDown(0) && !actionLayer.IsName("Attacking")) {
                _animator.SetTrigger("Attack");
            }
        }

        private float GetSpeedFromMouseDistance(float mouseDistance) {
            var speed = 0f;
            if (mouseDistance > _character.radius && mouseDistance < RunMouseDistance) {
                speed = WalkSpeed;
            } else if (mouseDistance >= RunMouseDistance) {
                speed = RunSpeed;
            }
            return speed;
        }

        private void CalculateMovement() {
            var direction = Vector3.zero;
            float mouseDistance = -1;
            var plane = new Plane(transform.up, transform.position);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (plane.Raycast(ray, out rayDistance)) {
                var delta = ray.GetPoint(rayDistance) - transform.position;
                direction = delta;
                mouseDistance = delta.magnitude;
            }

            var speed = Input.GetMouseButton(1) ? GetSpeedFromMouseDistance(mouseDistance) : 0;

            direction.y = 0;
            direction.Normalize();
            _animator.SetFloat("MovementSpeed", speed);
            _character.SimpleMove(direction * speed);

            if (direction.sqrMagnitude > 0.001) {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
