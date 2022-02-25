# Pause System - Demonstration and Tests

This project demonstrates a viable simple pause system for games, that don't depend on the 
Time.timeScale property.

This project was based on the micro-game at https://github.com/DiogoDeAndrade/CBS_Platformer.

![Image](https://github.com/DiogoDeAndrade/CBS_Platformer/raw/master/Screenshots/screen01.png)

The minigame is a simple platformer game, built mainly on composition of components, using very little inheritance.

This uses a submodule, so either pull recursively with 
- git clone --recursive https://github.com/DiogoDeAndrade/CBS_Platformer, 

or after cloning the repo, use 
- git submodule init
- git submodule update

## Why avoid Time.timeScale = 0

There are several reasons to avoid Time.timeScale = 0 as a pause system.

* It's indiscriminate: it pauses everything, whatever we want it or not. There is no control on what elements get paused on not, which may be necessary in certain situations.
  * For example, in multiplayer games, we may want the pause to pop up a menu and stop player control, while allowing for remote players to continue moving without any limitations
* In some circumstances, when collisions are happening at the exact moment Time.timeScale = 0 is called, when time resumes it may lead to errors, leading to objects flying off screen and other such behaviour
* It's difficult to combine with other effects that also change the time scale (special effects like bullet time, etc)

## Usage

The PauseManager should be used as a singleton object (there's no code to force this, since everybody has their own way to enforce it.
Just create an empty GameObject and add it the PauseManager Monobehaviour to it. The current version of the PauseManager will pause the game when "P" is pressed, but this should be adapted to your own game. A GameObject that can be used as a panel for a pause menu and such can also be referenced to be displayed when the pause is turned on.

Then, all the objects that have to be paused when there's a pause event should have a PauseController Monobehaviour.
Under the PauseController there's an "Autofill" button. This adds all the components in the current GameObject to the components that have to be enabled/disabled when pause is toggled. Check the generated list of components afterwards. For example, it's normal to want to add Animator components to that list, but usually you don't want colliders to be added (but they will be auto-added as well, since they are Behaviours). You can edit the list after the auto-fill.

There's also a "Disable Physics" toggle, which defaults to true. If toggled, the Pause will include any rigidbody (2d and 3d) components on the GameObject. Rigidbody components have to be enabled/disabled in different ways than general components (see tech description below).

You can also use the PauseManager in a different way, by hooking a callback into the PauseManager.onPaused event, for more complex work with the pause (for example, if you want better decoupling).

## Tech overview

The PauseController searches for a PauseManager on the scene. If it doesn't find one, it will delete the component, since it doesn't have a place to hook itself.
After finding the PauseController, it will register the OnPause callback function with the PauseController, so that it gets called when the state changes. When the PauseController is destroyed, OnDestroy is responsible for removing the OnPause function from the PauseManager.onPause event.

When pause is toggled, all the functions registered are called with a boolean stating if we're pausing or unpausing.

What the implementation of OnPause on the PauseController does is:
* Store the state of all the components that are marked for pausing, and disables them.
* If disable physics is active, it will pause the controllers.
  * To pause a 2d rigidbody, we just need to set Rigidbody2d.simulated to false, and reactivate it in the end. Previous state is preserved when pause is toggled off, so that we don't activate a rigidbody that was already turned off when pause was pressed.
  * To pause a 3d rigidbody, the process is a bit more complex. Velocity (linear and angular) get recorded and the rigidbody becomes kinematic (so physics are not handled by Unity anymore). When unpaused, the velocities get restored to their previous state, we set the kinematic state to what it was when pause was triggered, and then we call Rigidbody.WakeUp(), to guarantee that the rigidbody indeed wakes up without application of forces, etc.

## Improvements or variations

The system can be changed in other ways to suit the game architecture better.

* The PauseManager could do a FindObjectsOfType<PauseController> instead of relying on registration. I don't like this system that much because it becomes a bit indiscriminate and we can't flag objects to not be paused at runtime (that we can currently).
* The previous solution pairs nicely with using an interface class like IPause to handle the pausing without requiring registration and handling special cases.
* PauseManager could be handled as a pure static class (not a Monobehaviour) and then we didn't have to depend on having a PauseManager on the scene.

## Credits

* Code done by [Diogo Andrade]
* [NaughtyAttributes] by Denis Rizov 
* Character by [IMakeGames]

## Licenses

All code in this repo is made available through the [GPLv3] license.
The text and all the other files made by [Diogo Andrade] are made available through the [CC BY-NC-SA 4.0] license.
The rest are covered by their own licenses!

## Metadata

* Autor: [Diogo Andrade][]

[Diogo Andrade]:https://github.com/DiogoDeAndrade
[GPLv3]:https://www.gnu.org/licenses/gpl-3.0.en.html
[CC-BY-SA 3.0.]:http://creativecommons.org/licenses/by-sa/3.0/
[CC BY-NC-SA 4.0]:https://creativecommons.org/licenses/by-nc-sa/4.0/
[NaughtyAttributes]:https://github.com/dbrizov/NaughtyAttributes
[IMakeGames]:https://opengameart.org/users/imakegames
