using System;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    [Serializable]
    public class MapData
    {
        public string[,] BlockIds;
        public CustomVector3 LevelEndTriggerPosition;
        public int MoveCount;
    }

    [Serializable]
    public class CustomVector3
    {
        public float X;
        public float Y;
        public float Z;
    }
}