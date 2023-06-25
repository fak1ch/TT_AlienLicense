using System;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.UI.ButtonSpace;
using App.Scripts.Scenes.Level;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.LevelCreator
{
    public class CreateLevelSettingsPopUp : PopUp
    {
        public event Action<int, int> OnInitializeButtonClicked;
        
        [SerializeField] private TMP_InputField _rows;
        [SerializeField] private TMP_InputField _columns;
        [SerializeField] private CustomButton _initializeButton;
        
        #region events

        private void OnEnable()
        {
            _initializeButton.OnClickOccurred.AddListener(InitializeButtonClickedCallback);
        }

        private void OnDisable()
        {
            _initializeButton.OnClickOccurred.RemoveListener(InitializeButtonClickedCallback);
        }

        #endregion

        private void InitializeButtonClickedCallback()
        {
            int rows = int.Parse(_rows.text);
            int columns = int.Parse(_columns.text);
            
            OnInitializeButtonClicked?.Invoke(rows, columns);
            
            HidePopUp();
        }
    }
}