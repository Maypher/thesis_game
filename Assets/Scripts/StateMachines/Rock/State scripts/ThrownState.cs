using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Rock/Thrown")]
public class ThrownState : State<RockController>
{
    [Header("Damage")]
    [SerializeField] private float _damage;

    [Header("After throw")]
    [Tooltip("How long the component should stay visible after it's been thrown and has fully stopped moving")]
    [SerializeField] private float _lifetimeAfterThrow = 5;
    [SerializeField] private float _fadeTime = 3;

    private float _idleTime;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _sprite;

    public override void Init(RockController parent)
    {
        base.Init(parent);

        if (_rb == null) _rb = controller.GetComponent<Rigidbody2D>();
        if (_collider == null) _collider = controller.GetComponent<Collider2D>();
        if (_sprite == null) _sprite = controller.GetComponent<SpriteRenderer>();

        _idleTime = 0;

        AddForce(controller.ThrowForce, controller.Torque);
    }

    public override void CaptureInput() {}

    public override void ChangeState() {}

    public override void Exit() {}

    public override void FixedUpdate() {}

    public override void Update()
    {
        if (_rb.velocity == Vector2.zero)
        {
            // Disable all collision and physics simulation
            _rb.simulated = false;
            _collider.enabled = false;

            _idleTime += Time.deltaTime;
            if (_idleTime >= _lifetimeAfterThrow) controller.StartCoroutine(FadeOut());
        }
    }

    public void AddForce(Vector2 force, float torque)
    {
        // Activate the rigidbody and box collider before applying the force
        _rb.simulated = true;
        _collider.isTrigger = false;

        _rb.AddForce(force, ForceMode2D.Impulse);
        _rb.AddTorque(torque);
    }

    IEnumerator FadeOut()
    {
        float counter = 0;
        //Get current color
        Color spriteColor = _sprite.material.color;

        while (counter < _fadeTime)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / _fadeTime);

            //Change alpha only
            _sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
        Destroy(controller.gameObject);
    }
}
