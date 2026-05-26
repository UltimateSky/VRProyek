using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables; // Namespace 3.x
using UnityEngine.XR.Interaction.Toolkit.Interactors;    // Namespace 3.x

public class XRButton : XRBaseInteractable
{
    public Transform buttonTransform = null;
    public float pressDistance = 0.1f;
    public UnityEvent OnPress = new UnityEvent();
    public UnityEvent OnRelease = new UnityEvent();

    private float yMin, yMax;
    private IXRInteractor hoverInteractor = null; // Gunakan Interface
    private float hoverHeight, startHeight;
    private bool previousPress = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        hoverEntered.AddListener(StartPress);
        hoverExited.AddListener(EndPress);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        hoverEntered.RemoveListener(StartPress);
        hoverExited.RemoveListener(EndPress);
    }

    private void StartPress(HoverEnterEventArgs eventArgs)
    {
        // Di 3.x, gunakan interactorObject
        hoverInteractor = eventArgs.interactorObject;
        hoverHeight = GetLocalYPosition(hoverInteractor.transform.position);
        startHeight = buttonTransform.localPosition.y;
    }

    private void EndPress(HoverExitEventArgs eventArgs)
    {
        hoverInteractor = null;
        ApplyHeight(yMax);
    }

    private void Start() => SetMinMax();

    private void SetMinMax()
    {
        yMin = buttonTransform.localPosition.y - pressDistance;
        yMax = buttonTransform.localPosition.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && hoverInteractor != null)
        {
            float currentY = GetLocalYPosition(hoverInteractor.transform.position);
            float hoverDifference = hoverHeight - currentY;
            ApplyHeight(startHeight - hoverDifference);
        }
    }

    private float GetLocalYPosition(Vector3 position) => transform.InverseTransformPoint(position).y;

    private void ApplyHeight(float position)
    {
        Vector3 newPos = buttonTransform.localPosition;
        newPos.y = Mathf.Clamp(position, yMin, yMax);
        buttonTransform.localPosition = newPos;

        bool inPosition = buttonTransform.localPosition.y < (yMin + (pressDistance * 0.5f));
        if (inPosition != previousPress)
        {
            previousPress = inPosition;
            if (inPosition) OnPress.Invoke(); else OnRelease.Invoke();
        }
    }

    // WAJIB: Update signature agar tidak error Obsolete
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return false;
    }
}