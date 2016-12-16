@echo off

set _msbuild=tools/msbuild


call gnt /p:ngconfig="packages.config" /nologo /v:m /m:4 || goto err
call %_msbuild% -notamd64 "Conari.sln" /v:normal /l:"packages\vsSBE.CI.MSBuild\bin\CI.MSBuild.dll" /m:4 /t:Rebuild /p:Configuration=Release || goto err


goto exit

:err

echo. Build failed. 1>&2

:exit
