@echo off
for %%a in ("%cd%") do set "CurDir=%%~nxa"

echo Checking for changes in D:/%CurDir%...
git pull --set-upstream origin master
git status
echo Press any key to continue...
pause >nul
cls

echo Adding changes to commit...
git add .
git status
echo Press any key to continue...
pause >nul
cls

set /p message=Changes ready to commit. Please enter a Commit Message: 
echo Committing changes...
git commit -a -m "%message%"
echo Changes successfully committed! Press any key to continue...
cls

echo Pushing commit to D:/%CurDir%...
git push --set-upstream origin master
cd /D D:\%CurDir%
git reset --hard master
echo Commit successfully pushed to D:/%CurDir%! Press any key to continue...
pause >nul
cls

echo Pushing commit to E:/%CurDir%...
git push thumbdrive master
cd /D E:\%CurDir%
git reset --hard master
echo Commit successfully pushed to E:/%CurDir%! Press any key to continue...
pause >nul
cls

echo Pushing commit to https://www.github.com/TheXobrat/%CurDir%...
cd /D D:\%CurDir%
git push origin master
echo Commit successfully pushed to https://www.github.com/TheXobrat/%CurDir%! Press any key to exit...
pause >nul