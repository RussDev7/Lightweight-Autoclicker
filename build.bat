:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
::Install Spammer Via MSBuild                                    ::
::Gihub https://github.com/RussDev7/Lightweight-Autoclicker      ::
::Developed, Maintained, And Sponsored By D.RUSS#2430            ::
:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
@ECHO OFF

Rem | Set Params
Set "VersionPrefix=1.0.8"
Set "filename=ClickSpammer-%VersionPrefix%"

Rem | Install SLN Under Any CPU Profile
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe "Click Spammer.sln" /p:Configuration=Release /p:Platform="Any CPU"

Rem | Delete Paths & Create Paths
rmdir /s /q "release"
mkdir "release"

Rem | Copy Over Items
xcopy /E /Y "WindowsApp2\bin\Release" "release\%filename%\"

Rem | Clean Up Files
del /f /q /s "release\*.xml"
del /f /q /s "release\*.pdb"
del /f /q /s "release\*.config"

Rem | Delete & Create ZIP Release
if exist "%filename%.zip" (del /f ".\%filename%.zip")
powershell.exe -nologo -noprofile -command "Compress-Archive -Path "release\*" -DestinationPath "%filename%.zip""

Rem | Operation Complete
echo(
pause