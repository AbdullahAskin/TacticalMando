public class HealerEnemyProperty : EnemyProperty
{
    new void Start()
    {
        SetHp(50);
        base.Start();
        CommonData.AddEnemy(transform);

    }


}
