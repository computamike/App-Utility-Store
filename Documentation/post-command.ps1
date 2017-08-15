echo "VARIABLES"
$root = $Env:ProjectDir
Set-Location $root

$relative = "..\packages\Doxygen.1.8.13\tools\doxygen.exe"
$doxyEXE = Resolve-Path  $relative
$DoxyFile = $root+"Documentation.doxygen"

echo "Doxygen EXE     : " +$doxyEXE
echo "Doxygen Project : " +$DoxyFile

Start-Process -FilePath "$doxyEXE" -ArgumentList  "$DoxyFile"  -NoNewWindow -Wait

echo "end"