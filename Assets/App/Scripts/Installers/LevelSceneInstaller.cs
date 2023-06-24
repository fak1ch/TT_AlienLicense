using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.Level;
using UnityEngine;

namespace App.Scripts.Installers
{
    public class LevelSceneInstaller : Installer
    {
        [SerializeField] private BlockGrid _blockGrid;
        [SerializeField] private Block _movableBlockPrefab;
        [SerializeField] private InputSystem _inputSystem;
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private MapColliders _mapColliders;
        
        private void Awake()
        {
            PopUpSystem.Instance.enabled = true;
            
            _blockGrid.Initialize(9,9);

            Block block1 = Instantiate(_movableBlockPrefab);
            Block block2 = Instantiate(_movableBlockPrefab);
            Block block3 = Instantiate(_movableBlockPrefab);
            Block block4 = Instantiate(_movableBlockPrefab);
            Block block5 = Instantiate(_movableBlockPrefab);
            
            block1.Initialize(_inputSystem, _cameraContainer.MainCamera);
            block2.Initialize(_inputSystem, _cameraContainer.MainCamera);
            block3.Initialize(_inputSystem, _cameraContainer.MainCamera);
            block4.Initialize(_inputSystem, _cameraContainer.MainCamera);
            block5.Initialize(_inputSystem, _cameraContainer.MainCamera);

            _blockGrid.SetBlock(4, 4, block1);
            _blockGrid.SetBlock(1, 7, block2);
            _blockGrid.SetBlock(4, 8, block3);
            _blockGrid.SetBlock(2, 4, block4);
            _blockGrid.SetBlock(1, 1, block5);

            BlockGridCell leftTopCell = _blockGrid.GetCell(0, 0);
            BlockGridCell rightBottomCell = _blockGrid.GetCell(_blockGrid.RowsCount, _blockGrid.ColumnsCount);
            
            Vector3 firstCellPosition = leftTopCell.transform.position;
            Vector3 lastCellPosition = rightBottomCell.transform.position;
            Vector3 centerMapPosition = (lastCellPosition + firstCellPosition) / 2;
            
            _cameraContainer.SetCameraPosition(centerMapPosition);
            _mapColliders.Initialize(_blockGrid, centerMapPosition);
        }
    }
}