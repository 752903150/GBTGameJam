using DataCs;
using MyGameFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Security.Cryptography;
using static UnityEditor.PlayerSettings;

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
    float attack_distance;
    float attack_tower_distance;
    float attack_big_tower_distance;
    float follow_distance;

    int layermask;
    private Camera cam;
    private Scrollbar HpBar;

    public GameObject Canva;

    private int MaxHp;
    private int CurrHp;

    bool isDead;
    bool isAttack;
    bool isGameOver;

    Vector2 rv2;

    int attack_mode;

    Vector3 follow_location;
    
    void Start()
    {
        float rx = Random.Range(0, 1);
        float ry = Random.Range(0, 1);
        rv2.x = rx;
        rv2.y = ry;
        isDead = false;
        isAttack = false;
        attack_mode = 1;//mode 1 = small 2 = big 3 = player;
        GetComponent<CircleCollider2D>().enabled = true;

        MaxHp = 10;
        CurrHp = MaxHp;
       
        cam = Camera.main;

        distance = 0.5f;
        attack_distance = 1f;
        follow_distance = 3f;
        attack_tower_distance = 4.5f;
        attack_big_tower_distance = 5f;
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
        //CreateHPBar();
    }

    // Update is called once per frame
    void Update()
    {
        Select();

        if (attack_mode == 1)
        {
            follow_location = Tower1.transform.localPosition;
        }
        else if(attack_mode == 2)
        {
            follow_location = BigTower2.transform.localPosition;
        }
        else
        {
            follow_location = Player.localPosition;
        }
        Attack(follow_location);
        Move(follow_location);
        HpBarMove();
        HpBar.value = 1f;
        
    }

    public void init(Tower stower,Tower btower,Transform player,GameObject canva)
    {
        Tower1 = stower;
        BigTower2 = btower;
        Player = player;
        Canva = canva;
        isDead = false;
        isAttack = false;
        isGameOver = false;
        GetComponent<CircleCollider2D>().enabled = true;
        CreateHPBar();
        HpBar.size = 1f;
        MaxHp = 10;
        CurrHp = MaxHp;

        EventManagerSystem.Instance.Add2(Data_EventName.GameOver_str, GameOver);
    }

    void Select()
    {
        if (attack_mode == 3)
        {
            return;
        }
        Vector3 direct = Player.localPosition - this.transform.position;
        if (follow_distance * follow_distance > direct.z * direct.z + direct.y * direct.y + direct.x * direct.x)
        {
            attack_mode = 3;
        }
        if (attack_mode == 1)//当前攻击塔
        {
            if (Tower1.isDead)//小塔死了
            {
                attack_mode = 2;//攻击主塔
            }
        }
    }

    void Attack(Vector3 pos)
    {
        if (isDead || isAttack)
        {
            return;
        }
        Vector3 direct = pos - this.transform.position;
        Debug.Log(direct.z * direct.z + direct.y * direct.y + direct.x * direct.x);
        if(attack_big_tower_distance * attack_big_tower_distance > direct.z * direct.z + direct.y * direct.y + direct.x * direct.x)
        {
            if (attack_mode == 2)
            {
                AttackTower(BigTower2);
            }
        }
        if(attack_tower_distance * attack_tower_distance > direct.z * direct.z + direct.y * direct.y + direct.x * direct.x)
        {
            if (attack_mode == 1)
            {
                AttackTower(Tower1);
            }
        }
        else if (attack_distance * attack_distance > direct.z* direct.z+ direct.y* direct.y+ direct.x* direct.x)
        {
            if (attack_mode == 3)
            {
                Attack(Player.gameObject);
            }
        }
    }

    void Move(Vector3 pos)
    {
        if (isDead || isAttack)
        {
            return;
        }
        Vector3 direct = pos - this.transform.position ;
        direct.z = 0;
        direct.x += rv2.x;
        direct.y += rv2.y;
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
        HpBar.gameObject.GetComponent<RectTransform>().position = new Vector3(playerScreenPos.x, playerScreenPos.y + 55f, playerScreenPos.z);

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
        HpBar.value = 1f;
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
        EventManagerSystem.Instance.Delete2(Data_EventName.GameOver_str, GameOver);
        isDead = true;
        GetComponent<CircleCollider2D>().enabled = false;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            if (!isGameOver)
            {
                ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_HPBar].ID, HpBar.gameObject);
                ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_EnemyA].ID, this.gameObject);
                EventManagerSystem.Instance.Invoke2(Data_EventName.KillMonster_str, KillMonsterEventArgs.Create(0));
            }
        })
        .SetDelay(1f);
    }

    void Attack(GameObject Player)
    {
        float DPS = TOOLS.GetMonsterDps(1, Player.GetComponent<PlayerMove>().CurrPlayerHp);
        //Debug.Log(DPS);
        isAttack = true;
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            Player.GetComponent<PlayerMove>().Injure(DPS);// -= DPS;
            EventManagerSystem.Instance.Invoke2(Data_EventName.PlayerInjure_str, PlayerInjureEventArgs.Create(DPS));
        });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            isAttack = false;
        });

    }

    void AttackTower(Tower tower)
    {
        Debug.Log("AttackTower");
        isAttack = true;
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            tower.Injure(1);// -= DPS;
            //EventManagerSystem.Instance.Invoke2(Data_EventName.PlayerInjure_str, PlayerInjureEventArgs.Create(DPS));
        });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {
            isAttack = false;
        });
    }

    void GameOver(IEventArgs eventArgs)
    {
        isGameOver = true;
        //GameOverEventArgs gameOverEventArgs = (GameOverEventArgs)eventArgs;
        ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_HPBar].ID, HpBar.gameObject);
        ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_EnemyA].ID, this.gameObject);
    }
}
