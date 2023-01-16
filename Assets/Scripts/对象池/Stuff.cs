using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.对象池
{
    public class Stuff : MonoBehaviour
    {
        public void Init(Vector3 pos) 
        {
            transform.position = pos;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 如果出界
        /// </summary>
        /// <returns></returns>
        public bool IsOutOfBoundary()
        {
            if (transform.position.x > Manager._instance.MaxX 
                || transform.position.x < -Manager._instance.MaxX
                || transform.position.y > Manager._instance.MaxY
                || transform.position.y < -Manager._instance.MaxY)
            {
                return true;
            }
            return false;
        }
    }
}
