using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.ObjectPool;
using App.Scripts.General.Singleton;
using App.Scripts.General.SystemPopUps.PopUps;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public class PopUpSystem : MonoSingleton<PopUpSystem>
    {
        [SerializeField] private PoolObjectInformation<PopUp>[] _popUpList;
        [SerializeField] private Canvas _canvasParent;
        
        private List<PopUp> _activePopUps = new List<PopUp>();
        private PopUpContainerByType _popUpContainerByType;

        public int ActivePopUpsCount => _activePopUps.Count;
        public PopUp LastActivePopUp => _activePopUps.Last();

        protected override void Awake()
        {
            base.Awake();
            
            _popUpContainerByType = new PopUpContainerByType(_popUpList, _canvasParent.transform);
        }

        public T ShowPopUp<T>() where T : PopUp
        {
            PopUp popUp = _popUpContainerByType.GetPopUpFromPoolByType(typeof(T));
            popUp!.OnPopUpClose += DeletePopUpFromActivePopUps;
            _activePopUps.Add(popUp);
            popUp.gameObject.SetActive(true);
            popUp.ShowPopUp();

            return (T)popUp;
        }

        public T GetPopUpWithoutShow<T>()  where T : PopUp
        {
            T popUp = (T)_popUpContainerByType.GetPopUpFromPoolByType(typeof(T));
            _popUpContainerByType.ReturnPopUpToPool(popUp);

            return popUp;
        }

        private void DeletePopUpFromActivePopUps(PopUp popUp)
        {
            popUp.OnPopUpClose -= DeletePopUpFromActivePopUps;
            _popUpContainerByType.ReturnPopUpToPool(popUp);
            _activePopUps.Remove(popUp);
        }

        public void HideAllActivePopUps()
        {
            while (_activePopUps.Count > 0)
            {
                _activePopUps[0].HidePopUp();
            }
        }
    }
}