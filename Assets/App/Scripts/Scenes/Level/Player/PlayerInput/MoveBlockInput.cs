using System;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.Utils;
using App.Scripts.Scenes.Level.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class MoveBlockInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private BlockGrid _blockGrid;
        [SerializeField] private MoveCounter _moveCounter;

        private Block _selectedBlock;
        private BlockMovement _selectedBlockMovement;
        private Vector3 _mousePointOffset;
        
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
            if (_selectedBlockMovement != null)
            {
                Vector2 mousePosition = _inputSystem.MousePosition;
                Vector3 vector3MousePosition = new Vector3(mousePosition.x, mousePosition.y,
                    _camera.transform.position.y);
            
                Vector3 worldMousePosition = _camera.ScreenToWorldPoint(vector3MousePosition);
                worldMousePosition.y = transform.position.y;
                
                _selectedBlockMovement.Move(worldMousePosition + _mousePointOffset);
            }
        }
        
        private void MouseDownCallback()
        {
            if(PopUpSystem.Instance.ActivePopUpsCount > 0) return;
            
            if (TryGetComponentByRay(out _selectedBlock))
            {
                _selectedBlockMovement = _selectedBlock.BlockMovement;
                _selectedBlockMovement.SetCanMove(true);
                _blockGrid.ClearCellByBlock(_selectedBlock);
                
                Vector3 position = _selectedBlock.transform.position;
                Vector3 mouseWorldPosition = MathUtils.GetMouseWorldPosition(_camera,
                    _selectedBlockMovement.transform.position.y);

                _mousePointOffset = position - mouseWorldPosition;
                _mousePointOffset.y = 0;
            }
        }

        private void MouseUpCallback()
        {
            if(_selectedBlock == null || _selectedBlockMovement == null) return;
            
            _selectedBlockMovement.SetCanMove(false);
            _blockGrid.AddBlockToNearestCell(_selectedBlock);
            _moveCounter.MakeMove();
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