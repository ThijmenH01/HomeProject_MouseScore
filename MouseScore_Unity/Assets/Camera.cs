using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("Values")]
    public float acceleration;
    public float moveSpeed;
    public float waitTime;
    public int startTime;
    public int maxRange;

    [Header("Positions")]
    [SerializeField] private Vector2 currentPosition;
    [SerializeField] private Vector2 targetPosition;
    private int randomTarget;
    private bool gameStarted = false;
    private bool allowedToMove = true;

    void Start() {
        randomTarget = Random.Range(-maxRange, maxRange);
        StartCoroutine("StartGame", startTime);
    }

    void Update() {
        currentPosition = transform.position;
        targetPosition.x = randomTarget;
        if (currentPosition.x != randomTarget) {
            MoveCamera();
        }
        if (currentPosition.x == randomTarget) {
            if (allowedToMove) {
                StartCoroutine("WaitTime", waitTime);
                allowedToMove = false;
            }
        }
    }

    void MoveCamera() {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private IEnumerator StartGame(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        MoveCamera();
    }

    private IEnumerator WaitTime(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        randomTarget = Random.Range(-maxRange, maxRange);
        allowedToMove = true;
    }
}
