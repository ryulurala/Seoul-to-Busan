﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaveDefault : MonoBehaviour
{
    [SerializeField]
    GameObject caveNumberList= null;

    [SerializeField]
    GameObject caveNumberPrefab = null;
    GameObject caveNumber;

    private GameObject personManager;

    [SerializeField]
    private int defaultPopulation;

    [SerializeField]
    private int maxPopulation;

    public int population;

    [Range(1.0f, 8.0f)]
    [SerializeField]
    private float increaseSpeed;

    public bool isDefault;
    private bool isDestroy = false;

    Camera mCamera;

    // Start is called before the first frame update
    void Start()
    {
        isDefault = true;
        mCamera = Camera.main;
        caveNumber = Instantiate(caveNumberPrefab, this.transform.position, Quaternion.identity, caveNumberList.transform);
        caveNumber.GetComponent<BarController>().maxNumber = maxPopulation;
        personManager = GameObject.Find("Person");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefault)
        {
            
            if (population > defaultPopulation)
            {
                population -= defaultPopulation;
                isDefault = false;
            }
            else
            {
                defaultPopulation -= population;
            }
            caveNumber.GetComponentInChildren<Text>().text = defaultPopulation.ToString();
            caveNumber.GetComponentInChildren<Text>().color = Color.white;
            caveNumber.transform.position = mCamera.WorldToScreenPoint(this.transform.position + new Vector3(0, 0.6f, 0));
        }
        else if (!isDefault)
        {
            if (population < maxPopulation)
            {
                population += (int)(10.0f * increaseSpeed * Time.deltaTime);

                caveNumber.GetComponentInChildren<Text>().text = population.ToString();
                caveNumber.GetComponentInChildren<Text>().color = Color.cyan;
                caveNumber.transform.position = mCamera.WorldToScreenPoint(this.transform.position + new Vector3(0, 0.6f, 0));
            }
        }

        // 충돌하면 3초후 Destroy
        if (!isDestroy)
        {
            StartCoroutine("DestroyCharacter");
        }

    }

    // 일정 시간 후 resource, dest UI clear 및 Destroy Character
    IEnumerator DestroyCharacter()
    {
        isDestroy = true;
        yield return new WaitForSeconds(3f);
        //Debug.Log("3초후");

        /////////2초 후 일어날 일 Destroy(person), Destroy(personTransform), Destroy(mNumberList)

        isDestroy = false;
    }
}
