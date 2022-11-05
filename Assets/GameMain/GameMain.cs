using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCs;

namespace MyGameFrameWork
{
    public partial class GameMain : MonoBehaviour
    {
        public GameObject Enity1;
        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);

        }
        void Start()
        {
            Debug.Log("GameMainStart");

            StateInit();//状态初始化
            sceneStateC.SetData("Enity1", Enity1);
        }

        // Update is called once per frame
        void Update()
        {
            sceneStateC.Update();
        }
    }
}
