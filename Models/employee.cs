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
    [KnownType(typeof(faculty))]
    [KnownType(typeof(role))]
    [KnownType(typeof(note))]
    [KnownType(typeof(program))]
    public partial class employee: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public string employeeid
        {
            get { return _employeeid; }
            set
            {
                if (_employeeid != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'employeeid' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _employeeid = value;
                    OnPropertyChanged("employeeid");
                }
            }
        }
        private string _employeeid;
    
        [DataMember]
        public string fname
        {
            get { return _fname; }
            set
            {
                if (_fname != value)
                {
                    _fname = value;
                    OnPropertyChanged("fname");
                }
            }
        }
        private string _fname;
    
        [DataMember]
        public string lname
        {
            get { return _lname; }
            set
            {
                if (_lname != value)
                {
                    _lname = value;
                    OnPropertyChanged("lname");
                }
            }
        }
        private string _lname;
    
        [DataMember]
        public string phonenum
        {
            get { return _phonenum; }
            set
            {
                if (_phonenum != value)
                {
                    _phonenum = value;
                    OnPropertyChanged("phonenum");
                }
            }
        }
        private string _phonenum;
    
        [DataMember]
        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("email");
                }
            }
        }
        private string _email;
    
        [DataMember]
        public string faculty
        {
            get { return _faculty; }
            set
            {
                if (_faculty != value)
                {
                    ChangeTracker.RecordOriginalValue("faculty", _faculty);
                    if (!IsDeserializing)
                    {
                        if (faculty1 != null && faculty1.fname != value)
                        {
                            faculty1 = null;
                        }
                    }
                    _faculty = value;
                    OnPropertyChanged("faculty");
                }
            }
        }
        private string _faculty;
    
        [DataMember]
        public string role
        {
            get { return _role; }
            set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged("role");
                }
            }
        }
        private string _role;
    
        [DataMember]
        public string password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("password");
                }
            }
        }
        private string _password;
    
        [DataMember]
        public string passhint
        {
            get { return _passhint; }
            set
            {
                if (_passhint != value)
                {
                    _passhint = value;
                    OnPropertyChanged("passhint");
                }
            }
        }
        private string _passhint;
    
        [DataMember]
        public string permcode
        {
            get { return _permcode; }
            set
            {
                if (_permcode != value)
                {
                    ChangeTracker.RecordOriginalValue("permcode", _permcode);
                    if (!IsDeserializing)
                    {
                        if (role1 != null && role1.permcode != value)
                        {
                            role1 = null;
                        }
                    }
                    _permcode = value;
                    OnPropertyChanged("permcode");
                }
            }
        }
        private string _permcode;
    
        [DataMember]
        public bool archived
        {
            get { return _archived; }
            set
            {
                if (_archived != value)
                {
                    _archived = value;
                    OnPropertyChanged("archived");
                }
            }
        }
        private bool _archived;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<appointment> appointments
        {
            get
            {
                if (_appointments == null)
                {
                    _appointments = new TrackableCollection<appointment>();
                    _appointments.CollectionChanged += Fixupappointments;
                }
                return _appointments;
            }
            set
            {
                if (!ReferenceEquals(_appointments, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_appointments != null)
                    {
                        _appointments.CollectionChanged -= Fixupappointments;
                    }
                    _appointments = value;
                    if (_appointments != null)
                    {
                        _appointments.CollectionChanged += Fixupappointments;
                    }
                    OnNavigationPropertyChanged("appointments");
                }
            }
        }
        private TrackableCollection<appointment> _appointments;
    
        [DataMember]
        public faculty faculty1
        {
            get { return _faculty1; }
            set
            {
                if (!ReferenceEquals(_faculty1, value))
                {
                    var previousValue = _faculty1;
                    _faculty1 = value;
                    Fixupfaculty1(previousValue);
                    OnNavigationPropertyChanged("faculty1");
                }
            }
        }
        private faculty _faculty1;
    
        [DataMember]
        public role role1
        {
            get { return _role1; }
            set
            {
                if (!ReferenceEquals(_role1, value))
                {
                    var previousValue = _role1;
                    _role1 = value;
                    Fixuprole1(previousValue);
                    OnNavigationPropertyChanged("role1");
                }
            }
        }
        private role _role1;
    
        [DataMember]
        public TrackableCollection<note> notes
        {
            get
            {
                if (_notes == null)
                {
                    _notes = new TrackableCollection<note>();
                    _notes.CollectionChanged += Fixupnotes;
                }
                return _notes;
            }
            set
            {
                if (!ReferenceEquals(_notes, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_notes != null)
                    {
                        _notes.CollectionChanged -= Fixupnotes;
                    }
                    _notes = value;
                    if (_notes != null)
                    {
                        _notes.CollectionChanged += Fixupnotes;
                    }
                    OnNavigationPropertyChanged("notes");
                }
            }
        }
        private TrackableCollection<note> _notes;
    
        [DataMember]
        public TrackableCollection<program> programs
        {
            get
            {
                if (_programs == null)
                {
                    _programs = new TrackableCollection<program>();
                    _programs.CollectionChanged += Fixupprograms;
                }
                return _programs;
            }
            set
            {
                if (!ReferenceEquals(_programs, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_programs != null)
                    {
                        _programs.CollectionChanged -= Fixupprograms;
                    }
                    _programs = value;
                    if (_programs != null)
                    {
                        _programs.CollectionChanged += Fixupprograms;
                    }
                    OnNavigationPropertyChanged("programs");
                }
            }
        }
        private TrackableCollection<program> _programs;

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
            appointments.Clear();
            faculty1 = null;
            role1 = null;
            notes.Clear();
            programs.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void Fixupfaculty1(faculty previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.employees.Contains(this))
            {
                previousValue.employees.Remove(this);
            }
    
            if (faculty1 != null)
            {
                if (!faculty1.employees.Contains(this))
                {
                    faculty1.employees.Add(this);
                }
    
                faculty = faculty1.fname;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("faculty1")
                    && (ChangeTracker.OriginalValues["faculty1"] == faculty1))
                {
                    ChangeTracker.OriginalValues.Remove("faculty1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("faculty1", previousValue);
                }
                if (faculty1 != null && !faculty1.ChangeTracker.ChangeTrackingEnabled)
                {
                    faculty1.StartTracking();
                }
            }
        }
    
        private void Fixuprole1(role previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.employees.Contains(this))
            {
                previousValue.employees.Remove(this);
            }
    
            if (role1 != null)
            {
                if (!role1.employees.Contains(this))
                {
                    role1.employees.Add(this);
                }
    
                permcode = role1.permcode;
            }
            else if (!skipKeys)
            {
                permcode = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("role1")
                    && (ChangeTracker.OriginalValues["role1"] == role1))
                {
                    ChangeTracker.OriginalValues.Remove("role1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("role1", previousValue);
                }
                if (role1 != null && !role1.ChangeTracker.ChangeTrackingEnabled)
                {
                    role1.StartTracking();
                }
            }
        }
    
        private void Fixupappointments(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (appointment item in e.NewItems)
                {
                    item.employee = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("appointments", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (appointment item in e.OldItems)
                {
                    if (ReferenceEquals(item.employee, this))
                    {
                        item.employee = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("appointments", item);
                    }
                }
            }
        }
    
        private void Fixupnotes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (note item in e.NewItems)
                {
                    item.employee = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("notes", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (note item in e.OldItems)
                {
                    if (ReferenceEquals(item.employee, this))
                    {
                        item.employee = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("notes", item);
                    }
                }
            }
        }
    
        private void Fixupprograms(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (program item in e.NewItems)
                {
                    item.employee = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("programs", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (program item in e.OldItems)
                {
                    if (ReferenceEquals(item.employee, this))
                    {
                        item.employee = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("programs", item);
                    }
                }
            }
        }

        #endregion
    }
}
