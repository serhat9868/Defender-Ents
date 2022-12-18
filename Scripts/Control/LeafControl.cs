using RPG.Control;
using System.Collections;
using UnityEngine;

public class LeafControl : MonoBehaviour,IRaycastable
{
    [SerializeField] float spawningKickX;
    [SerializeField] float spawningKickY;
    [SerializeField] float movingTime;
    [SerializeField] int cashAmount = 25;
    float time = 0;
    Vector2 zeroVelocity;
    float randomSpeedX;
    bool isClicked = false;
    MainPlayerController mainPlayerController;
    void Start()
    {
        mainPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerController>();
        zeroVelocity = new Vector2(0, 0);
        randomSpeedX = Random.Range(0, spawningKickX);
        GetComponent<Rigidbody2D>().velocity = new Vector2(randomSpeedX, spawningKickY);
    }

    void Update()
    {
        if (time > movingTime)
        {
            Stop();
        }

        time += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        if (isClicked) return;
        
        mainPlayerController.EarnLeaf(cashAmount);
        isClicked = true;
        Destroy(gameObject);
    }

    private void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = zeroVelocity;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    public CursorType GetCursorType()
    {
        return CursorType.None;
    }

    public bool HandleRaycast(MainPlayerController callingController)
    {
        if (isClicked) return false;

        if (Input.GetMouseButtonDown(0))
        {
            callingController.EarnLeaf(cashAmount);
            isClicked = true;
            Destroy(gameObject);
        }      
        return true;
    }
}