namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SwitchOnOff : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        public void Switch()
        {
            _target.gameObject.SetActive(!_target.gameObject.activeInHierarchy);
        }
    }
}

