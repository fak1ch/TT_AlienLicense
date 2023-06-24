using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class BlockGrid : MonoBehaviour
    {
        public float CellSize => _config.CellSize;
        public int RowsCount => _blockCellsGrid.GetLength(0) - 1;
        public int ColumnsCount => _blockCellsGrid.GetLength(1) - 1;
        
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
            block.transform.position = _blockCellsGrid[row, column].transform.position;
        }

        public Block ReplaceBlock(int row, int column, Block newBlock)
        {
            Block replacedBlock = GetBlock(row, column);
            SetBlock(row, column, newBlock);

            return replacedBlock;
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
            BlockGridCell resultCell = null;
            Vector3 blockPosition = block.transform.position;
            float minDistance = float.MaxValue;

            foreach (var blockGridCell in _blockCellsGrid)
            {
                if(blockGridCell.IsEmpty == false) continue;

                float distance = Vector3.Distance(blockGridCell.transform.position, blockPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    resultCell = blockGridCell;
                }
            }

            if (resultCell != null)
            {
                resultCell.SetBlock(block);
                block.BlockMovement.MoveToPosition(resultCell.transform.position);
            }
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
    }
}