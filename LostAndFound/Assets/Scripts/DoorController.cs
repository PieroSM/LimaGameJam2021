using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool triggerCollision = false;
    [SerializeField] ItemDisplay itemRequired;
    [SerializeField] int numberOfItemsToOpen;
    [SerializeField] Sprite openDoorSprite;
    [SerializeField] DoorController otherSideDoor;
    bool interactable = true;
    Player player;
    // [SerializeField] GameObject Pivot;
    // [SerializeField] Animator doorAnimator;

    [SerializeField] AudioClip lockedSound;
    [SerializeField] [Range(0, 1)] float volumeLocked = 0.8f;
    [SerializeField] AudioClip unlockedSound;
    [SerializeField] [Range(0, 1)] float volumeUnlocked = 0.8f;

    AudioSource audioSource;
    bool finishedAudio = true;

    void Start()
    {
        // doorAnimator.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (triggerCollision && Input.GetKeyDown("e"))
        {
            player.AnimateUse();
            if (itemRequired.GetNumberOfItems() >= numberOfItemsToOpen)
            {
                PlayAudioWithWait(unlockedSound, volumeUnlocked);
                HandleOpenningDoor();
                if (otherSideDoor != null)
                {
                    otherSideDoor.HandleOpenningDoor();
                }
            }
            else
            {
                PlayAudioWithWait(lockedSound, volumeLocked);
            }
        }
    }

    void PlayAudioWithWait(AudioClip clip, float volume)
    {
        if (finishedAudio)
        {
            finishedAudio = false;
            StartCoroutine(ProcessAudio(clip, volume));
        }
    }

    IEnumerator ProcessAudio(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
        yield return new WaitForSeconds(clip.length);
        finishedAudio = true;
    }

    private void HandleOpenningDoor()
    {
        itemRequired.TakeItemsFromInventory(numberOfItemsToOpen);
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<DoorTeleport>().enabled = true;
        interactable = false;
        ChangeToOpenDoorSprite();
    }

    private void ChangeToOpenDoorSprite()
    {
        GetComponent<SpriteRenderer>().sprite = openDoorSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Player>() && interactable)
        {
            player = other.GetComponent<Player>();
            triggerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<Player>() && interactable)
        {
            triggerCollision = false;
        }
    }

    IEnumerator AnimateDoor()
    {
        if (transform.parent.localScale.x < 0)
        {
            transform.parent.Rotate(Vector3.forward, -90f * Time.deltaTime);    
            while (Mathf.Abs(transform.parent.rotation.eulerAngles.z) >= 270f)
            {
                transform.parent.Rotate(Vector3.forward, -90f * Time.deltaTime);
                yield return null;
            }
        }
        else if (transform.parent.localScale.x > 0)
        {
            transform.parent.Rotate(Vector3.forward, 90f * Time.deltaTime);
            while (Mathf.Abs(transform.parent.rotation.eulerAngles.z) < 90f)
            {
                transform.parent.Rotate(Vector3.forward, 90f * Time.deltaTime);
                yield return null;
            }
        }
        
    }

}
