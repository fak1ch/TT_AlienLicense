using UnityEngine;

namespace App.Scripts.Scenes.MainMenu
{
    public class LevelRepository
    {
        private readonly string LevelNameKey = "Level";
        private const string IsCompleteKey = "IsComplete";

        public bool IsComplete => PlayerPrefs.GetInt(LevelNameKey + IsCompleteKey, 0) == 1;
        public readonly int Index;

        public LevelRepository(int index)
        {
            LevelNameKey += index;
            Index = index;
        }

        public void SetLevelIsComplete()
        {
            PlayerPrefs.SetInt(LevelNameKey + IsCompleteKey, 1);
        }
    }
}