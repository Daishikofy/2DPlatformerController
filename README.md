# 2D Platformer Controller in Unity using C#
This repository contains two different scripts for a 2D platfromer Player Controller in Unity. Both controllers allows you to move you character left and right at a custom speed and to controle it's direction mid-air.  
If you are interested in learning how you can use a **Rigidbody2D** to create a physic based controller, you can get a look on the first [PlayerController](https://github.com/Daishikofy/2DPlatformerController/blob/master/PlayerController.cs). If you just want to use a simple functional controller for your game, [PlayerController_2](https://github.com/Daishikofy/2DPlatformerController/blob/master/PlayerController_2.cs) is smoother, less buggy and allows multiples mid-air jumps. 

## Player Controller 1
Those are my tries in using **MovePosition** and **AddForce** to create a platformer controller. The main problem I encontered is that **MovePosition** does not give a value to **velocity** meaning there is no transition in speed between ground movements and air movements. Worst, the velocity of the player is eventually calculated when the character jumps meaning that the velocity stored on jump 1 will be used on jump 2 which is a probleme when the two jumps are in oposite directions.
For those reasons I had to use a small state machine which made everything more complicated then necessary, not smooth and buggy. It is still interesting to study to understand the behaviours of rigidbody2D **velocity, movePosition and addForce**.

## Player Controller 2
*__! You will need to create a physic material 2D with a friction of 0 and assign it to your character's rigidbody__*
Thise one is the script you should use for your game. It is kinda small and works pretty well. Here are the variables you can twick in the editor:

* **Speed:** The speed your character will have on the ground.
* **Jump Height:** The force of your character's jump, the higher the value, the higher te jump.
* **Extra Jumps :** Numbers of mid-air jumps the player posess.
* **Air Reactivity:** Value between 0 and 1
  * 0 means your character wont be able to change it's direction mid-air.
  * 1 means your character will be able to move mid-air as freely as on the ground.

The ground detection could be improve by having a specific collider used to detect whether your character is on a platform or not. This collider should be placed on the base of your sprite.

## Player Controller 3
*__Documentation in cnstruction__*
Added a ground reactivity to add some controle over the characteristics of the movimentation
Changed the way ground is detected or a more scripting newbie friendly version and more robuste to.

* **Speed:** The speed your character will have on the ground.
* **Jump Height:** The force of your character's jump, the higher the value, the higher te jump.
* **Extra Jumps :** Numbers of mid-air jumps the player posess.
* **Air Reactivity:** Value between 0 and 1
  * 0 means your character wont be able to change it's direction mid-air.
  * 1 means your character will be able to move mid-air as freely as on the ground.
* **Ground Reactivity:** Value between 0 and 1
  * 0 means your character wont be able to move at all.
  * 1 means your character will have really fast reactions with no acceleration nor decceleration time.
