using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stalker : EntityController
{
  
    private void FixedUpdate()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        if (inputVector != Vector2.zero)
        {
            MovePosition(inputVector);
        }
        else
        {
            StopBody();
        }
                
    }   

    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }
    public void Ganha()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Vitoria");
    }
}
