using System;
using App.Scripts.General.LoadScene;
using App.Scripts.General.UI.ButtonSpace;
using App.Scripts.Scenes.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu
{
    public class LevelCell : MonoBehaviour
    {
        [SerializeField] private CustomButton _startLevelButton;
        [SerializeField] private LevelCellView _levelCellView;

        private LevelsConfig _levelsConfig;
        private int _levelIndex;
        
        #region Events

        private void OnEnable()
        {
            _startLevelButton.OnClickOccurred.AddListener(StartLevel);
        }

        private void OnDisable()
        {
            _startLevelButton.OnClickOccurred.RemoveListener(StartLevel);
        }

        #endregion

        public void Initialize(int levelIndex, LevelsConfig levelsConfig)
        {
            _levelsConfig = levelsConfig;
            _levelIndex = levelIndex;
            _levelCellView.SetIndex(_levelIndex + 1);
        }
        
        private void StartLevel()
        {
            _levelsConfig.SelectedLevelIndex = _levelIndex;
            SceneLoader.Instance.LoadScene(SceneEnum.Level);
        }
    }
}