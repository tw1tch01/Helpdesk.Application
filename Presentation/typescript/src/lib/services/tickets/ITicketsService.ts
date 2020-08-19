import { PagedCollection } from './../../models/common';
import { TicketLookupParams, TicketDetails, TicketLookup } from './../../models/tickets';
import { AxiosPromise } from 'axios';

export interface ITicketService {
  get(id: string | number): AxiosPromise<TicketDetails>;

  lookupAll(params: TicketLookupParams): AxiosPromise<TicketLookup[]>;

  pagedLookup(page: number, pageSize: number, params: TicketLookupParams): AxiosPromise<PagedCollection<TicketLookup>>;
}
