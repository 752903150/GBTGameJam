using DataCs;
using MyGameFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    Transform ts;
    public Tower Tower1;
    public Tower BigTower2;
    public Transform Player;
    Vector2[] directs;

    float distance;

    int layermask;
    private Camera cam;
    private Scrollbar HpBar;

    public GameObject Canva;

    private int MaxHp;
    private int CurrHp;

    bool isDead;

    void Start()
    {
        isDead = false;
        GetComponent<CircleCollider2D>().enabled = true;

        MaxHp = 10;
        CurrHp = MaxHp;
       
        cam = Camera.main;

        distance = 0.5f;
        ts = GetComponent<Transform>();
        directs = new Vector2[8]
        {
            new Vector2(0,1),
            new Vector2(0,-1),
            new Vector2(1,0),
            new Vector2(-1,0),

            new Vector2(1,1),
            new Vector2(-1,-1),
            new Vector2(1,-1),
            new Vector2(-1,1)
        };
        layermask = (1 << 7) | (1 << 10);
        CreateHPBar();
    }

    // Update is called once per frame
    void Update()
    {
        Move(Player.localPosition);
        HpBarMove();
    }

    public void init(Tower stower,Tower btower,Transform player,GameObject canva)
    {
        Tower1 = stower;
        BigTower2 = btower;
        Player = player;
        Canva = canva;
        isDead = false;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    void Move(Vector3 pos)
    {
        if (isDead)
        {
            return;
        }
        Vector3 direct = pos - this.transform.position ;
        direct.z = 0;
        direct = Vector3.Normalize(direct);

        RaycastHit2D hit;
        for (int i = 0; i < 8; i++)
        {
            hit = Physics2D.Raycast(this.transform.localPosition, directs[i], distance, layermask);
            if (hit.collider)
            {
                if (directs[i].x * direct.x> 0f)
                {
                    direct.y += direct.x;
                    direct.x = 0;
                }
                else if(directs[i].y * direct.y > 0f)
                {
                    direct.x += direct.y;
                    direct.y = 0;
                }
                break;
            }
        }
        direct = Vector3.Normalize(direct);
        this.transform.localPosition += direct * speed * Time.deltaTime;
    }

    void HpBarMove()
    {
        var playerScreenPos = cam.WorldToScreenPoint(this.transform.position);
        //再把人物坐标Y加一个高度给到人物
        HpBar.gameObject.GetComponent<RectTransform>().position = new Vector3(playerScreenPos.x, playerScreenPos.y + 70f, playerScreenPos.z);

    }

    void CreateHPBar()
    {
        //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int id = Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_HPBar].ID;
        GameObject temp;
        if (ObjectPoolSystem.Instance.TestGameObjectPool(id))
        {
            temp = ObjectPoolSystem.Instance.GetGameObjectFormPool(id);
        }
        else
        {
            string path = Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_HPBar].path;
            temp = GameObject.Instantiate((GameObject)Resources.Load(path));
        }
        temp.SetActive(true);
        temp.transform.SetParent(Canva.transform);
        HpBar = temp.GetComponent<Scrollbar>();
    }

    public void Injure()
    {
        CurrHp--;
        HpBar.size = CurrHp / (float)MaxHp;
        if (CurrHp == 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        isDead = true;
        GetComponent<CircleCollider2D>().enabled = false;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_HPBar].ID, HpBar.gameObject);
            ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_EnemyA].ID, this.gameObject);
        })
        .SetDelay(1f);
    }
}
