import type { AxiosRequestConfig } from 'axios';

import { ApiException, SwaggerResponse } from './api';

interface ApiOptions extends AxiosRequestConfig<unknown> {
  headers: { [key: string]: string };
}

/**
 * Adds the default CSRF header to all requests and refreshes JWT tokens.
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

  /**
   * In case of an authentication error, attempts to refresh the JWT token and retry the initial request.
   * If the refresh fails, an action such as a redirect to the login page is performed.
   * @param error Response error to handle.
   */
  public static handleError<T>(error: ApiException, get: () => Promise<SwaggerResponse<T>>, set: (value: T) => void) {
    // eslint-disable-next-line no-console
    console.log('uhhh');
    // eslint-disable-next-line no-console
    console.log(error.status);

    // retry?
    get().then((res) => set(res.result));
  }

  private static isApiOptions(options: AxiosRequestConfig<unknown>): options is ApiOptions {
    return options.headers !== undefined;
  }
}
