using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.Configs;
using App.Scripts.Scenes.Level;
using App.Scripts.Scenes.Level.Config;
using UnityEngine;

namespace App.Scripts.Installers
{
    public class LevelSceneInstaller : Installer
    {
        [SerializeField] private BlockGrid _blockGrid;
        [SerializeField] private BlocksPoolContainer _blocksPoolContainer;
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private MapColliders _mapColliders;
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private LevelEndTrigger _levelEndTriggerPrefab;
        [SerializeField] private BlocksConfig _blocksConfig;
        [SerializeField] private InputSystem _inputSystem;
        
        private void Awake()
        {
            PopUpSystem.Instance.enabled = true;

            _blocksPoolContainer.Initialize(_blocksConfig.BlockInformation);
            LevelEndTrigger levelEndTrigger = Instantiate(_levelEndTriggerPrefab);
            
            MapData mapData = _levelsConfig.LoadLevelByIndex(_levelsConfig.SelectedLevelIndex);
            _blockGrid.Initialize(mapData.BlockIds.GetLength(0), mapData.BlockIds.GetLength(1));
            
            MapConverter mapConverter = new MapConverter();
            mapConverter.ConvertMapDataToBlocksGrid(mapData, _blockGrid, _blocksPoolContainer, levelEndTrigger,
                _inputSystem, _cameraContainer.MainCamera);
            
            BlockGridCell leftTopCell = _blockGrid.GetCell(0, 0);
            BlockGridCell rightBottomCell = _blockGrid.GetCell(_blockGrid.Rows - 1, _blockGrid.Columns - 1);
            
            Vector3 firstCellPosition = leftTopCell.transform.position;
            Vector3 lastCellPosition = rightBottomCell.transform.position;
            Vector3 centerMapPosition = (lastCellPosition + firstCellPosition) / 2;
            
            _cameraContainer.Initialize(centerMapPosition, _blockGrid);
            _mapColliders.Initialize(_blockGrid, centerMapPosition);
        }
    }
}