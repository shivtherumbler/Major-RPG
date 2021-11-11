using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDownward : MonoBehaviour
{
    private Animator animator;
    public GameObject ladder;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        animator = other.GetComponent<Animator>();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("ladderdown", true);
            animator.SetBool("climb", false);
            animator.SetBool("climbup", false);
            //StartCoroutine(LadderClimb(other.transform.position, other.transform.position + other.transform.for, 2f));
            other.GetComponent<Player>().enabled = false;
            
        }
        ladder.SetActive(true);



    }

    IEnumerator LadderClimb(Vector3 start, Vector3 end, float Duration)
    {
        float t = 0f;
        while (t < Duration)
        {
            transform.position = Vector3.Lerp(new Vector3(start.x, transform.position.y, start.z), new Vector3(end.x, transform.position.y, end.z), t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        animator.applyRootMotion = true;

    }
}
