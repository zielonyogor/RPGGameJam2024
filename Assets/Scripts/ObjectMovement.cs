using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed; //maybe changes with different types
    private Rigidbody2D objectRigidbody;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    enum WalkDirection
    {
        up, bottom, left, right
    }

    private WalkDirection walkDirection;

    void Start()
    {
        objectRigidbody = GetComponent<Rigidbody2D>();

        ChooseDirection();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking) {
            walkCounter -= Time.deltaTime;


            switch (walkDirection)
            {
                case WalkDirection.up:
                    objectRigidbody.linearVelocity = Vector2.up * moveSpeed;
                    break;
                case WalkDirection.bottom:
                    objectRigidbody.linearVelocity = Vector2.down * moveSpeed;
                    break;
                case WalkDirection.left:
                    objectRigidbody.linearVelocity = Vector2.left * moveSpeed;
                    break;
                case WalkDirection.right:
                    objectRigidbody.linearVelocity = Vector2.right * moveSpeed;
                    break;
            }


            if (walkCounter < 0)
            {
                isWalking = false;
                objectRigidbody.linearVelocity = Vector2.zero;

                waitCounter = waitTime;
            }

        }
        else
        {
            waitCounter -= Time.deltaTime;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }

    }

    public void ChooseDirection()
    {
        walkDirection = (WalkDirection)UnityEngine.Random.Range(0, 4);


        isWalking = true;
        walkCounter = walkTime;


    }
}
