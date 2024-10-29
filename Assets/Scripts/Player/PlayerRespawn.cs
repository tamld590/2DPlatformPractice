using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;
    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }
    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            Time.timeScale = 0;
            uiManager.GameOver();
            return;
        }
        RespawnLife();
    }
    private void RespawnLife()
    {
        playerHealth.Respawn(); //Restore player health and reset animation
        transform.position = currentCheckpoint.position; //Move player to checkpoint location

        //Move the camera to the checkpoint's room
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
        if (collision.transform.tag == "Finishline")
        {
            uiManager.FinishWin();
            return;
        }
    }
}
