
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

  public NavMeshAgent agent;

  void Start() {
       agent = GetComponent<NavMeshAgent>();
   }


    void Update()
    {
      //Debug.Log("position "+transform.position);
      transform.Translate(0, 0, -Time.deltaTime*2);
      //this.transform.Rotate(0, 3, 0, Space.Self);
    }
}
