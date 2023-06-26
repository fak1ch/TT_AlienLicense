using System.Collections.Generic;
using App.Scripts.General.ObjectPool;
using App.Scripts.Scenes.Level.Config;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class BlocksPoolContainer : MonoBehaviour
    {
        [SerializeField] private Transform _blocksPoolParent;

        private Dictionary<string, ObjectPool<Block>> _blockPoolByIdMap;

        public void Initialize(List<BlockInformation> blockInformationList)
        {
            _blockPoolByIdMap = new Dictionary<string, ObjectPool<Block>>();
            
            foreach (var blockInformation in blockInformationList)
            {
                var poolData = new PoolData<Block>
                {
                    size = blockInformation.PoolSize,
                    container = _blocksPoolParent,
                    prefab = blockInformation.Prefab
                };
                
                _blockPoolByIdMap.Add(blockInformation.Prefab.Id, new ObjectPool<Block>(poolData));
            }
        }

        public Block GetBlockById(string id)
        {
            return _blockPoolByIdMap[id].GetElement();
        }

        public void ReturnBlockToPool(Block block)
        {
            _blockPoolByIdMap[block.Id].ReturnElementToPool(block);
        }
    }
}