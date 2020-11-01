using System;

namespace ZipCo.Users.Domain.Entities.Members
{
    public class MemberSalary : AudibleBaseEntity
    {
        public long MemberId { get; set; }

        public decimal Amount { get; set; }

        public long PayFrequencyId { get; set; } 

        public Member Member { get; set; }

        public Frequency PayFrequency { get; set; }

        public bool IsAnnuallySalary => PayFrequencyId == (long)FrequencyIds.Annual;

        public bool IsMonthlySalary => PayFrequencyId == (long)FrequencyIds.Month;

        public decimal GetMonthlySalary()
        {
            if (IsAnnuallySalary)
            {
                return Amount / 12;
            }

            if (IsMonthlySalary)
            {
                return Amount;
            }

            throw new Exception("Invalid pay frequency.");
        }

    }
}
