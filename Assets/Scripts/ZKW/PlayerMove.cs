using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Transform Player;

    float speed;
    Vector3 temp;
    Vector2 temp2;
    float distance;

    Vector2[] directs;
    int layermask;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Transform>();
        speed = 8f;
        distance = 1f;
        directs = new Vector2[4]
        {
            new Vector2(0,1),
            new Vector2(0,-1),
            new Vector2(1,0),
            new Vector2(-1,0),
        };
        layermask = (1 << 7) | (1 << 10);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        temp = Player.localPosition;
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
        Player.localPosition = temp;
    }
}
