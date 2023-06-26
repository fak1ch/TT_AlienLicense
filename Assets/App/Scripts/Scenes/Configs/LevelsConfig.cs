using System.IO;
using App.Scripts.General;
using App.Scripts.Scenes.Level;
using UnityEngine;

namespace App.Scripts.Scenes.Configs
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "App/LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        public const string LevelsFolder = "Levels";
        public const string LevelName = "level";
        public const string FileType = ".json";

        public int LevelsCount => _levelsCount;
        public int SelectedLevelIndex;

        [SerializeField] private int _levelsCount = 0;

        public void SaveLevelByIndex(int index, MapData mapData)
        {
            string filename = $"{LevelName}{index}{FileType}";
            string path = Path.Combine(Application.dataPath, "App", "Resources", LevelsFolder, filename);
            
            JsonParser<MapData> jsonParser = new JsonParser<MapData>();
            jsonParser.SaveDataToFile(mapData, path);

            _levelsCount++;
        }

        public MapData LoadLevelByIndex(int index)
        {
            string filename = $"{LevelName}{index}";
            string path = Path.Combine(LevelsFolder, filename);
            
            JsonParser<MapData> jsonParser = new JsonParser<MapData>();
            MapData mapData = jsonParser.LoadDataFromFile(path);

            return mapData;
        }
    }
}