import { CentralClient } from './../common/CentralClient';
export class tokenService {
  private readonly _client: CentralClient;

  constructor(authorityUrl: string) {
    this._client = new CentralClient(authorityUrl);
  }
}
