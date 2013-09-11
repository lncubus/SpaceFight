set bin=..\SpaceFight_bin
set devenv="C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.com"
if not exist %devenv% {
set devenv="C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\devenv.com"
}
%devenv% /rebuild Debug %CD%\sf.sln

echo y | rmdir /S /Q %bin%

mkdir %bin%\server
mkdir %bin%\client

copy /Y Gunner\bin\Debug\*.* %bin%\client > nul
copy /Y Pilot\bin\Debug\*.* %bin%\client > nul
copy /Y Server\bin\Debug\*.* %bin%\server > nul
echo y | del %bin%\client\*.vshost.* > nul
echo y | del %bin%\server\*.vshost.* > nul
