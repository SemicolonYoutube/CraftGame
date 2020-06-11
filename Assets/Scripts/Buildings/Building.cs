namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Building : MonoBehaviour
    {
        [SerializeField] private string _buildingName;
        [SerializeField] private Sprite _buildingSprite;
        [SerializeField] private BuildingData _buildingData;

        public string BuildingName => _buildingName;
        public Sprite BuildingSprite => _buildingSprite;
        public BuildingData BuildingData => _buildingData;
    }
}

