using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public float ShowDuration = 10f;

    private float showTimer;

    public TextMesh textMesh;

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        showTimer -= Time.deltaTime;
        textMesh.text = ((int)showTimer + 1).ToString();

        if (showTimer <= 0f)
        {
            gameObject.SetActive(false);
            SquareSpawner.Instance.ReturnToPool2(this);
        }
    }

    public virtual void Activate(Vector2 position)
    {
        InitTimer();
        transform.position = position;
        gameObject.SetActive(true);
    }

    public virtual void Destroy()
    {
        gameObject.SetActive(false);
        SquareSpawner.Instance.ReturnToPool(this);
    }

    private void InitTimer()
    {
        showTimer = ShowDuration;
    }
}
