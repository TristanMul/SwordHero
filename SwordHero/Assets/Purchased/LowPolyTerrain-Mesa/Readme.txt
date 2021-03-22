LowPoly Terrain - Mesa (2020/12/12 - by Fatty War, id3644@gmail.com)

[Update V1.02 (Feb 17 2021)]
- Fort theme added. (See section 4)
Fort theme has been added to create more diverse terrain.

     -Mesa50 x 13 types.
     -Single ramp side parts added.(+1 ground, +1 grass, +1 snow, +1 lava, +1 fort)
     -Fort props added.(rampart, spire roof, window, barrel, Scaffold, Step Stool, Weapon Rack, StrawBale, Tarp)


[Update V1.01 (2021/1/29)]
- Lava theme added. (See section3)
Lava theme has been added to create more diverse terrain.

     -Mesa04 x 13 types.
     -Bg_RockCliff11.
     -Bg_RockCube14.
     -Bg_RockCube15.
     -Bg_SquareRock00Lava.

- Fixed Missing Prefabs in scene. (Assets\LowPolyTerrain-Mesa\Scenes\Demo_Mesa)

==This document explains how to use the pack.==

Introduction

    This package is a modular terrain that can be used to create cliff landscapes in the game.
    It is useful for small islands, arenas, stadiums, etc. It contains demo scenes with 50M arena and 100M terrain as in preview video.

     Features:
     * Easily assemble with drag and drop.
     * The cliff block is 1Mx1Mx1M and can be customized in various sizes. (1M = 1Unit/Unity)
     * You can connect cliffs using the ramp module.
     * Cliffs can be made with ground, grass, and snow themes and can be mixed. (Use decals on seams.)
     * Tree, stone, grass and mine rail props are included.
	
	Note).
	-All meshes have small UVs, so textures larger than 1px cannot be used.
	-All meshes used only one material & texture. Each mesh has its own UV for each color.

1. File Naming
     1 Slope. (Assets\LowPolyTerrain-Mesa\Prefabs\Slope)

	ex) B1x1Mesa00C00 (Size + Theme + Function)

	-Theme
	 1. Mesa00 = Ground.
	 2. Mesa01 = Grass.
	 3. Mesa02 = Snow.
	 4. Mesa04 = Lava. (V1.01 added)
	 5. Mesa50 = Stone Ground (V1.02 added)

	-Function
	 1. C = Corner side.
	 2. G = Ground.
	 3. H = H Shaped cliff side.
	 4. I = Cliff side.
	 5. Ramp = Cliff connection ramp. 
	 6. RampSide = Side connecting ramp. (single RampSide, RampSide L, RampSide R)
	 7. Sigle = A closed cliff.
	 8. Slope = Cliff connection ramp & Mesh Collider.
	 9. U = U Shaped cliff side.
	 
	 
 2. Scripts. (Assets\LowPolyTerrain-Mesa\Scripts)
	
	*Bullet, Mob, Potal, Shooter scripts
	   -Contains the script used in the preview scene, This is an ugly script and is not recommended.

 3. Lava theme (V1.01 added)
	The lava theme added this time consists of two versions for visual effects.
	Normal prefabs are in the existing path, glow prefabs are in the "Emission" folder.

	note)
	Glow Prefab was used in the example scene.
	Glow Prefab is effective when used with Post FX (there is no Post FX in the pack).
	Multi-materials use more resources than single materials.

	*Normal Prefab
	This is a prefab with no emission settings.

	*Glow Prefab
	This is a prefab with emission settings, the material of the stone and lava parts are separated.
	You can adjust the color and glow by adjusting the material of the disadvantaged lava part (ex. Lava can be changed to blue, yellow, green, etc. in addition to red).
	Glow prefab is in the "emission" folder.
	Glow prefabs are marked with the suffix (E)mission at the end.

4. Fort theme (V1.02 added)
	Build a fortress with added terrain and walls.

	Added fort props.
		rampart x2 (3x1, 1x1)
		spire roof x1.
		window x3 type
		barrel x1 (x3 variation)
		Broken barrel x1
		Scaffold x2 (1x1, 3x1ramp)
		Step Stool x1
		Weapon Rack x1
		Straw Bale x2 (1x1, 2x1)
		Tarp x1 (x5 color variation)
		Iron fence x9 (low, high)

	Added example scene.
		Demo_Mesa_FortNight - A night example scene using the fortress theme has been added.
		Display_Mesa102Fort - The props added in V1.02 are on display.

*If you have any questions or suggestions about the assets, please contact me.(id3644@gmail.com)
Thank you for your purchase.
