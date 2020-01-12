﻿using Newtonsoft.Json;
using System;
using System.Windows;
using VSMonoDebugger.Views;

namespace VSMonoDebugger.Settings
{
    public class UserSettings : BaseViewModel
    {
        public readonly static int DEFAULT_DEBUGGER_AGENT_PORT = 11000;

        public UserSettings()
        {
            Id = Guid.NewGuid().ToString();

            Description = "";
            LastIp = "127.0.0.1";

            SSHHostIP = "127.0.0.1";
            SSHPort = 22;
            SSHUsername = string.Empty;
            SSHPassword = string.Empty;
            SSHPrivateKeyFile = string.Empty;
            SSHDeployPath = "./MonoDebugTemp/";
            SSHMonoDebugPort = DEFAULT_DEBUGGER_AGENT_PORT;
            SSHPdb2mdbCommand = "mono /usr/lib/mono/4.5/pdb2mdb.exe";

            DeployAndDebugOnLocalWindowsSystem = false;
            WindowsDeployPath = "";

            UseDeployPathFromProjectFileIfExists = true;
            MaxConnectionAttempts = 10;
            TimeBetweenConnectionAttemptsInMs = 1000;
            RedirectOutputOption = RedirectOutputOptions.RedirectStandardOutput;

            PreDebugScriptWithParametersWindows = DefaultPreDebugScriptWithParametersWindows;
            PreDebugScriptWithParameters = DefaultPreDebugScriptWithParameters;
            DebugScriptWithParametersWindows = DefaultDebugScriptWithParametersWindows;
            DebugScriptWithParameters = DefaultDebugScriptWithParameters;
        }

        #region General

        private string _id;
        public string Id { get => _id; set { _id = value; NotifyPropertyChanged(); } }

        private string _description;
        public string Description { get => _description; set { _description = value; NotifyPropertyChanged(); } }

        private string _lastIp;
        public string LastIp { get => _lastIp; set { _lastIp = value; NotifyPropertyChanged(); } }

        public string FullDescription
        {
            get
            {
                if (DeployAndDebugOnLocalWindowsSystem)
                {
                    return $"{Description} localhost (Local Windows Machine)";
                }
                else
                {
                    return $"{Description} {SSHUsername}@{SSHHostIP}:{SSHPort}";
                }
            }
            set
            {
                NotifyPropertyChanged();
            }
        }

        private bool _useDeployPathFromProjectFileIfExists;
        public bool UseDeployPathFromProjectFileIfExists { get => _useDeployPathFromProjectFileIfExists; set { _useDeployPathFromProjectFileIfExists = value; NotifyPropertyChanged(); } }

        #endregion

        #region SSH properties

        public string SSHFullUrl
        {
            get
            {
                return $"{SSHUsername}@{SSHHostIP}:{SSHPort}";
            }
            set
            {
                NotifyPropertyChanged();
            }
        }

        private string _sSHHostIP;
        public string SSHHostIP { get => _sSHHostIP; set { _sSHHostIP = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(SSHFullUrl)); } }

        private int _sSHPort;
        public int SSHPort { get => _sSHPort; set { _sSHPort = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(SSHFullUrl)); } }

        private string _sSHUsername;
        public string SSHUsername { get => _sSHUsername; set { _sSHUsername = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(SSHFullUrl)); } }

        private string _sSHPassword;
        public string SSHPassword { get => _sSHPassword; set { _sSHPassword = value; NotifyPropertyChanged(); } }

        private string _sSHPrivateKeyFile;
        public string SSHPrivateKeyFile { get => _sSHPrivateKeyFile; set { _sSHPrivateKeyFile = value; NotifyPropertyChanged(); } }

        private string _sSHDeployPath;
        public string SSHDeployPath { get => _sSHDeployPath; set { _sSHDeployPath = value; NotifyPropertyChanged(); } }

        private string _sSHPdb2mdbCommand;
        public string SSHPdb2mdbCommand { get => _sSHPdb2mdbCommand; set { _sSHPdb2mdbCommand = value; NotifyPropertyChanged(); } }

        #endregion

        #region Local Windows properties

        private bool _deployAndDebugOnLocalWindowsSystem;
        public bool DeployAndDebugOnLocalWindowsSystem { get => _deployAndDebugOnLocalWindowsSystem; set { _deployAndDebugOnLocalWindowsSystem = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(ShowSSHOptions)); NotifyPropertyChanged(nameof(ShowWindowsOptions)); } }
        public Visibility ShowSSHOptions { get => DeployAndDebugOnLocalWindowsSystem ? Visibility.Collapsed : Visibility.Visible; }
        public Visibility ShowWindowsOptions { get => DeployAndDebugOnLocalWindowsSystem ? Visibility.Visible : Visibility.Collapsed; }

        private string _windowsDeployPath;
        public string WindowsDeployPath { get => _windowsDeployPath; set { _windowsDeployPath = value; NotifyPropertyChanged(); } }

        #endregion

        #region Mono softdebugger connection properties

        private RedirectOutputOptions _redirectOutputOption;
        public RedirectOutputOptions RedirectOutputOption { get => _redirectOutputOption; set { _redirectOutputOption = value; NotifyPropertyChanged(); } }

        private int _sSHMonoDebugPort;
        public int SSHMonoDebugPort { get => _sSHMonoDebugPort; set { _sSHMonoDebugPort = value; NotifyPropertyChanged(); } }

        private uint _maxConnectionAttempts;
        public uint MaxConnectionAttempts { get => _maxConnectionAttempts; set { _maxConnectionAttempts = value; NotifyPropertyChanged(); } }

        private uint _timeBetweenConnectionAttemptsInMs;
        public uint TimeBetweenConnectionAttemptsInMs { get => _timeBetweenConnectionAttemptsInMs; set { _timeBetweenConnectionAttemptsInMs = value; NotifyPropertyChanged(); } }

        #endregion

        public void SetDefaultPreDebugScript()
        {
            if (DeployAndDebugOnLocalWindowsSystem)
            {
                PreDebugScriptWithParametersWindows = DefaultPreDebugScriptWithParametersWindows;
            }
            else
            {
                PreDebugScriptWithParameters = DefaultPreDebugScriptWithParameters;
            }
        }

        public void SetDefaultDebugScript()
        {
            if (DeployAndDebugOnLocalWindowsSystem)
            {
                DebugScriptWithParametersWindows = DefaultDebugScriptWithParametersWindows;
            }
            else
            {
                DebugScriptWithParameters = DefaultDebugScriptWithParameters;
            }
        }

        #region Debug scripts Linux

        private string _preDebugScriptWithParameters;
        public string PreDebugScriptWithParameters
        {
            get
            {
                return _preDebugScriptWithParameters;
                //return string.IsNullOrWhiteSpace(_preDebugScriptWithParameters) ? DefaultPreDebugScriptWithParameters : _preDebugScriptWithParameters;
            }
            set
            {
                _preDebugScriptWithParameters = value;
                NotifyPropertyChanged();
            }
        }

        private string _debugScriptWithParameters;
        public string DebugScriptWithParameters
        {
            get
            {
                return _debugScriptWithParameters;
                //return string.IsNullOrWhiteSpace(_debugScriptWithParameters) ? DefaultDebugScriptWithParameters : _debugScriptWithParameters;
            }
            set
            {
                _debugScriptWithParameters = value;
                NotifyPropertyChanged();
            }
        }

        [JsonIgnore]
        public string DefaultPreDebugScriptWithParameters
        {
            get
            {
                return $"kill $(lsof -i | grep 'mono' | grep '\\*:{MONO_DEBUG_PORT}' | awk '{{print $2}}');\r\nkill $(ps w | grep '[m]ono --debugger-agent=address' | awk '{{print $1}}');";
            }
        }

        [JsonIgnore]
        public string DefaultDebugScriptWithParameters
        {
            get
            {
                return $"mono --debugger-agent=address=0.0.0.0:{MONO_DEBUG_PORT},transport=dt_socket,server=y --debug=mdb-optimizations {TARGET_EXE_FILENAME} {START_ARGUMENTS} &";
            }
        }

        #endregion

        #region Debug scripts Windows        

        private string _preDebugScriptWithParametersWindows;
        public string PreDebugScriptWithParametersWindows
        {
            get
            {
                return _preDebugScriptWithParametersWindows;
                //return string.IsNullOrWhiteSpace(_preDebugScriptWithParametersWindows) ? DefaultPreDebugScriptWithParametersWindows : _preDebugScriptWithParametersWindows;
            }
            set
            {
                _preDebugScriptWithParametersWindows = value;
                NotifyPropertyChanged();
            }
        }

        private string _debugScriptWithParametersWindows;
        public string DebugScriptWithParametersWindows
        {
            get
            {
                return _debugScriptWithParametersWindows;
                //return string.IsNullOrWhiteSpace(_debugScriptWithParametersWindows) ? DefaultDebugScriptWithParametersWindows : _debugScriptWithParametersWindows;
            }
            set
            {
                _debugScriptWithParametersWindows = value;
                NotifyPropertyChanged();
            }
        }

        [JsonIgnore]
        public string DefaultPreDebugScriptWithParametersWindows
        {
            get
            {
                return $"#WARNING: This will kill all mono processes\ntaskkill /IM mono.exe /F";
            }
        }

        [JsonIgnore]
        public string DefaultDebugScriptWithParametersWindows
        {
            get
            {
                return $"mono.exe --debugger-agent=address=0.0.0.0:{MONO_DEBUG_PORT},transport=dt_socket,server=y --debug=mdb-optimizations {TARGET_EXE_FILENAME} {START_ARGUMENTS}";
            }
        }

        #endregion

        #region SupportedScriptParameters

        public readonly string MONO_DEBUG_PORT = "$(MONO_DEBUG_PORT)";
        public readonly string TARGET_EXE_FILENAME = "$(TARGET_EXE_FILENAME)";
        public readonly string START_ARGUMENTS = "$(START_ARGUMENTS)";

        [JsonIgnore]
        public string SupportedScriptParameters
        {
            get
            {
                return $@"1) An empty script will replaced by the default script.
2) Windows new line '\r\n' will be replaced by '\n'.
3) You can use following Parameters in the debug scripts:
{MONO_DEBUG_PORT} = Will be replaced by the mono debug port.
{TARGET_EXE_FILENAME} = Replaced by the application name (*.exe) results from the StartupProject.
{START_ARGUMENTS} = Is replaced by the startup parameters set in the properties of the StartupProject.";
            }
        }

        #endregion
    }

    [Flags]
    public enum RedirectOutputOptions
    {
        None = 0,
        RedirectStandardOutput = 1,
        RedirectErrorOutput = 2,
        RedirectAll = RedirectStandardOutput | RedirectErrorOutput
    }
}