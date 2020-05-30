@echo off
for %%a in ("%cd%") do set "CurDir=%%~nxa"

git pull --set-upstream origin master
git status
::git add .
set /p message=Commit Message: 
git commit -a -m "%message%"
git push --set-upstream origin master

cd /D D:\%CurDir%
git reset --hard master
git push thumbdrive master

cd /D E:\%CurDir%
git reset --hard master

cd /D D:\%CurDir%
git push origin master

pause