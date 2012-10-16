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
    [KnownType(typeof(aspnet_Users))]
    public partial class aspnet_Profile: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
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
        public string PropertyNames
        {
            get { return _propertyNames; }
            set
            {
                if (_propertyNames != value)
                {
                    _propertyNames = value;
                    OnPropertyChanged("PropertyNames");
                }
            }
        }
        private string _propertyNames;
    
        [DataMember]
        public string PropertyValuesString
        {
            get { return _propertyValuesString; }
            set
            {
                if (_propertyValuesString != value)
                {
                    _propertyValuesString = value;
                    OnPropertyChanged("PropertyValuesString");
                }
            }
        }
        private string _propertyValuesString;
    
        [DataMember]
        public byte[] PropertyValuesBinary
        {
            get { return _propertyValuesBinary; }
            set
            {
                if (_propertyValuesBinary != value)
                {
                    _propertyValuesBinary = value;
                    OnPropertyChanged("PropertyValuesBinary");
                }
            }
        }
        private byte[] _propertyValuesBinary;
    
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
            aspnet_Users = null;
        }

        #endregion
        #region Association Fixup
    
        private void Fixupaspnet_Users(aspnet_Users previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && ReferenceEquals(previousValue.aspnet_Profile, this))
            {
                previousValue.aspnet_Profile = null;
            }
    
            if (aspnet_Users != null)
            {
                aspnet_Users.aspnet_Profile = this;
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
