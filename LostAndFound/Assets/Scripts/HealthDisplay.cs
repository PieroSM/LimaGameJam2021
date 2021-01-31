using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Image[] heartImages;


    public void DisableHeart(int index)
    {
        heartImages[index].enabled = false;
    }
}
