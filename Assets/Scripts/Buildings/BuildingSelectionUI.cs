
namespace CraftGame
{
    using TMPro;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class BuildingSelectionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Image _image;
        [SerializeField] private BuildingVariable _inPlacementBuilding;

        private Building _buildingReferenced;
        
        public void Init(string title, string cost, Sprite sprite, Building buildingReferenced)
        {
            _titleText.SetText(title);
            _costText.SetText(cost);
            _image.sprite = sprite;
            _buildingReferenced = buildingReferenced;
        }

        public void AssignInPlacementBuilding()
        {
            _inPlacementBuilding.Value = _buildingReferenced;
        }
    }
}