mono src/.nuget/NuGet.exe install NUnit.Runners -Version 3.7.0 -o packages

runTest()
{
    mono packages/NUnit.ConsoleRunner.3.7.0/tools/nunit3-console.exe -labels After -noresult $@

   if [ $? -ne 0 ]
   then   
     exit 1
   fi
}

runTest $1

exit $?