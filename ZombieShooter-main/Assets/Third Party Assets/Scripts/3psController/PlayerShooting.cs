using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public GameObject bulletHole;
    public LayerMask mask;
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    public ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    public Animator ani;

    void Awake ()
    {
        //shootableMask = LayerMask.GetMask ("Enemy");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light>();
    }
    private void OnEnable()
    {
        EventManager.Onclicked+=Cover;
    }
    private void OnDisable()
    {
        EventManager.Onclicked -= Cover;
    }
    void Cover()
    {
        Debug.Log("covered");
        ani.SetBool("Cover", true);
    }
    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }
       

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
       
    }
 
    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, mask))
        {
            if (shootHit.collider.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point , Quaternion.LookRotation(shootHit.normal));
              
            }
            else if (shootHit.collider.CompareTag("Untagged"))
            {
                Instantiate(bulletHole, shootHit.point , Quaternion.LookRotation(shootHit.normal));
                bulletHole.transform.up = shootHit.normal;
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
