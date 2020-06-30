# sutd-game-dev
SUTD 50.033 Foundations of Game Design and Development group 11

[Google Drive folder](https://drive.google.com/drive/folders/1Ud1KXlZMb4KVLvRn-s2iMxtHzPWAjqoB)

[Huanan's assets](https://raou.itch.io/dungeon-tileset-top-down-rpg/download/HN20XEELIig7SfJjNAmdglDbppSTP83Ty6HsTNIc)

Note: The gitignore has been updated to support subfolder support.
So you can put individual parts into their own folder without running into Unity file conflicts.

#### Example:
```
github_repo
│   README.md
│   .gitignore
│   LICENSE
|
└───level_design_folder
│   └───Assets
│   └───Library
│   └───obj
│   └───Packages
│   └───ProjectSettings
│   └─── ...
│   
└───player_interaction_folder
    └───Assets
    └───Library
    └───obj
    └───Packages
    └───ProjectSettings
    └─── ...
```

In the unlikely event that a Unity file conflict does occur and you committed the change, you can use the following command to remove the file from the staging area.
Note that this will not actually delete the file from your working tree.
```
git rm --cached <file>
```
