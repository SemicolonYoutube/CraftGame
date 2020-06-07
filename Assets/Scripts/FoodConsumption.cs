namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FoodConsumption : MonoBehaviour
    {
        [SerializeField] private IntVariable _food;
        
        void Start()
        {
            StartCoroutine(IeFoodConsumption());
        }

        private IEnumerator IeFoodConsumption()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                _food.Value -= 10;
            }
        }
    } 
}

