﻿using System;
using App.Scripts.General.Utils;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class CreateLevelMoveBlockInput : MonoBehaviour
    {
        private Camera _camera;
        private InputSystem _inputSystem;
        private BlockGrid _blockGrid;
        private Block _selectedBlock;
        private float _blockY;
        private Vector3 _offset;

        public void Initialize(Camera mainCamera, InputSystem inputSystem, BlockGrid blockGrid)
        {
            _inputSystem = inputSystem;
            _camera = mainCamera;
            _blockGrid = blockGrid;
            
            _blockY = _blockGrid.GetCell(0, 0).transform.position.y;
            
            _inputSystem.OnMouseDown += MouseDownCallback;
            _inputSystem.OnMouseUp += MouseUpCallback;
        }

        private void Update()
        {
            if (_selectedBlock != null)
            {
                Vector3 worldMousePosition = MathUtils.GetMouseWorldPosition(_camera, _blockY);

                _selectedBlock.transform.position = worldMousePosition + _offset;
            }
        }
        
        private void MouseDownCallback()
        {
            if (TryGetComponentByRay(out _selectedBlock))
            {
                _blockGrid.ClearCellByBlock(_selectedBlock);
            }
        }

        private void MouseUpCallback()
        {
            if(_selectedBlock == null) return;
            
            _blockGrid.AddBlockToNearestCell(_selectedBlock);
            _selectedBlock = null;
        }

        private bool TryGetComponentByRay<T>(out T t) where T : Component
        {
            t = default;
            Vector2 mousePosition = _inputSystem.MousePosition;

            Ray ray = _camera.ScreenPointToRay(mousePosition);
            
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.rigidbody == null) return false;
                
                if (hitInfo.rigidbody.TryGetComponent(out t))
                {
                    Vector3 position = t.transform.position;
                    Vector3 mouseWorldPosition = MathUtils.GetMouseWorldPosition(_camera, _blockY);

                    _offset = position - mouseWorldPosition;
                    _offset.y = 0;
                    
                    return true;
                }
            }

            return false;
        }
    }
}