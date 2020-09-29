using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour {

	public Rigidbody ball;
	public Transform target;

	public float h = 25;
	public float gravity = -18;

	public bool debugPath;
    [SerializeField] Transform[] drawTrajectory;

    bool isLaunce;
    void Start() {
		ball.useGravity = false;
        Physics.gravity = Vector3.up * gravity;
    }

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Launch ();
		}

		if (debugPath) {
			DrawPath ();
		}
	}

	void Launch() {
		
		ball.useGravity = true;
		ball.velocity = CalculateLaunchData ().initialVelocity;
        isLaunce = true;

    }

	LaunchData CalculateLaunchData() {
		float displacementY = target.position.y - ball.position.y;
		Vector3 displacementXZ = new Vector3 (target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath() {
		LaunchData launchData = CalculateLaunchData ();
		Vector3 previousDrawPoint = ball.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++) {
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = ball.position + displacement;
           if (!isLaunce && i < resolution && drawTrajectory.Length > 0) drawTrajectory[i - 1].transform.position = drawPoint;
            Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
			previousDrawPoint = drawPoint;
		}
	}

	struct LaunchData {
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
		
	}
}
	
