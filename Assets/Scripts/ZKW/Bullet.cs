using MyGameFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCs;

public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 direct;
    Transform ts;
    Vector3 temp;
    Vector2[] directs;

    float distance;

    float curr_time;
    int layermask;
    bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        curr_time = 0f;

        speed = 20f;
        ts = GetComponent<Transform>();
        directs = new Vector2[4]
        {
            new Vector2(0,1),
            new Vector2(0,-1),
            new Vector2(1,0),
            new Vector2(-1,0),
        };
        distance = 0.05f;
        layermask = (1 << 7) | (1 << 8) | (1 << 10);
    }

    // Update is called once per frame
    void Update()
    {
        curr_time += Time.deltaTime;
        if (curr_time >= 3f)
        {
            Back();
        }
        RaycastHit2D hit;
        ts.localPosition += direct * speed * Time.deltaTime;
        for(int i = 0; i < 4; i++)
        {
            hit = Physics2D.Raycast(this.transform.localPosition, directs[i], distance, layermask);
            if (hit.collider)
            {
                if(hit.collider.tag == "Enemy" && !isDead)
                {
                    isDead = true;
                    hit.collider.gameObject.GetComponent<EnemyMove>().Injure();
                }
                Back();
            }
            break;
        }
    }

    public void SetDirect(Vector3 Direct)
    {
        direct = Direct;
        direct.z = 0;
        direct = Vector3.Normalize(direct);
        curr_time = 0f;
    }

    void Back()
    {
        ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_FireBullet].ID, this.gameObject);
        isDead = false;
    }
}
