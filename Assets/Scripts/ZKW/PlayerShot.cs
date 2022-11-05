using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCs;
using MyGameFrameWork;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public class PlayerShot : MonoBehaviour
{
    Sequence seq;
    public GameObject AllBullet;
    float curr_shot_time;
    public float shot_time;

    PlayerMove pm;

    bool isAttack;

    Vector3 temp3;

    Vector3 XAV3;//用于反转
    Vector3 XBV3;
    // Start is called before the first frame update
    void Start()
    {
        //shot_time = 1f;
        isAttack = false;
        curr_shot_time = 1f;
        pm = GetComponent<PlayerMove>();
        seq =  DOTween.Sequence();
        XAV3 = new Vector3(-0.5f, 0.5f);
        XBV3 = new Vector3(0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        curr_shot_time += Time.deltaTime;
        if (curr_shot_time > shot_time)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Shot1();
                curr_shot_time = 0f;
            }
            else if(isAttack)
            {
                pm.animator.SetBool("isAttack", false);
                isAttack = false;
            }
        }
        temp3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.localPosition;
        if (temp3.x > 0f)
            transform.localScale = XAV3;
        else
            transform.localScale = XBV3;
    }

    void Shot1()
    {
        if (!isAttack)
        {
            pm.animator.SetBool("isAttack", true);
        }
        isAttack = true;
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

        
        /*seq.AppendInterval(0.2f);
        seq.AppendCallback(() => {
            pm.animator.SetBool("isAttack", false);
        });*/
    }
}
