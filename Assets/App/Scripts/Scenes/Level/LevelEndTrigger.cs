using App.Scripts.Scenes.Level.Player;
using UnityEngine;

namespace App.Scripts.Scenes.Level
{
    public class LevelEndTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBlock playerBlock))
            {
                LevelEvents.LevelPassed();
            }
        }
    }
}