using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.ScreenCursorRaycasting
{
    public class ScreenCursorRaycasting
    {
        public static RaycastHit CursorDirection()
        {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100);
            return hit;
        }
    }
}

