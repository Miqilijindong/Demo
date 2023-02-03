using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Spiderman
{
    public class UIManager : MonoBehaviour
    {
        public TMP_Text SpeedText;
        public Rigidbody rigidbody;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //SpeedText.text = "Speed:" + rigidbody.velocity.magnitude.ToString("f0");
        }
    }
}
