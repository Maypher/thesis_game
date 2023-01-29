using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/RaccooonAim")]
public class RaccoonAimState : State<BearmanCtrl>, IAttack
{

    private BearmanAnimationHandler _animationHandler;
    private LineRenderer _trajectory;

    [Header("Aiming")]
    // How fast the aiming will rotate
    [SerializeField] private float _rotationSpeed = 45;
    
    // Initial trajectory position
    [SerializeField, Min(3)] private int _lineSegments = 60;
    [SerializeField] private int _length = 5;

    [Header("Throwing")]
    [SerializeField] private GameObject _raccoon;
    [SerializeField] private float _raccoonMass;
    [SerializeField] private float _throwForce;
    private Transform _launchPosition;


    private bool _aiming;
    private bool _throwing;
    private bool _thrown;
    private float _aimDirection;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;
        if (_trajectory == null) _trajectory = parent.GetComponentInChildren<LineRenderer>();
        if (_launchPosition != null) _launchPosition = parent.transform.Find("RaccoonLaunchPos").transform;
        if (_launchPosition == null) _launchPosition = controller.transform.Find("RaccoonLaunchPos");
        
        _trajectory.enabled = true;
        _trajectory.positionCount = 0;

        _launchPosition.transform.localEulerAngles = Vector3.zero;

        _aiming = true;
        _throwing = false;
        _thrown = false;
        _aimDirection = controller.AnimationHandler.FacingDirection;

        // If this is set to true within the inspector then the parent element's position is not calculated properly
        _trajectory.useWorldSpace = true;
        _animationHandler.AimRaccoonAnimation(true);
    }

    public override void CaptureInput()
    {
        _aiming = controller.UserInput.Player.RaccoonAim.IsPressed();
        _throwing = controller.UserInput.Player.Throw.WasPerformedThisFrame();
    }
   
    public override void Update() 
    {
        if (_aiming && !_throwing)
        {
            _launchPosition.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * _rotationSpeed, 60));
            ShowTrajectoryLine(_launchPosition.position, _launchPosition.right * _throwForce / _raccoonMass * _aimDirection);
        }

        //Placeholder while throw animation is made
        if (_throwing) Attack();
    }
    
    public override void FixedUpdate() {}


    public override void ChangeState()
    {
        // Canceled throw by releasing right click or threw raccoon
        if ((!_aiming && !_throwing) || _thrown) controller.SetState(typeof(IdleState));
    }

    public override void Exit() 
    { 
        _animationHandler.AimRaccoonAnimation(false);
        _trajectory.enabled = false;
        _trajectory.useWorldSpace = false;
    }

    // Shamelessly copied from https://www.youtube.com/watch?v=U3hovyIWBLk
    private void ShowTrajectoryLine(Vector3 startPos, Vector2 startVel)
    {
        // The more points the smoother the line
        float timestep = (float) _length / _lineSegments;

        Vector3[] lineRedererPts = CalculateTrajectoryLine(startPos, startVel, timestep);

        _trajectory.positionCount = _lineSegments;
        _trajectory.SetPositions(lineRedererPts);
    }

    private Vector3[] CalculateTrajectoryLine(Vector3 startPos, Vector2 startVel, float timestep)
    {
        Vector3[] lineRedererPts = new Vector3[_lineSegments];

        lineRedererPts[0] = startPos;

        for (int i=1; i < _lineSegments; i++)
        {
            float timeOffset = timestep * i;

            Vector3 progressBeforeGrav = startVel * timeOffset;
            Vector3 gravityOffset = -0.5f * Mathf.Pow(timeOffset, 2) * Physics2D.gravity.y * Vector2.up;
            Vector3 newPos = startPos + progressBeforeGrav - gravityOffset;
            lineRedererPts[i] = newPos;
        }

        return lineRedererPts;
    }

    public void Attack()
    {
        GameObject raccoon = Instantiate(_raccoon, position: _launchPosition.position, Quaternion.identity);
        Rigidbody2D raccoonRb = raccoon.GetComponent<Rigidbody2D>();

        raccoonRb.mass = _raccoonMass;
        raccoonRb.AddForce(controller.AnimationHandler.FacingDirection * _throwForce * _launchPosition.transform.right, ForceMode2D.Impulse);

        //Placeholder while throw animation is done
        FinishAttack();
    }

    public void FinishAttack() => _thrown = true;
}
