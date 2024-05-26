using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using  TMPro;


public class SampahManager2D : MonoBehaviour
{
    public float waktuHilang = 2.0f; 

    public float jarakMaksimum = 2.0f;

    public int totalSampah = 2;

    private GameObject sampahTerpilih;
    private Animator animatorSampah;

    private Coroutine coroutineHilang;

    private List<GameObject> sampahTerkumpul = new List<GameObject>();

    public TextMeshProUGUI labelSampahText;
    public TextMeshProUGUI listSampahText;

    public Transform player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSampahClick();
        }
        else if (Input.GetMouseButton(0))
        {
            HilangkanSampahTerpilih();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (sampahTerpilih != null && coroutineHilang != null)
            {
                StopCoroutine(coroutineHilang);
                animatorSampah.SetFloat("FadeAmount", 0f); 
                sampahTerkumpul.Remove(sampahTerpilih);
            }

            sampahTerpilih = null;
            coroutineHilang = null;
        }

        
        UpdateSampahTerkumpulText();
    }

    void HandleSampahClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity);

        if (hit.collider != null)
        {
            GameObject objekTerkena = hit.collider.gameObject;

            if (objekTerkena.CompareTag("Sampah"))
            {
                float jarakDariPlayer = Vector2.Distance(objekTerkena.transform.position, player.position);

                if (jarakDariPlayer <= jarakMaksimum)
                {
                    sampahTerpilih = objekTerkena;
                    animatorSampah = objekTerkena.GetComponent<Animator>();
                }
                else
                {
                    
                }
            }
        }
    }
    void ResetSampah(GameObject sampah)
    {
        // reset animsi
        sampah.transform.position = sampah.transform.position; 
        sampah.transform.localScale = Vector3.one; 
    }

    void HilangkanSampahTerpilih()
    {
        if (sampahTerpilih != null)
        {
            float jarakDariPlayer = Vector2.Distance(sampahTerpilih.transform.position, player.position);

            if (jarakDariPlayer <= jarakMaksimum)
            {
                if (coroutineHilang == null)
                {
                    animatorSampah.SetFloat("FadeAmount", 1f); // Memulai animasi penghilangan
                    coroutineHilang = StartCoroutine(HilangkanSampah(sampahTerpilih));
                }
            }
            else
            {
                
                animatorSampah.SetFloat("FadeAmount", 0f);
                sampahTerpilih = null;
                coroutineHilang = null;
            }
        }
    }

    IEnumerator HilangkanSampah(GameObject sampah)
    {
        float waktuSisa = waktuHilang;
        while (waktuSisa > 0)
        {
            float jarakDariPlayer = Vector2.Distance(sampah.transform.position, player.position);

            if (jarakDariPlayer > jarakMaksimum)
            {
                // reset sampah jika jarak player terlalu jauh
                animatorSampah.SetFloat("FadeAmount", 0f);
                sampahTerkumpul.Remove(sampah);
                sampahTerpilih = null;
                coroutineHilang = null;
                yield break;
            }

            yield return null;
            waktuSisa -= Time.deltaTime;
        }

       
        sampahTerkumpul.Add(sampah);

        
        Destroy(sampah);

       
        animatorSampah.SetFloat("FadeAmount", 0f);

        sampahTerpilih = null;
        coroutineHilang = null;

        UpdateSampahTerkumpulText();
    }

    void UpdateSampahTerkumpulText()
    {
        if (listSampahText != null)
        {
            int jumlahSampahTerkumpul = sampahTerkumpul.Count;
            string textSampahTerkumpul;

            if (jumlahSampahTerkumpul < totalSampah)
            {
                textSampahTerkumpul = jumlahSampahTerkumpul + "/" + totalSampah;
            }
            else
            {
                textSampahTerkumpul = "Complete";
            }

            listSampahText.SetText(textSampahTerkumpul);
        }
    }
}