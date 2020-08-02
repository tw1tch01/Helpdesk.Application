using System;
using Data.Extensions;
using Helpdesk.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Helpdesk.Persistence.Actions
{
    public class AddedStateAction
    {
        public string CreatedBy { get; set; }

        public string CreatedProcess { get; set; }

        public void SetCreatedAuditFields(EntityEntry entity)
        {
            entity.Entity.TrySetProperty(nameof(ICreatedAudit.CreatedBy), CreatedBy);
            entity.Entity.TrySetProperty(nameof(ICreatedAudit.CreatedOn), DateTimeOffset.UtcNow);
            entity.Entity.TrySetProperty(nameof(ICreatedAudit.CreatedProcess), CreatedProcess);
        }
    }
}