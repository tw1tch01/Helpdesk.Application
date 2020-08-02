using System;
using Data.Extensions;
using Helpdesk.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Helpdesk.Persistence.Actions
{
    public class ModifiedStateAction
    {
        public string ModifiedBy { get; set; }

        public string ModifiedProcess { get; set; }

        public void SetModifiedAuditFields(EntityEntry entity)
        {
            entity.Entity.TrySetProperty(nameof(IModifiedAudit.ModifiedBy), ModifiedBy);
            entity.Entity.TrySetProperty(nameof(IModifiedAudit.ModifiedOn), DateTimeOffset.UtcNow);
            entity.Entity.TrySetProperty(nameof(IModifiedAudit.ModifiedProcess), ModifiedProcess);
        }
    }
}