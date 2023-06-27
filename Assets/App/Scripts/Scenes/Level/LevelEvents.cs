using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.Level.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class LevelEvents : MonoBehaviour
    {
        [SerializeField] private MoveBlockInput _moveBlockInput;
        
        public static void LevelPassed()
        {
            if(PopUpSystem.Instance.ActivePopUpsCount > 0) return;
            
            LevelPassedPopUp levelPassedPopUp = PopUpSystem.Instance.ShowPopUp<LevelPassedPopUp>();
        }

        public static void GameOver()
        {
            if(PopUpSystem.Instance.ActivePopUpsCount > 0) return;
            
            GameOverPopUp gameOverPopUp = PopUpSystem.Instance.ShowPopUp<GameOverPopUp>();
        }
    }
}