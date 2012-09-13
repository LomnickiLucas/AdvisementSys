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
    [KnownType(typeof(requiredcourse))]
    [KnownType(typeof(coordinator))]
    [KnownType(typeof(program))]
    [KnownType(typeof(professor))]
    public partial class course: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int coursecode
        {
            get { return _coursecode; }
            set
            {
                if (_coursecode != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'coursecode' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _coursecode = value;
                    OnPropertyChanged("coursecode");
                }
            }
        }
        private int _coursecode;
    
        [DataMember]
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("name");
                }
            }
        }
        private string _name;
    
        [DataMember]
        public decimal creditvalue
        {
            get { return _creditvalue; }
            set
            {
                if (_creditvalue != value)
                {
                    _creditvalue = value;
                    OnPropertyChanged("creditvalue");
                }
            }
        }
        private decimal _creditvalue;
    
        [DataMember]
        public bool pla
        {
            get { return _pla; }
            set
            {
                if (_pla != value)
                {
                    _pla = value;
                    OnPropertyChanged("pla");
                }
            }
        }
        private bool _pla;
    
        [DataMember]
        public decimal version
        {
            get { return _version; }
            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged("version");
                }
            }
        }
        private decimal _version;
    
        [DataMember]
        public string status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("status");
                }
            }
        }
        private string _status;
    
        [DataMember]
        public decimal totalhours
        {
            get { return _totalhours; }
            set
            {
                if (_totalhours != value)
                {
                    _totalhours = value;
                    OnPropertyChanged("totalhours");
                }
            }
        }
        private decimal _totalhours;
    
        [DataMember]
        public System.DateTime effectivedate
        {
            get { return _effectivedate; }
            set
            {
                if (_effectivedate != value)
                {
                    _effectivedate = value;
                    OnPropertyChanged("effectivedate");
                }
            }
        }
        private System.DateTime _effectivedate;
    
        [DataMember]
        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("description");
                }
            }
        }
        private string _description;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<requiredcourse> requiredcourses
        {
            get
            {
                if (_requiredcourses == null)
                {
                    _requiredcourses = new TrackableCollection<requiredcourse>();
                    _requiredcourses.CollectionChanged += Fixuprequiredcourses;
                }
                return _requiredcourses;
            }
            set
            {
                if (!ReferenceEquals(_requiredcourses, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_requiredcourses != null)
                    {
                        _requiredcourses.CollectionChanged -= Fixuprequiredcourses;
                    }
                    _requiredcourses = value;
                    if (_requiredcourses != null)
                    {
                        _requiredcourses.CollectionChanged += Fixuprequiredcourses;
                    }
                    OnNavigationPropertyChanged("requiredcourses");
                }
            }
        }
        private TrackableCollection<requiredcourse> _requiredcourses;
    
        [DataMember]
        public TrackableCollection<requiredcourse> requiredcourses1
        {
            get
            {
                if (_requiredcourses1 == null)
                {
                    _requiredcourses1 = new TrackableCollection<requiredcourse>();
                    _requiredcourses1.CollectionChanged += Fixuprequiredcourses1;
                }
                return _requiredcourses1;
            }
            set
            {
                if (!ReferenceEquals(_requiredcourses1, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_requiredcourses1 != null)
                    {
                        _requiredcourses1.CollectionChanged -= Fixuprequiredcourses1;
                    }
                    _requiredcourses1 = value;
                    if (_requiredcourses1 != null)
                    {
                        _requiredcourses1.CollectionChanged += Fixuprequiredcourses1;
                    }
                    OnNavigationPropertyChanged("requiredcourses1");
                }
            }
        }
        private TrackableCollection<requiredcourse> _requiredcourses1;
    
        [DataMember]
        public TrackableCollection<coordinator> coordinators
        {
            get
            {
                if (_coordinators == null)
                {
                    _coordinators = new TrackableCollection<coordinator>();
                    _coordinators.CollectionChanged += Fixupcoordinators;
                }
                return _coordinators;
            }
            set
            {
                if (!ReferenceEquals(_coordinators, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_coordinators != null)
                    {
                        _coordinators.CollectionChanged -= Fixupcoordinators;
                    }
                    _coordinators = value;
                    if (_coordinators != null)
                    {
                        _coordinators.CollectionChanged += Fixupcoordinators;
                    }
                    OnNavigationPropertyChanged("coordinators");
                }
            }
        }
        private TrackableCollection<coordinator> _coordinators;
    
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
    
        [DataMember]
        public TrackableCollection<professor> professors
        {
            get
            {
                if (_professors == null)
                {
                    _professors = new TrackableCollection<professor>();
                    _professors.CollectionChanged += Fixupprofessors;
                }
                return _professors;
            }
            set
            {
                if (!ReferenceEquals(_professors, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_professors != null)
                    {
                        _professors.CollectionChanged -= Fixupprofessors;
                    }
                    _professors = value;
                    if (_professors != null)
                    {
                        _professors.CollectionChanged += Fixupprofessors;
                    }
                    OnNavigationPropertyChanged("professors");
                }
            }
        }
        private TrackableCollection<professor> _professors;

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
            requiredcourses.Clear();
            requiredcourses1.Clear();
            coordinators.Clear();
            programs.Clear();
            professors.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void Fixuprequiredcourses(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (requiredcourse item in e.NewItems)
                {
                    item.course = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("requiredcourses", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (requiredcourse item in e.OldItems)
                {
                    if (ReferenceEquals(item.course, this))
                    {
                        item.course = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("requiredcourses", item);
                    }
                }
            }
        }
    
        private void Fixuprequiredcourses1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (requiredcourse item in e.NewItems)
                {
                    item.course1 = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("requiredcourses1", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (requiredcourse item in e.OldItems)
                {
                    if (ReferenceEquals(item.course1, this))
                    {
                        item.course1 = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("requiredcourses1", item);
                    }
                }
            }
        }
    
        private void Fixupcoordinators(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (coordinator item in e.NewItems)
                {
                    if (!item.courses.Contains(this))
                    {
                        item.courses.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("coordinators", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (coordinator item in e.OldItems)
                {
                    if (item.courses.Contains(this))
                    {
                        item.courses.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("coordinators", item);
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
                    if (!item.courses.Contains(this))
                    {
                        item.courses.Add(this);
                    }
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
                    if (item.courses.Contains(this))
                    {
                        item.courses.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("programs", item);
                    }
                }
            }
        }
    
        private void Fixupprofessors(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (professor item in e.NewItems)
                {
                    if (!item.courses.Contains(this))
                    {
                        item.courses.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("professors", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (professor item in e.OldItems)
                {
                    if (item.courses.Contains(this))
                    {
                        item.courses.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("professors", item);
                    }
                }
            }
        }

        #endregion
    }
}
