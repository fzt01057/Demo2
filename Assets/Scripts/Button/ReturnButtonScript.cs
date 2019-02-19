using UnityEngine;

namespace Demo2
{
    public class ReturnButtonScript : MonoBehaviour
    {
        UIButton returnButton;

        private void Awake()
        {
            returnButton = transform.GetComponent<UIButton>();
            returnButton.onClick.Add(new EventDelegate(OnReturnClick));
        }

        private void OnReturnClick()
        {
            GameController.Instance.Init();
        }
    }
}
