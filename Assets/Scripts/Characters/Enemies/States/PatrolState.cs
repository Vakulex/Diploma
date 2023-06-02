using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    private float _timer;
    private List<GameObject> wayPoints = new List<GameObject>();
    private NavMeshAgent _agent;
    private Transform _player;
    private float _chaseRange = 15;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _timer = 0;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        List<GameObject> go = new List<GameObject>();
        go.AddRange(GameObject.FindGameObjectsWithTag("Waypoints"));
        foreach (GameObject wp in go)
        {
            wayPoints.Add(wp);
        }
        _agent.speed = 1.4f;
        ChangeRandomWaypoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_agent.remainingDistance <= _agent.stoppingDistance)
            ChangeRandomWaypoint();
        
        _timer += Time.deltaTime;    
        if(_timer > 10)
            animator.SetBool("isPatroling", false);
        
        float distance = Vector3.Distance(_player.position, animator.transform.position);
        
        if(distance < _chaseRange)
            animator.SetBool("isChasing", true);
    }

    private void ChangeRandomWaypoint()
    {
        if(wayPoints.Count < 0)
            return;
        if(wayPoints == null)
            return;
        _agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].transform.position);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
