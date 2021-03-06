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
    [KnownType(typeof(campu))]
    [KnownType(typeof(faculty))]
    [KnownType(typeof(course))]
    public partial class professor: IObjectWithChangeTracker, INotifyPropertyChanged
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
        public string campus
        {
            get { return _campus; }
            set
            {
                if (_campus != value)
                {
                    ChangeTracker.RecordOriginalValue("campus", _campus);
                    if (!IsDeserializing)
                    {
                        if (campu != null && campu.cname != value)
                        {
                            campu = null;
                        }
                    }
                    _campus = value;
                    OnPropertyChanged("campus");
                }
            }
        }
        private string _campus;

        #endregion
        #region Navigation Properties
    
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
        public TrackableCollection<course> courses
        {
            get
            {
                if (_courses == null)
                {
                    _courses = new TrackableCollection<course>();
                    _courses.CollectionChanged += Fixupcourses;
                }
                return _courses;
            }
            set
            {
                if (!ReferenceEquals(_courses, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_courses != null)
                    {
                        _courses.CollectionChanged -= Fixupcourses;
                    }
                    _courses = value;
                    if (_courses != null)
                    {
                        _courses.CollectionChanged += Fixupcourses;
                    }
                    OnNavigationPropertyChanged("courses");
                }
            }
        }
        private TrackableCollection<course> _courses;

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
            campu = null;
            faculty1 = null;
            courses.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void Fixupcampu(campu previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.professors.Contains(this))
            {
                previousValue.professors.Remove(this);
            }
    
            if (campu != null)
            {
                if (!campu.professors.Contains(this))
                {
                    campu.professors.Add(this);
                }
    
                campus = campu.cname;
            }
            else if (!skipKeys)
            {
                campus = null;
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
    
        private void Fixupfaculty1(faculty previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.professors.Contains(this))
            {
                previousValue.professors.Remove(this);
            }
    
            if (faculty1 != null)
            {
                if (!faculty1.professors.Contains(this))
                {
                    faculty1.professors.Add(this);
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
    
        private void Fixupcourses(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (course item in e.NewItems)
                {
                    if (!item.professors.Contains(this))
                    {
                        item.professors.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("courses", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (course item in e.OldItems)
                {
                    if (item.professors.Contains(this))
                    {
                        item.professors.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("courses", item);
                    }
                }
            }
        }

        #endregion
    }
}
