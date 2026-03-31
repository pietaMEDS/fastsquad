using Sandbox;

public sealed class ZombieAi : Component
{
    private float health = 20f;
    private NavMeshAgent agent;
    private GameObject targetEndPoint;
    
    protected override void OnStart()
    {
        agent = GameObject.GetComponent<NavMeshAgent>();
        targetEndPoint = Scene.Directory.FindByName("ZombieEndPath", false).FirstOrDefault();
    }
    
    protected override void OnUpdate()
    {
        if (agent == null || targetEndPoint == null)
            return;
        
        agent.MoveTo(targetEndPoint.WorldPosition);
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        Log.Info($"{GameObject.Name} took {damage} damage! HP: {health}/20");
        
        if (health <= 0)
        {
            Log.Info($"{GameObject.Name} died!");
            GameObject.Destroy();
        }
    }
}


// [
//   {
//     "__guid": "547f6be0-9bfb-4a08-b41a-00f2b80eab6a",
//     "__version": 2,
//     "Flags": 0,
//     "Name": "ZombieEndPath",
//     "Position": "-2678.995,-165.4318,45.27068",
//     "Rotation": "0,0,0,1",
//     "Scale": "1,1,1",
//     "Tags": "",
//     "Enabled": true,
//     "NetworkMode": 2,
//     "NetworkFlags": 0,
//     "NetworkOrphaned": 0,
//     "NetworkTransmit": true,
//     "OwnerTransfer": 1,
//     "Components": [],
//     "Children": []
//   }
// ]