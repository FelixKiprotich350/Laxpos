; Script generated by the Inno Script Studio Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Laxpos Retail"
#define MyAppVersion "1.0"
#define MyAppPublisher "Autosoft, Inc."
#define MyAppURL "http://www.example.com/"
#define MyAppExeName "LaxPos.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B319797C-B4AA-4455-9D08-BE32369D89A1}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
LicenseFile=C:\Users\IT USER\Desktop\task.txt
InfoBeforeFile=C:\Users\IT USER\Desktop\before.txt
InfoAfterFile=C:\Users\IT USER\Desktop\after.txt
OutputDir=C:\Users\IT USER\Desktop\laxpos\installer
OutputBaseFilename=setup
SetupIconFile=C:\Users\IT USER\Downloads\mytesticon.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\LaxPos.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\BouncyCastle.Crypto.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.Licensing.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuButton.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuButton.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuCheckBox.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuCheckBox.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuCircleProgress.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuCircleProgress.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuColorTransition.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuColorTransition.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuDataGridView.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuDataGridView.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuFormDock.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuFormDock.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuLabel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuLabel.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuPages.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuPages.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuTransition.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu.UI.WinForms.BunifuTransition.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Bunifu_UI_v1.5.3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\EnvDTE.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Google.Protobuf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Google.Protobuf.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Google.Protobuf.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\K4os.Compression.LZ4.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\K4os.Compression.LZ4.Streams.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\K4os.Compression.LZ4.Streams.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\K4os.Compression.LZ4.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\K4os.Hash.xxHash.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\K4os.Hash.xxHash.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\LaxExpanderPanel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\LaxExpanderPanel.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\LaxPos.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\LaxPos.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\LaxPos.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.ReportViewer.Common.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.ReportViewer.Design.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.ReportViewer.ProcessingObjectModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.ReportViewer.WinForms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.SqlServer.Types.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.VisualStudio.OLE.Interop.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.VisualStudio.Shell.Interop.8.0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.VisualStudio.Shell.Interop.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.VisualStudio.TextManager.Interop.8.0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Microsoft.VisualStudio.TextManager.Interop.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\MySql.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\MySql.Data.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\MySqlBackup.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\stdole.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\System.Buffers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\System.Buffers.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\System.Memory.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\System.Memory.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\System.Numerics.Vectors.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\System.Numerics.Vectors.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\System.Runtime.CompilerServices.Unsafe.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\System.Runtime.CompilerServices.Unsafe.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\Ubiety.Dns.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\ZstdNet.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\de\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\es\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\fr\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\it\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\ja\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\ko\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\IT USER\Desktop\laxpos\LaxPos\bin\Debug\pt\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent