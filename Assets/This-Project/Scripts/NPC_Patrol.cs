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
  Vector3 rayVector = new Vector3(1,0,0);

  [SerializeField]
  Vector3 rayHeight = new Vector3(0,.05f,0);



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      drawRays();
    }

    public void drawRays()
    {
      rayVector = new Vector3(10,0,0);
      // the view range divided by the number of rays.
      separation = viewRange/rayNumber;
      //print(separation);
      //print(separation%viewRange);
      RaycastHit hit;

      for(int i = 0; i < rayNumber; i++)
      {
        //Need to figure out why there is not a ray for when i == 0
        rayVector = Quaternion.Euler(0, (separation)%viewRange, 0) * rayVector;


        Debug.DrawRay(transform.position + rayHeight, rayVector, Color.green);
        //Debug.DrawRay(transform.position + rayHeight, transform.right, Color.green);
        //Debug.DrawRay(transform.position + rayHeight, -transform.right, Color.green);
        //Debug.DrawRay(transform.position + rayHeight, transform.right, Color.green);
        //Debug.DrawRay(transform.position + rayHeight, -transform.forward, Color.green);

        //******This is what works for now******
        if (Physics.Raycast(transform.position+ rayHeight, rayVector, out hit, 10f))
        {
          hit.collider.gameObject.tag = "NPC_Hit";

          print(hit.point);
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
    }

    void OnCollisionEnter(){
      print("collision");
    }
}
