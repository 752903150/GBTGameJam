using MyGameFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public EPlayerHpState playerState;

    Transform Player;

    public float PlayerHp;
    public float CurrPlayerHp;

    float speed;
    Vector3 temp;
    Vector2 temp2;
    float distance;

    Vector2[] directs;
    int layermask;

    bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        PlayerHp = TOOLS.GetPlayerMaxHp();
        CurrPlayerHp = PlayerData.GetDefaultObject().InitialHp;
        playerState = TOOLS.GetPlayerHpState(CurrPlayerHp);
        Player = GetComponent<Transform>();
        speed = 5f;
        distance = 0.5f;
        directs = new Vector2[8]
        {
            new Vector2(0, 1),
            new Vector2(0,-1),
            new Vector2(1, 0),
            new Vector2(-1,0),

            new Vector2(1,1),
            new Vector2(-1,-1),
            new Vector2(1,-1),
            new Vector2(-1,1)
        };
        layermask = (1 << 7) | (1 << 10);
    }

    public void PlayerInit()
    {
        isDead = false;
        transform.localPosition = new Vector3(2.22f, 0, 0);
        PlayerHp = TOOLS.GetPlayerMaxHp();
        CurrPlayerHp = PlayerData.GetDefaultObject().InitialHp;
        playerState = TOOLS.GetPlayerHpState(CurrPlayerHp);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        temp = Vector3.zero;
        temp2.x = Player.localPosition.x;
        temp2.y = Player.localPosition.y;
        if (Input.GetKey(KeyCode.W))
        {
            Debug.DrawRay(Player.localPosition, directs[0],Color.blue, distance);
            hit = Physics2D.Raycast(temp2 , directs[0], distance, layermask);
            if (!hit.collider)
            {
                
                temp.y += Time.deltaTime * speed;
            }
            else
            {
                //Debug.Log("CrushW");
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.DrawRay(Player.localPosition, directs[3], Color.blue, distance);
            hit = Physics2D.Raycast(temp2 , directs[3], distance, layermask);
            if (!hit.collider)
            {
                temp.x -= Time.deltaTime * speed;
               
            }
            else
            {
                //Debug.Log("CrushA");
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.DrawRay(Player.localPosition, directs[1], Color.blue, distance);
            hit = Physics2D.Raycast(temp2 , directs[1], distance, layermask);
            if (!hit.collider)
            {
                temp.y -= Time.deltaTime * speed;
               
            }
            else
            {
                //Debug.Log("CrushS");
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.DrawRay(Player.localPosition, directs[2], Color.blue, distance);
            hit = Physics2D.Raycast(temp2, directs[2], distance, layermask);
            if (!hit.collider)
            {
                temp.x += Time.deltaTime * speed;
            }
            else
            {
                //Debug.Log("CrushD");
            }
        }

        for (int i = 0; i < 8; i++)
        {
            hit = Physics2D.Raycast(this.transform.localPosition, directs[i], distance, layermask);
            if (hit.collider)
            {
                if (directs[i].x * temp.x > 0f)
                {
                    //temp.y += temp.x;
                    temp.x = 0;
                }
                else if (directs[i].y * temp.y > 0f)
                {
                    //temp.x += temp.y;
                    temp.y = 0;
                }
                break;
            }
        }
        Player.localPosition += temp;
    }

    public void Injure(float DPS)
    {
        CurrPlayerHp -= DPS;
        playerState = TOOLS.GetPlayerHpState(CurrPlayerHp);
        //Debug.Log(CurrPlayerHp);
        if (CurrPlayerHp <= 0 && !isDead)
        {
            CurrPlayerHp = 0;
            playerState = TOOLS.GetPlayerHpState(CurrPlayerHp);
            isDead = true;
            Debug.Log("Dead");
            EventManagerSystem.Instance.Invoke2(DataCs.Data_EventName.GameOver_str, GameOverEventArgs.Create());
        }
    }
}
