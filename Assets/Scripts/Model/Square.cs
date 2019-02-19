using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo2
{
    public class Square : MonoBehaviour
    {
        public int index = 0;
        [HideInInspector]
        public Cell[] cells;
        [HideInInspector]
        public Sprite[] imgs;
        //[HideInInspector]
        public Vector2 pos;
        [HideInInspector]
        public bool CanMove = true;
        bool CanRotate = true;
        [HideInInspector]
        public bool isDone = false;

        protected void Awake()
        {
            pos = new Vector2(5,16);
            transform.localPosition = new Vector3(25, 500, 0);
            imgs = Resources.LoadAll<Sprite>("Image");
            cells = GetCells(imgs[Random.Range(0, imgs.Length)]);
            index = Random.Range(0, 4);
            SetCellsPosition(cells, index,true);
        }

        public Cell[] GetCells(Sprite img)
        {
            Cell[] mCell = transform.GetComponentsInChildren<Cell>();
            for (int i = 0; i < mCell.Length; i++)
            {
                mCell[i].SetSprite(img);
                mCell[i].parent = this;
            }
            return mCell;
        }

        public void SetCellsPosition(Cell[] cells, int index,bool isMove)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Vector2 vec = cells[i].SetPosition(index, isMove);
                if (vec.y > 14)
                    continue;
                else if (vec.x > 9 || vec.x < 0 || vec.y < 0 || GameController.Instance.gameArea[(int)vec.x, (int)vec.y] != null)
                {
                    CanRotate = false;
                    break;
                }
            }
        }

        public void Rotate()
        {
            int temp = (index + 1) % 4;
            SetCellsPosition(cells, temp,false);
            if (CanRotate)
            {
                SetCellsPosition(cells, temp,true);
                index = temp;
            }
            else
                CanRotate = true;
        }

        public void Move(Vector2 dir)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Vector2 vec = cells[i].MoveToTarget(dir);
                if (vec.x > 9 || vec.x < 0)
                {
                    CanMove = false;
                    break;
                }
                else if (vec.y > 14)
                    continue;
                else if (dir.y != 0 && (vec.y < 0 || GameController.Instance.gameArea[(int)vec.x, (int)vec.y] != null))
                {
                    isDone = true;
                    break;
                }
                else if (GameController.Instance.gameArea[(int)vec.x, (int)vec.y] != null)
                {
                    CanMove = false;
                    break;
                }
            }
            if (isDone)
                ReleaseCells();
            if (CanMove)
            {
                transform.localPosition += new Vector3(dir.x * 50, dir.y * 50, 0);
                pos += dir;
            }
            else
                CanMove = true;
        }

        public void ReleaseCells()
        {
            for (int i = 0; i<cells.Length ;i++)
            {
                cells[i].transform.parent = transform.parent;
                cells[i].x += (int)pos.x;
                cells[i].y += (int)pos.y;
                if (cells[i].x < GameController.Instance.gameArea.GetLength(0) && cells[i].y < GameController.Instance.gameArea.GetLength(1))
                    GameController.Instance.gameArea[cells[i].x, cells[i].y] = cells[i];
                else
                    GameController.Instance.isLost = true;
            }
            GameController.Instance.target = null;
            Destroy(gameObject);
        }
    }
}