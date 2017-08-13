# Doxygen Execution Code.
<#$Env:BUILD_SOURCESDIRECTORY#>
$FakeEnv = "C:\Projects\App-Utility-Store";
$DoxPath = $FakeEnv + "\Documentation";
$DoxFile = $FakeEnv + "\Documentation\Documentation.doxygen";
$CMD = $FakeEnv + '\packages\Doxygen.1.8.9.2\tools\doxygen.exe'  

cd $DoxPath 

&$CMD $DoxFile