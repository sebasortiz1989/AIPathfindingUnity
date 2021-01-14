using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent agent;

    Animator anim;
    public float velocityagent;
    public float distanceDestiny;
    bool destinationSet = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {          
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Move out agent
                agent.isStopped = false;
                agent.SetDestination(hit.point);
                destinationSet = true;
            }
        }

        if (agent.velocity.magnitude > 5)
        {
            anim.SetBool("isMoving", true); 
        }
        else if (agent.velocity.magnitude < 5)
            anim.SetBool("isMoving", false);

        if (agent.velocity.magnitude > agent.speed + 5)
        {
            anim.SetBool("isMoving", false);
            anim.SetTrigger("isJumping");
            Invoke("ResetJump", 0.9f);
        }

        if (agent.remainingDistance < agent.stoppingDistance && destinationSet && anim.GetBool("isMoving"))
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.SetDestination(transform.position);
            destinationSet = false;
        }

        velocityagent = agent.velocity.magnitude;
        distanceDestiny = agent.remainingDistance;
    }

    public void ResetJump()
    {
        anim.ResetTrigger("isJumping");
    }
}
