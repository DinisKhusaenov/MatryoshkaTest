using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CookingPrototype.UI
{
    public sealed class StartWindow : MonoBehaviour
    {
        public event Action<int> PlayClicked;

        [SerializeField] private Button _play;
        [SerializeField] private TMP_InputField _victoryCondition;

        private int _victory;

        public void OnEnable()
        {
            _play.onClick.AddListener(() => PlayClicked?.Invoke(_victory));

            _victoryCondition.onValueChanged.AddListener(OnValueChanged);
            _victoryCondition.onValidateInput += ValidateInput;
        }

        public void OnDisable()
        {
            _play.onClick.RemoveListener(() => PlayClicked?.Invoke(_victory));

            _victoryCondition.onValueChanged.RemoveListener(OnValueChanged);
            _victoryCondition.onValidateInput -= ValidateInput;
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        private void OnValueChanged(string value)
        {
            if (!IsNumeric(value))
            {
                _victoryCondition.text = "";
            }

            if (value != "")
                _victory = int.Parse(value);
        }

        private char ValidateInput(string text, int charIndex, char addedChar)
        {
            if (!char.IsDigit(addedChar))
            {
                return '\0';
            }

            return addedChar;
        }

        private bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }
    }
}
