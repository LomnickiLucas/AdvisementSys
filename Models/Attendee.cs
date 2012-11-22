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
    [KnownType(typeof(appointment))]
    public partial class Attendee: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'id' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _id = value;
                    OnPropertyChanged("id");
                }
            }
        }
        private System.Guid _id;
    
        [DataMember]
        public System.Guid appointmentid
        {
            get { return _appointmentid; }
            set
            {
                if (_appointmentid != value)
                {
                    ChangeTracker.RecordOriginalValue("appointmentid", _appointmentid);
                    if (!IsDeserializing)
                    {
                        if (appointment != null && appointment.appointmentid != value)
                        {
                            appointment = null;
                        }
                    }
                    _appointmentid = value;
                    OnPropertyChanged("appointmentid");
                }
            }
        }
        private System.Guid _appointmentid;
    
        [DataMember]
        public string attendee1
        {
            get { return _attendee1; }
            set
            {
                if (_attendee1 != value)
                {
                    _attendee1 = value;
                    OnPropertyChanged("attendee1");
                }
            }
        }
        private string _attendee1;
    
        [DataMember]
        public Nullable<bool> confirmed
        {
            get { return _confirmed; }
            set
            {
                if (_confirmed != value)
                {
                    _confirmed = value;
                    OnPropertyChanged("confirmed");
                }
            }
        }
        private Nullable<bool> _confirmed;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public appointment appointment
        {
            get { return _appointment; }
            set
            {
                if (!ReferenceEquals(_appointment, value))
                {
                    var previousValue = _appointment;
                    _appointment = value;
                    Fixupappointment(previousValue);
                    OnNavigationPropertyChanged("appointment");
                }
            }
        }
        private appointment _appointment;

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
            appointment = null;
        }

        #endregion
        #region Association Fixup
    
        private void Fixupappointment(appointment previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Attendees.Contains(this))
            {
                previousValue.Attendees.Remove(this);
            }
    
            if (appointment != null)
            {
                if (!appointment.Attendees.Contains(this))
                {
                    appointment.Attendees.Add(this);
                }
    
                appointmentid = appointment.appointmentid;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("appointment")
                    && (ChangeTracker.OriginalValues["appointment"] == appointment))
                {
                    ChangeTracker.OriginalValues.Remove("appointment");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("appointment", previousValue);
                }
                if (appointment != null && !appointment.ChangeTracker.ChangeTrackingEnabled)
                {
                    appointment.StartTracking();
                }
            }
        }

        #endregion
    }
}
