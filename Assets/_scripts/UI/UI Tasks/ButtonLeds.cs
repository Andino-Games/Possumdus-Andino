using UnityEngine;
using UnityEngine.UI;

namespace _scripts.UI.UI_Tasks
{
    public class ButtonLeds : MonoBehaviour
    {
        public Image ledImage;
        public Image buttonImage;

        public Sprite ledOnSprite;
        public Sprite ledOffSprite;
        public Sprite buttonOffSprite;
        public Sprite buttonOnSprite;

        public int state;
        public int targetState;

        public delegate void ButtonStateChanged(ButtonLeds button);
        public event ButtonStateChanged OnStateChanged;
        public void InitializeState()
        {
            state = (targetState == 0) ? 1 : 0;
            UpdateVisuals();
        }

        public void ToggleState()
        {
            state = (state == 0) ? 1 : 0;
            OnStateChanged?.Invoke(this);
            UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            ledImage.sprite = (state == targetState) ? ledOnSprite : ledOffSprite;
            buttonImage.sprite = (state == 1) ? buttonOnSprite : buttonOffSprite;
        }
    }
}
