using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu
{
    public class LevelCellView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _indexText;

        public void SetIndex(int index)
        {
            _indexText.text = index.ToString();
        }
    }
}