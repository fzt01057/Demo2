using UnityEngine;

namespace Demo2
{
    public class StartButtonScript : MonoBehaviour
    {
        UIButton startButton;

        private void Awake()
        {
            startButton = transform.GetComponent<UIButton>();
            startButton.onClick.Add(new EventDelegate(OnStartClick));
        }

        private void OnStartClick()
        {
            GameController.Instance.GameStart();
        }
    }
}