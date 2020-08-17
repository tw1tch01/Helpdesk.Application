import HttpClient from "./base/HttpClient";
import { AxiosRequestConfig } from "axios";
import ApiConfig from "./base/ApiConfig";
import { API_VERSION_HEADER_NAME } from "./ApiConstants";

export class HelpdeskClient extends HttpClient {
  private _token: string;

  public constructor(config: ApiConfig) {
    super(config.baseUrl);

    this._axios.interceptors.request.use(
      this._handleRequest,
      this._handleError
    );

    this.setHeader(API_VERSION_HEADER_NAME, config.version);
  }

  private _handleRequest = (config: AxiosRequestConfig) => {
    config.headers["Authorization"] = "Bearer " + this._token;
    return config;
  };
}
