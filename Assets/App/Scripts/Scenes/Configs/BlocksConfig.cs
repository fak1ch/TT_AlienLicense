using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Level.Config
{
    [CreateAssetMenu(fileName = "BlocksConfig", menuName = "App/BlocksConfig")]
    public class BlocksConfig : ScriptableObject
    {
        public List<BlockInformation> BlockInformation;
    }

    [Serializable]
    public class BlockInformation
    {
        public Block Prefab;
        public int PoolSize;
    }
}