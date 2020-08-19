import { PagedCollection } from '../../models/common';
import { TicketDetails, TicketLookup, TicketLookupParams } from '../../models/tickets';
import { IApiConfig } from '../../common/ApiConfig';
import { HelpdeskClient } from '../../common/HelpdeskClient';
import { PAGE_HEADER_NAME, PAGESIZE_HEADER_NAME } from '../../common/ApiConstants';
import { AxiosResponse } from 'axios';
import { ITicketService } from './ITicketsService';
import { inject } from 'tsyringe';

export class TicketsService implements ITicketService {
  private readonly _endpoint: string = 'tickets';

  constructor(@inject(nameof<HelpdeskClient>()) private _client: HelpdeskClient) {}

  public async get(id: string | number): Promise<AxiosResponse<TicketDetails>> {
    return this._client.get(this._endpoint, id);
  }

  public async lookupAll(params: TicketLookupParams): Promise<AxiosResponse<TicketLookup[]>> {
    return this._client.query(this._endpoint, params);
  }

  public async pagedLookup(
    page: number,
    pageSize: number,
    params: TicketLookupParams,
  ): Promise<AxiosResponse<PagedCollection<TicketLookup>>> {
    this._client.setHeader(PAGE_HEADER_NAME, page.toString());
    this._client.setHeader(PAGESIZE_HEADER_NAME, pageSize.toString());
    return this._client.query(this._endpoint, params);
  }
}
