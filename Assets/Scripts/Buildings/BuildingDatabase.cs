namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "BuildingDatabase", menuName = "Buildings/BuildingDatabase")]
    public class BuildingDatabase : ScriptableObject
    {
        [SerializeField] private List<Building> _buildings;

        public List<Building> Buildings => _buildings;
    }
}

