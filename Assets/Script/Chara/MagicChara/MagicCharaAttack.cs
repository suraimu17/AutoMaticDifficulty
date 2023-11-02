using UnityEngine;
using Enemy;
using UniRx;
using UniRx.Triggers;

public class MagicCharaAttack : MonoBehaviour,ICharaAttack
{
    [SerializeField] private GameObject shot;
    [field:SerializeField]private float shotSpeed = 1.5f;
    public void Attack(GameObject enemy,float attackPower) 
    {
        if (enemy == null) return;
        var shotIns=Instantiate(shot, this.transform.position, Quaternion.identity);

        //Õ“Ëˆ—
        shotIns.OnTriggerEnter2DAsObservable()
            .Where(collsion => collsion.gameObject.tag == "Enemy")
            .Subscribe(_ =>
            {
                var enemyHp = enemy.GetComponent<IEnemyHp>();
                enemyHp.DecreaseHp(attackPower);
                Destroy(shotIns);
            })
            .AddTo(this);

        //’e‚Ìˆ—
        shotIns.UpdateAsObservable()
            .Subscribe(_ =>
            {
                if (enemy == null) { 
                    Destroy(shotIns);
                    return;
                    };
                shotIns.transform.position = Vector2.MoveTowards(
                    shotIns.transform.position,
                    new Vector2(enemy.transform.position.x, enemy.transform.position.y),
                    shotSpeed * Time.deltaTime
                    );
            })
            .AddTo(this);
    }


}
