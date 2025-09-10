using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    
    public int gameIndex = 1; //oyun sýrasýný belirler
    
    public TMP_Text birinciZar;
    public TMP_Text ikinciZar;

    public TMP_Text birinciSira;
    public TMP_Text ikinciSira;

    public GameObject xKazandi;
    public GameObject oKazandi;
    public GameObject oyunBerabere;
    public UnityEngine.UI.Button durdurTusu;

    

    public UnityEngine.UI.Image birinciPanel;
    public UnityEngine.UI.Image ikinciPanel;

    public GameObject duraklat;
    


    public bool oyunBittiMi = false;

    public int firstDiceTime = 3;// zar hakký
    public int secondDiceTime = 3;// zar hakký

    public int tabloBoyutu = 5;
    public int hedefSira = 5;

    public List<GameObject> boxList = new List<GameObject>();
    public List<GameObject> spawnedObjects = new List<GameObject>();

   

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
        
    }

    
    void Update()
    {
        ZarHakkiniBelirt();
        SirayiBelirt();
      
    }

    

    public void ZarHakkiniBelirt()
    {
        birinciZar.text = diceController.instance.xZarHakki.ToString();
        ikinciZar.text = diceController.instance.oZarHakki.ToString();


        
        if(diceController.instance.xZarHakki <= 0)
        {
            
            birinciZar.text = "0";
            diceController.instance.xZarHakki = 0;
        }
        else if (diceController.instance.oZarHakki <= 0)
        {
            
            ikinciZar.text = "0";
            diceController.instance.oZarHakki = 0;
        }

    }

    public void SirayiBelirt()
    {
        if(gameIndex % 2 == 1)
        {
            birinciSira.gameObject.SetActive(true);
            ikinciSira.gameObject.SetActive(false);
            
            birinciPanel.color = new Color32(230, 126, 34, 255);
            ikinciPanel.color = new Color32(78, 52, 46, 255);

        }
        else
        {
            ikinciSira.gameObject.SetActive(true);
            birinciSira.gameObject.SetActive(false);
            birinciPanel.color = new Color32(78, 52, 46, 255);
            ikinciPanel.color = new Color32(230, 126, 34, 255);
        }
    }


    public string KazananVarMi()
    {
        string[,] tablo = new string[tabloBoyutu, tabloBoyutu];
        for (int i = 0; i < boxList.Count; i++)
        {
            int r = i / tabloBoyutu;
            int c = i % tabloBoyutu;
            tablo[r, c] = boxList[i].tag;
        }

        if (SiraliVarMi(tablo, "XXX", hedefSira)) return "XXX";
        if (SiraliVarMi(tablo, "OOO", hedefSira)) return "OOO";
        return null;
    }

    public bool SiraliVarMi(string[,] t, string tag, int hedef)
    {
        int n = tabloBoyutu;

        // SATIR KONTROLÜ

        for (int r = 0; r<n; r++)
        {
            int say = 0;

            for (int c = 0; c<n; c++)
            {
                say = (t[r, c] == tag) ? say + 1 : 0;
                if (say >= hedef) return true;             
            }
        }

        //  SÜTUN KONTROLÜ

        for (int c = 0; c<n; c++)
        {
            int say = 0;

            for (int r = 0; r<n; r++)
            {
                say = (t[r, c] == tag) ? say + 1 : 0;
                if (say >= hedef) return true;
            }
        }

        // SOL ÜSTTEN SAÐ ALTA ÇAPRAZ KONTROLÜ

        for (int start = 0; start < n; start++ )
        {
            //sol kenardan baþlayanlar
            int say = 0;

            for (int r = start, c=0; r<n && c<n; r++, c++)
            {
                say = (t[r, c] == tag) ? say + 1 : 0;
                if (say >= hedef) return true;
            }
        }

        for (int start = 1; start <n; start++)
        {
            //üstten baþlayanlar
            int say = 0;

            for (int c = start, r=0; r<n && c<n; r++, c++)
            {
                say = (t[r, c] == tag) ? say + 1 : 0;
                if (say >= hedef) return true;
            }
        }

        //SAÐ ÜSTTEN SOL ALTA ÇAPRAZ KONTROLÜ

        for (int start = 0; start < n; start++)
        {
            int say = 0;

            for (int r = start, c= n-1; r<n && c>=0; r++,c--)
            {
                say = (t[r, c] == tag) ? say + 1 : 0;
                if (say >= hedef) return true;
            }

        }

        for (int start = n-2; start>= 0 ; start--) 
        {
            int say = 0;

            for (int r= 0, c = start; r<n && c>=0; r++, c--)
            {
                say = (t[r, c] == tag) ? say + 1 : 0;
                if (say >= hedef) return true;
            }
        }

        return false;

    }
    
    public void KazanVeBitir()
    {
        string kazanan = KazananVarMi();
        if(kazanan!= null)
        {
            oyunBittiMi = true;
            Time.timeScale = 0;
            Debug.Log((kazanan == "XXX" ? "X" : "O") + " kazandý!");
            uýController.instance.oyunDurdu = true;
            if(KazananVarMi() == "XXX")
            {
                xKazandi.SetActive(true);
                durdurTusu.interactable = false;
            }
            else
            {
                oKazandi.SetActive(true);
                durdurTusu.interactable = false;
            }
        }
        if(kazanan == null && spawnedObjects.Count == 25)
        {
            oyunBittiMi = true;
            Time.timeScale = 0;
            Debug.Log("bitti");
            oyunBerabere.SetActive(true);
            uýController.instance.oyunDurdu = true;
        }
    }

    public void PencereyiKapat()
    {
        xKazandi.SetActive(false);
        oKazandi.SetActive(false);
        oyunBerabere.SetActive(false);
        durdurTusu.interactable = true;
    }

    


    


    
}
