call npm run build_dev

xcopy ".\src\static\js\server.js" .\dist /E /D /S /I /Q /Y /F
xcopy ".\src\static\js\mask.js" .\dist\src\static /E /D /S /I /Q /Y /F
xcopy "favicon.ico" .\dist /Y /F
xcopy .\src\static\html .\dist /E /D /S /I /Q /Y /F
xcopy .\src\static\css .\dist\src\static\css /E /D  /S /I /Q /Y /F
xcopy .\src\static\img .\dist\src\static\img /E /D /S /I /Q /Y /F
xcopy .\src\static\vendor .\dist\src\static\vendor /E /D /S /I /Q /Y /F
xcopy .\src\static\files .\dist\src\static\files /E /D  /S /I /Q /Y /F

node dist/server.js
