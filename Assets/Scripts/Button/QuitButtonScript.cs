using UnityEngine;

namespace Demo2
{
    public class QuitButtonScript : MonoBehaviour
    {
        UIButton quitButton;
        private void Awake()
        {
            quitButton = transform.GetComponent<UIButton>();
            quitButton.onClick.Add(new EventDelegate(OnQuitClick));
        }

        private void OnQuitClick()
        {
            Application.Quit();
        }
    }
}