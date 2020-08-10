## Git notes for the team brought over from previous README


### Git command workflow for working in /main
1. Switch to master with `git checkout master`
1. Get latest commits with `git pull`
1. Checkout your own branch with `git checkout -b my-branch`
1. Make your changes in the main folder. Try as far as possible to only make changes to your scene.
1. While still in your branch, use `git pull origin master`. This pulls the latest changes on the master branch from github and tries to merge it with your local branch. 
1. Alternatively, if you want to look around at what changes have occured in master, use `git fetch origin` first. From here you can see all branches using `git branch --all`. To look around the changes on the remote master, use `git checkout origin/master`. To merge, use `git merge origin/master` while in your branch.
1. From here, the easiest way I found to resolve merge conflicts is to use visual studio code (one click resolution of merge conflicts, much better than GitHub's)
1. If you find you have more conflicts than lines of code and want to abort the merge, use `git merge --abort`

[useful guide on this](https://stackoverflow.com/questions/20101994/git-pull-from-master-into-the-development-branch)


Note: The gitignore has been updated to support subfolder support.
So you can put individual parts into their own folder without running into Unity file conflicts.

#### Example:
```
github_repo
│   README.md
│   .gitignore
│   LICENSE
|
└───main
│   └───Assets
│   └───Library
│   └───obj
│   └───Packages
│   └───ProjectSettings
│   └─── ...
|
└───player_controls
│   └───Assets
│   └───Library
│   └───obj
│   └───Packages
│   └───ProjectSettings
│   └─── ...
│   
└───test_monster
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
