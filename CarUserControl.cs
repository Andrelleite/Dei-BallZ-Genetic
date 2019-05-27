using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;


[RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public int player;

        // jncor
        [Header("Sensors")]
        public Vector3 frontSensorPosition = new Vector3(0, 1.0f, 2.0f);
        public float sideSensorPosition = .5f;
        public float sensorLength = 10f;
        public float angle = 30f;
        public float[] frontSensorValues;
        public int tookHit = 0;
        public GameObject[] checkpoints;
        public int numberOfCheckpoints;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            frontSensorValues = new float[3];
        }

        // jncor
        public void SensorHandling()
        {
            RaycastHit hit;

            Vector3 sensorStartPos = transform.position;
            sensorStartPos += transform.forward * frontSensorPosition.z;
            sensorStartPos += transform.up * frontSensorPosition.y;

            // frontal
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point, Color.red);
                //Debug.Log ("[0] Front "+ sensorStartPos + " " + hit.point + " dist: " + (sensorStartPos - hit.point).magnitude);
                frontSensorValues[0] = (sensorStartPos - hit.point).magnitude;
            }
            else
            {
                frontSensorValues[0] = 0;
            }


            // direita 
            sensorStartPos += transform.right * sideSensorPosition;
            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(angle, transform.up) * transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point, Color.red);
                //Debug.Log ("[1] Left "+ sensorStartPos + " " + hit.point + " dist: " + (sensorStartPos - hit.point).magnitude);
                frontSensorValues[1] = (sensorStartPos - hit.point).magnitude;
            }
            else
            {
                frontSensorValues[1] = 0;
            }

            // esquerda
            sensorStartPos -= transform.right * 2 * sideSensorPosition;
            if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-angle, transform.up) * transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(sensorStartPos, hit.point, Color.red);
                //Debug.Log ("[2] Right "+ sensorStartPos + " " + hit.point + " dist: " + (sensorStartPos - hit.point).magnitude);
                frontSensorValues[2] = (sensorStartPos - hit.point).magnitude;
            }
            else
            {
                frontSensorValues[2] = 0;
            }

            int layer_mask = 1 << 2;
            float distanceToNextCheckpoint;
            //RaycastHit hit;
            float rayAngle = 30f;
            for (int i = 0; i * angle <= 360f; i++)
            {
                if (Physics.Raycast(m_Car.transform.position, Quaternion.AngleAxis(-rayAngle * i, transform.up) * transform.forward, out hit, Mathf.Infinity, layer_mask))
                {
                    Debug.DrawRay(m_Car.transform.position, Quaternion.AngleAxis((-rayAngle * i), transform.up) * transform.forward * hit.distance, Color.red);
                    distanceToNextCheckpoint = (m_Car.transform.position - hit.point).magnitude;
                    break;
                }
            }
        }


        private void FixedUpdate()
        {

            // pass the input to the car!
            float h = (player == 0 ? CrossPlatformInputManager.GetAxis("Horizontal") : CrossPlatformInputManager.GetAxis("Horizontal1"));
            float v = (player == 0 ? CrossPlatformInputManager.GetAxis("Vertical") : CrossPlatformInputManager.GetAxis("Vertical1"));

            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
            // jncor updating
            SensorHandling();
        }
    }
