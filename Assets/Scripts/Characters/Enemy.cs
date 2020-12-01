using DemoShooter.Managers;

namespace DemoShooter.Characters
{
    public class Enemy : Character
    {
        private void Start()
        {
            EnemyManager.instance.AddEnemy(this);
        }
        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
        private void OnDestroy()
        {
            EnemyManager.instance.RemoveEnemy(this);
        }
    }
}
