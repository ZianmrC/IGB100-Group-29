using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookFound : MonoBehaviour
{
    public static BookFound instance;

    public static int book = 0;
    public int bookCount = 0;
    private int bookInt = 0;

    public Transform move;
    public float moveDuration = 3f;

    private bool isMoving = false;

    void Awake()
    {
        instance = this;
    }

    public void AddBook(int newBookValue)
    {
        book += newBookValue;
        if (book >= 3)
        {
            StartCoroutine(MoveShelf());
            isMoving = true;
        }
    }

    private IEnumerator MoveShelf()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(startPosition, move.position, t);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = move.position;
        isMoving = false;
    }
}
