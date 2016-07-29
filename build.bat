@echo off

set msbuild=tools/msbuild

call %msbuild% gnt.core /p:ngconfig="packages.config" /nologo /v:m /m:4
call %msbuild% "Conari.sln" /verbosity:normal /l:"packages\vsSBE.CI.MSBuild\bin\CI.MSBuild.dll" /m:4 /t:Rebuild /p:Configuration=Release