using System;
using App.Scripts.General.ObjectPool;

namespace App.Scripts.Scenes.Level
{
    [Serializable]
    public class BlockGridConfig
    {
        public PoolData<BlockGridCell> BlockGridCellPoolData;
        public float CellSize = 1;
        public bool MirrorX = false;
        public bool MirrorZ = true;
    }
}