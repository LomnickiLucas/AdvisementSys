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
    [KnownType(typeof(issue))]
    public partial class applicationForTermOrCompleteProgramWithdrawal: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public System.Guid withdrawid
        {
            get { return _withdrawid; }
            set
            {
                if (_withdrawid != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'withdrawid' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _withdrawid = value;
                    OnPropertyChanged("withdrawid");
                }
            }
        }
        private System.Guid _withdrawid;
    
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
        public string withdrawalterm
        {
            get { return _withdrawalterm; }
            set
            {
                if (_withdrawalterm != value)
                {
                    _withdrawalterm = value;
                    OnPropertyChanged("withdrawalterm");
                }
            }
        }
        private string _withdrawalterm;
    
        [DataMember]
        public string returnterm
        {
            get { return _returnterm; }
            set
            {
                if (_returnterm != value)
                {
                    _returnterm = value;
                    OnPropertyChanged("returnterm");
                }
            }
        }
        private string _returnterm;
    
        [DataMember]
        public string reasonwithdrawal
        {
            get { return _reasonwithdrawal; }
            set
            {
                if (_reasonwithdrawal != value)
                {
                    _reasonwithdrawal = value;
                    OnPropertyChanged("reasonwithdrawal");
                }
            }
        }
        private string _reasonwithdrawal;
    
        [DataMember]
        public string continuingedcoursecode
        {
            get { return _continuingedcoursecode; }
            set
            {
                if (_continuingedcoursecode != value)
                {
                    _continuingedcoursecode = value;
                    OnPropertyChanged("continuingedcoursecode");
                }
            }
        }
        private string _continuingedcoursecode;
    
        [DataMember]
        public bool refundapproved
        {
            get { return _refundapproved; }
            set
            {
                if (_refundapproved != value)
                {
                    _refundapproved = value;
                    OnPropertyChanged("refundapproved");
                }
            }
        }
        private bool _refundapproved;
    
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
    
            if (previousValue != null && previousValue.applicationForTermOrCompleteProgramWithdrawals.Contains(this))
            {
                previousValue.applicationForTermOrCompleteProgramWithdrawals.Remove(this);
            }
    
            if (issue != null)
            {
                if (!issue.applicationForTermOrCompleteProgramWithdrawals.Contains(this))
                {
                    issue.applicationForTermOrCompleteProgramWithdrawals.Add(this);
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
