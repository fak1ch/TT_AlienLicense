using App.Scripts.Scenes.Level;
using UnityEngine;

namespace App.Scripts.Scenes.Configs
{
    [CreateAssetMenu(fileName = "LevelSceneConfig", menuName = "App/LevelSceneConfig")]
    public class LevelSceneConfig : ScriptableObject
    {
        public BlockGridConfig BlockGridConfig;
    }
}