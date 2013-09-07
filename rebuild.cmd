"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv" /rebuild Debug %CD%\sf.sln
set bin=..\SpaceFight_bin

echo y | rmdir /S /Q %bin%

mkdir %bin%\server
mkdir %bin%\client

copy /Y Gunner\bin\Debug\*.* %bin%\client
copy /Y Pilot\bin\Debug\*.* %bin%\client
copy /Y Server\bin\Debug\*.* %bin%\server
echo y | del %bin%\client\*.vshost.*
echo y | del %bin%\server\*.vshost.*
