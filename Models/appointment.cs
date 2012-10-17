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
    [KnownType(typeof(employee))]
    [KnownType(typeof(student))]
    [KnownType(typeof(campu))]
    public partial class appointment: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid appointmentid
        {
            get { return _appointmentid; }
            set
            {
                if (_appointmentid != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'appointmentid' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _appointmentid = value;
                    OnPropertyChanged("appointmentid");
                }
            }
        }
        private System.Guid _appointmentid;
    
        [DataMember]
        public System.DateTime starttime
        {
            get { return _starttime; }
            set
            {
                if (_starttime != value)
                {
                    _starttime = value;
                    OnPropertyChanged("starttime");
                }
            }
        }
        private System.DateTime _starttime;
    
        [DataMember]
        public System.DateTime endtime
        {
            get { return _endtime; }
            set
            {
                if (_endtime != value)
                {
                    _endtime = value;
                    OnPropertyChanged("endtime");
                }
            }
        }
        private System.DateTime _endtime;
    
        [DataMember]
        public string subject
        {
            get { return _subject; }
            set
            {
                if (_subject != value)
                {
                    _subject = value;
                    OnPropertyChanged("subject");
                }
            }
        }
        private string _subject;
    
        [DataMember]
        public bool confirmed
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
        private bool _confirmed;
    
        [DataMember]
        public string reasonForCancelation
        {
            get { return _reasonForCancelation; }
            set
            {
                if (_reasonForCancelation != value)
                {
                    _reasonForCancelation = value;
                    OnPropertyChanged("reasonForCancelation");
                }
            }
        }
        private string _reasonForCancelation;
    
        [DataMember]
        public string employeeid
        {
            get { return _employeeid; }
            set
            {
                if (_employeeid != value)
                {
                    ChangeTracker.RecordOriginalValue("employeeid", _employeeid);
                    if (!IsDeserializing)
                    {
                        if (employee != null && employee.employeeid != value)
                        {
                            employee = null;
                        }
                    }
                    _employeeid = value;
                    OnPropertyChanged("employeeid");
                }
            }
        }
        private string _employeeid;
    
        [DataMember]
        public string studentid
        {
            get { return _studentid; }
            set
            {
                if (_studentid != value)
                {
                    ChangeTracker.RecordOriginalValue("studentid", _studentid);
                    if (!IsDeserializing)
                    {
                        if (student != null && student.studentid != value)
                        {
                            student = null;
                        }
                    }
                    _studentid = value;
                    OnPropertyChanged("studentid");
                }
            }
        }
        private string _studentid;
    
        [DataMember]
        public string cname
        {
            get { return _cname; }
            set
            {
                if (_cname != value)
                {
                    ChangeTracker.RecordOriginalValue("cname", _cname);
                    if (!IsDeserializing)
                    {
                        if (campu != null && campu.cname != value)
                        {
                            campu = null;
                        }
                    }
                    _cname = value;
                    OnPropertyChanged("cname");
                }
            }
        }
        private string _cname;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public employee employee
        {
            get { return _employee; }
            set
            {
                if (!ReferenceEquals(_employee, value))
                {
                    var previousValue = _employee;
                    _employee = value;
                    Fixupemployee(previousValue);
                    OnNavigationPropertyChanged("employee");
                }
            }
        }
        private employee _employee;
    
        [DataMember]
        public student student
        {
            get { return _student; }
            set
            {
                if (!ReferenceEquals(_student, value))
                {
                    var previousValue = _student;
                    _student = value;
                    Fixupstudent(previousValue);
                    OnNavigationPropertyChanged("student");
                }
            }
        }
        private student _student;
    
        [DataMember]
        public campu campu
        {
            get { return _campu; }
            set
            {
                if (!ReferenceEquals(_campu, value))
                {
                    var previousValue = _campu;
                    _campu = value;
                    Fixupcampu(previousValue);
                    OnNavigationPropertyChanged("campu");
                }
            }
        }
        private campu _campu;

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
            employee = null;
            student = null;
            campu = null;
        }

        #endregion
        #region Association Fixup
    
        private void Fixupemployee(employee previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.appointments.Contains(this))
            {
                previousValue.appointments.Remove(this);
            }
    
            if (employee != null)
            {
                if (!employee.appointments.Contains(this))
                {
                    employee.appointments.Add(this);
                }
    
                employeeid = employee.employeeid;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("employee")
                    && (ChangeTracker.OriginalValues["employee"] == employee))
                {
                    ChangeTracker.OriginalValues.Remove("employee");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("employee", previousValue);
                }
                if (employee != null && !employee.ChangeTracker.ChangeTrackingEnabled)
                {
                    employee.StartTracking();
                }
            }
        }
    
        private void Fixupstudent(student previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.appointments.Contains(this))
            {
                previousValue.appointments.Remove(this);
            }
    
            if (student != null)
            {
                if (!student.appointments.Contains(this))
                {
                    student.appointments.Add(this);
                }
    
                studentid = student.studentid;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("student")
                    && (ChangeTracker.OriginalValues["student"] == student))
                {
                    ChangeTracker.OriginalValues.Remove("student");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("student", previousValue);
                }
                if (student != null && !student.ChangeTracker.ChangeTrackingEnabled)
                {
                    student.StartTracking();
                }
            }
        }
    
        private void Fixupcampu(campu previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.appointments.Contains(this))
            {
                previousValue.appointments.Remove(this);
            }
    
            if (campu != null)
            {
                if (!campu.appointments.Contains(this))
                {
                    campu.appointments.Add(this);
                }
    
                cname = campu.cname;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("campu")
                    && (ChangeTracker.OriginalValues["campu"] == campu))
                {
                    ChangeTracker.OriginalValues.Remove("campu");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("campu", previousValue);
                }
                if (campu != null && !campu.ChangeTracker.ChangeTrackingEnabled)
                {
                    campu.StartTracking();
                }
            }
        }

        #endregion
    }
}
