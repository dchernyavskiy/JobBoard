export class ClientBase {
    // protected transformOptions(options: RequestInit) {
    //     const token = localStorage.getItem('token');
    //     options.headers = {
    //         ...options.headers,
    //         Authorization: 'Bearer ' + token,
    //     };
    //     return Promise.resolve(options);
    // }
    authToken = '';
    protected constructor() {
    }
    
    setAuthToken(token: string) {
        this.authToken = token;
    }
  
    protected transformOptions(options: any): Promise<any> {
      options.headers = options.headers.append('authorization', `Bearer ${this.authToken}`);
      return Promise.resolve(options);
    }

}
