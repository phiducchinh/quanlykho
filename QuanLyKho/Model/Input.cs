//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyKho.Model
{
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public partial class Input:BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Input()
        {
            this.InputInfoes = new HashSet<InputInfo>();
        }
    
        public string Id { get; set; }
        private Nullable<System.DateTime> DateInput;
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputInfo> InputInfoes { get; set; }

        public DateTime? DateInput1
        {
            get
            {
                return DateInput;
            }

            set
            {
                DateInput = value;
                OnPropertyChanged();
            }
        }
    }
}