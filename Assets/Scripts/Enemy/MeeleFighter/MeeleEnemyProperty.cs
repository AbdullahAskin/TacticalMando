public class MeeleEnemyProperty : EnemyProperty
{
    private new void Start()
    {
        SetHp(200);
        base.Start();
        CommonData.AddEnemy(transform);
    }


}
