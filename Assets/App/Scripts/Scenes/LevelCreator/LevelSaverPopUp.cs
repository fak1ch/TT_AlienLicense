using System;
using App.Scripts.General;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.UI.ButtonSpace;
using App.Scripts.Scenes.Configs;
using App.Scripts.Scenes.Level;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.LevelCreator
{
    public class LevelSaverPopUp : PopUp
    {
        [SerializeField] private CustomButton _saveButton;
        [SerializeField] private BlockGrid _blockGrid;
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private TMP_InputField _movesCountField;

        #region Events

        private void OnEnable()
        {
            _saveButton.OnClickOccurred.AddListener(SaveButtonClickedCallback);
        }

        private void OnDisable()
        {
            _saveButton.OnClickOccurred.RemoveListener(SaveButtonClickedCallback);
        }

        #endregion
        
        private void SaveButtonClickedCallback()
        {
            MapConverter mapConverter = new MapConverter();
            if (mapConverter.TryConvertBlocksGridToMapData(_blockGrid, int.Parse(_movesCountField.text), out MapData mapData))
            {
                _levelsConfig.SaveLevelByIndex(_levelsConfig.LevelsCount, mapData);
                HidePopUp();
            }
        }
    }
}