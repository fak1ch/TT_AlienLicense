using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.Level.UI
{
    public class MoveCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;

        private int _moveCount;
        
        public void Initialize(int moveCount)
        {
            _moveCount = moveCount;
            UpdateView();
        }

        public void MakeMove()
        {
            _moveCount--;
            UpdateView();

            if (_moveCount <= 0)
            {
                LevelEvents.GameOver();
            }
        }

        private void UpdateView()
        {
            _counterText.text = _moveCount.ToString();
        }
    }
}