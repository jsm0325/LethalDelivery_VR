using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{

    [SerializeField]
    private float pickupRange = 2.0f;
    [SerializeField]
    private LayerMask itemLayer;

    public Camera mainCamera;


    public SteamVR_LaserPointer steamVR_LaserPointer;
    public SteamVR_Behaviour_Pose poseBehaviour;
    public SteamVR_Input_Sources handType;


    [Header("���� Ŭ��")]
    private AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;



    private bool btn_mode1 = false;
    private bool btn_mode2 = false;
    //public AudioClip fireSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // LaserPointer �̺�Ʈ ����
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn += OnPointerIn;
            steamVR_LaserPointer.PointerClick += OnPointerClick;
        }
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ���� ����
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn -= OnPointerIn;
            steamVR_LaserPointer.PointerClick -= OnPointerClick;
        }
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        /* ��� 1 */
        if (e.target.gameObject.CompareTag("btn_mode1") == true)
        {
            btn_mode1 = true;
            PlaySound(hoverSound);
        }
        else
        {
            btn_mode1 = false;
        }

        /* ��� 2 */
        if (e.target.gameObject.CompareTag("btn_mode2") == true)
        {
            btn_mode2 = true;
            PlaySound(hoverSound);
        }
        else
        {
            btn_mode2 = false;
        }

    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        if (btn_mode1 == true)
        {
            PlaySound(clickSound);
            SceneManager.LoadScene("OutMap");
        }
        else if (btn_mode2 == true)
        {
            PlaySound(clickSound);
            SceneManager.LoadScene("SciFi_Warehouse");
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void Update()
    {

    }

}
