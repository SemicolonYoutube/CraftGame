namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "UI", menuName = "UI/UI")]
    public class UI : ScriptableObject
    {
        [SerializeField] private BaseUI _ui;

        private BaseUI _instantiation;

        public bool Instantiated => _instantiation != null;
        public bool Visible => _instantiation != null && _instantiation.gameObject.activeInHierarchy;
        public BaseUI Instantiation => _instantiation;
        
        public BaseUI InstantiateUI(Canvas mainCanvas)
        {
            _instantiation = Instantiate(_ui, mainCanvas.transform, false);
            _instantiation.Create();

            return _instantiation;
        }

        public void DestroyUI()
        {
            if (_instantiation != null)
            {
                _instantiation.Destroy(() =>
                {
                    Destroy(_instantiation.gameObject);
                    _instantiation = null;
                });
            }
        }
    }
}

