# Last of Us VR Prototype

This repository contains a lightweight Unity XR prototype inspired by a gritty first-person survival-horror VR experience for Meta Quest 3.

Features included in this prototype:
- First-person VR camera rig
- Controller-driven hand movement
- Grab and interact logic
- Weapon draw/store system
- Simple holster attachment
- Quick pickup interactions for items and weapons

This is not a full commercial clone of a game. It is an original prototype designed to capture the feel of a tense survival VR scene.

## Requirements
- Unity 2022.3 LTS or newer
- XR Interaction Toolkit
- Meta XR / Oculus integration for Quest 3
- Input System package enabled

## Setup
1. Create a new Unity project or open this repo as a Unity project.
2. Install `XR Interaction Toolkit` via Package Manager.
3. Install `Input System` if not enabled.
4. Create a `Player` object with:
   - `CharacterController`
   - `Camera`
   - `XR Origin` / `XRRig`
   - Left and right controllers
5. Attach `VRPlayerRig` to the player root.
6. Attach `VRHandController` to each hand.
7. Attach `WeaponHolster` to a weapon anchor point.
8. Add `XRGrabInteractable` to any pickupable asset and `InteractablePickup` to the same object.

## Controls
- Left/Right trigger: grab or interact
- Right thumbstick: move in VR space (if mapped)
- Weapon draw/store: toggle via script call or button binding
- Interaction objects: can be tagged as weapon or generic item

## Main scripts
- `VRPlayerRig.cs`
- `VRHandController.cs`
- `InteractablePickup.cs`
- `WeaponHolster.cs`

## Notes
This repository is intentionally a functional starting point for a VR prototype. You can expand it with:
- real melee animation
- enemy AI
- item inventory
- UI and pause menu
- quest flow
- static and dynamic lighting

