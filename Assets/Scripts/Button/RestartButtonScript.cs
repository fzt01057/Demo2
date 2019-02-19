using UnityEngine;

namespace Demo2
{
    public class RestartButtonScript : MonoBehaviour
    {
        UIButton restartButton;

        private void Awake()
        {
            restartButton = transform.GetComponent<UIButton>();
            restartButton.onClick.Add(new EventDelegate(OnRestartClick));
        }

        private void OnRestartClick()
        {
            GameController.Instance.GameStart();
        }
    }
}