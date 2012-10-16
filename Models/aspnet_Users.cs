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
    [KnownType(typeof(aspnet_Membership))]
    [KnownType(typeof(aspnet_PersonalizationPerUser))]
    [KnownType(typeof(aspnet_Profile))]
    [KnownType(typeof(aspnet_Roles))]
    public partial class aspnet_Users: IObjectWithChangeTracker, INotifyPropertyChanged
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
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        private System.Guid _userId;
    
        [DataMember]
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        private string _userName;
    
        [DataMember]
        public string LoweredUserName
        {
            get { return _loweredUserName; }
            set
            {
                if (_loweredUserName != value)
                {
                    _loweredUserName = value;
                    OnPropertyChanged("LoweredUserName");
                }
            }
        }
        private string _loweredUserName;
    
        [DataMember]
        public string MobileAlias
        {
            get { return _mobileAlias; }
            set
            {
                if (_mobileAlias != value)
                {
                    _mobileAlias = value;
                    OnPropertyChanged("MobileAlias");
                }
            }
        }
        private string _mobileAlias;
    
        [DataMember]
        public bool IsAnonymous
        {
            get { return _isAnonymous; }
            set
            {
                if (_isAnonymous != value)
                {
                    _isAnonymous = value;
                    OnPropertyChanged("IsAnonymous");
                }
            }
        }
        private bool _isAnonymous;
    
        [DataMember]
        public System.DateTime LastActivityDate
        {
            get { return _lastActivityDate; }
            set
            {
                if (_lastActivityDate != value)
                {
                    _lastActivityDate = value;
                    OnPropertyChanged("LastActivityDate");
                }
            }
        }
        private System.DateTime _lastActivityDate;

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
        public aspnet_Membership aspnet_Membership
        {
            get { return _aspnet_Membership; }
            set
            {
                if (!ReferenceEquals(_aspnet_Membership, value))
                {
                    var previousValue = _aspnet_Membership;
                    _aspnet_Membership = value;
                    Fixupaspnet_Membership(previousValue);
                    OnNavigationPropertyChanged("aspnet_Membership");
                }
            }
        }
        private aspnet_Membership _aspnet_Membership;
    
        [DataMember]
        public TrackableCollection<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser
        {
            get
            {
                if (_aspnet_PersonalizationPerUser == null)
                {
                    _aspnet_PersonalizationPerUser = new TrackableCollection<aspnet_PersonalizationPerUser>();
                    _aspnet_PersonalizationPerUser.CollectionChanged += Fixupaspnet_PersonalizationPerUser;
                }
                return _aspnet_PersonalizationPerUser;
            }
            set
            {
                if (!ReferenceEquals(_aspnet_PersonalizationPerUser, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_aspnet_PersonalizationPerUser != null)
                    {
                        _aspnet_PersonalizationPerUser.CollectionChanged -= Fixupaspnet_PersonalizationPerUser;
                    }
                    _aspnet_PersonalizationPerUser = value;
                    if (_aspnet_PersonalizationPerUser != null)
                    {
                        _aspnet_PersonalizationPerUser.CollectionChanged += Fixupaspnet_PersonalizationPerUser;
                    }
                    OnNavigationPropertyChanged("aspnet_PersonalizationPerUser");
                }
            }
        }
        private TrackableCollection<aspnet_PersonalizationPerUser> _aspnet_PersonalizationPerUser;
    
        [DataMember]
        public aspnet_Profile aspnet_Profile
        {
            get { return _aspnet_Profile; }
            set
            {
                if (!ReferenceEquals(_aspnet_Profile, value))
                {
                    var previousValue = _aspnet_Profile;
                    _aspnet_Profile = value;
                    Fixupaspnet_Profile(previousValue);
                    OnNavigationPropertyChanged("aspnet_Profile");
                }
            }
        }
        private aspnet_Profile _aspnet_Profile;
    
        [DataMember]
        public TrackableCollection<aspnet_Roles> aspnet_Roles
        {
            get
            {
                if (_aspnet_Roles == null)
                {
                    _aspnet_Roles = new TrackableCollection<aspnet_Roles>();
                    _aspnet_Roles.CollectionChanged += Fixupaspnet_Roles;
                }
                return _aspnet_Roles;
            }
            set
            {
                if (!ReferenceEquals(_aspnet_Roles, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_aspnet_Roles != null)
                    {
                        _aspnet_Roles.CollectionChanged -= Fixupaspnet_Roles;
                    }
                    _aspnet_Roles = value;
                    if (_aspnet_Roles != null)
                    {
                        _aspnet_Roles.CollectionChanged += Fixupaspnet_Roles;
                    }
                    OnNavigationPropertyChanged("aspnet_Roles");
                }
            }
        }
        private TrackableCollection<aspnet_Roles> _aspnet_Roles;

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
    
        protected virtual void ClearNavigationProperties()
        {
            aspnet_Applications = null;
            aspnet_Membership = null;
            aspnet_PersonalizationPerUser.Clear();
            aspnet_Profile = null;
            aspnet_Roles.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void Fixupaspnet_Applications(aspnet_Applications previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.aspnet_Users.Contains(this))
            {
                previousValue.aspnet_Users.Remove(this);
            }
    
            if (aspnet_Applications != null)
            {
                if (!aspnet_Applications.aspnet_Users.Contains(this))
                {
                    aspnet_Applications.aspnet_Users.Add(this);
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
    
        private void Fixupaspnet_Membership(aspnet_Membership previousValue)
        {
            // This is the principal end in an association that performs cascade deletes.
            // Update the event listener to refer to the new dependent.
            if (previousValue != null)
            {
                ChangeTracker.ObjectStateChanging -= previousValue.HandleCascadeDelete;
            }
    
            if (aspnet_Membership != null)
            {
                ChangeTracker.ObjectStateChanging += aspnet_Membership.HandleCascadeDelete;
            }
    
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && ReferenceEquals(previousValue.aspnet_Users, this))
            {
                previousValue.aspnet_Users = null;
            }
    
            if (aspnet_Membership != null)
            {
                aspnet_Membership.aspnet_Users = this;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("aspnet_Membership")
                    && (ChangeTracker.OriginalValues["aspnet_Membership"] == aspnet_Membership))
                {
                    ChangeTracker.OriginalValues.Remove("aspnet_Membership");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("aspnet_Membership", previousValue);
                    // This is the principal end of an identifying association, so the dependent must be deleted when the relationship is removed.
                    // If the current state of the dependent is Added, the relationship can be changed without causing the dependent to be deleted.
                    if (previousValue != null && previousValue.ChangeTracker.State != ObjectState.Added)
                    {
                        previousValue.MarkAsDeleted();
                    }
                }
                if (aspnet_Membership != null && !aspnet_Membership.ChangeTracker.ChangeTrackingEnabled)
                {
                    aspnet_Membership.StartTracking();
                }
            }
        }
    
        private void Fixupaspnet_Profile(aspnet_Profile previousValue)
        {
            // This is the principal end in an association that performs cascade deletes.
            // Update the event listener to refer to the new dependent.
            if (previousValue != null)
            {
                ChangeTracker.ObjectStateChanging -= previousValue.HandleCascadeDelete;
            }
    
            if (aspnet_Profile != null)
            {
                ChangeTracker.ObjectStateChanging += aspnet_Profile.HandleCascadeDelete;
            }
    
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && ReferenceEquals(previousValue.aspnet_Users, this))
            {
                previousValue.aspnet_Users = null;
            }
    
            if (aspnet_Profile != null)
            {
                aspnet_Profile.aspnet_Users = this;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("aspnet_Profile")
                    && (ChangeTracker.OriginalValues["aspnet_Profile"] == aspnet_Profile))
                {
                    ChangeTracker.OriginalValues.Remove("aspnet_Profile");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("aspnet_Profile", previousValue);
                    // This is the principal end of an identifying association, so the dependent must be deleted when the relationship is removed.
                    // If the current state of the dependent is Added, the relationship can be changed without causing the dependent to be deleted.
                    if (previousValue != null && previousValue.ChangeTracker.State != ObjectState.Added)
                    {
                        previousValue.MarkAsDeleted();
                    }
                }
                if (aspnet_Profile != null && !aspnet_Profile.ChangeTracker.ChangeTrackingEnabled)
                {
                    aspnet_Profile.StartTracking();
                }
            }
        }
    
        private void Fixupaspnet_PersonalizationPerUser(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (aspnet_PersonalizationPerUser item in e.NewItems)
                {
                    item.aspnet_Users = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("aspnet_PersonalizationPerUser", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (aspnet_PersonalizationPerUser item in e.OldItems)
                {
                    if (ReferenceEquals(item.aspnet_Users, this))
                    {
                        item.aspnet_Users = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("aspnet_PersonalizationPerUser", item);
                    }
                }
            }
        }
    
        private void Fixupaspnet_Roles(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (aspnet_Roles item in e.NewItems)
                {
                    if (!item.aspnet_Users.Contains(this))
                    {
                        item.aspnet_Users.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("aspnet_Roles", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (aspnet_Roles item in e.OldItems)
                {
                    if (item.aspnet_Users.Contains(this))
                    {
                        item.aspnet_Users.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("aspnet_Roles", item);
                    }
                }
            }
        }

        #endregion
    }
}
