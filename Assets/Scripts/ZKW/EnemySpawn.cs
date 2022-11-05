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

    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            CreateEnemyA();
        });

        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            CreateEnemyA();
        });

        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            CreateEnemyA();
        });

        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            CreateEnemyA();
        });

        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            CreateEnemyA();
        });

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
        //temp.transform.SetParent(this.gameObject.transform);
        temp.transform.localPosition = this.gameObject.transform.localPosition;
        temp.GetComponent<EnemyMove>().init(smallTower,BigTower,Player, Canvas);
    }
}
