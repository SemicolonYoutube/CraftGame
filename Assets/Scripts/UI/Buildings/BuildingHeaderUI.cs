namespace CraftGame
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BuildingHeaderUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _upgradeContainer;
        [SerializeField] private GameObject _seeContainer;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
        }

        public void UpdateUI(bool canUpgrade, bool canSee)
        {
            _upgradeContainer.gameObject.SetActive(canUpgrade);
            _seeContainer.gameObject.SetActive(canSee);
        }
    }
}

