using System;
using TMPro;

namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class IntDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private IntVariable _variable;

        private void Start()
        {
            if (_variable != null)
            {
                UpdateDisplayer(_variable.Value);
            }
        }

        public void UpdateDisplayer(int value)
        {
            _text.SetText(value.ToString());
        }
    }
}