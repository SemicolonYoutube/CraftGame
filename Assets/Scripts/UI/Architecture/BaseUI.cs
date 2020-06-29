namespace CraftGame
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class BaseUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private bool _autoUpdate;
        
        private Selectable[] _selectables;
        
        public virtual void Create()
        {
            UpdateSelectables();
        }

        public virtual void Destroy(Action callback)
        {
            callback?.Invoke();
        }

        public virtual void Show(int canvasOrder = 1)
        {
            _canvas.overrideSorting = true;
            _canvas.sortingOrder = canvasOrder;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Focus(bool focus)
        {
            if (_autoUpdate)
                UpdateSelectables();

            foreach (Selectable selectable in _selectables)
            {
                selectable.interactable = focus;
            }
        }

        private void UpdateSelectables()
        {
            _selectables = gameObject.GetComponentsInChildren<Selectable>();
        }
    }
}

