using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxController : MonoBehaviour
{
    public Material redMaterial;
    public Material greenMaterial;
    public Material purpleMaterial;

    private bool tiklandiMi = false;
    

    private Material originMaterial;
    private Renderer rend;
    
    public GameObject kutu;
    public GameObject k�re;

    public bool hasRun = false;

    public int sayi = 1;

    public Vector3 xKonum;
    public Vector3 oKonum;
    public Vector3 randomKonum;




    void Start()
    {
        originMaterial = GetComponent<Renderer>().material;
        rend = GetComponent<Renderer>();
        xKonum = new Vector3((gameObject.transform.position.x + 2.65f), gameObject.transform.position.y, gameObject.transform.position.z);
        oKonum = new Vector3((gameObject.transform.position.x - 2.74f), (gameObject.transform.position.y + 0.98f), gameObject.transform.position.z);
        
    }

    
    void Update()
    {
        if (diceController.instance.ucMu)
        {
            RandomOlustur();
    
        }
    }

   

    
    
    
    
    
    private void OnMouseEnter()
    {
        if (u�Controller.instance.oyunDurdu)
        {
            return;
        }
        else
        {
            rend.enabled = true;

            if (tiklandiMi == true)
            {
                RedArea();
            }
            else
            {
                GreenArea();
            }
        }
     
    }

    private void OnMouseExit()
    {
        rend.enabled = false;
    }    
    


    private void OnMouseDown()
    {

        if (u�Controller.instance.oyunDurdu)
        {
            return;
        }
        else
        {

            if (hasRun)
            {
                if (diceController.instance.dortMu)
                {
                    for (int i = 0; i < gameManager.instance.spawnedObjects.Count; i++)
                    {
                        GameObject spawned = gameManager.instance.spawnedObjects[i];

                        if ((spawned.transform.position == xKonum) || (spawned.transform.position == oKonum))
                        {
                            Destroy(spawned);
                            gameManager.instance.spawnedObjects.RemoveAt(i);


                            diceController.instance.dortMu = false;
                            hasRun = false;
                            tiklandiMi = false;
                            gameManager.instance.gameIndex++;
                            GreenArea();

                            break;


                        }
                    }

                    diceController.instance.zarKullanildi = true;
                }

                else if (diceController.instance.besMi)
                {
                    for (int i = 0; i < gameManager.instance.spawnedObjects.Count; i++)
                    {
                        GameObject spawned = gameManager.instance.spawnedObjects[i];


                        if ((spawned.transform.position == xKonum) || (spawned.transform.position == oKonum))
                        {
                            Destroy(spawned);
                            gameManager.instance.spawnedObjects.RemoveAt(i);

                            Olustur();
                            gameManager.instance.KazanVeBitir();


                            diceController.instance.besMi = false;

                            RedArea();


                        }
                    }
                    gameManager.instance.gameIndex++;
                    diceController.instance.zarKullanildi = true;
                }


                else
                {
                    return;
                }

            }

            else
            {
                hasRun = true;  //kutuya bir daha t�klanmas�n� engeller
                tiklandiMi = true;//kutuyu t�kland� olarak i�aretler
                rend.enabled = false;

                diceController.instance.dortMu = false;// mor alan� kapat�r

                Olustur();
                diceController.instance.zarKullanildi = true;
                gameManager.instance.KazanVeBitir();
                RedArea();

                if (!diceController.instance.ikiMi)
                {
                    gameManager.instance.gameIndex += 1;//oyun s�ras�n� de�i�tirir
                }
                else
                {
                    diceController.instance.ikiMi = false;
                }


                if (diceController.instance.altiMi)
                {
                    diceController.instance.zarKullanildi = true;
                    diceController.instance.altiMi = false;

                }




            }
        }

        
    }







  
    
    public void GreenArea()
    {    
        rend.material = greenMaterial;
    }


    public void RedArea()
    {
        if (diceController.instance.dortMu)
        {
            PurpleArea();
        }
        else if (diceController.instance.besMi)
        {
            PurpleArea();
        }
        else
        {
            rend.material = redMaterial; // ta� koyulan yerin arkas�n� k�rm�z� g�sterir
        }
        
        

    }

    public void PurpleArea()
    {
        if ((gameManager.instance.gameIndex % 2 == 0 && gameObject.tag == "XXX") || (gameManager.instance.gameIndex % 2 == 1 && gameObject.tag == "OOO"))
        {
            rend.material = purpleMaterial;
        }     
    }

    
    public void Olustur()
    {
       
        
        if (gameManager.instance.gameIndex % 2 == 1)
        {
            XOlustur();
        }
        else
        {
            OOLU�TUR();
        }
    }
    
    public void XOlustur()
    {
        GameObject go = Instantiate(kutu, xKonum, kutu.transform.rotation);
        gameObject.tag = "XXX";
        gameManager.instance.spawnedObjects.Add(go);
    }

    public void OOLU�TUR()
    {
        GameObject go = Instantiate(k�re, oKonum, k�re.transform.rotation);
        gameObject.tag = "OOO";
        gameManager.instance.spawnedObjects.Add(go);
    }

    public void RandomOlustur()
    {
        if (transform != diceController.instance.randomPos) return;
            if (gameManager.instance.gameIndex % 2 == 1)
        {
            randomKonum = new Vector3((diceController.instance.randomPos.position.x + 2.65f) , (diceController.instance.randomPos.position.y), (diceController.instance.randomPos.position.z));


            GameObject go = Instantiate(kutu, randomKonum, kutu.transform.rotation);
            
            
            gameObject.tag = "XXX";
            gameManager.instance.spawnedObjects.Add(go);
            gameManager.instance.KazanVeBitir();
            gameManager.instance.gameIndex++;
            diceController.instance.zarKullanildi = true;
            
        }
            else
        {
            randomKonum = new Vector3((diceController.instance.randomPos.position.x - 2.74f), (diceController.instance.randomPos.position.y + 0.98f), (diceController.instance.randomPos.position.z));

            GameObject go = Instantiate(k�re, randomKonum, k�re.transform.rotation);
            
            
            gameObject.tag = "OOO";
            gameManager.instance.spawnedObjects.Add(go);
            gameManager.instance.KazanVeBitir();
            gameManager.instance.gameIndex++;
            diceController.instance.zarKullanildi = true;
            
        }

            diceController.instance.ucMu = false;
            hasRun = true;
            tiklandiMi = true;
            rend.enabled = false;
            RedArea();

    }
}
