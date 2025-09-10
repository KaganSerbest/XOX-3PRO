using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class diceController : MonoBehaviour
{
    public static diceController instance;

    public Rigidbody fizikim;
    public Vector3 dönmeVectoru;

    public bool zarKullanildi = true;
    
    public int diceIndex;
    public int boxIndex;

    public int xZarHakki = 3;
    public int oZarHakki = 3;
    public bool zarHakkiKaldix = true;
    public bool zarHakkiKaldio = true;

    public bool ikiMi = false;
    public bool ucMu = false;
    public bool dortMu = false;
    public bool besMi = false;
    public bool altiMi = false;

    public Transform randomPos;


    private void Awake()
    {
        if (instance != null && instance != this)
        {

            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {  
        fizikim = GetComponent<Rigidbody>();
        dönmeVectoru = new Vector3(20f, -20f, 20f);       
    }

    
    void Update()
    {
        ZarFirlatma();
    }

   
    
    public void ZarFirlatma()
    {
        if (uýController.instance.oyunDurdu)
        {
            return;
        }
        else
        {
            if (zarKullanildi)
            {
                if (!gameManager.instance.oyunBittiMi)

                {
                    if ((gameManager.instance.gameIndex % 2 == 1 && xZarHakki >= 1) || (gameManager.instance.gameIndex % 2 == 0 && oZarHakki >= 1))
                    {
                        if (Input.GetKey(KeyCode.Space))
                        {
                            ZariSalla();

                        }

                        if (Input.GetKeyUp(KeyCode.Space))
                        {
                            diceIndex = Random.Range(1, 7);
                            ZariBirak();
                            YuzuBelirle(diceIndex);

                        }
                    }
                }
            }
        }
        
        
        
    }   
    public void ZariSalla()
    {
        gameObject.transform.Rotate(dönmeVectoru);
        
    }
    public void ZariBirak()
    {      
        gameObject.transform.Rotate(0f, 0f, 0f);

        if(gameManager.instance.gameIndex % 2 == 1)
        {
            xZarHakki--;
            zarKullanildi = false;
        }
        else if(gameManager.instance.gameIndex % 2 == 0)
        {
            oZarHakki--;
            zarKullanildi = false;
        }

        


    }
    public void YuzuBelirle(int index)
    {
        Vector3 currentRotation = transform.eulerAngles;

        if (index == 1)
        {
            currentRotation.x = 270f;
            currentRotation.y = 180f;
            gameManager.instance.gameIndex++;
            zarKullanildi = true;
            //Debug.Log("BOÞ GEÇTÝN");
        }

        else if (index == 2)
        {
            currentRotation.x = 0f;
            currentRotation.z = 0f;

            ikiMi = true;
            

        }

        else if (index == 3)
        {
            currentRotation.x = 180f;
            currentRotation.z = 90f;

            ucMu = true;
            
            do
            {
                boxIndex = Random.Range(0, gameManager.instance.boxList.Count);
                
            }
            while (gameManager.instance.boxList[boxIndex].tag == "XXX" || gameManager.instance.boxList[boxIndex].tag == "OOO");

            randomPos = gameManager.instance.boxList[boxIndex].transform;
        }

        else if (index == 4)
        {
            currentRotation.x = 180f;
            currentRotation.z = -90f;
            dortMu = true;
            //Debug.Log("KARÞI TARAFIN BÝR TAÞINI SÝL");
            
            
        }

        else if (index == 5)
        {
            currentRotation.x = 180f;
            currentRotation.z = 0f;
            besMi = true;


            
            //Debug.Log("KARÞI TARAFIN BÝR TAÞINI KENDÝ TAÞINA DÖNÜÞTÜR");
        }

        else if (index == 6)
        {
            currentRotation.x = 90f;
            currentRotation.y = 0f;

            if(gameManager.instance.gameIndex % 2 == 0)
            {
                oZarHakki += 2;
            }

            else
            {
                xZarHakki += 2;
            }
            altiMi = true;


            //Debug.Log("BÝR ZAR HAKKI DAHA KAZANDIN");
        }

        transform.eulerAngles = currentRotation;
    }

    



}
