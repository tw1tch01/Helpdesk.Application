import axios, { AxiosInstance, AxiosResponse } from 'axios';

export abstract class HttpClient {
  protected readonly _axios: AxiosInstance;

  public constructor(baseUrl: string) {
    this._axios = axios.create({
      baseURL: baseUrl,
    });

    this._axios.interceptors.response.use(this._handleResponse, this._handleError);
  }

  public setHeader(name: string, value: string) {
    this._axios.defaults.headers[name] = value;
  }

  public async query(resource: string, params?: any) {
    return this._axios.get(resource, params);
  }

  public async get(resource: string, id: string | number) {
    return this._axios.get(`${resource}/${id}`);
  }

  public async post(resource: string, data?: any) {
    return this._axios.post(resource, data);
  }

  public async put(resource: string, data?: any) {
    return this._axios.put(resource, data);
  }

  public async patch(resource: string, data?: any) {
    return this._axios.patch(resource, data);
  }

  public async delete(resource: string, id: string | number) {
    return this._axios.delete(`${resource}/${id}`);
  }

  private _handleResponse = ({ data }: AxiosResponse) => data;

  protected _handleError = (error: any) => Promise.reject(error);
}
