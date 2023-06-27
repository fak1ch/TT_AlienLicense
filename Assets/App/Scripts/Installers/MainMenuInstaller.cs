using System;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.Configs;
using App.Scripts.Scenes.MainMenu;
using UnityEngine;

namespace App.Scripts.Installers
{
    public class MainMenuInstaller : Installer
    {
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private LevelCell _levelCellPrefab;
        [SerializeField] private Transform _container;
        
        private void Awake()
        {
            PopUpSystem.Instance.enabled = true;
            
            for (int i = 0; i < _levelsConfig.LevelsCount; i++)
            {
                LevelCell levelCell = Instantiate(_levelCellPrefab, _container);
                levelCell.Initialize(i, _levelsConfig);
            }
        }
    }
}