namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Consumption : MonoBehaviour
    {
        [SerializeField] private IntVariable _toConsume;
        
        void Start()
        {
            StartCoroutine(IeConsumption());
        }

        private IEnumerator IeConsumption()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                _toConsume.Value -= 10;
            }
        }
    }
}

