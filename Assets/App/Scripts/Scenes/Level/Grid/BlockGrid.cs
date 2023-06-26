using System.Collections.Generic;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class BlockGrid : MonoBehaviour
    {
        public float CellSize => _config.CellSize;
        public int Rows => _blockCellsGrid.GetLength(0);
        public int Columns => _blockCellsGrid.GetLength(1);
        
        [SerializeField] private LevelSceneConfig _levelSceneConfig;
        [SerializeField] private Transform _blockContainer;
        
        private ObjectPool<BlockGridCell> _blockGridCellPoll;
        private BlockGridCell[,] _blockCellsGrid;
        private BlockGridConfig _config => _levelSceneConfig.BlockGridConfig;

        public void Initialize(int rows, int columns)
        {
            _config.BlockGridCellPoolData.container = _blockContainer;
            _blockGridCellPoll = new ObjectPool<BlockGridCell>(_config.BlockGridCellPoolData);
            _blockCellsGrid = new BlockGridCell[rows,columns];
            
            SpawnCells(rows, columns);
        }
        
        public Block GetBlock(int row, int column)
        {
            return _blockCellsGrid[row, column].Block;
        }

        public BlockGridCell GetCell(int row, int column)
        {
            return _blockCellsGrid[row, column];
        }
        
        public void SetBlock(int row, int column, Block block)
        {
            _blockCellsGrid[row, column].SetBlock(block);
            Vector3 cellOffset = GetCellOffset(block);
            block.transform.position = _blockCellsGrid[row, column].transform.position + cellOffset;
            block.gameObject.SetActive(true);
        }

        private void SpawnCells(int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < columns; k++)
                {
                    BlockGridCell blockGridCell = _blockGridCellPoll.GetElement();
                    _blockCellsGrid[i, k] = blockGridCell;
                    
                    int mirrorX = _config.MirrorX ? -1 : 1;
                    int mirrorZ = _config.MirrorZ ? -1 : 1;
                    blockGridCell.transform.position = new Vector3(k * _config.CellSize * mirrorX, 0, 
                        i * _config.CellSize * mirrorZ);
                    blockGridCell.gameObject.SetActive(true);
                }
            }
        }

        public void AddBlockToNearestCell(Block block)
        {
            Vector3 cellOffset = GetCellOffset(block);
            Vector3 newCellPoint = block.transform.position - cellOffset;

            BlockGridCell nearestSell = GetNearestBlockGridCell(newCellPoint);
            Vector3 cellPosition = nearestSell.transform.position + cellOffset;

            nearestSell.SetBlock(block);
            block.BlockMovement.MoveToPosition(cellPosition);
        }

        public void ClearCellByBlock(Block selectedBlock)
        {
            foreach (var blockGridCell in _blockCellsGrid)
            {
                if (blockGridCell.Block == selectedBlock)
                {
                    blockGridCell.Clear();
                    return;
                }
            }
        }

        private Vector3 GetCellOffset(Block block)
        {
            float halfCellOffset = block.transform.localScale.x % 2 == 0 ?
                _config.CellSize / 2 : 0;
            return block.transform.right.normalized * halfCellOffset;
        }
        
        private BlockGridCell GetNearestBlockGridCell(Vector3 position)
        {
            BlockGridCell resultCell = null;
            
            float minDistance = float.MaxValue;
            foreach (var blockGridCell in _blockCellsGrid)
            {
                if(blockGridCell.IsEmpty == false) continue;

                float distance = Vector3.Distance(blockGridCell.transform.position, position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    resultCell = blockGridCell;
                }
            }

            return resultCell;
        }
    }
}