using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCs;
using MyGameFrameWork;

public class PlayerShot : MonoBehaviour
{
    public GameObject AllBullet;
    float curr_shot_time;
    public float shot_time;

    PlayerMove pm;
    // Start is called before the first frame update
    void Start()
    {
        //shot_time = 1f;
        curr_shot_time = 1f;
        pm = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        curr_shot_time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (curr_shot_time > shot_time)
            {
                Shot1();
                curr_shot_time = 0f;
            }
        }
    }

    void Shot1()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int id = Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_FireBullet].ID;
        GameObject temp;
        if (ObjectPoolSystem.Instance.TestGameObjectPool(id))
        {
            temp = ObjectPoolSystem.Instance.GetGameObjectFormPool(id);
        }
        else
        {
            string path = Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_FireBullet].path;
            temp = GameObject.Instantiate((GameObject)Resources.Load(path));
        }
        temp.SetActive(true);
        temp.transform.SetParent(AllBullet.transform);
        temp.transform.localPosition = this.transform.localPosition;
        temp.GetComponent<Bullet>().SetDirect(pos - this.transform.localPosition, pm);
    }
}
