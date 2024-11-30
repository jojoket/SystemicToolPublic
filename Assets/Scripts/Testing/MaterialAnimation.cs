using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Sirenix.Utilities;

public class MaterialAnimation : MonoBehaviour
{
    public List<MaterialChangement> materialChangements;




    // Start is called before the first frame update
    void Start()
    {
        foreach (var materialChangement in materialChangements)
        {
            materialChangement.SetMaterialInitialState();
            if(materialChangement.StartAnimOnStart){
                TriggerAnimation(materialChangement);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var materialChangement in materialChangements)
        {
            materialChangement.SetMaterialBackToInitialState();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    [Button]
    public void TriggerAllAnimations()
    {
        foreach (var materialChangement in materialChangements)
        {
            if (materialChangement.IsInRythm)
            {
                
                return;
            }
            StartCoroutine(StartAnimation(materialChangement));

        }
    }

    [Button]
    public void TriggerAnimationByIndex(int index)
    {
        if (materialChangements[index].IsInRythm)
        {
            
            return;
        }
        StartCoroutine(StartAnimation(materialChangements[index]));

    }

    public void TriggerAnimation(MaterialChangement materialChangement)
    {
        if (materialChangement.IsInRythm)
        {
            
            return;
        }
        StartCoroutine(StartAnimation(materialChangement));

    }


    private IEnumerator StartAnimation(MaterialChangement materialChangement)
    {
        if(materialChangement.Countdown > 0)
            yield return new WaitForSeconds(materialChangement.Countdown);
        float startTime = Time.time;
        float endTime = Time.time + materialChangement.LerpDuration;

        if (materialChangement.IsMaterialInstance)
        {
            materialChangement.Material = materialChangement.Renderer.materials[materialChangement.MaterialIndex];
        }
        if (materialChangement.IsAutoStart)
        {
            materialChangement.SetMaterialStartValues();
        }
        float delta = 0;
        while (Time.time < endTime)
        {
            delta += Time.deltaTime / materialChangement.LerpDuration;
            materialChangement.ChangeMaterialFromDelta(delta);
            yield return 0;
        }
        if (materialChangement.HasSound)
        {
            
        }
    }


}
