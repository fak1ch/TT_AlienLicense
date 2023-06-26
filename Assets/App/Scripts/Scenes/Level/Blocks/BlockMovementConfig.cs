using System;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    [Serializable]
    public class BlockMovementConfig
    {
        public float MoveSpeed = 1000;
        public Vector3 MaxVelocity = new (10, 0, 10);
        public float MoveToPositionDuration = 0.5f;
    }
}