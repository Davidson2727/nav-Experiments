using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{

  // The number of rays
  [SerializeField]
  public int rayNumber = 5;

  // The view range in degrees
  [SerializeField]
  public int viewRange = 180;

  public int separation;

  // This will be the length of the array? used in drawRays.
  [SerializeField]
  Vector3 rayVector;

  public float rayLength = 10f;

  public float dist;

  [SerializeField]
  Vector3 rayHeight = new Vector3(0.5f,.05f,0);

  public Vector3[] hitNavPoints;

  public Vector3 destination;

  public float maxDistance;



    // Start is called before the first frame update
    void Start()
    {
      hitNavPoints = new Vector3[rayNumber];
    }

    // Update is called once per frame
    void Update()
    {
      drawRays();
    }

    public void drawRays()
    {
      rayVector = transform.TransformDirection(Vector3.right);
      // the view range divided by the number of rays.
      separation = viewRange/rayNumber;
      //print(separation);
      //print(separation%viewRange);
      RaycastHit hit;

      for(int i = 0; i < rayNumber; i++)
      {
        //Need to figure out why there is not a ray for when i == 0
        rayVector = Quaternion.Euler(0, (separation)%viewRange, 0) * rayVector;


        Debug.DrawRay(transform.position + rayHeight, rayVector*rayLength, Color.green);
        //Debug.DrawRay(transform.position + rayHeight, transform.right, Color.green);
        //Debug.DrawRay(transform.position + rayHeight, -transform.right, Color.green);
        //Debug.DrawRay(transform.position + rayHeight, transform.right, Color.green);
        //Debug.DrawRay(transform.position + rayHeight, -transform.forward, Color.green);

        //******This is what works for now******
        if (Physics.Raycast(transform.position+ rayHeight, rayVector , out hit, rayLength))
        {
          hit.collider.gameObject.tag = "NPC_Hit";
          hitNavPoints[i] = hit.point;

          //Debug.Log("Point " + hit.point);
          //Debug.Log("The array point " + i + " is " + hitNavPoints[i]);
        }
        //if (Physics.Raycast(transform.position+ rayHeight, transform.TransformDirection(-Vector3.forward), out hit, 10f))
        //{
        //  hit.collider.gameObject.tag = "NPC_Hit";

        //  print(hit.point);
        //}
        //if (Physics.Raycast(transform.position+ rayHeight, transform.TransformDirection(Vector3.right), out hit, 10f))
        //{
        //  hit.collider.gameObject.tag = "NPC_Hit";

        //  print(hit.point);
        //}
        //if (Physics.Raycast(transform.position+ rayHeight, transform.TransformDirection(-Vector3.right), out hit, 10f))
        //{
        //  hit.collider.gameObject.tag = "NPC_Hit";

        //  print(hit.point);
        //}



      }
      destination = findDestination(hitNavPoints);
    }

    Vector3 findDestination(Vector3[] hitNavPoints)
    {
      destination = transform.position;
      for(int i = 0; i<hitNavPoints.Length;i++)
      {
        dist = Vector3.Distance(hitNavPoints[i], transform.position);
        if (dist>maxDistance)
        {
          maxDistance = dist;
          destination = hitNavPoints[i];
        }
        //print("Distance to other: " + dist);
        print("Destination1 "+ destination);
      }
      print("Destination2 " + destination);




      return  destination;
    }


}
