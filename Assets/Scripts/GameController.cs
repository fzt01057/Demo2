using System.Collections;
using UnityEngine;
using Common;

namespace Demo2
{
    public class GameController : MonoSingleTon<GameController>
    {
        public Cell[,] gameArea;
        private Square[] squares;
        public Square target = null;
        [HideInInspector]
        public bool isLost = false;
        GameObject GameOverPanel, GameStartPanel,GamePanel;

        private new void Awake()
        {
            squares = Resources.LoadAll<Square>("Prefab");
            GameOverPanel = GameObject.Find("GameOverPanel");
            GameStartPanel = GameObject.Find("GameStartPanel");
            GamePanel = GameObject.Find("GamePanel");
            gameArea = new Cell[10, 15];
        }

        public override void Init()
        {
            base.Init();
            GameStartPanel.SetActive(true);
            GameOverPanel.SetActive(false);
            GamePanel.SetActive(false);
        }

        public void GameStart()
        {
            GamePanel.SetActive(true);
            GameStartPanel.SetActive(false);
            GameOverPanel.SetActive(false);
            for (int i = 0, iMax = gameArea.GetLength(0); i < iMax; i++)
                for (int j = 0, jMax = gameArea.GetLength(1); j < jMax; j++)
                    if (gameArea[i, j] != null)
                        gameArea[i, j] = null;
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
            for (int i = 0, iMax = objects.Length; i < iMax; i++)
                Destroy(objects[i]);
            isLost = false;
            StartCoroutine(GameMain());
        }

        private IEnumerator GameMain()
        {
            while (true) {
                yield return new WaitForSeconds(0.5f);
                if (target == null)
                    target = Instantiate(squares[Random.Range(0, squares.Length)], transform);
                target.Move(new Vector2(0, -1));
                if (isLost)
                    break;
                for (int j = gameArea.GetLength(1) - 1, jMin = 0; j >= jMin; j--)
                {
                    bool isFilled = true;
                    for (int i = 0, iMax = gameArea.GetLength(0); i < iMax; i++)
                    {
                        if (gameArea[i, j] == null)
                        {
                            isFilled = false;
                            break;
                        }
                    }
                    if (isFilled)
                        ClearOneRow(gameArea, j);
                }
            }
            GameOver();
        }

        private void ClearOneRow(Cell[,] arr, int index)
        {
            for (int i = 0, iMax = arr.GetLength(0); i < iMax; i++)
            {
                Destroy(arr[i, index].gameObject);
                for (int j = index, jMax = arr.GetLength(1); j < jMax; j++)
                {
                    arr[i, j] = j == jMax - 1 ? null : arr[i, j + 1];
                    if (arr[i, j] != null)
                        arr[i, j].transform.localPosition += new Vector3(0, -50, 0);
                }
            }
        }

        private void GameOver()
        {
            GameOverPanel.SetActive(true);
        }

        public void GetInput(Vector2 direction,bool isRotate)
        {
            if (target != null && isRotate)
                target.Rotate();
            if (target != null && direction != Vector2.zero)
                target.Move(direction);
        }
    }
}