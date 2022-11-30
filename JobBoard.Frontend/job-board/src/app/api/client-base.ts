export class ClientBase {
  protected constructor() {}

  protected transformOptions(options: any): Promise<any> {
    let authToken = localStorage.getItem('token');
    console.log('In Client Base auth token: ' + authToken);
    options.headers = options.headers.append(
      'Authorization',
      'Bearer ' + authToken
    );

    return Promise.resolve(options);
  }
}
