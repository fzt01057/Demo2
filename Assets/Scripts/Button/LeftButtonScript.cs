using UnityEngine;

namespace Demo2
{
    public class LeftButtonScript : MonoBehaviour
    {
        UIButton leftButton;

        private void Awake()
        {
            leftButton = transform.GetComponent<UIButton>();
            if (leftButton != null)
                leftButton.onClick.Add(new EventDelegate(OnLeftClick));
        }

        private void OnLeftClick()
        {
            GameController.Instance.GetInput(new Vector2(-1, 0), false);
        }
    }
}