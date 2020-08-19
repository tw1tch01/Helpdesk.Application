import { Created, Modified, PagedCollection } from './common';

export interface TicketDetails {
  ticketId: number;
  ticketGuid: string;
  opened: Created;
  modified: Modified;
  client: string;
  assignee?: string;
  name: string;
  description: string;
  dueDate?: Date;
  severity: Severity;
  priority: Priority;
  assignedOn?: Date;
  StartedOn?: Date;
  startedBy?: string;
  pausedOn?: Date;
  pausedBy?: string;
  resolvedOn?: Date;
  resolvedBy?: string;
  closedOn?: Date;
  closedBy?: string;
  status: TicketStatus;
}

export interface TicketLookup {
  ticketId: number;
  ticketGuid: string;
  client: string;
  assignee?: string;
  name: string;
  dueDate?: Date;
  severity: Severity;
  priority: Priority;
}

export interface TicketLookupParams {
  createdAfter?: Date;
  createdBefore?: Date;
  searchBy?: string;
  ticketIds?: number[];
  filterByStatus?: TicketStatus;
  filterBySeverity?: Severity;
  filterByPriority?: Priority;
  sortBy?: SortTicketsBy;
}

export enum Priority {
  Urgent = 0,
  Major = 1,
  Moderate = 2,
  Minor = 3,
}

export enum Severity {
  Low = 0,
  Medium = 1,
  High = 2,
  Critical = 3,
}

export enum TicketStatus {
  Open = 0,
  Overdue = 1,
  Resolved = 2,
  Closed = 3,
  InProgress = 4,
  OnHold = 5,
}

export enum SortTicketsBy {
  NameAsc,
  NameDesc,
  DueDateAsc,
  DueDateDesc,
  SeverityAsc,
  SeverityDesc,
  PriorityAsc,
  PriorityDesc,
}
