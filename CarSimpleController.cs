using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;


[RequireComponent(typeof(CarController))]
public class CarSimpleController : MonoBehaviour
{
    public CarController m_Car; // the car controller we want to use
    public int player;
    public GameObject ball;
    public float speed = 1f;

    private void Awake()
    {
        // get the car controller
        m_Car = GetComponent<CarController>();
    }


    private void PointTowardsBall()
    {
        Vector3 desired = this.transform.position - (this.ball.transform.position - new Vector3(0f, this.ball.transform.localScale.y / 2,0));
        Quaternion rotation = Quaternion.LookRotation(desired, Vector3.up);
        this.transform.rotation = rotation;
        transform.rotation *= Quaternion.Euler(0, 180f, 0);

    }


    private void FixedUpdate()
    {
        PointTowardsBall();
        m_Car.Move(0, speed, 0, 0);
    }
}
