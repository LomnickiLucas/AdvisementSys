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
    [KnownType(typeof(course))]
    public partial class requiredcourse: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid unique
        {
            get { return _unique; }
            set
            {
                if (_unique != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'unique' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _unique = value;
                    OnPropertyChanged("unique");
                }
            }
        }
        private System.Guid _unique;
    
        [DataMember]
        public string coursecode
        {
            get { return _coursecode; }
            set
            {
                if (_coursecode != value)
                {
                    ChangeTracker.RecordOriginalValue("coursecode", _coursecode);
                    if (!IsDeserializing)
                    {
                        if (course != null && course.coursecode != value)
                        {
                            course = null;
                        }
                    }
                    _coursecode = value;
                    OnPropertyChanged("coursecode");
                }
            }
        }
        private string _coursecode;
    
        [DataMember]
        public string reqcoursecode
        {
            get { return _reqcoursecode; }
            set
            {
                if (_reqcoursecode != value)
                {
                    ChangeTracker.RecordOriginalValue("reqcoursecode", _reqcoursecode);
                    if (!IsDeserializing)
                    {
                        if (course1 != null && course1.coursecode != value)
                        {
                            course1 = null;
                        }
                    }
                    _reqcoursecode = value;
                    OnPropertyChanged("reqcoursecode");
                }
            }
        }
        private string _reqcoursecode;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public course course
        {
            get { return _course; }
            set
            {
                if (!ReferenceEquals(_course, value))
                {
                    var previousValue = _course;
                    _course = value;
                    Fixupcourse(previousValue);
                    OnNavigationPropertyChanged("course");
                }
            }
        }
        private course _course;
    
        [DataMember]
        public course course1
        {
            get { return _course1; }
            set
            {
                if (!ReferenceEquals(_course1, value))
                {
                    var previousValue = _course1;
                    _course1 = value;
                    Fixupcourse1(previousValue);
                    OnNavigationPropertyChanged("course1");
                }
            }
        }
        private course _course1;

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
            course = null;
            course1 = null;
        }

        #endregion
        #region Association Fixup
    
        private void Fixupcourse(course previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.requiredcourses.Contains(this))
            {
                previousValue.requiredcourses.Remove(this);
            }
    
            if (course != null)
            {
                if (!course.requiredcourses.Contains(this))
                {
                    course.requiredcourses.Add(this);
                }
    
                coursecode = course.coursecode;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("course")
                    && (ChangeTracker.OriginalValues["course"] == course))
                {
                    ChangeTracker.OriginalValues.Remove("course");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("course", previousValue);
                }
                if (course != null && !course.ChangeTracker.ChangeTrackingEnabled)
                {
                    course.StartTracking();
                }
            }
        }
    
        private void Fixupcourse1(course previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.requiredcourses1.Contains(this))
            {
                previousValue.requiredcourses1.Remove(this);
            }
    
            if (course1 != null)
            {
                if (!course1.requiredcourses1.Contains(this))
                {
                    course1.requiredcourses1.Add(this);
                }
    
                reqcoursecode = course1.coursecode;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("course1")
                    && (ChangeTracker.OriginalValues["course1"] == course1))
                {
                    ChangeTracker.OriginalValues.Remove("course1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("course1", previousValue);
                }
                if (course1 != null && !course1.ChangeTracker.ChangeTrackingEnabled)
                {
                    course1.StartTracking();
                }
            }
        }

        #endregion
    }
}
