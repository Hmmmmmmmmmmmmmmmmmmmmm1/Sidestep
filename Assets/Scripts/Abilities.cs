using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Assets.Scripts.CharacterControl;
using UnityEngine.Rendering.PostProcessing;

public class Abilities : MonoBehaviour
{
    //Movement

    //direction change      :)
    //blink                 :)
    //launch                :)
    //temp sturctures       :)
    //healing               :)

    //Battle

    //lifesteal
    //immobiliazaiton
    //fire aspect II
    //knockback
    //"DDOS"

    public enum MovementAbility { DirectionChange, Blink, Launch, LegoBuildMode, Heal }
    public static int Skill1;
    public KeyCode Skill1Trigger;

    public enum BattleAbility { Lifesteal, Immobilize, FireAspectII, Knockback, doxx }
    public static int Skill2;
    public KeyCode Skill2Trigger;

    private bool activated;

    public Text Skill1Text;
    public Text Skill2Text;
    private float Skill1Cooldown = 0;
    private float Skill2Cooldown = 0;

    //Lunch

    //Lego
    private int legoesPerAbility = 0;
    private GameObject legos;

    //lightspeed
    private Vector3 boxSize;
    private Ray ray;
    private float blinkDistance = 20;

    //Lymphoma
    private float timer;
    private float healInterval = 0f;

    //fireaspect
    private float evilTimer;
    private float burnTimer;
    private float dmgInterval = 0f;
    private Material originalMat;
    public bool fireActive = false;
    public bool burned = false;

    //knockback
    public bool knockActive = false;

    //lifesteal
    private float evilerTimer;
    private float vampTimer;
    public bool vampActive = false;

    //slow
    private Camera cam;
    private float slowTimer;
    //This timer is identical to a regular timer, but you need to doulbe the time because it stores time at 1/2x speed. (1/2)x not 1/(2x). x/2.
    private Vignette vignette;
    private ColorGrading colorGrading;

    //slow
    private float blindTimer;

    //general
    public float speedMultiplier;

    private PhotonView PV;

    //Classism
    public float classRegen;
    public float classPower;
    public GameObject ClassObject;
    private Classism classism;

    // Start is called before the first frame update
    void Start()
    {
        legos = Resources.Load<GameObject>("Wall");
        boxSize = gameObject.GetComponent<Collider>().bounds.size / 1.75f;
        originalMat = GetComponent<Renderer>().material;
        PV = gameObject.GetComponent<PhotonView>();

        if (transform.Find("Camera(Clone)")){
            cam = transform.Find("Camera(Clone)").GetComponent<Camera>();
            //vignette = cam.gameObject.GetComponent<PostProcessVolume>().profile.GetComponent<Vignette>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();
        CheckClass();
        SpeedCheck();
        slowDHigh();
        blindDHigh();

        //Lunch Mode
        if (Skill1 == 1)
        {
            if (Input.GetKeyDown(Skill1Trigger))
            {

                int lookingDown;
                if (Skill1Cooldown < 1)
                {
                    if ((Vector3.Dot(transform.Find("Camera(Clone)").forward, Vector3.up) + 1) * 9 > 1 - Vector3.Dot(transform.Find("Camera(Clone)").forward, Vector3.up))
                    {
                        lookingDown = 1;
                    }
                    else
                    {
                        lookingDown = -1;
                    }
                    gameObject.GetComponent<Rigidbody>().velocity = (Vector3.up * lookingDown * 100 + transform.forward * 20) * speedMultiplier;

                    gameObject.GetComponent<Rigidbody>().drag = 5;
                    activated = true;
                    Skill1Cooldown = 5 * classRegen;
                }

            }
            if (gameObject.GetComponent<Rigidbody>().velocity.y >= 20)
            {
                gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0, -3, 0);
            }

            if (gameObject.GetComponent<Rigidbody>().velocity.y <= 10 && activated)
            {
                //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,gameObject.GetComponent<Rigidbody>().velocity.y,0);
                gameObject.GetComponent<Rigidbody>().drag = 0;
                activated = false;
            }
        }
        //Legos Mode
        if (Skill1 == 2)
        {
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1)
            {
                //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                ray = transform.Find("Camera(Clone)").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                GameObject lego = PhotonNetwork.Instantiate("Wall", ray.GetPoint(20 * speedMultiplier), legos.transform.rotation);
                lego.GetComponent<SelfDestruct>().expire = (int)(lego.GetComponent<SelfDestruct>().expire * speedMultiplier);
                lego.transform.localScale *= (int)Math.Pow(speedMultiplier, 2);
                legoesPerAbility++;
                if (legoesPerAbility == 5)
                {
                    legoesPerAbility = 0;
                    Skill1Cooldown = 10 * classRegen;
                }
            }
        }
        //Lightspeed Mode
        if (Skill1 == 3)
        {
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1)
            {
                //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                ray = transform.Find("Camera(Clone)").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                //Debug.DrawLine(ray.origin,ray.GetPoint(grub),Color.cyan,100f);
                Collider[] slowTargets = Physics.OverlapBox(ray.GetPoint(blinkDistance), boxSize, Quaternion.identity);
                //Debug.Log(slowTargets.Length);
                while (slowTargets.Length > 0)
                {
                    blinkDistance -= 1;
                    slowTargets = Physics.OverlapBox(ray.GetPoint(blinkDistance), boxSize, Quaternion.identity);
                    //Debug.Log(slowTargets.Length);
                }
                gameObject.transform.position = ray.GetPoint(blinkDistance);
                blinkDistance = 20 * speedMultiplier;
                Skill1Cooldown = 3 * classRegen;
            }
        }
        //Lapse in Judgement Mode
        if (Skill1 == 4)
        {
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1)
            {
                //gameObject.GetComponent<Rigidbody>().velocity *= -1;
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x * -1, gameObject.GetComponent<Rigidbody>().velocity.y, gameObject.GetComponent<Rigidbody>().velocity.z * -1) * speedMultiplier / 2;
                Skill1Cooldown = 3 * classRegen;
            }
        }
        //Lymphoma Mode
        if (Skill1 == 5)
        {
            if (timer > 0)
            {
                if (healInterval > 0)
                {
                    healInterval -= Time.deltaTime;
                }
                if (healInterval <= 0)
                {
                    gameObject.GetComponent<PlayerHP2>().changeHealth(1);
                    healInterval = 0.65f;
                }
            }
            timer -= Time.deltaTime;
            if (Input.GetKeyDown(Skill1Trigger) && Skill1Cooldown < 1)
            {
                if (gameObject.GetComponent<PlayerHP2>().hp < 100)
                {
                    timer = 7.5f * speedMultiplier;
                    Skill1Cooldown = 9 * classRegen;
                }
            }
        }

        //burn Mode
        if (Skill2 == 1 && gameObject.name.Equals("Player 1"))
        {
            if (evilTimer < 0){
                PV.RPC("combatAbilityOn",RpcTarget.All,true);
                fireActive = false;
            }
            evilTimer -= Time.deltaTime;
            if (Input.GetKeyDown(Skill2Trigger) && Skill2Cooldown < 1)
            {
                PV.RPC("combatAbilityOn",RpcTarget.All,false);
                fireActive = true;
                evilTimer = 10f * speedMultiplier;
                Skill2Cooldown = 10 * classRegen;
            }
        }
        //punch II bow mode
        if (Skill2 == 2 && gameObject.name.Equals("Player 1"))
        {
            if (!knockActive){
                PV.RPC("combatAbilityOn",RpcTarget.All,true);
            }
            if (Input.GetKeyDown(Skill2Trigger) && Skill2Cooldown < 1)
            {
                knockActive = true;
                Skill2Cooldown = 4 * classRegen;
                PV.RPC("combatAbilityOn",RpcTarget.All,false);
            }
        }
        //bite Mode
        if (Skill2 == 3 && gameObject.name.Equals("Player 1"))
        {
            if (evilerTimer < 0){
                PV.RPC("combatAbilityOn",RpcTarget.All,true);
                vampActive = false;
            }
            evilerTimer -= Time.deltaTime;
            if (Input.GetKeyDown(Skill2Trigger) && Skill2Cooldown < 1)
            {
                PV.RPC("combatAbilityOn",RpcTarget.All,false);
                vampActive = true;
                evilerTimer = 10f * speedMultiplier;
                Skill2Cooldown = 100 * classRegen;
            }
        }
         //slow Mode
        if (Skill2 == 4 && gameObject.name.Equals("Player 1"))
        {
            if (Input.GetKeyDown(Skill2Trigger) && Skill2Cooldown < 1)
            {
                Skill2Cooldown  = 3f * classRegen;

                Collider[] slowTargets = Physics.OverlapCapsule(transform.position + (transform.forward * 10), transform.position + (transform.forward * 100), 2f);
                foreach (Collider x in slowTargets){
                    if (x.gameObject.GetComponent<Abilities>()){
                        Skill2Cooldown = 9 * classRegen;
                        x.transform.GetComponent<Abilities>().slowDown();
                        //Debug.Log((transform.position + (transform.forward * 10)) + "   and    " + (transform.position + (transform.forward * 100)) + "    beam");
                    }
                }
            }
        }
         //blind Mode
        if (Skill2 == 5 && gameObject.name.Equals("Player 1"))
        {
            if (Input.GetKeyDown(Skill2Trigger) && Skill2Cooldown < 1)
            {
                Skill2Cooldown  = 3f * classRegen;

                Collider[] blindTargets = Physics.OverlapCapsule(transform.position + (transform.forward * 10), transform.position + (transform.forward * 100), 2f);
                foreach (Collider x in blindTargets){
                    if (x.gameObject.GetComponent<Abilities>()){
                        Skill2Cooldown = 9 * classRegen;
                        x.transform.GetComponent<Abilities>().blindDown();
                        //Debug.Log((transform.position + (transform.forward * 10)) + "   and    " + (transform.position + (transform.forward * 100)) + "    beam");
                    }
                }
            }
        }
    }

    public void Cooldown()
    {
        Skill1Cooldown -= Time.deltaTime;
        Skill2Cooldown -= Time.deltaTime;

        Skill1Text.text = Skill1Cooldown.ToString().Substring(0, 1);
        Skill2Text.text = Skill2Cooldown.ToString().Substring(0, 1);

        if (Skill1Text.text.Equals("0"))
        {
            Skill1Text.text = "-";
        }
        if (Skill2Text.text.Equals("0"))
        {
            Skill2Text.text = "-";
        }
    }
    public void SpeedCheck()
    {
        speedMultiplier = (float)(Math.Pow(Speedometer.currentSpeed + 1, 1 / 7f));
        speedMultiplier *= classPower;
    }
    public void CheckClass()
    {
        ClassObject = GameObject.Find("Classes");
        classism = ClassObject.GetComponent<Classism>();
        if (classism.wizard == true)
        {
            classRegen = 0.75f;
            classPower = 1.25f;
        }
        else if (classism.assassin == true)
        {
            classRegen = 1.25f;
            classPower = 1.25f;
        }
        else 
        {
            classRegen = 1f;
            classPower = 1f;
        }
    }

    [PunRPC]
    void combatAbilityOn(bool changeToOld)
    {
        if (changeToOld){
            transform.Find("Sword Holder/Sword").GetComponent<MeshRenderer>().material = originalMat;
        }
        else{
            transform.Find("Sword Holder/Sword").GetComponent<MeshRenderer>().material = transform.Find("Marker").GetComponent<MeshRenderer>().material;
        }
    }

    public void knockedBack (Vector3 velocityFromHit){
        PV.RPC("knockedBackRPC",RpcTarget.All,velocityFromHit);
    }

    [PunRPC]
    void knockedBackRPC(Vector3 velocityFromHit)
    {
        gameObject.GetComponent<Rigidbody>().velocity += velocityFromHit * 2;
    }

    public void slowDHigh(){
        if (slowTimer < 0)
            {
                gameObject.GetComponent<Rigidbody>().mass = 1;
                gameObject.GetComponent<GrapplingHook>().enabled = true;

                if (vignette) vignette.intensity.value = 0;
                if (colorGrading) colorGrading.saturation.value = 0;
            }
            else{
                if (colorGrading) colorGrading.saturation.value = 4 * (10-slowTimer) - 60;
            }

        slowTimer -= Time.deltaTime;
    }

    public void slowDown (){
        PV.RPC("slowDownRPC",RpcTarget.All);
    }

    [PunRPC]
    void slowDownRPC()
    {
        gameObject.GetComponent<Rigidbody>().mass = 2.5f;
        gameObject.GetComponent<GrapplingHook>().enabled = false;

        if (cam){
            cam.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out vignette);
            vignette.intensity.value = 0.4f;

            cam.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out colorGrading);
            colorGrading.saturation.value = -60;
        }

        slowTimer = 6;
    }

    public void blindDHigh(){
        if (blindTimer < 0)
            {
                if (colorGrading) colorGrading.postExposure.value = 0;
            }
        else{
            if (colorGrading) colorGrading.postExposure.value = -0.45f * (10 - blindTimer) + 4.5f;
        }

        blindTimer -= Time.deltaTime;
    }

    public void blindDown (){
        PV.RPC("blindDownRPC",RpcTarget.All);
    }

    [PunRPC]
    void blindDownRPC()
    {
        if (cam){
            cam.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out colorGrading);
        }
        blindTimer = 10;
    }
}