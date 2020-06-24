using System;
using UnityEngine.EventSystems;

namespace CraftGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BuildingPlacementController : MonoBehaviour
    {
        [SerializeField] private float _gap = 0.001f;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerNoRaycast;
        [SerializeField] private int _rotationStep = 1;

        private float _angle;
        private Vector3 _inPlacementPosition;
        private bool _canBuild = false;
        private Building _selected = null;
        private Building _inPlacement = null;
        public void InPlacementBuildingChanged(Building building)
        {
            _inPlacement = Instantiate(building, Vector3.zero, Quaternion.identity);
            _inPlacement.InstantiateFbx();
        }

        private void UpdateStateInPlacement()
        {
            // Displacement
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~_layerNoRaycast))
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
                    
                _inPlacementPosition = new Vector3(hit.point.x, hit.point.y + _gap, hit.point.z);
            }
            
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

            if (Input.GetKey(KeyCode.A))
            {
                _angle -= _rotationStep;
            }
            if (Input.GetKey(KeyCode.E))
            {
                _angle += _rotationStep;
            }
        }

        private void UpdateStateNotInPlacement()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    Building building = hit.transform.GetComponent<Building>();
                    if (building != null)
                    {
                        ResetSelected();

                        _selected = building;
                        _selected.UpdateGroundState(GroundState.Selected, true);
                    }
                    else
                    {
                        ResetSelected();
                    }
                }
                else
                {
                    ResetSelected();
                }
            }
        }
        
        private void Update()
        {
            if (_inPlacement != null)
            {
                UpdateStateInPlacement();
            }
            else
            {
                UpdateStateNotInPlacement();
            }
        }

        private void FixedUpdate()
        {
            if (_inPlacement != null)
            {
                var _inPlacementTransform = _inPlacement.transform;
                
                // Affect Position
                _inPlacementTransform.position = _inPlacementPosition;
            
                // Affect Rotation
                Vector3 angleVector = new Vector3(0f, _angle, 0f);
                Quaternion angleQuaternion = Quaternion.identity;
                angleQuaternion.eulerAngles = angleVector;
                _inPlacementTransform.rotation = angleQuaternion;
            }
        }

        private void ResetSelected()
        {
            if (_selected)
            {
                _selected.UpdateGroundState(GroundState.None, true);
                _selected = null;
            }
        }

        private bool IsOnGround(GameObject inPlacement)
        {
            Vector3[] lowerCorners = LowerWorldCorners(inPlacement);
            if (lowerCorners == null)
            {
                return false;
            }
                
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
            Vector3 halfSize = size / 2;
            Vector3 center = boundsCollider.center;
            
            Vector3 vertex1 = new Vector3(center.x + halfSize.x, center.y - halfSize.y, center.z + halfSize.z);
            Vector3 vertex2 = new Vector3(center.x - halfSize.x, center.y - halfSize.y, center.z - halfSize.z);
            Vector3 vertex3 = new Vector3(center.x + halfSize.x, center.y - halfSize.y, center.z - halfSize.z);
            Vector3 vertex4 = new Vector3(center.x - halfSize.x, center.y - halfSize.y, center.z + halfSize.z);

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

