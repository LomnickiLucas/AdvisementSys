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
    [KnownType(typeof(applicationForReadmission))]
    [KnownType(typeof(applicationForTermOrCompleteProgramWithdrawal))]
    [KnownType(typeof(student))]
    [KnownType(typeof(note))]
    [KnownType(typeof(part_timeAnd_orAdditionalCourseRegistrationForm))]
    [KnownType(typeof(probationaryContractPlan))]
    [KnownType(typeof(requestForLateEnrolment))]
    public partial class issue: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid issueid
        {
            get { return _issueid; }
            set
            {
                if (_issueid != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'issueid' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _issueid = value;
                    OnPropertyChanged("issueid");
                }
            }
        }
        private System.Guid _issueid;
    
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
        public string issuename
        {
            get { return _issuename; }
            set
            {
                if (_issuename != value)
                {
                    _issuename = value;
                    OnPropertyChanged("issuename");
                }
            }
        }
        private string _issuename;
    
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
        public string urgency
        {
            get { return _urgency; }
            set
            {
                if (_urgency != value)
                {
                    _urgency = value;
                    OnPropertyChanged("urgency");
                }
            }
        }
        private string _urgency;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<applicationForReadmission> applicationForReadmissions
        {
            get
            {
                if (_applicationForReadmissions == null)
                {
                    _applicationForReadmissions = new TrackableCollection<applicationForReadmission>();
                    _applicationForReadmissions.CollectionChanged += FixupapplicationForReadmissions;
                }
                return _applicationForReadmissions;
            }
            set
            {
                if (!ReferenceEquals(_applicationForReadmissions, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_applicationForReadmissions != null)
                    {
                        _applicationForReadmissions.CollectionChanged -= FixupapplicationForReadmissions;
                    }
                    _applicationForReadmissions = value;
                    if (_applicationForReadmissions != null)
                    {
                        _applicationForReadmissions.CollectionChanged += FixupapplicationForReadmissions;
                    }
                    OnNavigationPropertyChanged("applicationForReadmissions");
                }
            }
        }
        private TrackableCollection<applicationForReadmission> _applicationForReadmissions;
    
        [DataMember]
        public TrackableCollection<applicationForTermOrCompleteProgramWithdrawal> applicationForTermOrCompleteProgramWithdrawals
        {
            get
            {
                if (_applicationForTermOrCompleteProgramWithdrawals == null)
                {
                    _applicationForTermOrCompleteProgramWithdrawals = new TrackableCollection<applicationForTermOrCompleteProgramWithdrawal>();
                    _applicationForTermOrCompleteProgramWithdrawals.CollectionChanged += FixupapplicationForTermOrCompleteProgramWithdrawals;
                }
                return _applicationForTermOrCompleteProgramWithdrawals;
            }
            set
            {
                if (!ReferenceEquals(_applicationForTermOrCompleteProgramWithdrawals, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_applicationForTermOrCompleteProgramWithdrawals != null)
                    {
                        _applicationForTermOrCompleteProgramWithdrawals.CollectionChanged -= FixupapplicationForTermOrCompleteProgramWithdrawals;
                    }
                    _applicationForTermOrCompleteProgramWithdrawals = value;
                    if (_applicationForTermOrCompleteProgramWithdrawals != null)
                    {
                        _applicationForTermOrCompleteProgramWithdrawals.CollectionChanged += FixupapplicationForTermOrCompleteProgramWithdrawals;
                    }
                    OnNavigationPropertyChanged("applicationForTermOrCompleteProgramWithdrawals");
                }
            }
        }
        private TrackableCollection<applicationForTermOrCompleteProgramWithdrawal> _applicationForTermOrCompleteProgramWithdrawals;
    
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
        public TrackableCollection<part_timeAnd_orAdditionalCourseRegistrationForm> part_timeAnd_orAdditionalCourseRegistrationForm
        {
            get
            {
                if (_part_timeAnd_orAdditionalCourseRegistrationForm == null)
                {
                    _part_timeAnd_orAdditionalCourseRegistrationForm = new TrackableCollection<part_timeAnd_orAdditionalCourseRegistrationForm>();
                    _part_timeAnd_orAdditionalCourseRegistrationForm.CollectionChanged += Fixuppart_timeAnd_orAdditionalCourseRegistrationForm;
                }
                return _part_timeAnd_orAdditionalCourseRegistrationForm;
            }
            set
            {
                if (!ReferenceEquals(_part_timeAnd_orAdditionalCourseRegistrationForm, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_part_timeAnd_orAdditionalCourseRegistrationForm != null)
                    {
                        _part_timeAnd_orAdditionalCourseRegistrationForm.CollectionChanged -= Fixuppart_timeAnd_orAdditionalCourseRegistrationForm;
                    }
                    _part_timeAnd_orAdditionalCourseRegistrationForm = value;
                    if (_part_timeAnd_orAdditionalCourseRegistrationForm != null)
                    {
                        _part_timeAnd_orAdditionalCourseRegistrationForm.CollectionChanged += Fixuppart_timeAnd_orAdditionalCourseRegistrationForm;
                    }
                    OnNavigationPropertyChanged("part_timeAnd_orAdditionalCourseRegistrationForm");
                }
            }
        }
        private TrackableCollection<part_timeAnd_orAdditionalCourseRegistrationForm> _part_timeAnd_orAdditionalCourseRegistrationForm;
    
        [DataMember]
        public TrackableCollection<probationaryContractPlan> probationaryContractPlans
        {
            get
            {
                if (_probationaryContractPlans == null)
                {
                    _probationaryContractPlans = new TrackableCollection<probationaryContractPlan>();
                    _probationaryContractPlans.CollectionChanged += FixupprobationaryContractPlans;
                }
                return _probationaryContractPlans;
            }
            set
            {
                if (!ReferenceEquals(_probationaryContractPlans, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_probationaryContractPlans != null)
                    {
                        _probationaryContractPlans.CollectionChanged -= FixupprobationaryContractPlans;
                    }
                    _probationaryContractPlans = value;
                    if (_probationaryContractPlans != null)
                    {
                        _probationaryContractPlans.CollectionChanged += FixupprobationaryContractPlans;
                    }
                    OnNavigationPropertyChanged("probationaryContractPlans");
                }
            }
        }
        private TrackableCollection<probationaryContractPlan> _probationaryContractPlans;
    
        [DataMember]
        public TrackableCollection<requestForLateEnrolment> requestForLateEnrolments
        {
            get
            {
                if (_requestForLateEnrolments == null)
                {
                    _requestForLateEnrolments = new TrackableCollection<requestForLateEnrolment>();
                    _requestForLateEnrolments.CollectionChanged += FixuprequestForLateEnrolments;
                }
                return _requestForLateEnrolments;
            }
            set
            {
                if (!ReferenceEquals(_requestForLateEnrolments, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_requestForLateEnrolments != null)
                    {
                        _requestForLateEnrolments.CollectionChanged -= FixuprequestForLateEnrolments;
                    }
                    _requestForLateEnrolments = value;
                    if (_requestForLateEnrolments != null)
                    {
                        _requestForLateEnrolments.CollectionChanged += FixuprequestForLateEnrolments;
                    }
                    OnNavigationPropertyChanged("requestForLateEnrolments");
                }
            }
        }
        private TrackableCollection<requestForLateEnrolment> _requestForLateEnrolments;

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
            applicationForReadmissions.Clear();
            applicationForTermOrCompleteProgramWithdrawals.Clear();
            student = null;
            notes.Clear();
            part_timeAnd_orAdditionalCourseRegistrationForm.Clear();
            probationaryContractPlans.Clear();
            requestForLateEnrolments.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void Fixupstudent(student previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.issues.Contains(this))
            {
                previousValue.issues.Remove(this);
            }
    
            if (student != null)
            {
                if (!student.issues.Contains(this))
                {
                    student.issues.Add(this);
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
    
        private void FixupapplicationForReadmissions(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (applicationForReadmission item in e.NewItems)
                {
                    item.issue = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("applicationForReadmissions", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (applicationForReadmission item in e.OldItems)
                {
                    if (ReferenceEquals(item.issue, this))
                    {
                        item.issue = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("applicationForReadmissions", item);
                    }
                }
            }
        }
    
        private void FixupapplicationForTermOrCompleteProgramWithdrawals(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (applicationForTermOrCompleteProgramWithdrawal item in e.NewItems)
                {
                    item.issue = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("applicationForTermOrCompleteProgramWithdrawals", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (applicationForTermOrCompleteProgramWithdrawal item in e.OldItems)
                {
                    if (ReferenceEquals(item.issue, this))
                    {
                        item.issue = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("applicationForTermOrCompleteProgramWithdrawals", item);
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
                    item.issue = this;
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
                    if (ReferenceEquals(item.issue, this))
                    {
                        item.issue = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("notes", item);
                    }
                }
            }
        }
    
        private void Fixuppart_timeAnd_orAdditionalCourseRegistrationForm(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (part_timeAnd_orAdditionalCourseRegistrationForm item in e.NewItems)
                {
                    item.issue = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("part_timeAnd_orAdditionalCourseRegistrationForm", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (part_timeAnd_orAdditionalCourseRegistrationForm item in e.OldItems)
                {
                    if (ReferenceEquals(item.issue, this))
                    {
                        item.issue = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("part_timeAnd_orAdditionalCourseRegistrationForm", item);
                    }
                }
            }
        }
    
        private void FixupprobationaryContractPlans(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (probationaryContractPlan item in e.NewItems)
                {
                    item.issue = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("probationaryContractPlans", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (probationaryContractPlan item in e.OldItems)
                {
                    if (ReferenceEquals(item.issue, this))
                    {
                        item.issue = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("probationaryContractPlans", item);
                    }
                }
            }
        }
    
        private void FixuprequestForLateEnrolments(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (requestForLateEnrolment item in e.NewItems)
                {
                    item.issue = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("requestForLateEnrolments", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (requestForLateEnrolment item in e.OldItems)
                {
                    if (ReferenceEquals(item.issue, this))
                    {
                        item.issue = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("requestForLateEnrolments", item);
                    }
                }
            }
        }

        #endregion
    }
}
