using System;
using UnityEngine;

namespace App.Scripts.Scenes.Level.Blocks
{
    public class DisappearBlock : Block
    {
        [SerializeField] private int _disappearBlocksCount = 1;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody.TryGetComponent(out Block block))
            {
                Disappear(block);
            }
        }

        private void Disappear(Block block)
        {
            block.Destroy();

            _disappearBlocksCount--;
            if (_disappearBlocksCount <= 0)
            {
                Destroy();
            }
        }
    }
}