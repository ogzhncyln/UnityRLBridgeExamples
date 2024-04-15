using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RLBridge;

[RequireComponent(typeof(Rigidbody))]
public class CubeAgent : AgentBehavior
{
    public float speed;
    public Transform goal;

    private float _d;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnConnected()
    {
        Debug.Log("Connected");
        gameObject.SetActive(true);
    }

    public override void CollectObservations(ref double[] observations)
    {
        observations[0] = transform.localPosition.x;
        observations[1] = transform.localPosition.y;
        observations[2] = transform.localPosition.z;
        observations[3] = goal.localPosition.x;
        observations[4] = goal.localPosition.y;
        observations[5] = goal.localPosition.z;

        float d = Vector3.Distance(transform.localPosition, goal.localPosition);
        float reward = (_d - d) * 0.1f;
        AddReward(reward);
        _d = d;
    }

    public override void OnActionReceived(double[] continuous_actions, int[] discrete_actions)
    {
        rb.velocity = new Vector3((float)continuous_actions[0], 0, (float)continuous_actions[1]) * speed;
    }

    public override void OnEpisodeBegin()
    {
        goal.transform.localPosition = new Vector3(Random.Range(-8f, 8f), goal.transform.localPosition.y, Random.Range(-8f, 8f));
        transform.localPosition = new Vector3(Random.Range(-8f, 8f), transform.localPosition.y, Random.Range(-8f, 8f));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            AddReward(10f);
            EndEpisode();
        }else if(other.gameObject.tag == "Wall")
        {
            AddReward(-10f);
            EndEpisode();
        }
    }

}
