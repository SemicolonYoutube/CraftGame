
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

        public void Init(string title, string cost, Sprite sprite)
        {
            _titleText.SetText(title);
            _costText.SetText(cost);
            _image.sprite = sprite;
        }
    }
}