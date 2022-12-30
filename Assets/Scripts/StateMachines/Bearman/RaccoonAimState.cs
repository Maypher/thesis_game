using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/RaccooonAim")]
public class RaccoonAimState : State<BearmanCtrl>
{

    private BearmanAnimationHandler _animationHandler;
    private LineRenderer _trajectory;

    // How fast the aiming will rotate
    [SerializeField] private float _rotationSpeed = 45;
    
    // Initial trajectory position
    [SerializeField, Min(3)] private int _lineSegments = 60;
    [SerializeField, Min(1)] private int _length = 5;


    private bool _aiming;
    private bool _throwing;

    public override void Init(BearmanCtrl parent)
    {
        base.Init(parent);
        if (_animationHandler == null) _animationHandler = controller.AnimationHandler;
        if (_trajectory == null) _trajectory = parent.GetComponentInChildren<LineRenderer>();
        
        _trajectory.enabled = true;
        _trajectory.positionCount = 0;

        controller.launchPosition = parent.transform.Find("RaccoonLaunchPos").transform;
        controller.launchPosition.transform.localEulerAngles = Vector3.zero;

        _aiming = true;
        _throwing = false;

        _animationHandler.AimRaccoonAnimation(true);
    }

    public override void CaptureInput()
    {
        _aiming = Input.GetKey(KeyCode.Mouse1);
        _throwing = _aiming && Input.GetKeyUp(KeyCode.Mouse0);
    }
    public override void Update() 
    {
        controller.launchPosition.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * _rotationSpeed, 60));
        ShowTrajectoryLine(controller.launchPosition.position, controller.launchPosition.right * controller.throwForce / controller.raccoonMass);
    }
    
    public override void FixedUpdate() {}


    public override void ChangeState()
    {
        // Canceled throw by releasing right click
        if (!_aiming && !_throwing) controller.SetState(typeof(IdleState));
        // Threw by left clicking while aiming
        else if (_aiming && _throwing) controller.SetState(typeof(ThrowState));
    }

    public override void Exit() 
    { 
        _animationHandler.AimRaccoonAnimation(false);
        _trajectory.enabled = false;
    }

    // Shamelessly copied from https://www.youtube.com/watch?v=U3hovyIWBLk
    private void ShowTrajectoryLine(Vector3 startPos, Vector2 startVel)
    {
        // The more points the smoother the line
        float timestep = (float) _length / (float) _lineSegments;

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
}
