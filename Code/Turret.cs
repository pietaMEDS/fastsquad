using Sandbox;
using System.Linq;

public sealed class Turret : Component
{
    [Property] public float RotationSpeed { get; set; } = 180f;
    [Property] public float DetectionRange { get; set; } = 1000f;
    [Property] public string EnemyTag { get; set; } = "enemy";
    [Property] public bool InstantRotation { get; set; } = false;
    
    protected override void OnUpdate()
    {
        // Ищем все объекты с тегом "enemy"
        var enemies = Scene.GetAllComponents<ZombieAi>()
            .Where(z => z.GameObject.Tags.Has(EnemyTag))
            .ToList();
        
        if (!enemies.Any())
        {
            Log.Info("No enemies found");
            return;
        }
        
        var myPos = GameObject.WorldPosition;
        
        // Находим ближайшего врага в радиусе действия
        var closest = enemies
            .Where(z => z.GameObject.WorldPosition.Distance(myPos) <= DetectionRange)
            .OrderBy(z => z.GameObject.WorldPosition.Distance(myPos))
            .FirstOrDefault();
        
        if (closest == null)
        {
            Log.Info("No enemies in detection range");
            return;
        }
        
        var targetPos = closest.GameObject.WorldPosition;
        var distance = targetPos.Distance(myPos);
        var direction = (targetPos - myPos).Normal;
        
        Log.Info($"Target found: {closest.GameObject.Name}, Distance: {distance:F2}");
        
        if (InstantRotation)
        {
            // Мгновенный поворот
            GameObject.WorldRotation = Rotation.LookAt(direction);
            Log.Info($"Instant rotation to target");
        }
        else
        {
            // Плавный поворот с ограничением скорости
            var targetRotation = Rotation.LookAt(direction);
            var newRotation = Rotation.Slerp(
                GameObject.WorldRotation, 
                targetRotation, 
                RotationSpeed * Time.Delta / 180f
            );
            GameObject.WorldRotation = newRotation;
        }
    }
}