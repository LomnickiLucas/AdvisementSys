//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace AdvisementSys.Models
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(aspnet_Applications))]
    [KnownType(typeof(aspnet_Users))]
    public partial class aspnet_Membership: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid ApplicationId
        {
            get { return _applicationId; }
            set
            {
                if (_applicationId != value)
                {
                    ChangeTracker.RecordOriginalValue("ApplicationId", _applicationId);
                    if (!IsDeserializing)
                    {
                        if (aspnet_Applications != null && aspnet_Applications.ApplicationId != value)
                        {
                            aspnet_Applications = null;
                        }
                    }
                    _applicationId = value;
                    OnPropertyChanged("ApplicationId");
                }
            }
        }
        private System.Guid _applicationId;
    
        [DataMember]
        public System.Guid UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'UserId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    if (!IsDeserializing)
                    {
                        if (aspnet_Users != null && aspnet_Users.UserId != value)
                        {
                            aspnet_Users = null;
                        }
                    }
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        private System.Guid _userId;
    
        [DataMember]
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        private string _password;
    
        [DataMember]
        public int PasswordFormat
        {
            get { return _passwordFormat; }
            set
            {
                if (_passwordFormat != value)
                {
                    _passwordFormat = value;
                    OnPropertyChanged("PasswordFormat");
                }
            }
        }
        private int _passwordFormat;
    
        [DataMember]
        public string PasswordSalt
        {
            get { return _passwordSalt; }
            set
            {
                if (_passwordSalt != value)
                {
                    _passwordSalt = value;
                    OnPropertyChanged("PasswordSalt");
                }
            }
        }
        private string _passwordSalt;
    
        [DataMember]
        public string MobilePIN
        {
            get { return _mobilePIN; }
            set
            {
                if (_mobilePIN != value)
                {
                    _mobilePIN = value;
                    OnPropertyChanged("MobilePIN");
                }
            }
        }
        private string _mobilePIN;
    
        [DataMember]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private string _email;
    
        [DataMember]
        public string LoweredEmail
        {
            get { return _loweredEmail; }
            set
            {
                if (_loweredEmail != value)
                {
                    _loweredEmail = value;
                    OnPropertyChanged("LoweredEmail");
                }
            }
        }
        private string _loweredEmail;
    
        [DataMember]
        public string PasswordQuestion
        {
            get { return _passwordQuestion; }
            set
            {
                if (_passwordQuestion != value)
                {
                    _passwordQuestion = value;
                    OnPropertyChanged("PasswordQuestion");
                }
            }
        }
        private string _passwordQuestion;
    
        [DataMember]
        public string PasswordAnswer
        {
            get { return _passwordAnswer; }
            set
            {
                if (_passwordAnswer != value)
                {
                    _passwordAnswer = value;
                    OnPropertyChanged("PasswordAnswer");
                }
            }
        }
        private string _passwordAnswer;
    
        [DataMember]
        public bool IsApproved
        {
            get { return _isApproved; }
            set
            {
                if (_isApproved != value)
                {
                    _isApproved = value;
                    OnPropertyChanged("IsApproved");
                }
            }
        }
        private bool _isApproved;
    
        [DataMember]
        public bool IsLockedOut
        {
            get { return _isLockedOut; }
            set
            {
                if (_isLockedOut != value)
                {
                    _isLockedOut = value;
                    OnPropertyChanged("IsLockedOut");
                }
            }
        }
        private bool _isLockedOut;
    
        [DataMember]
        public System.DateTime CreateDate
        {
            get { return _createDate; }
            set
            {
                if (_createDate != value)
                {
                    _createDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }
        private System.DateTime _createDate;
    
        [DataMember]
        public System.DateTime LastLoginDate
        {
            get { return _lastLoginDate; }
            set
            {
                if (_lastLoginDate != value)
                {
                    _lastLoginDate = value;
                    OnPropertyChanged("LastLoginDate");
                }
            }
        }
        private System.DateTime _lastLoginDate;
    
        [DataMember]
        public System.DateTime LastPasswordChangedDate
        {
            get { return _lastPasswordChangedDate; }
            set
            {
                if (_lastPasswordChangedDate != value)
                {
                    _lastPasswordChangedDate = value;
                    OnPropertyChanged("LastPasswordChangedDate");
                }
            }
        }
        private System.DateTime _lastPasswordChangedDate;
    
        [DataMember]
        public System.DateTime LastLockoutDate
        {
            get { return _lastLockoutDate; }
            set
            {
                if (_lastLockoutDate != value)
                {
                    _lastLockoutDate = value;
                    OnPropertyChanged("LastLockoutDate");
                }
            }
        }
        private System.DateTime _lastLockoutDate;
    
        [DataMember]
        public int FailedPasswordAttemptCount
        {
            get { return _failedPasswordAttemptCount; }
            set
            {
                if (_failedPasswordAttemptCount != value)
                {
                    _failedPasswordAttemptCount = value;
                    OnPropertyChanged("FailedPasswordAttemptCount");
                }
            }
        }
        private int _failedPasswordAttemptCount;
    
        [DataMember]
        public System.DateTime FailedPasswordAttemptWindowStart
        {
            get { return _failedPasswordAttemptWindowStart; }
            set
            {
                if (_failedPasswordAttemptWindowStart != value)
                {
                    _failedPasswordAttemptWindowStart = value;
                    OnPropertyChanged("FailedPasswordAttemptWindowStart");
                }
            }
        }
        private System.DateTime _failedPasswordAttemptWindowStart;
    
        [DataMember]
        public int FailedPasswordAnswerAttemptCount
        {
            get { return _failedPasswordAnswerAttemptCount; }
            set
            {
                if (_failedPasswordAnswerAttemptCount != value)
                {
                    _failedPasswordAnswerAttemptCount = value;
                    OnPropertyChanged("FailedPasswordAnswerAttemptCount");
                }
            }
        }
        private int _failedPasswordAnswerAttemptCount;
    
        [DataMember]
        public System.DateTime FailedPasswordAnswerAttemptWindowStart
        {
            get { return _failedPasswordAnswerAttemptWindowStart; }
            set
            {
                if (_failedPasswordAnswerAttemptWindowStart != value)
                {
                    _failedPasswordAnswerAttemptWindowStart = value;
                    OnPropertyChanged("FailedPasswordAnswerAttemptWindowStart");
                }
            }
        }
        private System.DateTime _failedPasswordAnswerAttemptWindowStart;
    
        [DataMember]
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
        private string _comment;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public aspnet_Applications aspnet_Applications
        {
            get { return _aspnet_Applications; }
            set
            {
                if (!ReferenceEquals(_aspnet_Applications, value))
                {
                    var previousValue = _aspnet_Applications;
                    _aspnet_Applications = value;
                    Fixupaspnet_Applications(previousValue);
                    OnNavigationPropertyChanged("aspnet_Applications");
                }
            }
        }
        private aspnet_Applications _aspnet_Applications;
    
        [DataMember]
        public aspnet_Users aspnet_Users
        {
            get { return _aspnet_Users; }
            set
            {
                if (!ReferenceEquals(_aspnet_Users, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added && value != null)
                    {
                        // This the dependent end of an identifying relationship, so the principal end cannot be changed if it is already set,
                        // otherwise it can only be set to an entity with a primary key that is the same value as the dependent's foreign key.
                        if (UserId != value.UserId)
                        {
                            throw new InvalidOperationException("The principal end of an identifying relationship can only be changed when the dependent end is in the Added state.");
                        }
                    }
                    var previousValue = _aspnet_Users;
                    _aspnet_Users = value;
                    Fixupaspnet_Users(previousValue);
                    OnNavigationPropertyChanged("aspnet_Users");
                }
            }
        }
        private aspnet_Users _aspnet_Users;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        // This entity type is the dependent end in at least one association that performs cascade deletes.
        // This event handler will process notifications that occur when the principal end is deleted.
        internal void HandleCascadeDelete(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                this.MarkAsDeleted();
            }
        }
    
        protected virtual void ClearNavigationProperties()
        {
            aspnet_Applications = null;
            aspnet_Users = null;
        }

        #endregion
        #region Association Fixup
    
        private void Fixupaspnet_Applications(aspnet_Applications previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.aspnet_Membership.Contains(this))
            {
                previousValue.aspnet_Membership.Remove(this);
            }
    
            if (aspnet_Applications != null)
            {
                if (!aspnet_Applications.aspnet_Membership.Contains(this))
                {
                    aspnet_Applications.aspnet_Membership.Add(this);
                }
    
                ApplicationId = aspnet_Applications.ApplicationId;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("aspnet_Applications")
                    && (ChangeTracker.OriginalValues["aspnet_Applications"] == aspnet_Applications))
                {
                    ChangeTracker.OriginalValues.Remove("aspnet_Applications");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("aspnet_Applications", previousValue);
                }
                if (aspnet_Applications != null && !aspnet_Applications.ChangeTracker.ChangeTrackingEnabled)
                {
                    aspnet_Applications.StartTracking();
                }
            }
        }
    
        private void Fixupaspnet_Users(aspnet_Users previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && ReferenceEquals(previousValue.aspnet_Membership, this))
            {
                previousValue.aspnet_Membership = null;
            }
    
            if (aspnet_Users != null)
            {
                aspnet_Users.aspnet_Membership = this;
                UserId = aspnet_Users.UserId;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("aspnet_Users")
                    && (ChangeTracker.OriginalValues["aspnet_Users"] == aspnet_Users))
                {
                    ChangeTracker.OriginalValues.Remove("aspnet_Users");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("aspnet_Users", previousValue);
                }
                if (aspnet_Users != null && !aspnet_Users.ChangeTracker.ChangeTrackingEnabled)
                {
                    aspnet_Users.StartTracking();
                }
            }
        }

        #endregion
    }
}