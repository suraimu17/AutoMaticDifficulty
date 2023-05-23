using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Base;

public class TestEnemyController : MonoBehaviour, IEnemyController
{
    protected TestEnemyMover enemyMover;
    private IEnemyHp testEnemyHp;
    private BaseHP baseHP;

    public Transform target;
    private void Start()
    {
        enemyMover = GetComponent<TestEnemyMover>();
        testEnemyHp = GetComponent<IEnemyHp>();

        //�_���[�W���菈�� ��������HP�o�[�ɂ��Hp�c�ʏ���
        this.ObserveEveryValueChanged(_ => testEnemyHp.enemyHp)
            .Subscribe(_ =>
            {

            })
            .AddTo(this);
    }

    private void Update()
    {
        enemyMover.EnemyMove(target);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Base") 
        {
            baseHP = collision.GetComponent<BaseHP>();
            if (baseHP == null) return;

            baseHP.DecreaseHp();
            Destroy(gameObject);
        }
    }

}
