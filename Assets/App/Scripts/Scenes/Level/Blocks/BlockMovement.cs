using System;
using App.Scripts.General.Utils;
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
        private bool _canMove = false;
        private Tween _moveToPositionTween;
        
        public void Initialize()
        {
            SetCanMove(false);
        }

        public void Move(Vector3 targetPosition)
        {
            Vector3 velocity = GetVelocityToPosition(targetPosition);

            velocity.x = _blockMovementType == BlockMovementTypes.Horizontal ? velocity.x : 0;
            velocity.z = _blockMovementType == BlockMovementTypes.Vertical ? velocity.z : 0;

            DrawDebugRay(velocity.normalized);
            if (CheckCollisionByRay(velocity.normalized))
            {
                velocity = Vector3.zero;
            }

            velocity = MathUtils.ClampVector3(velocity, _config.MaxVelocity * -1, _config.MaxVelocity);
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

        private bool CheckCollisionByRay(Vector3 direction)
        {
            float rayDistance = transform.lossyScale.x/2;
            Vector3 origin = transform.position;

            return Physics.Raycast(origin, direction, out RaycastHit raycastHit,
                rayDistance, Physics.AllLayers);
        }

        private void DrawDebugRay(Vector3 direction)
        {
            #if UNITY_EDITOR == false
                return
            #endif
            
            float rayDistance = transform.lossyScale.x/2;
            Vector3 origin = transform.position;
            
            bool isCollision = CheckCollisionByRay(direction);
            Color color = isCollision ? Color.red : Color.green;
            
            Debug.DrawRay(origin, transform.right * rayDistance, color);
        }
    }
}