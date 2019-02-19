using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo2
{
    public class Cell : MonoBehaviour
    {
        public Sprite img;
        public int x;
        public int y;
        public Vector2[] pos;
        [HideInInspector]
        public Square parent;
        public Vector2 SetPosition(int index,bool isMove)
        {
            int tempx = (int)pos[index].x;
            int tempy = (int)pos[index].y;
            if (isMove)
            {
                x = tempx;
                y = tempy;
                transform.localPosition = new Vector3(x * 50, y * 50, 0);
            }
            return parent.pos + new Vector2(tempx,tempy);
        }

        public Vector2 MoveToTarget(Vector2 dir)
        {
            return dir + parent.pos + new Vector2(x, y);
        }

        public void SetSprite(Sprite img)
        {
            UISprite mSprite = transform.GetComponent<UISprite>();
            if (mSprite != null)
                mSprite.spriteName = img.name;
        }
    }
}