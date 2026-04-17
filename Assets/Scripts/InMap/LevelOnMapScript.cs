using UnityEngine;
using Unity.Netcode;
using UnityEngine.Rendering;
public class LevelOnMapScript : NetworkBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    //[SerializeField] private int level;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        sr = rb.GetComponent<SpriteRenderer>();
        if (true)
        {
            if (rb.name == "Lvl1")
            {
                
                if (SaveLoadManager.Instance.GetLvl1Clear() )
                {
                    sr.color = Color.darkBlue;
                } 
                else
                {
                    sr.color = Color.red;
                }
            } 
            else if(rb.name == "Lvl2")
            {
                Debug.Log("hereful");
                if (!SaveLoadManager.Instance.GetLvl1Clear() )
                {
                    Debug.Log("lvl1 cleared");
                    sr.color = Color.black;
                }
                else if (SaveLoadManager.Instance.GetLvl2Clear() )
                {
                    Debug.Log("lvl2 cleared");
                    sr.color = Color.darkBlue;
                } 
                else
                {
                    Debug.Log("lvl2 not cleared");
                    sr.color = Color.red;
                }
            }
            else
            {
                Debug.Log("did we even get here");
            }
            
            //UpdateMapIconsRpc(sr.color);
        }
    }

    
}
