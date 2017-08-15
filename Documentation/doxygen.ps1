# Doxygen Execution Code.
clear 
echo "Env:BUILD_SOURCESDIRECTORY =  $Env:BUILD_SOURCESDIRECTORY"

echo "Building Documentation"
pause
<#$Env:BUILD_SOURCESDIRECTORY#>
#$FakeEnv = "C:\Projects\App-Utility-Store";
#$v = $PSScriptRoot
#$FakeEnv = "C:\Projects\App-Utility-Store";
#$DoxPath = $FakeEnv + "\Documentation";
#$DoxFile = $FakeEnv + "\Documentation\Documentation.doxygen";
clear
& "..\packages\Doxygen.1.8.9.2\tools\doxygen.exe" .\Documentation.doxygen  1>doxygen_stdout.txt 2>doxygen_stderr.txt

#$CMD = $FakeEnv + '\packages\Doxygen.1.8.9.2\tools\doxygen.exe'  



#cd $DoxPath 

#&$CMD $DoxFile