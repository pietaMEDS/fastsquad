using Sandbox;
using System.Linq;

public sealed class Turret : Component
{
    [Property] public float RotationSpeed { get; set; } = 180f;
    [Property] public float DetectionRange { get; set; } = 1000f;
    [Property] public float FireRange { get; set; } = 800f;
    [Property] public float Damage { get; set; } = 5f;
    [Property] public float FireRate { get; set; } = 2f;
    [Property] public string EnemyTag { get; set; } = "enemy";
    [Property] public bool InstantRotation { get; set; } = false;
    
    private float nextFireTime;
    private GameObject currentTarget;
    
    protected override void OnUpdate()
    {
        FindClosestEnemy();
        
        if (currentTarget != null && currentTarget.IsValid())
        {
            RotateTowardsTarget();
            
            var distance = GameObject.WorldPosition.Distance(currentTarget.WorldPosition);
            if (distance <= FireRange && Time.Now >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.Now + (1f / FireRate);
            }
        }
    }
    
    private void FindClosestEnemy()
    {
        var enemies = Scene.GetAllComponents<ZombieAi>()
            .Where(z => z.GameObject.Tags.Has(EnemyTag))
            .ToList();
        
        if (!enemies.Any())
        {
            currentTarget = null;
            return;
        }
        
        var myPos = GameObject.WorldPosition;
        
        var closest = enemies
            .Where(z => z.GameObject.WorldPosition.Distance(myPos) <= DetectionRange)
            .OrderBy(z => z.GameObject.WorldPosition.Distance(myPos))
            .FirstOrDefault();
        
        currentTarget = closest?.GameObject;
    }
    
    private void RotateTowardsTarget()
    {
        if (currentTarget == null) return;
        
        var myPos = GameObject.WorldPosition;
        var targetPos = currentTarget.WorldPosition;
        var direction = (targetPos - myPos).Normal;
        
        if (InstantRotation)
        {
            GameObject.WorldRotation = Rotation.LookAt(direction);
        }
        else
        {
            var targetRotation = Rotation.LookAt(direction);
            var newRotation = Rotation.Slerp(
                GameObject.WorldRotation, 
                targetRotation, 
                RotationSpeed * Time.Delta / 180f
            );
            GameObject.WorldRotation = newRotation;
        }
    }
    
    private void Shoot()
    {
        if (currentTarget == null || !currentTarget.IsValid())
            return;
        
        var startPos = GameObject.WorldPosition;
        var direction = (currentTarget.WorldPosition - startPos).Normal;
        
        var tr = Scene.Trace.Ray(startPos, startPos + direction * FireRange)
            .IgnoreGameObject(GameObject)
            .Run();
        
        var enemy = tr.GameObject?.Components.Get<ZombieAi>();
        if (enemy != null)
        {
            var healthComp = enemy.GameObject.GetComponent<HealthComponent>();
            healthComp?.TakeDamage(Damage);
        }
    }
}