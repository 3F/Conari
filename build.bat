:: build [[configuration] | [# [option [keys]]]
:: https://github.com/3F/Conari
@echo off & if "%~1"=="#" (
    if /I "%~2"=="CI" (
        shift & shift & setlocal
            cd .tools & call netfx4sdk -mode sys || call netfx4sdk -mode pkg
        endlocal

    ) else if "%~2"=="" ( call .tools\hMSBuild ~x -GetNuTool & exit /B0 ) else goto err
)

set reltype=%~1
if not defined reltype set reltype=Release

call .tools\gnt & call packages\vsSolutionBuildEvent\cim.cmd ~x ~c %reltype% || goto err
exit /B 0

:err
    echo Failed build>&2
exit /B 1
