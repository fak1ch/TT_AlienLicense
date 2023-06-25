using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class MapConverter
    {
        public MapData ConvertBlocksGridToMapData(BlockGrid blockGrid)
        {
            string[,] blockIds = new string[blockGrid.RowsCount, blockGrid.ColumnsCount];

            for (int i = 0; i < blockIds.GetLength(0); i++)
            {
                for (int k = 0; k < blockIds.GetLength(1); k++)
                {
                    Block block = blockGrid.GetBlock(i, k);
                    string blockId = block == null ? null : block.Id;

                    blockIds[i, k] = blockId;
                }
            }

            return new MapData()
            {
                BlockIds = blockIds,
            };
        }

        public void ConvertMapDataToBlocksGrid(MapData mapData, BlockGrid blockGrid, BlocksPoolContainer blocksPoolContainer)
        {
            string[,] blockIds = mapData.BlockIds;
            
            for (int i = 0; i < blockIds.GetLength(0); i++)
            {
                for (int k = 0; k < blockIds.GetLength(1); k++)
                {
                    Block block = blocksPoolContainer.GetBlockById(blockIds[i, k]);
                    blockGrid.SetBlock(i,k, block);
                }
            }
        }
    }
}