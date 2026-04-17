using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{   

    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    private bool isGrounded = false;

    private NetworkVariable<int> health = new NetworkVariable<int>(10);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
        SpriteRenderer sr = rb.GetComponent<SpriteRenderer>();
        for (int i = 0; i<NetworkManager.Singleton.ConnectedClientsIds.Count; i++)
        {
            if (NetworkManager.Singleton.ConnectedClientsIds[i] == NetworkObject.OwnerClientId)
            {
            if (i == 0)
            {
                if (sr != null) 
                {
                    sr.color = Color.blue;
                }
            }
            else if (i == 1)
            {
                if (sr != null) 
                {
                    sr.color = Color.red;
                }
            }
            else if (i == 2)
            {
                if (sr != null) 
                {
                    sr.color = Color.green;
                }
            }
            else if (i == 3)
            {
                if (sr != null) 
                {
                    sr.color = Color.yellow;
                }
            }
            }
        }
        GameManager.Instance.setLevel(SceneManager.GetActiveScene().name);



    }

    void Update()
    {
        if (IsOwner) 
        //if (true)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocityY);

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
                AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.jumpSound);
            }
        }

        if (health.Value<=0 && gameObject.activeInHierarchy == true)
        {
            gameObject.SetActive(false);
        }

        

    }

    void sendScore()
    {

        for (int i = 0; i<NetworkManager.Singleton.ConnectedClientsIds.Count; i++)
        {
            if (NetworkManager.Singleton.ConnectedClientsIds[i] == NetworkObject.OwnerClientId)
            {
                GameManager.Instance.AddScoreServ(i+1);
                AddScoreRpc(i, GameManager.Instance.p1Score, GameManager.Instance.p2Score, GameManager.Instance.p3Score, GameManager.Instance.p4Score);
                AudioManager.Instance.playCoinSound(10) ;


                
            }
        }
    }

    [Rpc(SendTo.Everyone)]
    void AddScoreRpc(int i, int s1, int s2, int s3, int s4)
    {
        GameManager.Instance.AddScore(i+1, s1, s2, s3, s4);
        AudioManager.Instance.playCoinSound(10) ;
        //Debug.Log(s1);
    }
 
    void OnCollisionEnter2D(Collision2D collision)  // Ground and enemy detection
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            isGrounded = true;
        }
        else if ( collision.gameObject.CompareTag("Enemy"))
        {
            /*if (IsOwner)
            {
                GameManager.Instance.TakeDamage(10);
                health.Value -=10;
                if (health.Value<=0)
                {
                    gameObject.SetActive(false);
                    //GameManager.Instance.addDied();
                }
            }
            else
            {
                health.Value -=10;
                if (health.Value<=0)
                {
                    gameObject.SetActive(false);
                    if (IsServer) GameManager.Instance.addDied();
                }
                Debug.Log(health);
            }*/

            Debug.Log("Touching Enemy");
            if (IsServer)
            {
                health.Value -=10;
                if (health.Value<=0)
                {
                    GameManager.Instance.addDied();
                }
            }
            AudioManager.Instance.playDamageSound(1);


        }
    }

    void OnCollisionExit2D(Collision2D collision)   // Ungrounded detection
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) // Enters goal or grabs coin
    {
        if (collision.CompareTag("Coin"))
        {
            if (IsServer) 
            //if(true)
            {
                sendScore();
                
            }
            
            //Destroy(collision.gameObject);
            CoinPoolManager.Instance.ReturnCoin(collision.gameObject);
            
        } 

        if (collision.CompareTag("Finish"))
        {   
            
            if (IsServer)
            {
                
                health.Value = 0;
                for (int i = 0; i<NetworkManager.Singleton.ConnectedClientsIds.Count; i++)
                {
                    if (NetworkManager.Singleton.ConnectedClientsIds[i] == NetworkObject.OwnerClientId)
                    {
                        GameManager.Instance.addCleared(i+1);
                    }
                }
            }
                
            
            //NetworkManager.SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
    }

    

}
