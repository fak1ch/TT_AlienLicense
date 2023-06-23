using System;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class MoveBlockInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private BlockGrid _blockGrid;

        private Block _selectedBlock;
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

        private void Update()
        {
            _selectedBlockMovement!?.Move();
        }
        
        private void MouseDownCallback()
        {
            if (TryGetComponentByRay(out _selectedBlock))
            {
                _selectedBlockMovement = _selectedBlock.BlockMovement;
                _selectedBlockMovement.SetCanMove(true);
                _blockGrid.ClearCellByBlock(_selectedBlock);
            }
        }

        private void MouseUpCallback()
        {
            if(_selectedBlock == null || _selectedBlockMovement == null) return;
            
            _selectedBlockMovement.SetCanMove(false);
            _blockGrid.AddBlockToNearestCell(_selectedBlock);
            _selectedBlockMovement = null;
            _selectedBlock = null;
        }

        private bool TryGetComponentByRay<T>(out T t)
        {
            t = default;
            Vector2 mousePosition = _inputSystem.MousePosition;

            Ray ray = _camera.ScreenPointToRay(mousePosition);
            
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.rigidbody.TryGetComponent(out t))
                {
                    return true;
                }
            }

            return false;
        }
    }
}