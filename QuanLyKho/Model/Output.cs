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

    public partial class Output : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Output()
        {
            this.OutputInfoes = new HashSet<OutputInfo>();
        }
    
        public string Id { get; set; }
        private Nullable<System.DateTime> _DateOutput;
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutputInfo> OutputInfoes { get; set; }

        public DateTime? DateOutput
        {
            get
            {
                return _DateOutput;
            }

            set
            {
                _DateOutput = value;
                OnPropertyChanged();
            }
        }
    }
}
