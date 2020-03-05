ThirdPersonCameraPivot contains the camera object.

Camera object has 3 modes:

Locked Camera - Locked to and rotates with the player object (if any)

Detached Camera - Rotates freely around its own pivot point.

Hybrid Camera - Uses Locked Camera settings unless mouse button 0 is
held, at which point it uses Detached Camera settings.
Resets rotation to match player object if held button is released.


Only known cases of errors:

If a camera pivot is placed without a parent "Player" object of some kind
and either "Locked" or "Hybrid" cameras are used.

(It's attempting to access the parent to rotate it but there's no parent
to actually rotate).

Settings to chane:
RotationSpeed - How fast we rotate around.
InvertHorizontal / InvertVertical - inverts respective axis rotation controls.
MaximumBoomLength - Maximum length of camera boom arm.
MinimumBoomLength - Minimum length of camera boom arm.
CanZoom - Can the camera zoom in and out between minimum boom arm length
and maximum zoom available (maximum boom arm length unles wall is blocking)

MinimumRotationAngle - Minimum Angle camera can rotate.
MaximumRotationAngle - Maximum Angle camera can rotate.