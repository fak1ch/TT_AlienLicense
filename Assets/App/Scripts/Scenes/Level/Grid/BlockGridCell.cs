﻿using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class BlockGridCell : MonoBehaviour
    {
        public Block Block { get; private set; }
        public bool IsEmpty => Block == null;
        
        public void SetBlock(Block block)
        {
            Block = block;
        }

        public void Clear()
        {
            Block = null;
        }
    }
}