using System;

namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BuildingCreationUI : MonoBehaviour
    {
        [SerializeField] private BuildingDatabase _database;
        [SerializeField] private BuildingSelectionUI _prefabSelection;
        [SerializeField] private Transform _containerParent;
        
        private void Start()
        {
            Clean();

            foreach (Building building in _database.Buildings)
            {
                BuildingSelectionUI createdSelection = Instantiate(_prefabSelection, _containerParent, false);
                createdSelection.Init(building.BuildingName, building.BuildingData.CoinCost.ToString(), building.BuildingSprite);
            }
        }

        private void Clean()
        {
            foreach (Transform child in _containerParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
}