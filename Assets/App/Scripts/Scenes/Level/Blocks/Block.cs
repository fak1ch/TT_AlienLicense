using System;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class Block : MonoBehaviour
    {
        public event Action<Block> OnDestroy;
        
        public BlockMovement BlockMovement => _blockMovement;
        public string Id => _id;
        public bool IsInitialized { get; private set; }
        
        [SerializeField] private BlockMovement _blockMovement;
        [SerializeField] private string _id;

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}