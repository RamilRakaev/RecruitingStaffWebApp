using System;
using System.Runtime.Serialization;

namespace RecruitingStaff.Infrastructure.CQRS
{
    [Serializable]
    internal class RecruitingStaff.Infrastructure.CQRSSampleDomainException : Exception
    {
        public RecruitingStaff.Infrastructure.CQRSSampleDomainException()
        {
        }

        public RecruitingStaff.Infrastructure.CQRSSampleDomainException(string message) : base(message)
        {
        }

        public RecruitingStaff.Infrastructure.CQRSSampleDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RecruitingStaff.Infrastructure.CQRSSampleDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
