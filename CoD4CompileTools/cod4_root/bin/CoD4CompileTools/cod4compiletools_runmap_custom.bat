@ECHO OFF

set treepath=%~1
set mapname=%2
set mpmap=%3
set cmdOptions=%~4

echo CONSOLESTATUS:RUN MAP
echo.

set exeName=iw3sp.exe
if "%mpmap%" == "1" (
	set exeName=iw3mp.exe
)

cd %treepath%
echo %exeName% %cmdOptions% +set com_introplayed 1 +devmap %mapname% 
%exeName% %cmdOptions% +set com_introplayed 1 +devmap %mapname% 

cls