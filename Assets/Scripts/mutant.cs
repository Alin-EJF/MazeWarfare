using UnityEngine;

public class mutant : MonoBehaviour
{
	public Transform[] waypoints;
	private int waypointIndex;
	public int speed;
	private float dist;


	void Start()
	{
		waypointIndex = 0;
		transform.LookAt(waypoints[waypointIndex].position);
	}

	void Update()
	{
		dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
		if (dist <= 10f)
		{
			print("swap point");
			IncreaseIndex();
		}
		Patrool();
	}

	void Patrool()
	{
		//transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void IncreaseIndex()
	{
		waypointIndex = (waypointIndex + 1) % waypoints.Length;
		//waypointIndex++;
		//if (waypointIndex >= waypoints.Length) {
		//	waypointIndex = 0;
		//}
		transform.LookAt(waypoints[waypointIndex].position);
	}

}
