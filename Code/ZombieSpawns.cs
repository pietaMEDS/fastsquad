using Sandbox;

public sealed class ZombieSpawns : Component
{
	[Property]
	GameObject ZombiePrefab { get; set; }

	public float lastSpawnTime = Time.Now;

	protected override void OnUpdate()
	{
		if (Time.Now - lastSpawnTime > 5f)
		{
			var zombie = ZombiePrefab.Clone( WorldPosition );
			lastSpawnTime = Time.Now;
		}	
	}
}
