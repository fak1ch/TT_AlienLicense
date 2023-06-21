using System;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class MoveBlockInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private InputSystem _inputSystem;

        private BlockMovement _selectedBlockMovement;
        
        #region Events

        private void OnEnable()
        {
            _inputSystem.OnMouseDown += MouseDownCallback;
            _inputSystem.OnMouseUp += MouseUpCallback;
        }

        private void OnDisable()
        {
            _inputSystem.OnMouseDown -= MouseDownCallback;
            _inputSystem.OnMouseUp -= MouseUpCallback;
        }

        #endregion

        private void MouseDownCallback()
        {
            if (TryGetBlockMovementByRay(out BlockMovement blockMovement))
            {
                _selectedBlockMovement = blockMovement;
                _selectedBlockMovement.SetCanMove(true);
            }
        }
        
        private void MouseUpCallback()
        {
            if(_selectedBlockMovement == null) return;
            
            _selectedBlockMovement.SetCanMove(false);
            _selectedBlockMovement = null;
        }

        private bool TryGetBlockMovementByRay(out BlockMovement blockMovement)
        {
            blockMovement = null;
            Vector2 mousePosition = _inputSystem.MousePosition;

            Ray ray = _camera.ScreenPointToRay(mousePosition);
            
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.rigidbody.TryGetComponent(out blockMovement))
                {
                    return true;
                }
            }

            return false;
        }
    }
}