public class AvatarEnemyProperty : EnemyProperty
{
    private new void Start()
    {
        SetHp(100);
        base.Start();
        CommonData.AddEnemy(transform);
    }



}
