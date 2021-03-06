#define APP_NAME "PPBackup"
#define APP_VERSION "0.1"
#define APP_FULL_VERSION "0.1"
#define APP_PUBLISHER "Andreas Weizel"
#define APP_URL "https://github.com/awzhome/pp-backup"
#define APP_EXE_FILE "ppbackup-ui.exe"

[Setup]
AppId={{D32AB7B1-334D-43A1-9A7C-EE692FCA7A8A}
AppName={#APP_NAME}
AppVersion={#APP_VERSION}
AppVerName={#APP_NAME} {#APP_FULL_VERSION}
AppPublisher={#APP_PUBLISHER}
AppPublisherURL={#APP_URL}
AppSupportURL={#APP_URL}
AppUpdatesURL={#APP_URL}
DefaultDirName={localappdata}\{#APP_NAME}
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
OutputBaseFilename=ppbackup-setup-win64-{#APP_FULL_VERSION}
OutputDir=build\artifacts
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "build\publish-win64\*"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#APP_NAME}"; Filename: "{app}\{#APP_EXE_FILE}"
Name: "{autodesktop}\{#APP_NAME}"; Filename: "{app}\{#APP_EXE_FILE}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#APP_EXE_FILE}"; Description: "{cm:LaunchProgram,{#StringChange(APP_NAME, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

