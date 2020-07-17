
[Guide]

IMPORTANT: 	>>Make sure to drag the Inventory Canvas prefab and _Immortal_Crafting Manager prefab to the scene.

----Items:--------
			
	>>The Item script by default is not a Scriptable Object (so you can attach it to GameObjects), 
		 but you can easily make it an scriptable object.

----Crafting:-----
			
	>>I have impeltented a minecraft-style crafting system,
		Navigate to [ Window -> Crafting Recipe ] to open the crafting recipe window in order to create new Recipes visually 
		or you can manually create Recipes as Scriptable Object Assets from [Right Click in Assets folder -> Create -> Custom -> Crafting Recipe].

	>>The int fields next to the item slots in the Crafting Recipe window defines how many of that specific item is needed.

	>>This crafting system can take in multiple items as input in the recipe but it can only output one item, 
		You can easily change some code in the Crafting Manager and Crafting Recipe script to 
		output multiple items, however it will break the custom Crafting Recipe window unless
		you can modify the CraftingRecipeWindow script accordingly as well or  you will need to 
		manually make new Crafting recipes.

	>>If there are any null reference / key not found exceptions just fill in the public references in inspector according to the error as all the dependencies
		are includeed in the project.

------UI-------
	>>Inventory backgroung images are felxibly changable with no dependencies.
	>>Do not unparent or change the orders of any of the inventory slot childrens as it will break some of the hard coded functions.


[Info]
	>>Creator: Shariar Papon 
	>>Email: skpwork@outlook.com
	>>	Created: July 8th, 2020
	>>Original Version: Unity 2019.4.1f1 LTS (long time support)
	>>Scripts: CSharp
	>>UI Art Assets: Unity default UI sprites

	