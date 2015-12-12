-

Theme: Two button controls and growing.

Tools
SourceTree
Unity 5.3.0f4


To open:
Run unity.
Open project
Select folder "<GIT_WORKING_FOLDER>\LUDUMDARE34\unityproject\LD34"
Press Open
**I had an issue where I had to open a sample project, and then open the project from the file menu

Ideas:
1. Literal binary tree.
	Pull Water resource from the root, and traverse the tree with Left / Right buttons to a leaf.
	Once at a leaf, the Water is spent and the leaf gains one or two children.
	If leaves are not watered, they start to wither. If a leaf if watered too much, it will terminate permanently in a Fruit instead of gaining children.
	The more Fruit, the more you win.

2. Speed-platformer.
	Character runs constantly forward in an endless level (either procedurally generated on-demand or cylinder).
	One button jumps, and one button enters Color Choose Mode.
	While in the air, the jump button does an air dodge and falls faster to the ground.
	In Color Choose Mode, both buttons are used to navigate a binary tree to select one of 16 colors / tags.
	When the character passes over a Taggable Spot, they tag it with their current Color. The goal is to match the Taggable Spot's desired Color with their selected Color.
	The character must jump to ensure collision with Taggable Spots in the air, or to avoid obstacles or Taggable Spots that they don't think they can tag correctly.