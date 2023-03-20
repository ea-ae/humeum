import type { AxiosRequestConfig } from 'axios';

interface ApiOptions extends AxiosRequestConfig<unknown> {
  headers: { [key: string]: string };
}

/**
 * Adds the default CSRF header to all requests.
 */
export default class ApiClient {
  // eslint-disable-next-line class-methods-use-this
  protected async transformOptions(options: AxiosRequestConfig<unknown>): Promise<AxiosRequestConfig<unknown>> {
    if (ApiClient.isApiOptions(options)) {
      const newOptions = options;
      newOptions.headers['X-Requested-With'] = 'axios';
      return options;
    }
    throw new Error('Invalid options');
  }

  private static isApiOptions(options: AxiosRequestConfig<unknown>): options is ApiOptions {
    return options.headers !== undefined;
  }
}
