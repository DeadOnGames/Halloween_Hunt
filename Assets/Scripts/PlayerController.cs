using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;

    public Rigidbody2D rigidbody2d;
    private float horizontal;
    private float vertical;
    private IInteractable interactable;

    [SerializeField]
    private int candyCount;
    private int maxCandyCount = 10;
    private AudioSource audioSource;

    public Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        candyCount = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.X))
        {
            interact();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void interact()
    {
        if(interactable != null)
        {
            interactable.interact();
        }
    }

    public void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.tag == "Chest")
        {
            interactable = collison.GetComponent<IInteractable>();
        }
    }

    public void OnTriggerExit2D(Collider2D collison)
    {
        if (collison.tag == "Chest")
        {
            if(interactable != null)
            {
                interactable.stopInteract();
                interactable = null;
            }
        }
    }

    public int getCandyCount()
    {
        return candyCount;
    }

    public void setCandyCount(int i)
    {
        candyCount += i;
        if(candyCount < 0)
        {
            candyCount = 0;
        }
        CandyCounter.instance.SetValue(candyCount / (float)maxCandyCount);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
