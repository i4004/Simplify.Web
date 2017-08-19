mono --runtime=v4.0 src/.nuget/NuGet.exe install NUnit.Runners -Version 3.7.0 -o packages

runTest()
{
    mono --runtime=v4.0 packages/NUnit.ConsoleRunner.3.7.0/tools/nunit3-console.exe -noxml -nodots -labels $@
   if [ $? -ne 0 ]
   then   
     exit 1
   fi
}

runTest $1 -exclude:Windows

exit $?