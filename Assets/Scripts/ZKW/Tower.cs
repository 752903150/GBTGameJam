using DataCs;
using MyGameFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    ETurrutType turrutType;
    public bool isDead;
    bool isGameOver;

    public float MaxHP;
    public float CurrHp;

    private Camera cam;
    private Scrollbar HpBar;

    public GameObject Canva;
    // Start is called before the first frame update
    void Start()
    {
        
        isDead = false;
        isGameOver = false;
    }

    public void init(ETurrutType eTurrutType,int level)
    {
        this.gameObject.SetActive(true);
        EventManagerSystem.Instance.Add2(Data_EventName.GameOver_str, GameOver2);
        EventManagerSystem.Instance.Add2(Data_EventName.GameOK_str, GameOver2);
        turrutType = eTurrutType;
        cam = Camera.main;
        isGameOver = false;
        isDead = false;
        TOOLS.GetTurrutHps(eTurrutType, (uint)level, out MaxHP, out CurrHp);//获取血量
        CreateHPBar();
        HpBarMove();

    }

    // Update is called once per frame
    void Update()
    {
        if(HpBar!=null)
            HpBarMove();
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
        HpBar.size = 1f;
    }

    void HpBarMove()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }
        var playerScreenPos = cam.WorldToScreenPoint(this.transform.position);
        //再把人物坐标Y加一个高度给到人物
        if(turrutType==ETurrutType.Normal)
            HpBar.gameObject.GetComponent<RectTransform>().position = new Vector3(playerScreenPos.x, playerScreenPos.y + 100f, playerScreenPos.z);
        else
            HpBar.gameObject.GetComponent<RectTransform>().position = new Vector3(playerScreenPos.x, playerScreenPos.y + 180f, playerScreenPos.z);
    }

    public void Injure(int monsterID)
    {
        float DPS = TOOLS.GetMonsterDps((uint)monsterID, CurrHp);

        CurrHp-= DPS;

        HpBar.size = CurrHp / (float)MaxHP;
        if (CurrHp <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        if (!isGameOver)
        {
            isDead = true;
            this.gameObject.SetActive(false);
            EventManagerSystem.Instance.Delete2(Data_EventName.GameOver_str, GameOver2);
            EventManagerSystem.Instance.Delete2(Data_EventName.GameOK_str, GameOver2);
            ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_HPBar].ID, HpBar.gameObject);
            if(turrutType == ETurrutType.Center)
            {
                EventManagerSystem.Instance.Invoke2(Data_EventName.GameOver_str, GameOverEventArgs.Create());
            }
        }
    }

    void GameOver2(IEventArgs eventArgs)
    {
        isGameOver = true;
        //GameOverEventArgs gameOverEventArgs = (GameOverEventArgs)eventArgs;
        Destroy(HpBar.gameObject);
        //ObjectPoolSystem.Instance.ReBackGameObjectPool(Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_HPBar].ID, HpBar.gameObject);
        EventManagerSystem.Instance.Delete2(Data_EventName.GameOver_str, GameOver2);
    }
}
