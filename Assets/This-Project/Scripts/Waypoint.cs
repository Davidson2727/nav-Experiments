using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    float debugDrawRadius = 1.0F;

    void Update()
    {
      if(gameObject.tag == "NPC_Hit")
      {
        // Here there will be a 4-7 second cound unitl the tag changes back to normal.
      }

    }
    void OnCollisionEnter()
    {
      print("Collision");
      //this.gameObject.tag == "NPC_Hit";
    }

    // This function is called every update cycle. It is only used during the development
    // phase.
    //
    void OnDrawGizmos()
    {
        if(gameObject.tag == "NPC_Hit")
        {
          Gizmos.color = Color.green;
          StartCoroutine(timer());
        }
        else
        {
          Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }

    void onRayHit()
    {
      Debug.Log("HEre");
    }

    public IEnumerator timer()
    {
      yield return new WaitForSeconds(.04f);
      gameObject.tag = "ThisNavPoint";
    }

}
