﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChinesseCheckersClient.GameService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OperationResult", Namespace="http://schemas.datacontract.org/2004/07/Logic")]
    public enum OperationResult : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sucessfull = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unknown = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ConnectionLost = 100,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        OperationNoValid = 200,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Player", Namespace="http://schemas.datacontract.org/2004/07/DataAccess")]
    [System.SerializableAttribute()]
    public partial class Player : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdPlayerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NicknameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int IdPlayer {
            get {
                return this.IdPlayerField;
            }
            set {
                if ((this.IdPlayerField.Equals(value) != true)) {
                    this.IdPlayerField = value;
                    this.RaisePropertyChanged("IdPlayer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nickname {
            get {
                return this.NicknameField;
            }
            set {
                if ((object.ReferenceEquals(this.NicknameField, value) != true)) {
                    this.NicknameField = value;
                    this.RaisePropertyChanged("Nickname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GameService.IPlayerMgt")]
    public interface IPlayerMgt {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerMgt/RegisterPlayer", ReplyAction="http://tempuri.org/IPlayerMgt/RegisterPlayerResponse")]
        ChinesseCheckersClient.GameService.OperationResult RegisterPlayer(string _nickname, string _password, string _email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerMgt/RegisterPlayer", ReplyAction="http://tempuri.org/IPlayerMgt/RegisterPlayerResponse")]
        System.Threading.Tasks.Task<ChinesseCheckersClient.GameService.OperationResult> RegisterPlayerAsync(string _nickname, string _password, string _email);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerMgt/Login", ReplyAction="http://tempuri.org/IPlayerMgt/LoginResponse")]
        ChinesseCheckersClient.GameService.Player Login(string _email, string _password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerMgt/Login", ReplyAction="http://tempuri.org/IPlayerMgt/LoginResponse")]
        System.Threading.Tasks.Task<ChinesseCheckersClient.GameService.Player> LoginAsync(string _email, string _password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerMgt/DeletePlayer", ReplyAction="http://tempuri.org/IPlayerMgt/DeletePlayerResponse")]
        ChinesseCheckersClient.GameService.OperationResult DeletePlayer(int _idPlayer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPlayerMgt/DeletePlayer", ReplyAction="http://tempuri.org/IPlayerMgt/DeletePlayerResponse")]
        System.Threading.Tasks.Task<ChinesseCheckersClient.GameService.OperationResult> DeletePlayerAsync(int _idPlayer);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPlayerMgtChannel : ChinesseCheckersClient.GameService.IPlayerMgt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PlayerMgtClient : System.ServiceModel.ClientBase<ChinesseCheckersClient.GameService.IPlayerMgt>, ChinesseCheckersClient.GameService.IPlayerMgt {
        
        public PlayerMgtClient() {
        }
        
        public PlayerMgtClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PlayerMgtClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PlayerMgtClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PlayerMgtClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ChinesseCheckersClient.GameService.OperationResult RegisterPlayer(string _nickname, string _password, string _email) {
            return base.Channel.RegisterPlayer(_nickname, _password, _email);
        }
        
        public System.Threading.Tasks.Task<ChinesseCheckersClient.GameService.OperationResult> RegisterPlayerAsync(string _nickname, string _password, string _email) {
            return base.Channel.RegisterPlayerAsync(_nickname, _password, _email);
        }
        
        public ChinesseCheckersClient.GameService.Player Login(string _email, string _password) {
            return base.Channel.Login(_email, _password);
        }
        
        public System.Threading.Tasks.Task<ChinesseCheckersClient.GameService.Player> LoginAsync(string _email, string _password) {
            return base.Channel.LoginAsync(_email, _password);
        }
        
        public ChinesseCheckersClient.GameService.OperationResult DeletePlayer(int _idPlayer) {
            return base.Channel.DeletePlayer(_idPlayer);
        }
        
        public System.Threading.Tasks.Task<ChinesseCheckersClient.GameService.OperationResult> DeletePlayerAsync(int _idPlayer) {
            return base.Channel.DeletePlayerAsync(_idPlayer);
        }
    }
}
