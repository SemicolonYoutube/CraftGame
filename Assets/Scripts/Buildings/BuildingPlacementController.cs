using System;

namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BuildingPlacementController : MonoBehaviour
    {
        [SerializeField] private float _gap = 0.001f;
        [SerializeField] private Camera _camera;
        [SerializeField] private BuildingCreationUI _creationUI;
        [SerializeField] private LayerMask _layerNoRaycast;

        private bool _canBuild = false;
        private Building _inPlacement = null;
        public void InPlacementBuildingChanged(Building building)
        {
            _inPlacement = Instantiate(building, Vector3.zero, Quaternion.identity);
            _inPlacement.InstantiateFbx();
            _creationUI.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_inPlacement != null)
            {
                // Confirm
                if (Input.GetMouseButtonDown(0) && _canBuild)
                {
                    _inPlacement.UpdateGroundState(GroundState.None, true);
                    _inPlacement = null;
                }
            
                // Cancel
                if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
                {
                    if (_inPlacement != null)
                    {
                        Destroy(_inPlacement.gameObject);
                        _inPlacement = null;
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (_inPlacement != null)
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~_layerNoRaycast))
                {
                    // Displacement
                    if (hit.transform)
                    {
                        if (IsOnGround(_inPlacement.gameObject) && _inPlacement.NbTriggerCollider == 0)
                        {
                            _canBuild = true;
                            _inPlacement.UpdateGroundState(GroundState.Buildable, true);
                        }
                        else
                        {
                            _canBuild = false;
                            _inPlacement.UpdateGroundState(GroundState.NotBuildable, true);
                        }
                        
                        Vector3 newPosition = new Vector3(hit.point.x, hit.point.y + _gap, hit.point.z);
                        _inPlacement.transform.position = newPosition;
                    }
                }
            }
            else
            {
                _canBuild = false;
            }
        }

        private bool IsOnGround(GameObject inPlacement)
        {
            Vector3[] lowerCorners = LowerWorldCorners(inPlacement);
            foreach (Vector3 corner in lowerCorners)
            {
                if (Physics.Raycast(corner, -gameObject.transform.up, out var hitInfo, Mathf.Infinity) == false)
                {
                    return false;
                }
            }

            return true;
        }

        private Vector3[] LowerWorldCorners(GameObject inPlacement)
        {
            Collider collider = inPlacement.GetComponent<Collider>();
            if (collider == null)
                return null;

            Bounds boundsCollider = collider.bounds;
            Vector3 size = boundsCollider.size;
            Vector3 center = boundsCollider.center;
            
            Vector3 vertex1 = new Vector3(center.x + size.x / 2, center.y - size.y / 2, center.z + size.z / 2);
            Vector3 vertex2 = new Vector3(center.x - size.x / 2, center.y - size.y / 2, center.z - size.z / 2);
            Vector3 vertex3 = new Vector3(center.x + size.x / 2, center.y - size.y / 2, center.z - size.z / 2);
            Vector3 vertex4 = new Vector3(center.x - size.x / 2, center.y - size.y / 2, center.z + size.z / 2);

            return new[]
            {
                transform.TransformPoint(vertex1),
                transform.TransformPoint(vertex2),
                transform.TransformPoint(vertex3),
                transform.TransformPoint(vertex4)
            };
        }
    }
}

