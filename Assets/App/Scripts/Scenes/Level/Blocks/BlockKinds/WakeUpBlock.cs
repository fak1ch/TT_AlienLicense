using System;
using App.Scripts.Scenes.Level.Player;
using UnityEngine;

namespace App.Scripts.Scenes.Level.Blocks
{
    public class WakeUpBlock : Block
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(IsInitialized == false) return;
            
            if (collision.gameObject.TryGetComponent(out PlayerBlock playerBlock))
            {
                LevelEvents.GameOver();
            }
        }
    }
}