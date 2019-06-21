using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Patrol : MonoBehaviour
{

  // The number of rays
  [SerializeField]
  public int _rayNumber = 5;

  // The view range in degrees.
  [SerializeField]
  public int _viewRange = 180;

  // The distance between the rays.
  [SerializeField]
  public int _separation;

  // The direction of the ray.
  [SerializeField]
  Vector3 _rayVector;

  // The length of the ray.
  public float _rayLength = 10f;

  // The distance between NPC and navPoint.
  public float _dist;

  // The height of the ray from the ground.
  [SerializeField]
  Vector3 _rayHeight = new Vector3(0.5f,.05f,0);

  // May have to move this back into update
  RaycastHit _hit;

  // Array of navPoints hit by the NPC
  public Vector3[] _hitNavPoints;

  // The destination chosen from _hitNavPoints
  public Vector3 _destination;

  // The
  public float _maxDistance;

  // Check to see in the NPC is moving
  bool _onRoute = false;

  // NavMeshAgent component
  NavMeshAgent _agent;


    // Start is called before the first frame update
    void Start()
    {
      _agent = this.GetComponent<NavMeshAgent>();
      _destination = transform.position;
      if (_agent == null)
      {
        Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
      }
      _hitNavPoints = new Vector3[_rayNumber];
      // When _hitNavPoints is initialized all the indicies are given a Vector3 and (0,0,0),
      // if all the rays do not hit a navPoint on the first call,
      for (int i = 0; i < _rayNumber; i++)
      {
        _hitNavPoints[i] = transform.position;
      }
    }

    // Update is called once per frame
    void Update()
    {
      if(_onRoute==false)
      {
        _hitNavPoints = drawRays();
        _destination = findDestination(_hitNavPoints);
        setDestination(_destination);

        turnAround(_hitNavPoints);

      }
      //drawRays();

      if(_onRoute && _agent.remainingDistance <= 1.0f)
        {
            _destination = transform.position;
            _onRoute = false;
        }
    }
    Vector3[] drawRays()
    {
      _rayVector = transform.TransformDirection(Vector3.right);
      // the view range divided by the number of rays.
      _separation = _viewRange/_rayNumber;
      for(int i = 0; i < _rayNumber; i++)
      {
        //Need to figure out why there is not a ray for when i == 0
        _rayVector = Quaternion.Euler(0, (_separation)%_viewRange, 0) * _rayVector;
        Debug.DrawRay(transform.position + _rayHeight, -_rayVector*_rayLength, Color.green);
        //**************************************
        //******This is what works for now******
        //**************************************
        if (Physics.Raycast(transform.position+ _rayHeight, -_rayVector , out _hit, _rayLength))
        {
          _hit.collider.gameObject.tag = "NPC_Hit";
          _hitNavPoints[i] = _hit.point;
        }
        else
        {
          _hitNavPoints[i] = transform.position;
        }
      }
      return _hitNavPoints;
    }


    Vector3 findDestination(Vector3[] hitNavPoints)
    {
      Vector3 nextDestination = transform.position;
      _maxDistance = 0;

      for(int i = 0; i<hitNavPoints.Length;i++)
      {
        _dist = Vector3.Distance(hitNavPoints[i], transform.position);
        if (_dist>_maxDistance)
        {
          _maxDistance = _dist;
          nextDestination = hitNavPoints[i];
        }
      }
      return  nextDestination;
    }

    void setDestination(Vector3 destination)
    {

      if (destination!=transform.position)
      {
        _onRoute = true;
        _agent.SetDestination(_destination);
      }
    }

    void turnAround(Vector3[] _hitNavPoints)
    {

    }
}
