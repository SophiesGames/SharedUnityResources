The general rule: Push/pull submodules before their main branch.
Submodules should be commited and pushed before main repo.
Submodules should be pulled and merged before main repo.


Pulling a main repo that has changes to the submodules before the branches are done results in the branhc breaking.
To reassociate it, go into it in SourceTree and right click the branch and checkout as master. 
Select from list if your on Mac and right click checkout as master. 
This will reverse the submodule changes that came when you pulled main branch.
Now you can update these seperately like you should have done first!


If a library is not being worked on it can be ignored but as it will be updated with main branch it might
cause user to think that its fine to edit. These edits want be able to push to the main branch. To do that 
the branch must be reverted (above paragraph). This will cause all changes to be lost.
Note: Quick test of the above issue showed not to be too much of an issue. Involved a few steps but got it 
back up to scratch. Did however, keep a lot of files i wnated deleted, possibly due to the order i choose to 
commit and pull. May cause issues with putting bad lines of code back into it.