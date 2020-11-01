using System;

namespace ZipCo.Users.Domain.Entities.Members
{
    public class MemberExpense : AudibleBaseEntity
    {
        public long MemberId { get; set; }

        public decimal Amount { get; set; }

        public long BillFrequencyId { get; set; }

        public Member Member { get; set; }

        public Frequency BillFrequency { get; set; }

        public bool IsAnnuallyBill => BillFrequencyId == (long)FrequencyIds.Annual;
        
        public bool IsMonthlyBill => BillFrequencyId == (long)FrequencyIds.Month;
        
        public decimal GetMonthlyExpense()
        {
            if (IsAnnuallyBill)
            {
                return Amount / 12;
            }

            if (IsMonthlyBill)
            {
                return Amount;
            }

            throw new Exception("Invalid bill frequency.");
        }
    }
}
