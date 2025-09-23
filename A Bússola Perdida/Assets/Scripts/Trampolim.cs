using UnityEngine;

public class Trampolim : MonoBehaviour
{
    [SerializeField] private float Mutiplier;

    //private Animator anim;
    private Player _player;

    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    public void PlayerJump()
    {
        _player.JumpFromTrampoline(Multiplier);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<Player>();
            //anim.SetTrigger("Jump");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player = null;
        }
    }
}
