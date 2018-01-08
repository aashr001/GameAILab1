# GameAILab1

The topic that I chose for my Individual Project was Behaviour Trees as I found Lab 1 very interesting. I’ve implemented the Behaviour Trees on the tutorial game, Tanks! It uses NPBehave to write event driven behaviour trees. The main difference between a traditional behaviour tree and an event driven one is that in a traditional behaviour tree a frame is restarted from the root node each time and every traversal starts from the root node. In an event driven behaviour tree, a frame stays in its current state and a traversal occurs only when required.
So, keeping this concept in mind I’ve created a few Behaviour Trees, which depending on the type of node used, and the subsequent actions implemented, perform different behaviours. As Tanks! is a tutorial game, a lot of the code was already at hand. I only changed the Behaviours.cs file, and created all my Behaviour Trees there, and used the TankAI.cs file for methods for basic actions, like Fire, Shoot, Move. To make it easier for you to understand what exact changes I’ve made, the original Tanks! game code was uploaded on the GitHub repository, and any changes made after that were mine.
The main new concept that I applied in this assignment was the use of the distance between the two agents (tanks). I did this by making a new blackboard in the UpdatePerception() method. Two vectors were used, “targetPos” and “localPos”. The new blackboard “agentDistance” returns the distance and based on that different actions take place.
I made changes to 3 behaviour trees that I previously made in Lab 1. The details of what to expect from each type of behaviours are as follows:
1)	DeadlyBehaviour: The Non Player Character (NPC) agent tracks the other Player Character, and moves towards it at a constant speed. When it reaches a certain distance, it starts shooting at a certain velocity. The Player Character doesn’t get much time to react, and as it must turn towards the NPC in order to effectively shoot it, it almost always loses. This is a more sophisticated approach, as it doesn’t unnecessarily constantly shoot, allowing the other player a fair chance to attack as well. 
2)	FrightenedBehaviour: The NPC keeps track of the other Player Character, and as soon as it moves from behind the building and comes towards it, the NPC shifts into hibernate mode and backtracks. It doesn’t shoot or try to defend itself, it just focuses on moving away from its enemy. As soon as the other Player Character moves away, it tries to come out of its hiding place. So the NPC basically moves in and out of its hiding place, depending on how far away its enemy is. It’s an improvement from Lab 1, as it at least waits for the enemy agent to come closer, rather than just speeding backwards as soon as the game starts.
3)	CrazyBehaviour: As soon as the game starts, the NPC starts spinning in a clockwise direction and randomly starts shooting in different directions. When the other Player Character comes towards it, after covering a certain amount of distance, it suddenly just stops shooting and only spins on its position in a bigger circle. Then as soon as the Player Character moves away it goes loco again and starts spinning and shooting randomly. It’s crazy as when it needs to attack the other player (i.e. when they’re closer), it doesn’t and vice versa. 
To implement these behaviours, just change the relevant behaviour number in the Behaviour tab in the Game Manager (Script) in the Inspector window in Unity. The corresponding behaviour tree will start and execute the desired behaviour.

All the above mentioned behaviours are demonstrated in the video demo, and the link to it is:
https://www.youtube.com/watch?v=WHINT7teKQo&feature=youtu.be

The NPC agent is the red tank, and the blue tank is being controlled by myself. Firstly, I demonstrated the two basic behaviours that already come with Tanks! tutorial [1]. Next, every change in scene demonstrates a different type of behaviour, in the above order. As you can see, each behaviour pretty much does what is described above. Also, I used the TrackBehaviour() logic in the first two behaviours.

Citation:
[1] SpinBehaviour(float turn, float shoot) and TrackBehaviour() in Assets\_Completed-Assets\Scripts\Tank Behaviours.cs

Please follow this link in the GitHub URL for my work: GameAIProject/Assets/_Completed-Assets/Scripts/Tank/Behaviours.cs


 
