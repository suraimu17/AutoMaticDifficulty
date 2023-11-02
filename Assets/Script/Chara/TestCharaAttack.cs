using UnityEngine;
using Enemy;
using UniRx.Triggers;
using UniRx;

public class TestCharaAttack : MonoBehaviour,ICharaAttack
{
    [SerializeField] private GameObject slash;
    public void Attack(GameObject enemy,float attackPower) 
    {
        if (enemy == null) return;
        var slashIns = Instantiate(slash, enemy.transform.position, Quaternion.identity);

        //Õ“Ëˆ—
        slashIns.OnTriggerEnter2DAsObservable()
            .Where(collsion => collsion.gameObject.tag == "Enemy")
            .Subscribe(_ =>
            {
                var enemyHp = enemy.GetComponent<IEnemyHp>();
                enemyHp.DecreaseHp(attackPower);
            })
            .AddTo(this);
        Destroy(slashIns, 1.0f);
    }
}
