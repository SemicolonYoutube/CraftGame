﻿using System;

namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public enum GroundState
    {
        NotBuildable,
        Buildable,
        None
    }
    
    public class Building : MonoBehaviour
    {
        [SerializeField] private string _buildingName;
        [SerializeField] private Sprite _buildingSprite;
        [SerializeField] private BuildingData _buildingData;
        [SerializeField] private Renderer _groundStateFbx;

        private GameObject _fbx;
        private int _nbTriggerCollider;
        
        public string BuildingName => _buildingName;
        public Sprite BuildingSprite => _buildingSprite;
        public BuildingData BuildingData => _buildingData;
        public int NbTriggerCollider => _nbTriggerCollider;
        
        private void OnTriggerEnter(Collider other)
        {
            _nbTriggerCollider++;
            Building otherBuilding = other.GetComponent<Building>();
            if (otherBuilding)
            {
                otherBuilding.UpdateGroundState(GroundState.NotBuildable, false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _nbTriggerCollider--;
            Building otherBuilding = other.GetComponent<Building>();
            if (otherBuilding)
            {
                otherBuilding.UpdateGroundState(GroundState.None, false);
            }
        }

        public void InstantiateFbx()
        {
            if (_fbx != null)
            {
                Destroy(_fbx.gameObject);
            }

            _fbx = Instantiate(BuildingData.Fbx, transform, false);
        }

        public void UpdateGroundState(GroundState state, bool updateTransparency)
        {
            Color color = Color.red;
            switch (state)
            {
                case GroundState.Buildable:
                    UpdateFbxAlpha(0.5f, updateTransparency);
                    _groundStateFbx.gameObject.SetActive(true);
                    color = Color.green;
                    break;
                case GroundState.NotBuildable:
                    UpdateFbxAlpha(0.5f, updateTransparency);
                    _groundStateFbx.gameObject.SetActive(true);
                    color = Color.red;
                    break; 
                case GroundState.None:
                    UpdateFbxAlpha(1f, updateTransparency);
                    _groundStateFbx.gameObject.SetActive(false);
                    break;
            }

            _groundStateFbx.material.color = color;
        }

        private void UpdateFbxAlpha(float alpha, bool updateTransparency)
        {
            if (_fbx != null && updateTransparency)
            {
                Renderer[] renderers = _fbx.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    var material = renderer.material;
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
            }
        }
    }
}

