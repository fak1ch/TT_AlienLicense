﻿using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.Level;
using App.Scripts.Scenes.LevelCreator;
using UnityEngine;

namespace App.Scripts.Installers
{
    public class LevelCreatorSceneInstaller : Installer
    {
        [SerializeField] private CreateLevelSettingsPopUp _createLevelSettingsPopUp;
        [SerializeField] private BlockGrid _blockGrid;
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private MapColliders _mapColliders;
        
        private void Awake()
        {
            PopUpSystem.Instance.enabled = true;

            _createLevelSettingsPopUp.OnInitializeButtonClicked += InitializeLevelCreatorScene;
            _createLevelSettingsPopUp.ShowPopUp();
        }

        private void InitializeLevelCreatorScene(int rows, int columns)
        {
            _blockGrid.Initialize(rows, columns);
            
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