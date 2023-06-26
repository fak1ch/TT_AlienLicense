using System;
using App.Scripts.General;
using App.Scripts.Scenes.Level.Player;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.Level
{
    public class MapConverter
    {
        public bool TryConvertBlocksGridToMapData(BlockGrid blockGrid, out MapData mapData)
        {
            mapData = new MapData();
            LevelEndTrigger levelEndTrigger = Object.FindObjectOfType<LevelEndTrigger>();
            PlayerBlock playerBlock = Object.FindObjectOfType<PlayerBlock>();

            if (levelEndTrigger == null || playerBlock == null) return false;
            
            string[,] blockIds = new string[blockGrid.Rows, blockGrid.Columns];

            for (int i = 0; i < blockIds.GetLength(0); i++)
            {
                for (int k = 0; k < blockIds.GetLength(1); k++)
                {
                    Block block = blockGrid.GetBlock(i, k);
                    string blockId = block == null ? null : block.Id;

                    blockIds[i, k] = blockId;
                }
            }

            mapData = new MapData()
            {
                BlockIds = blockIds,
                LevelEndTriggerPosition = levelEndTrigger.transform.position.ToCustomVector3(),
            };

            return true;
        }

        public void ConvertMapDataToBlocksGrid(MapData mapData, BlockGrid blockGrid, BlocksPoolContainer blocksPoolContainer,
            LevelEndTrigger levelEndTrigger, InputSystem inputSystem, Camera mainCamera)
        {
            string[,] blockIds = mapData.BlockIds;
            
            for (int i = 0; i < blockIds.GetLength(0); i++)
            {
                for (int k = 0; k < blockIds.GetLength(1); k++)
                {
                    if(blockIds[i,k] == null) continue;
                    
                    Block block = blocksPoolContainer.GetBlockById(blockIds[i, k]);
                    block.Initialize();
                    blockGrid.SetBlock(i,k, block);
                }
            }

            levelEndTrigger.transform.position = mapData.LevelEndTriggerPosition.ToUnityVector3();
        }
    }
}