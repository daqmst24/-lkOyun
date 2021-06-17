using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerImput : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&player.isGrouned)
        {
            player.Jump();
            player.canDoubleJump = true;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && !player.isGrouned&&player.canDoubleJump)
        {
            player.DoubleJump();
            player.canDoubleJump = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            player.ShootProject();
        }
        
    }
}
