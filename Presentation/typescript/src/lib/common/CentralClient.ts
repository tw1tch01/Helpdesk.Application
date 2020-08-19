import { HttpClient } from './base/HttpClient';

export class CentralClient extends HttpClient {
  constructor(authorityUrl: string) {
    super(authorityUrl);
  }
}
