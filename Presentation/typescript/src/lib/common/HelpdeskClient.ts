import { IApiConfig } from './ApiConfig';
import { HttpClient } from './base/HttpClient';
import { AxiosRequestConfig } from 'axios';
import { API_VERSION_HEADER_NAME } from './ApiConstants';
import { inject, injectable } from 'tsyringe';

@injectable()
export class HelpdeskClient extends HttpClient {
  private _token: string;

  public constructor(@inject(nameof<IApiConfig>()) config: IApiConfig) {
    super(config.baseUrl);

    this._token = 'temp';

    this._axios.interceptors.request.use(this._handleRequest, this._handleError);

    this.setHeader(API_VERSION_HEADER_NAME, config.version);
  }

  private _handleRequest = (config: AxiosRequestConfig) => {
    config.headers['Authorization'] = 'Bearer ' + this._token;
    return config;
  };
}
