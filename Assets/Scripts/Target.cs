using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Target : MonoBehaviour
{
    [SerializeField] VisualEffect _explosion;
    [SerializeField] TextMeshProUGUI score;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            _explosion.gameObject.SetActive(true);
            Destroy(gameObject);
            score.text = "Score: 20";
        }
    }


    void Start()
    {
        _explosion.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
