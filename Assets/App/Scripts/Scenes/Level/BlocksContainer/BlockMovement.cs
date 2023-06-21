using System;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class BlockMovement : MonoBehaviour
    {
        [SerializeField] private BlockMovementConfig _config;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BlockMovementTypes _blockMovementType;

        private InputSystem _inputSystem;
        private bool _canMove = false;
        
        public void Initialize(InputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }

        private void Update()
        {
            Vector2 mouseInput = _inputSystem.MouseInput;
            Vector3 velocity = Vector3.zero;

            if (_blockMovementType == BlockMovementTypes.Horizontal)
            {
                velocity.x = ApplySpeed(mouseInput.x);
            }
            
            if (_blockMovementType == BlockMovementTypes.Vertical)
            {
                velocity.z = ApplySpeed(mouseInput.y);
            }

            velocity = _canMove ? velocity : Vector3.zero;
            _rigidbody.velocity = velocity;
        }

        private float ApplySpeed(float input)
        {
            return input * Time.deltaTime * _config.MoveSpeed;
        }

        public void SetCanMove(bool value)
        {
            _canMove = value;
        }
    }
}