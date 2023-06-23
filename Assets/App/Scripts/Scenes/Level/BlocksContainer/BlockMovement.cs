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
        private Camera _mainCamera;
        
        public void Initialize(InputSystem inputSystem, Camera mainCamera)
        {
            _inputSystem = inputSystem;
            _mainCamera = mainCamera;
            SetCanMove(false);
        }

        public void Move()
        {
            Vector2 mousePosition = _inputSystem.MousePosition;
            Vector3 vector3MousePosition = new Vector3(mousePosition.x, mousePosition.y, _mainCamera.transform.position.y);
            
            Vector3 worldMousePosition = _mainCamera.ScreenToWorldPoint(vector3MousePosition);
            worldMousePosition.y = transform.position.y;

            Vector3 direction = worldMousePosition - transform.position;
            Vector3 velocity = direction * Time.deltaTime * _config.MoveSpeed;

            velocity.x = _blockMovementType == BlockMovementTypes.Horizontal ? direction.x : 0;
            velocity.z = _blockMovementType == BlockMovementTypes.Vertical ? direction.z : 0;
            
            _rigidbody.velocity = velocity;
        }

        public void SetCanMove(bool value)
        {
            _canMove = value;
            _rigidbody.constraints = value ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeAll;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}