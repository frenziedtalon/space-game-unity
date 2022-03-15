using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [Header("--- Ship Movement Settings ---")]
    [SerializeField]
    private float _yawTorque = 100f;
    [SerializeField]
    private float _pitchTorque = 100f;
    [SerializeField]
    private float _rollTorque = 100f;

    [SerializeField]
    private float _thrust = 100f;
    [SerializeField]
    private float _upThrust = 50f;
    [SerializeField]
    private float _strafeThrust = 50f;

    [Header("--- Boost Settings ---")]
    [SerializeField]
    private float _maxBoostAmount = 200f;
    [SerializeField]
    private float _boostDepreciationRate = 0.5f;
    [SerializeField]
    private float _boostRechargeRate = 0.15f;
    [SerializeField]
    private float _boostMultiplier = 10f;
    public bool _boosting; // TODO: Make private
    public float _currentBoostAmount; // TODO: Make private

    [SerializeField, Range(0.001f, 0.999f)]
    private float _thrustGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float _upDownGlideReduction = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float _leftRightGlideReduction = 0.111f;

    float glide = 0f;
    float verticalGlide = 0f;
    float horizontalGlide = 0f;

    Rigidbody rigidbody;

    // Input values
    private float thrust1D;
    private float upDown1D;
    private float strafe1D;
    private float roll1D;
    private Vector2 pitchYaw;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _currentBoostAmount = _maxBoostAmount;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleBoosting();
    }

    private void HandleMovement()
    {
        // Roll
        rigidbody.AddRelativeTorque(Vector3.forward * roll1D * _rollTorque * Time.deltaTime);
        // Pitch
        rigidbody.AddRelativeTorque(Vector3.right * Mathf.Clamp(pitchYaw.y, -1f, 1f) * _pitchTorque * Time.deltaTime);
        // Yaw
        rigidbody.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1f, 1f) * _yawTorque * Time.deltaTime);

        // Thrust
        if (thrust1D > 0.1f || thrust1D < -0.1f)
        {
            float currentThrust = _thrust;

            if (_boosting)
            {
                currentThrust = _thrust * _boostMultiplier;
            }
            else
            {
                currentThrust = _thrust;
            }

            rigidbody.AddRelativeForce(Vector3.forward * thrust1D * currentThrust * Time.deltaTime);
            glide = thrust1D;
        }
        else
        {
            rigidbody.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide *= _thrustGlideReduction;
        }

        // Up/Down
        if (upDown1D > 0.1f || upDown1D < -0.1f)
        {
            rigidbody.AddRelativeForce(Vector3.up * upDown1D * _upThrust * Time.deltaTime);
            verticalGlide = upDown1D * _upThrust;
        }
        else
        {
            rigidbody.AddRelativeForce(Vector3.up * verticalGlide * Time.deltaTime);
            verticalGlide *= _upDownGlideReduction;
        }

        // Strafing
        if (strafe1D > 0.1f || strafe1D < -0.1f)
        {
            rigidbody.AddRelativeForce(Vector3.right * strafe1D * _upThrust * Time.deltaTime);
            horizontalGlide = strafe1D * _strafeThrust;
        }
        else
        {
            rigidbody.AddRelativeForce(Vector3.right * horizontalGlide * Time.deltaTime);
            horizontalGlide *= _leftRightGlideReduction;
        }
    }

    private void HandleBoosting()
    {
        if (_boosting && _currentBoostAmount > 0f)
        {
            _currentBoostAmount -= _boostDepreciationRate;
            if (_currentBoostAmount <= 0f)
            {
                _boosting = false;
            }
        }
        else
        {
            if (_currentBoostAmount < _maxBoostAmount)
            {
                _currentBoostAmount += _boostRechargeRate;
            }
        }
    }

    public void OnThrust(InputAction.CallbackContext context)
    {
        thrust1D = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }
    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        _boosting = context.performed;
    }
}
