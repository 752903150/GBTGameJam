using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCs;

namespace MyGameFrameWork
{
    public partial class GameMain : MonoBehaviour
    {
        public GameObject Enity1;
        public GameObject Player;

        public GameObject Spawn1;
        public GameObject Spawn2;
        public GameObject Spawn3;
        public GameObject Spawn4;
        public GameObject Spawn5;
        public GameObject Spawn6;

        public GameObject HPBarCanvas;
        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);

        }
        void Start()
        {
            Debug.Log("GameMainStart");

            StateInit();//状态初始化
            sceneStateC.SetData("Enity1", Enity1);
            sceneStateC.SetData("Player", Player);
            sceneStateC.SetData("Spawn1", Spawn1);
            sceneStateC.SetData("Spawn2", Spawn2);
            sceneStateC.SetData("Spawn3", Spawn3);
            sceneStateC.SetData("Spawn4", Spawn4);
            sceneStateC.SetData("Spawn5", Spawn5);
            sceneStateC.SetData("Spawn6", Spawn6);
            sceneStateC.SetData("HPBarCanvas", HPBarCanvas);
        }

        // Update is called once per frame
        void Update()
        {
            sceneStateC.Update();
        }
    }
}
