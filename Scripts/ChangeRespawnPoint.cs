using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRespawnPoint : MonoBehaviour
{
    public GameObject spawnPos;
    public GameObject player;
    public GameControllerAnimated GameController;
    private GameData gameData;
    public float cameraFollowSpeed = 100; //maybe for really long rooms have a seperate collider for just this
    private int i = 0;
    public bool changeJumpForce = false;
    public float jumpForce;
    public int bodyLimit;
    private GameObject tempObj;
    private GameObject characterMesh;
    private Renderer characterRenderer;
    private float electricityEffectModifier = -1f;
    private float negative = -1f;
    public ParticleSystem smoke;

    private void Start()
    {
        bodyLimit = 9999;
        tempObj = GameObject.Find("GameController");
        GameController = tempObj.GetComponent<GameControllerAnimated>();
        gameData = tempObj.GetComponent<GameData>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && i == 0)
        {
            GameController.respawnPoint = spawnPos;
            gameData.exitRoom = true;
            //GameController.GetComponent<PerformanceController>().exitRoomPerf = true;
            player = GameController.spawnedPlayer;
            GameController.bodyLimit = bodyLimit;
            GameController.bodiesUsed = 0;

            //reimplement this code if we need to change how much the player can jump in certain places 
            //if (changeJumpForce)
            //{
            //    player.GetComponent<PlayerController>().jumpForce = jumpForce;
            //}

            i++; //iterator to make the spawn change only work once
        }
    }

    private void Update()
    {
        if (characterRenderer != null)
        {
            electricityEffectModifier = Mathf.Lerp(electricityEffectModifier, 0.5f, 0.01f);
            Debug.Log(electricityEffectModifier);
            //electricityEffectModifier *= negative;
            characterRenderer.material.SetFloat("_EmissionForHoles", electricityEffectModifier);
        }

        if (electricityEffectModifier > 0.4f && characterRenderer != null)
        {
            smoke.gameObject.transform.position = characterRenderer.transform.root.root.gameObject.transform.position;
            //Instantiate(smoke);
            Destroy(characterRenderer.transform.root.root.gameObject);
            characterRenderer = null;
            electricityEffectModifier = -1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // look at pick up body when you want to implement this
        //This is broken, cant acces top parent game object of the collider picked up
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerControllerAnimated>().heldObj != null)
            {
                characterMesh = other.gameObject.GetComponent<PlayerControllerAnimated>().heldObj;
                characterMesh = characterMesh.transform.root.gameObject;
                characterMesh = characterMesh.transform.Find("parent").gameObject;
                characterMesh = characterMesh.transform.Find("KS_CHaracter_Rig_GRP").gameObject;
                characterMesh = characterMesh.transform.Find("Character_Mesh").gameObject;
                characterMesh = characterMesh.transform.Find("CHarcter_Skin_Geo").gameObject;

                characterRenderer = characterMesh.GetComponent<Renderer>();
                other.gameObject.GetComponent<PlayerControllerAnimated>().DropObject();

            }
        }

        if (other.gameObject.CompareTag("canPickUp"))
        {
            characterMesh = other.gameObject.transform.root.gameObject;
            characterMesh = characterMesh.transform.Find("parent").gameObject;
            characterMesh = characterMesh.transform.Find("KS_CHaracter_Rig_GRP").gameObject;
            characterMesh = characterMesh.transform.Find("Character_Mesh").gameObject;
            characterMesh = characterMesh.transform.Find("CHarcter_Skin_Geo").gameObject;

            characterRenderer = characterMesh.GetComponent<Renderer>();

            //this was to make the player drop the ragdoll when only the ragdoll collides but this doesnt work
            //if (other.gameObject.GetComponent<PlayerControllerAnimated>().heldObj != null)
            //{
            //    other.gameObject.GetComponent<PlayerControllerAnimated>().heldObj = null;
            //}
        }

        if (other.gameObject.CompareTag("canPickUpObject"))
        {
            characterMesh = other.gameObject;
            Destroy(characterMesh);
            smoke.transform.position = other.transform.position;
            Instantiate(smoke);
        }
    }
}
