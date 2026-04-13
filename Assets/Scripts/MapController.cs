using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using System;

public class MapController : NetworkBehaviour
{
    private Rigidbody2D rb;
    public int level;
    public int world;

    public TextMeshProUGUI worldLevel;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        world = 1;
        level = 1;
        worldLevel.text = world + " - " + level;
    }

    
    void Update()
    {
        Movement();
        Select();
    }

    void Movement()
    {
        if (Input.GetAxis("Horizontal") > 0 && level == 1)
        {
            level = 2;
            rb.linearVelocityX = 10;
            
        }
        else if (Input.GetAxis("Horizontal") < 0 && level == 2)
        {
            level = 1;
            rb.linearVelocityX = -10;
        }

        if (rb.position.x <-6.94 && rb.linearVelocityX <0 || rb.position.x >-2.65 && rb.linearVelocityX >0)
        {
            rb.linearVelocityX = 0;
        }
        if (IsServer)
        {
            UpdateWorldTextRpc(world + " - " + level);
        }
    }

    void Select()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //SceneManager.LoadScene(""+world + " - " + level);
            NetworkManager.Singleton.SceneManager.LoadScene(""+world + " - " + level, LoadSceneMode.Single);
        }
    }

    [Rpc(SendTo.Everyone)]
    void UpdateWorldTextRpc(String lvl)
    {
        worldLevel.text = lvl;
        //Debug.Log(lvl);
    }
}


