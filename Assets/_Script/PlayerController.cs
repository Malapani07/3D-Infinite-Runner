using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    private Rigidbody rb;
    private CapsuleCollider CC;
    private SphereCollider SC;
    [HideInInspector]public Animator anim;
    [SerializeField]int track;
    [SerializeField] float Jumpforce;

    void Start()
    {
        GameManager.Instance= this;
        rb = GetComponent<Rigidbody>();
        CC = GetComponent<CapsuleCollider>();
        SC = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        track = 2;
    }

    void Update()
    {
        if (GameManager.instance.gamestate == GameState.play)
        {          
             transform.position += transform.forward * Speed * Time.deltaTime;
          
            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && track < 3)
            {
                track++;
                transform.position += new Vector3(2.33f, 0f, 0f);
            }
            else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && track > 1)
            {
                track--;
                transform.position += new Vector3(-2.33f, 0f, 0f);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                anim.SetTrigger("Jump");
                rb.AddForce(Vector3.up * Jumpforce, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
            {
                anim.SetTrigger("Roll");
                StartCoroutine(Roll());
            }
        }
        GameManager.instance.WriteDistance?.Invoke();
    }

    IEnumerator Roll()
    {
        yield return new WaitForSeconds(.2f);
      
        CC.enabled = false;
        SC.enabled = true;
        yield return new WaitForSeconds(.5f);
        CC.enabled = true;
        SC.enabled = false;

    }


    private void OnCollisionEnter(Collision collision)
    {       
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.OnGameEnd?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            GameManager.instance.ScoreIncrement?.Invoke();
            other.gameObject.SetActive(false);
        }
        else if (other is BoxCollider)
        {
            GameManager.instance.Spawn?.Invoke();
        }
    }

}
