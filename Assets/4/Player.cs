using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    // The speed at which the player moves
    [SerializeField] private float Speed = 2;

    // Audio clips to play when the player is moving on different floor types
    [SerializeField] private AudioClip[] woodFloorClips;
    [SerializeField] private AudioClip[] metalFloorClips;
    [SerializeField] private AudioClip[] grassFloorClips;
    [SerializeField] private AudioClip[] noneFloorClips;

    // Indicates whether the player is moving
    private bool IsMoving { get; set; }
    public bool IsPlayingSoundRandomly { get; private set; }

    // The type of floor the player is currently on
    public FloorType CurrentFloor { get; private set; }

    // The AudioSource component used to play audio clips
    private AudioSource _audioSource;

    int _randomIndex;
    
    void Awake()
    {
        // Get the AudioSource component on this game object
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Update the player's position, IsMoving and CurrentFloor properties,
        // and play the appropriate floor audio
        Move();
        UpdateIsMoving();
        UpdateCurrentFloor();
        PlayFloorAudio();
        PlayFootStepsRandomly();
    }

    private void Move()
    {
        // Update the player's position based on the user's input
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += Speed * Time.deltaTime * direction;
    }

    private void PlayFootStepsRandomly()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            IsPlayingSoundRandomly = (IsPlayingSoundRandomly == true) ? false : true;
        }
    }

    private void UpdateIsMoving()
    {
        // Update the IsMoving property based on the user's input
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        IsMoving = direction.sqrMagnitude > 0.1f;
    }

    private void UpdateCurrentFloor()
    {
        // Update the CurrentFloor property based on the player's position
        CurrentFloor = GetFloorUnderneath();
    }

    private void PlayFloorAudio()
    {
        // If the player is moving, play the appropriate audio clip based on the CurrentFloor property
        if (IsMoving)
        {
            if (_audioSource.isPlaying) return;
            switch (CurrentFloor)
            {
                case FloorType.Wood:
                    if (IsPlayingSoundRandomly) _randomIndex = Random.Range(0, woodFloorClips.Length);
                    _audioSource.PlayOneShot(woodFloorClips[_randomIndex]);
                    break;
                case FloorType.Metal:
                    if (IsPlayingSoundRandomly) _randomIndex = Random.Range(0, metalFloorClips.Length);
                    _audioSource.PlayOneShot(metalFloorClips[_randomIndex]);
                    break;
                case FloorType.Grass:
                    if (IsPlayingSoundRandomly)_randomIndex = Random.Range(0, grassFloorClips.Length);
                    _audioSource.PlayOneShot(grassFloorClips[_randomIndex]);
                    break;
                case FloorType.None:
                    if (IsPlayingSoundRandomly) _randomIndex = Random.Range(0, noneFloorClips.Length);
                    _audioSource.PlayOneShot(noneFloorClips[_randomIndex]);
                    break;
            }
        }
    }

    private FloorType GetFloorUnderneath()
    {
        // Raycast downwards to determine the type of floor the player is currently on
        Ray ray = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(ray, out RaycastHit hit, 50))
            return FloorType.None;

        Floor floor = hit.collider.GetComponent<Floor>();
        if (floor == null)
            return FloorType.None;

        return floor.Type;
    }
}