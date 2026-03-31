using Sandbox;

public sealed class HealthComponent : Component
{
	[Property] private float health = 20f;
    [Property] private float maxHealth = 20f;

	public void TakeDamage(float damage)
	{
		health -= damage;
		Log.Info($"{GameObject.Name} took {damage} damage! HP: {health}/{maxHealth}");
		
		if (health <= 0)
		{
			Log.Info($"{GameObject.Name} died!");
			// GameObject.Destroy();
		}
	}

	public void TakeHeal(float healAmount)
	{
		health += healAmount;
		if (health > maxHealth) health = maxHealth; // Cap health at max
		Log.Info($"{GameObject.Name} healed {healAmount} HP! HP: {health}/{maxHealth}");
	}

	protected override void OnUpdate()
	{
		// Optional: Add regeneration or other health-related logic here
	}
}
