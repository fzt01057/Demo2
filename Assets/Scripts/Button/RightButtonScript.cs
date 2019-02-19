using UnityEngine;

namespace Demo2
{
    public class RightButtonScript : MonoBehaviour
    {
        UIButton rightButton;

        private void Awake()
        {
            rightButton = transform.GetComponent<UIButton>();
            if (rightButton != null)
                rightButton.onClick.Add(new EventDelegate(OnRightClick));
        }

        private void OnRightClick()
        {
            GameController.Instance.GetInput(new Vector2(1, 0), false);
        }
    }
}