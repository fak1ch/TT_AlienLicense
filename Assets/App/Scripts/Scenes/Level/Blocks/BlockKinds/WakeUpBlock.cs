using System;
using App.Scripts.Scenes.Level.Player;
using UnityEngine;

namespace App.Scripts.Scenes.Level.Blocks
{
    public class WakeUpBlock : Block
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.rigidbody.TryGetComponent(out PlayerBlock playerBlock))
            {
                Debug.Log("game over");
            }
        }
    }
}