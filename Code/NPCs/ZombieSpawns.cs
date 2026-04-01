using Sandbox;

public sealed class ZombieSpawns : Component
{
	[Property]
	GameObject ZombiePrefab { get; set; }

	[Property]
	private float BaseCooldown = 5f;

	public float lastSpawnTime = Time.Now;

	protected override void OnUpdate()
	{
		if ( Time.Now - lastSpawnTime > BaseCooldown )
		{
			// Размер куба (половина, потому что центр)
			Vector3 halfSize = WorldScale * 20f;

			Log.Info( $"Spawning zombie at {WorldPosition} with half size {halfSize}" );

			// Случайная позиция внутри куба
			Vector3 randomOffset = new Vector3(
				Game.Random.Float( -halfSize.x, halfSize.x ),
				Game.Random.Float( -halfSize.y, halfSize.y ),
				Game.Random.Float( -halfSize.z, halfSize.z )
			);

			Vector3 spawnPosition = WorldPosition + randomOffset;

			var zombie = ZombiePrefab.Clone( spawnPosition );

			lastSpawnTime = Time.Now;
		}
	}
}