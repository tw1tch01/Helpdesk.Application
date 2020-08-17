import { HelpdeskClient } from "../common/HelpdeskClient";
import ApiConfig from "../common/base/ApiConfig";
import { AxiosResponse } from "axios";
import { TicketQueryParams } from "../models/ticketQueryParams";
import { PAGE_HEADER_NAME, PAGESIZE_HEADER_NAME } from "../common/ApiConstants";
import { PagedCollection } from "../models/common/PagedCollection";
import { TicketLookup } from "../models/TicketLookup";
import { TicketDetails } from "../models/TicketDetails";

export default class TicketsService {
  private readonly _endpoint: string = "tickets";
  private readonly _client: HelpdeskClient;

  constructor() {
    let config: ApiConfig = require("../../config.json");

    this._client = new HelpdeskClient(config);
  }

  public async get(id: string | number): Promise<AxiosResponse<TicketDetails>> {
    return this._client.get(this._endpoint, id);
  }

  public async lookupAll(
    params: TicketQueryParams
  ): Promise<AxiosResponse<TicketLookup[]>> {
    return this._client.query(this._endpoint, params);
  }

  public async pagedLookup(
    page: number,
    pageSize: number,
    params: TicketQueryParams
  ): Promise<AxiosResponse<PagedCollection<TicketLookup>>> {
    this._client.setHeader(PAGE_HEADER_NAME, page.toString());
    this._client.setHeader(PAGESIZE_HEADER_NAME, pageSize.toString());
    return this._client.query(this._endpoint, params);
  }
}
