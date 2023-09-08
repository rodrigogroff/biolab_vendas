@echo off
cls
set /p n=Numero de threads:

rem Cria as pastas
for /l %%i in (1,1,%n%) do (
mkdir net6.0_%%i
robocopy net6.0 net6.0_%%i /e
)

rem Loop nas copias e copia o conteudo da pasta "net6.0" e executa
for /l %%i in (1,1,%n%) do (
cd net6.0_%%i
start StresserBiolab.exe
cd..
pause
)