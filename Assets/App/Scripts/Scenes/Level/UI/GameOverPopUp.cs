using App.Scripts.General.LoadScene;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.UI.ButtonSpace;
using UnityEngine;

namespace App.Scripts.Scenes.Level.UI
{
    public class GameOverPopUp : PopUp
    {
        [SerializeField] private CustomButton _restartButton;
        [SerializeField] private CustomButton _backButton;

        #region Events

        private void OnEnable()
        {
            _restartButton.OnClickOccurred.AddListener(RestartButtonClickedCallback);
            _backButton.OnClickOccurred.AddListener(BackButtonClickedCallback);
        }

        private void OnDisable()
        {
            _restartButton.OnClickOccurred.RemoveListener(RestartButtonClickedCallback);
            _backButton.OnClickOccurred.RemoveListener(BackButtonClickedCallback);
        }

        #endregion

        private void RestartButtonClickedCallback()
        {
            SceneLoader.Instance.LoadScene(SceneEnum.Level);
            HidePopUp();
        }
        
        private void BackButtonClickedCallback()
        {
            SceneLoader.Instance.LoadScene(SceneEnum.MainMenu);
            HidePopUp();
        }
    }
}