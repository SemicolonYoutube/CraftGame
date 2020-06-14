namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "BuildingData", menuName = "Buildings/BuildingData")]
    public class BuildingData : ScriptableObject
    {
        [SerializeField] private int _maxWorker;
        [SerializeField] private int _coinCost;
        [SerializeField] private GameObject _fbx;

        public int CoinCost => _coinCost;
        public GameObject Fbx => _fbx;
    }
}

