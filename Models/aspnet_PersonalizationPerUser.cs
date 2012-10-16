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
    [KnownType(typeof(aspnet_Paths))]
    [KnownType(typeof(aspnet_Users))]
    public partial class aspnet_PersonalizationPerUser: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'Id' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private System.Guid _id;
    
        [DataMember]
        public Nullable<System.Guid> PathId
        {
            get { return _pathId; }
            set
            {
                if (_pathId != value)
                {
                    ChangeTracker.RecordOriginalValue("PathId", _pathId);
                    if (!IsDeserializing)
                    {
                        if (aspnet_Paths != null && aspnet_Paths.PathId != value)
                        {
                            aspnet_Paths = null;
                        }
                    }
                    _pathId = value;
                    OnPropertyChanged("PathId");
                }
            }
        }
        private Nullable<System.Guid> _pathId;
    
        [DataMember]
        public Nullable<System.Guid> UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    ChangeTracker.RecordOriginalValue("UserId", _userId);
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
        private Nullable<System.Guid> _userId;
    
        [DataMember]
        public byte[] PageSettings
        {
            get { return _pageSettings; }
            set
            {
                if (_pageSettings != value)
                {
                    _pageSettings = value;
                    OnPropertyChanged("PageSettings");
                }
            }
        }
        private byte[] _pageSettings;
    
        [DataMember]
        public System.DateTime LastUpdatedDate
        {
            get { return _lastUpdatedDate; }
            set
            {
                if (_lastUpdatedDate != value)
                {
                    _lastUpdatedDate = value;
                    OnPropertyChanged("LastUpdatedDate");
                }
            }
        }
        private System.DateTime _lastUpdatedDate;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public aspnet_Paths aspnet_Paths
        {
            get { return _aspnet_Paths; }
            set
            {
                if (!ReferenceEquals(_aspnet_Paths, value))
                {
                    var previousValue = _aspnet_Paths;
                    _aspnet_Paths = value;
                    Fixupaspnet_Paths(previousValue);
                    OnNavigationPropertyChanged("aspnet_Paths");
                }
            }
        }
        private aspnet_Paths _aspnet_Paths;
    
        [DataMember]
        public aspnet_Users aspnet_Users
        {
            get { return _aspnet_Users; }
            set
            {
                if (!ReferenceEquals(_aspnet_Users, value))
                {
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
    
        protected virtual void ClearNavigationProperties()
        {
            aspnet_Paths = null;
            aspnet_Users = null;
        }

        #endregion
        #region Association Fixup
    
        private void Fixupaspnet_Paths(aspnet_Paths previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.aspnet_PersonalizationPerUser.Contains(this))
            {
                previousValue.aspnet_PersonalizationPerUser.Remove(this);
            }
    
            if (aspnet_Paths != null)
            {
                if (!aspnet_Paths.aspnet_PersonalizationPerUser.Contains(this))
                {
                    aspnet_Paths.aspnet_PersonalizationPerUser.Add(this);
                }
    
                PathId = aspnet_Paths.PathId;
            }
            else if (!skipKeys)
            {
                PathId = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("aspnet_Paths")
                    && (ChangeTracker.OriginalValues["aspnet_Paths"] == aspnet_Paths))
                {
                    ChangeTracker.OriginalValues.Remove("aspnet_Paths");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("aspnet_Paths", previousValue);
                }
                if (aspnet_Paths != null && !aspnet_Paths.ChangeTracker.ChangeTrackingEnabled)
                {
                    aspnet_Paths.StartTracking();
                }
            }
        }
    
        private void Fixupaspnet_Users(aspnet_Users previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.aspnet_PersonalizationPerUser.Contains(this))
            {
                previousValue.aspnet_PersonalizationPerUser.Remove(this);
            }
    
            if (aspnet_Users != null)
            {
                if (!aspnet_Users.aspnet_PersonalizationPerUser.Contains(this))
                {
                    aspnet_Users.aspnet_PersonalizationPerUser.Add(this);
                }
    
                UserId = aspnet_Users.UserId;
            }
            else if (!skipKeys)
            {
                UserId = null;
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
