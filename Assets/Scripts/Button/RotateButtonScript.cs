using UnityEngine;

namespace Demo2
{
    public class RotateButtonScript : MonoBehaviour
    {
        UIButton rotateButton;

        private void Awake()
        {
            rotateButton = transform.GetComponent<UIButton>();
            if (rotateButton != null)
                rotateButton.onClick.Add(new EventDelegate(OnRotateClick));
        }

        private void OnRotateClick()
        {
            GameController.Instance.GetInput(Vector2.zero, true);
        }
    }
}