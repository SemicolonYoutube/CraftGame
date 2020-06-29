using System;
using System.Linq;

namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public enum UIOpenMode
    {
       Single,
       Additive
    }
    
    [CreateAssetMenu(fileName = "UIDatabase", menuName = "UI/UIDatabase")]
    public class UIDatabase : ScriptableObject
    {
        [SerializeField] private string _canvasTag = "MainCanvas";
        [SerializeField] private List<UI> _UIs;
        [SerializeField] private UIVariable _closingVariable;

        [NonSerialized] private int _canvasOrder = 1;
        [NonSerialized] private Canvas _mainCanvas;
        [NonSerialized] private List<UI> _queue = new List<UI>();

        private void SearchMainCanvas()
        {
            _mainCanvas = GameObject.FindGameObjectWithTag(_canvasTag).GetComponent<Canvas>();
        }

        private void ManageOpenMode(UIOpenMode mode)
        {
            switch (mode)
            {
                case UIOpenMode.Single:
                    _canvasOrder = 1;
                    _queue.ForEach(element => element.DestroyUI());
                    _queue.Clear();
                    break;
                case UIOpenMode.Additive:
                    UI last = _queue.LastOrDefault();
                    if (last != null)
                    {
                        last.Instantiation.Focus(false);
                    }
                    break;
            }
        }

        public void OpenSingle(UI ui)
        {
            Open(ui, UIOpenMode.Single);
        }

        public void OpenAdditive(UI ui)
        {
            Open(ui, UIOpenMode.Additive);
        }
        
        public void Open(UI ui, UIOpenMode mode)
        {
            // Expensive call
            // Search main canvas
            if (_mainCanvas == null)
            {
                SearchMainCanvas();
                if (_mainCanvas == null)
                    return;
            }

            // Ensure that instantiation is valid
            BaseUI instantiation = null;
            if (ui.Instantiated == false)
                instantiation = ui.InstantiateUI(_mainCanvas);
            else
                instantiation = ui.Instantiation;
            
            // Show / Focus
            ManageOpenMode(mode);
            instantiation.Show(_canvasOrder);
            instantiation.Focus(true);
            _queue.Add(ui);
            _canvasOrder++;
        }

        public void Close(UI ui)
        {
            if (ui.Instantiated && ui.Visible)
            {
                ui.Instantiation.Hide();
                ui.DestroyUI();
                _queue.Remove(ui);

                UI lastUI = _queue.LastOrDefault();
                if(lastUI != null)
                {
                    lastUI.Instantiation.Focus(true);
                }

                _closingVariable.Value = ui;
            }
        }
    }
}

