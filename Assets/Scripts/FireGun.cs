using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour {

    [SerializeField] Transform firingOrigin;
    [SerializeField] float cooldown = 0.5f;

    RevolverController revController;
    private bool onCooldown;

    private List<SteamVR_TrackedController> touchingControllers;

    // Use this for initialization
    void Start()
    {
        touchingControllers = new List<SteamVR_TrackedController>();
        revController = GetComponent<RevolverController>();
        onCooldown = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("GameController"))
        {
            SteamVR_TrackedController controller = other.transform.GetComponent<SteamVR_TrackedController>();
            if (!touchingControllers.Contains(controller))
            {
                controller.PadClicked += OnControllerTouchpadClick;
                touchingControllers.Add(controller);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("GameController"))
        {
            SteamVR_TrackedController controller = other.transform.GetComponent<SteamVR_TrackedController>();
            if (touchingControllers.Contains(controller))
            {
                controller.PadClicked -= OnControllerTouchpadClick;
                touchingControllers.Remove(controller);
            }
        }
    }

    private void OnControllerTouchpadClick(object obj, ClickedEventArgs eventArgs)
    {
        Fire();
    }

    private void Fire()
    {
        if (onCooldown) return;
        else onCooldown = true;

        var a = GetComponentInChildren<AudioSource>();
        a.Stop();
        a.pitch = 1 + Random.Range(-0.1f, 0.1f);
        a.Play();

        // Haptic feedback on first touching controller (hacky...)
        if (touchingControllers.Count > 0) {
            var controller = touchingControllers[0];
            controller.TriggerHapticPulse();
        }

        RaycastHit rayHit;
        bool hitSomething = Physics.Raycast(firingOrigin.position, firingOrigin.forward, out rayHit);
        StartCoroutine(AnimateCylinder());

        if(hitSomething)
        {
            TargetHit target = rayHit.transform.GetComponent<TargetHit>();
            if (target != null) target.Bullseye();
        }
    }

    private IEnumerator AnimateCylinder()
    {
        revController.revRotSpeed = 60.0f / cooldown;
        revController.RotateCyl();
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
