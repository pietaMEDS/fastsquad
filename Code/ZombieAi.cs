using Sandbox;

public sealed class ZombieAi : Component
{
	protected override void OnUpdate()
	{



		NavMeshAgent agent = GameObject.GetComponent<NavMeshAgent>();

		var ZobmieEnd = Scene.Directory.FindByName("ZombieEndPath", false).FirstOrDefault();

		agent.MoveTo(ZobmieEnd.WorldPosition);


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