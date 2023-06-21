using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class BlockGrid : MonoBehaviour
    {
        [SerializeField] private LevelSceneConfig _levelSceneConfig;
        [SerializeField] private Transform _blockContainer;
        
        private ObjectPool<Block> _emptyGridBlockPool;
        private Block[,] _blocksGrid;
        private BlockGridConfig _config => _levelSceneConfig.BlockGridConfig;

        public void Initialize(int rows, int columns)
        {
            _config.EmptyGridBlockPoolData.container = _blockContainer;
            _emptyGridBlockPool = new ObjectPool<Block>(_config.EmptyGridBlockPoolData);
            _blocksGrid = new Block[rows,columns];

            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < columns; k++)
                {
                    Block emptyGridBlock = _emptyGridBlockPool.GetElement();
                    SetBlock(i,k, emptyGridBlock);
                }
            }
        }
        
        public Block GetBlock(int row, int column)
        {
            return _blocksGrid[row, column];
        }

        private void SetBlock(int row, int column, Block block)
        {
            _blocksGrid[row, column] = block;
            int mirrorX = _config.MirrorX ? -1 : 1;
            int mirrorZ = _config.MirrorZ ? -1 : 1;
            block.transform.position = new Vector3(column * _config.CellSize * mirrorX, 0, 
                row * _config.CellSize * mirrorZ);
            block.gameObject.SetActive(true);
        }

        public Block ReplaceBlock(int row, int column, Block newBlock)
        {
            Block replacedBlock = GetBlock(row, column);
            SetBlock(row, column, newBlock);

            return replacedBlock;
        }
    }
}