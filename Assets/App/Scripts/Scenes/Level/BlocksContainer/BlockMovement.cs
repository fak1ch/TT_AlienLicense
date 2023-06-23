using System;
using App.Scripts.Scenes.Configs;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class BlockMovement : MonoBehaviour
    {
        [SerializeField] private LevelSceneConfig _levelSceneConfig;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private BlockMovementTypes _blockMovementType;
        
        private BlockMovementConfig _config => _levelSceneConfig.BlockMovementConfig;
        private InputSystem _inputSystem;
        private bool _canMove = false;
        private Camera _mainCamera;
        private Tween _moveToPositionTween;
        
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

            Vector3 velocity = GetVelocityToPosition(worldMousePosition);

            velocity.x = _blockMovementType == BlockMovementTypes.Horizontal ? velocity.x : 0;
            velocity.z = _blockMovementType == BlockMovementTypes.Vertical ? velocity.z : 0;
            
            _rigidbody.velocity = velocity;
        }

        public void MoveToPosition(Vector3 targetPosition)
        {
            _moveToPositionTween?.Kill();
            _moveToPositionTween = transform.DOMove(targetPosition, _config.MoveToPositionDuration);
        }

        private Vector3 GetVelocityToPosition(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - transform.position;
            return direction * Time.deltaTime * _config.MoveSpeed;
        }
        
        public void SetCanMove(bool value)
        {
            _canMove = value;
            _rigidbody.constraints = value ? RigidbodyConstraints.FreezeRotation : RigidbodyConstraints.FreezeAll;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}