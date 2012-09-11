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

namespace AdvisementSys
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(issue))]
    public partial class applicationForReadmission: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid readmissionid
        {
            get { return _readmissionid; }
            set
            {
                if (_readmissionid != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'readmissionid' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _readmissionid = value;
                    OnPropertyChanged("readmissionid");
                }
            }
        }
        private System.Guid _readmissionid;
    
        [DataMember]
        public System.Guid issueid
        {
            get { return _issueid; }
            set
            {
                if (_issueid != value)
                {
                    ChangeTracker.RecordOriginalValue("issueid", _issueid);
                    if (!IsDeserializing)
                    {
                        if (issue != null && issue.issueid != value)
                        {
                            issue = null;
                        }
                    }
                    _issueid = value;
                    OnPropertyChanged("issueid");
                }
            }
        }
        private System.Guid _issueid;
    
        [DataMember]
        public System.DateTime date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged("date");
                }
            }
        }
        private System.DateTime _date;
    
        [DataMember]
        public string oldprogram
        {
            get { return _oldprogram; }
            set
            {
                if (_oldprogram != value)
                {
                    _oldprogram = value;
                    OnPropertyChanged("oldprogram");
                }
            }
        }
        private string _oldprogram;
    
        [DataMember]
        public bool oldfulltime
        {
            get { return _oldfulltime; }
            set
            {
                if (_oldfulltime != value)
                {
                    _oldfulltime = value;
                    OnPropertyChanged("oldfulltime");
                }
            }
        }
        private bool _oldfulltime;
    
        [DataMember]
        public string oldsemyear
        {
            get { return _oldsemyear; }
            set
            {
                if (_oldsemyear != value)
                {
                    _oldsemyear = value;
                    OnPropertyChanged("oldsemyear");
                }
            }
        }
        private string _oldsemyear;
    
        [DataMember]
        public string oldcampus
        {
            get { return _oldcampus; }
            set
            {
                if (_oldcampus != value)
                {
                    _oldcampus = value;
                    OnPropertyChanged("oldcampus");
                }
            }
        }
        private string _oldcampus;
    
        [DataMember]
        public string newprogram
        {
            get { return _newprogram; }
            set
            {
                if (_newprogram != value)
                {
                    _newprogram = value;
                    OnPropertyChanged("newprogram");
                }
            }
        }
        private string _newprogram;
    
        [DataMember]
        public bool newfulltime
        {
            get { return _newfulltime; }
            set
            {
                if (_newfulltime != value)
                {
                    _newfulltime = value;
                    OnPropertyChanged("newfulltime");
                }
            }
        }
        private bool _newfulltime;
    
        [DataMember]
        public string newsemyear
        {
            get { return _newsemyear; }
            set
            {
                if (_newsemyear != value)
                {
                    _newsemyear = value;
                    OnPropertyChanged("newsemyear");
                }
            }
        }
        private string _newsemyear;
    
        [DataMember]
        public string newcampus
        {
            get { return _newcampus; }
            set
            {
                if (_newcampus != value)
                {
                    _newcampus = value;
                    OnPropertyChanged("newcampus");
                }
            }
        }
        private string _newcampus;
    
        [DataMember]
        public string recamendedprogram
        {
            get { return _recamendedprogram; }
            set
            {
                if (_recamendedprogram != value)
                {
                    _recamendedprogram = value;
                    OnPropertyChanged("recamendedprogram");
                }
            }
        }
        private string _recamendedprogram;
    
        [DataMember]
        public Nullable<bool> recamendedfulltime
        {
            get { return _recamendedfulltime; }
            set
            {
                if (_recamendedfulltime != value)
                {
                    _recamendedfulltime = value;
                    OnPropertyChanged("recamendedfulltime");
                }
            }
        }
        private Nullable<bool> _recamendedfulltime;
    
        [DataMember]
        public string recamendedsemyear
        {
            get { return _recamendedsemyear; }
            set
            {
                if (_recamendedsemyear != value)
                {
                    _recamendedsemyear = value;
                    OnPropertyChanged("recamendedsemyear");
                }
            }
        }
        private string _recamendedsemyear;
    
        [DataMember]
        public string recamendedcampus
        {
            get { return _recamendedcampus; }
            set
            {
                if (_recamendedcampus != value)
                {
                    _recamendedcampus = value;
                    OnPropertyChanged("recamendedcampus");
                }
            }
        }
        private string _recamendedcampus;
    
        [DataMember]
        public bool approved
        {
            get { return _approved; }
            set
            {
                if (_approved != value)
                {
                    _approved = value;
                    OnPropertyChanged("approved");
                }
            }
        }
        private bool _approved;
    
        [DataMember]
        public string acceptableconditional
        {
            get { return _acceptableconditional; }
            set
            {
                if (_acceptableconditional != value)
                {
                    _acceptableconditional = value;
                    OnPropertyChanged("acceptableconditional");
                }
            }
        }
        private string _acceptableconditional;
    
        [DataMember]
        public string term
        {
            get { return _term; }
            set
            {
                if (_term != value)
                {
                    _term = value;
                    OnPropertyChanged("term");
                }
            }
        }
        private string _term;
    
        [DataMember]
        public string conditions
        {
            get { return _conditions; }
            set
            {
                if (_conditions != value)
                {
                    _conditions = value;
                    OnPropertyChanged("conditions");
                }
            }
        }
        private string _conditions;
    
        [DataMember]
        public string probation
        {
            get { return _probation; }
            set
            {
                if (_probation != value)
                {
                    _probation = value;
                    OnPropertyChanged("probation");
                }
            }
        }
        private string _probation;
    
        [DataMember]
        public bool special
        {
            get { return _special; }
            set
            {
                if (_special != value)
                {
                    _special = value;
                    OnPropertyChanged("special");
                }
            }
        }
        private bool _special;
    
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
        public string purpose
        {
            get { return _purpose; }
            set
            {
                if (_purpose != value)
                {
                    _purpose = value;
                    OnPropertyChanged("purpose");
                }
            }
        }
        private string _purpose;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public issue issue
        {
            get { return _issue; }
            set
            {
                if (!ReferenceEquals(_issue, value))
                {
                    var previousValue = _issue;
                    _issue = value;
                    Fixupissue(previousValue);
                    OnNavigationPropertyChanged("issue");
                }
            }
        }
        private issue _issue;

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
            issue = null;
        }

        #endregion
        #region Association Fixup
    
        private void Fixupissue(issue previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.applicationForReadmissions.Contains(this))
            {
                previousValue.applicationForReadmissions.Remove(this);
            }
    
            if (issue != null)
            {
                if (!issue.applicationForReadmissions.Contains(this))
                {
                    issue.applicationForReadmissions.Add(this);
                }
    
                issueid = issue.issueid;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("issue")
                    && (ChangeTracker.OriginalValues["issue"] == issue))
                {
                    ChangeTracker.OriginalValues.Remove("issue");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("issue", previousValue);
                }
                if (issue != null && !issue.ChangeTracker.ChangeTrackingEnabled)
                {
                    issue.StartTracking();
                }
            }
        }

        #endregion
    }
}
