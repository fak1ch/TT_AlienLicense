using System;
using App.Scripts.General.LoadScene;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.UI.ButtonSpace;
using App.Scripts.Scenes.Configs;
using App.Scripts.Scenes.MainMenu;
using UnityEngine;

namespace App.Scripts.Scenes.Level.UI
{
    public class LevelPassedPopUp : PopUp
    {
        [SerializeField] private CustomButton _nextButton;
        [SerializeField] private CustomButton _backButton;
        [SerializeField] private LevelsConfig _levelsConfig;

        #region Events

        private void OnEnable()
        {
            _nextButton.OnClickOccurred.AddListener(NextButtonClickedCallback);
            _backButton.OnClickOccurred.AddListener(BackButtonClickedCallback);

            LevelPassedOnEnable();
        }

        private void OnDisable()
        {
            _nextButton.OnClickOccurred.RemoveListener(NextButtonClickedCallback);
            _backButton.OnClickOccurred.RemoveListener(BackButtonClickedCallback);
        }

        #endregion
        
        private void LevelPassedOnEnable()
        {
            LevelRepository levelRepository = new LevelRepository(_levelsConfig.SelectedLevelIndex);
            levelRepository.SetLevelIsComplete();
            
            _levelsConfig.SelectedLevelIndex++;
        }
        
        private void NextButtonClickedCallback()
        {
            SceneEnum loadSceneEnum = SceneEnum.Level;

            if (_levelsConfig.SelectedLevelIndex >= _levelsConfig.LevelsCount)
            {
                loadSceneEnum = SceneEnum.MainMenu;
            }

            SceneLoader.Instance.LoadScene(loadSceneEnum);
            HidePopUp();
        }
        
        private void BackButtonClickedCallback()
        {
            SceneLoader.Instance.LoadScene(SceneEnum.MainMenu);
            HidePopUp();
        }
    }
}