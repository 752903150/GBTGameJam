using DataCs;
using MyGameFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawn : MonoBehaviour
{
    public Tower smallTower;
    public Tower BigTower;
    public Transform Player;
    public GameObject Canvas;
    public GameObject RootParent;

    bool isDead;

    Sequence seq;// = DOTween.Sequence();

    float curr_time;
    float spawn_time;
    Queue<int> queueMonsters;

    private void Start()
    {
        spawn_time = 1f;
        curr_time = 1f;
        queueMonsters = new Queue<int>();
        EventManagerSystem.Instance.Add2(Data_EventName.GameOver_str, GameOver);
    }

    public void SpawnPlanA()
    {
        isDead = false;
        queueMonsters.Enqueue(0);
        queueMonsters.Enqueue(0);
        queueMonsters.Enqueue(0);
        queueMonsters.Enqueue(0);
        queueMonsters.Enqueue(0);
        /*seq = DOTween.Sequence();
        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            if(!isDead)
                CreateEnemyA();
        });

        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            if (!isDead)
                CreateEnemyA();
        });

        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            if (!isDead)
                CreateEnemyA();
        });

        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            if (!isDead)
                CreateEnemyA();
        });

        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            if (!isDead)
                CreateEnemyA();
        });*/
    }
    public void CreateEnemyA()
    {
        int id = Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_EnemyA].ID;
        GameObject temp;
        if (ObjectPoolSystem.Instance.TestGameObjectPool(id))
        {
            temp = ObjectPoolSystem.Instance.GetGameObjectFormPool(id);
        }
        else
        {
            string path = Data_GameObjectID.Dic[DataCs.Data_GameObjectID.key_EnemyA].path;
            temp = GameObject.Instantiate((GameObject)Resources.Load(path));
        }
        temp.SetActive(true);
        temp.transform.SetParent(RootParent.transform);
        temp.transform.localPosition = this.gameObject.transform.localPosition;
        temp.GetComponent<EnemyMove>().init(smallTower,BigTower,Player, Canvas);
    }

    private void Update()
    {
        curr_time += Time.deltaTime;
        if (curr_time >= spawn_time)
        {
            curr_time = 0f;
            if (queueMonsters.Count != 0)
            {
                int id = queueMonsters.Dequeue();
                if (id == 0)
                {
                    CreateEnemyA();
                }
            }
        }
    }

    void GameOver(IEventArgs eventArgs)
    {

        //GameOverEventArgs gameOverEventArgs = (GameOverEventArgs)eventArgs;
        //DOTween.Kill(seq.id);
        isDead = true;
    }
}
