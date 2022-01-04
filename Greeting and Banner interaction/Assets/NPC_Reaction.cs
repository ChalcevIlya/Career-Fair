using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Reaction : MonoBehaviour
{
    public Transform target;
    public Transform basepoint;
    public float speed = 5f;
    public float turnback = 2f;
    private bool enter;
    public Animation GreetingAnim;
    public GameObject Greeting;
    public GameObject NPC_Info;

    void Start()
    {

    }

    void Update()
    {
        if (enter)
        {
            Quaternion rotation = Quaternion.LookRotation(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        }

        if (!enter)
        {
            Quaternion rotation = Quaternion.LookRotation(new Vector3(basepoint.transform.position.x, transform.position.y, basepoint.transform.position.z) - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnback * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        enter = true;

        GreetingAnim = GetComponent<Animation>();
        GreetingAnim.Play("mixamo.com");

        if (col.name == "Player")
        {
            Greeting.SetActive(true);
            NPC_Info.SetActive(false);
        }
    }
    void OnTriggerExit(Collider col)
    {
        enter = false;
        EndDialog();
    }

    public void GetResponse()
    {
        Greeting.SetActive(false);
        NPC_Info.SetActive(true);
    }

    public void EndDialog()
    {
        Greeting.SetActive(false);
        NPC_Info.SetActive(false);
    }
}
