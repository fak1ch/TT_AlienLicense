using System;
using App.Scripts.Scenes.Level.Config;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class Block : MonoBehaviour
    {
        public event Action<Block> OnDestroy;
        
        public BlockMovement BlockMovement => _blockMovement;
        public string Id => _blockInformation.Id;
        
        [SerializeField] private BlockMovement _blockMovement;

        private BlockInformation _blockInformation;

        public void Initialize(InputSystem inputSystem, Camera mainCamera, BlockInformation blockInformation)
        {
            _blockMovement.Initialize(inputSystem, mainCamera);
            _blockInformation = blockInformation;
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}