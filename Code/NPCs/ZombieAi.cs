using Sandbox;

public sealed class ZombieAi : Component
{
    [Property] private float damage = 5f;
    [Property] private float attackRange = 35f;
    [Property] private float attackCooldown = 1f;
    private NavMeshAgent agent;
    private GameObject targetEndPoint;

    private float attackTimer = Time.Now;

    protected override void OnStart()
    {
        agent = GameObject.GetComponent<NavMeshAgent>();
        targetEndPoint = Scene.Directory.FindByName("ZombieEndPath", false).FirstOrDefault();
    }
    
    public void Attack(GameObject target)
    {
        
        if (attackTimer > Time.Now) return; // Still in cooldown
        
        var healthComp = target.GetComponent<HealthComponent>();
        if (healthComp != null)
        {
            healthComp.TakeDamage(damage);
            attackTimer = Time.Now + attackCooldown; // Reset cooldown
        }
    }

    protected override void OnUpdate()
    {
        if (agent == null || targetEndPoint == null)
            return;
        
        agent.MoveTo(targetEndPoint.WorldPosition);

        if (GameObject.WorldPosition.Distance(targetEndPoint.WorldPosition) < attackRange)
        {
            Attack(targetEndPoint.Parent);
        }
    }
}