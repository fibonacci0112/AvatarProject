using UnityEngine;
using System.Collections;

public class RayTeleport : MonoBehaviour
{

    public Transform gunEnd;
    //public Rigidbody rich; // GameObject
    public bool laser = false;

    private LineRenderer laserLine;										// Reference to the LineRenderer component which will display our laserline
    public RaycastHit ray;
    public CharacterController player;

    /*The Instance of the Wii Controller*/
    private WiiController wii;

    /*Indicates if the Wii is used*/
    private bool wiiInUse;

    void Start()
    {
        // Get and store a reference to our LineRenderer component
        laserLine = GetComponent<LineRenderer>();

        //rich = GetComponent<Rigidbody>();  // GetGameObject<>
        player = GetComponentInParent<CharacterController>();


        wii = WiiController.Instance;
        if (wii != null)
        {
            wiiInUse = wii.useWii;
        }

    }
    private GameObject rayObject()
    {
        Vector3 rayOrigin = gunEnd.TransformDirection(new Vector3(1, 0, 0));

        if (Physics.Raycast(gunEnd.position, rayOrigin, out ray, 1000))
            return ray.collider.gameObject;
        else
            return null;

    }

    private void Teleport()
    {
        //fpsCam.transform.position = ray.point + ray.normal * 2;
        // rich.transform.position = ray.point + ray.normal * 2; // transform position gameobject
        player.transform.position = ray.point + ray.normal * 2;
    }

    void Update()
    {

        //if ((Input.GetButton("Fire1")) && laser == false)
        if (wii.holdButtonOne() == true && laser == false)
        {
            laser = true;
        }


        if (laser)//(Input.GetButton("Fire1")) && Time.time > nextFire) 
        {
            if (rayObject() != null)
            {
                laserLine.enabled = true;
                // Create a vector at the center of our camera's viewport
                //Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                Vector3 rayOrigin = gunEnd.TransformDirection(new Vector3(1, 0, 0));

                // Declare a raycast hit to store information about what our raycast has hit
                RaycastHit hit;


                // Set the start position for our visual effect for our laser to the position of gunEnd
                laserLine.SetPosition(0, gunEnd.position);

                // Check if our raycast has hit anyhing



                if (Physics.Raycast(gunEnd.position, rayOrigin, out hit, 1000))
                {
                    laserLine.SetPosition(1, hit.point);

                }
                else
                    laserLine.SetPosition(1, hit.point);


                //if (Input.GetButton("Fire2"))
                if (wii.holdButtonTwo() == true)
                {

                    Teleport();
                    laser = false;
                    laserLine.enabled = false;

                }
            }
            else
                laserLine.enabled = false;

        }
    }
}


