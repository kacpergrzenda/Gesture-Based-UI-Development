using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    /* Private Variables */
    private Vector3 target = new Vector3(0.0f, 1.0f, 0.0f);

    private GameInformation gI;
    /* Public Variables */
    public int moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }
    private void MoveEnemy() {
        var step =  moveSpeed * Time.deltaTime; // calculate distance to move
        //transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, target, step);

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Weapon")
        {
            GameInformation.score++; // Increment Score by 1 When enemy gets destroyed
            Destroy(gameObject);
        }
    }
}
