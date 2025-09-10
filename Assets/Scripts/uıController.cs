using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uıController : MonoBehaviour
{
    public GameObject nasilOynanir;
    public GameObject duraklat;
    public bool oyunDurdu = false;

    public static uıController instance;



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
        
    }

    public void OyunuBaslat()
    {
        SceneManager.LoadScene(1);
    }

    public void OyundanCik()
    {
        Application.Quit();
    }

    public void NasılOynanir()
    {
        nasilOynanir.SetActive(true);
    }

    public void PaneliKapat()
    {
        nasilOynanir.SetActive(false);
        gameManager.instance.oyunBerabere.SetActive(false);
        gameManager.instance.durdurTusu.interactable = true;
    }

    public void OyunuDuraklat()
    {
        duraklat.SetActive(true);
        Time.timeScale = 0;
        oyunDurdu = true;
        
    }

    public void OyunuDevamEttir()
    {
        duraklat.SetActive(false);
        Time.timeScale = 1;
        oyunDurdu = false;
    }

    public void OyunuResetle()
    {
        SceneManager.LoadScene(1);
    }

    public void MenüyeDön()
    {
        SceneManager.LoadScene(0);
    }


}
