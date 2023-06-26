using System;
using App.Scripts.General.PopUpSystemSpace;
using UnityEngine;

namespace App.Scripts.General.UI.ButtonSpace
{
    public class OpenLocalPopUp : MonoBehaviour
    {
        [SerializeField] private CustomButton _customButton;
        [SerializeField] private PopUp _popUp;

        #region Events

        private void OnEnable()
        {
            _customButton.OnClickOccurred.AddListener(OpenLocalPopUpButtonClickedCallback);
        }

        private void OnDisable()
        {
            _customButton.OnClickOccurred.RemoveListener(OpenLocalPopUpButtonClickedCallback);
        }

        #endregion
        
        private void OpenLocalPopUpButtonClickedCallback()
        {
            _popUp.ShowPopUp();
        }
    }
}