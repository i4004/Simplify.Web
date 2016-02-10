@ECHO OFF

echo Uninstalling Simplify.Web.Examples.WindowsService...
c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe /u /LogFile= /LogToConsole=true Simplify.Web.Examples.WindowsService.exe
pause