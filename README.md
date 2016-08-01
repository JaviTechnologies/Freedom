# Freedom

This project is a protoype of an air battle ship game that includes:
* MVC architecture
* Player's ship controlled by keyboard on PC and touch in mobile
* Enemy ships spawned in configurable formations
* Bullets
* Scrolling level generation
* Saving player's data
* Recycling objects

## Class Diagram ##
The architeture was intended to be MVC, meaning that all the game logic occurs in the domain, not the view. Domaing componenets use View components to update the visual of the game.
![Alt text](ReadmeResources/Freedom_ClassDiagram.jpg?raw=true "Class Diagram")

## Next Steps ##
* Change mobile input strategy in order to use a joystick instead of touch (more user friendly)
* Implement data model (maybe with ScriptableObjects) to control model's configuration in a better way.
* Add VFX
* Implement more enemy movements
* Profile and optimize code
