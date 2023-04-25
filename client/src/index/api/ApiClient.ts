import type { AxiosError, AxiosRequestConfig, AxiosResponseTransformer, CanceledError, CancelToken } from 'axios';

import { ApiException, SwaggerResponse, UsersClient } from './api';

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
   * Call an authenticated endpoint and perform a token refresh upon need.
   * @param get Function that attempts to call an endpoint.
   * @param set Function that handles a successful endpoint call.
   * @param fail Function that handles a failed endpoint call.
   * @param userId User ID for token refreshes.
   * @param token Cancellation token.
   */
  public static callAuthenticatedEndpoint<T>(
    get: () => Promise<SwaggerResponse<T>>,
    set: (value: T) => void,
    fail: () => void,
    userId: number,
    token: CancelToken
  ) {
    get().then(
      (res) => set(res.result),
      (err) => this.handleError(err, userId, get, set, fail, token)
    );
  }

  /**
   * In case of an authentication error, attempts to refresh the JWT token and retry the initial request.
   * If the refresh fails, an action such as a redirect to the login page is performed.
   * @param error Error to handle.
   * @param get Function that retries the request.
   * @param set Function to run if the retried request succeeds.
   * @param fail Function to run if the retried request fails or a retry is not possible.
   */
  public static handleError<T>(
    error: Error | AxiosError, // ApiException?
    userId: number,
    get: () => Promise<SwaggerResponse<T>>,
    set: (value: T) => void,
    fail: () => void,
    token: CancelToken
  ) {
    // if the error is not an authentication error, we can't attempt to refresh the token
    if (error.name !== 'Error') {
      // in case of a cancelled request from a page change, do not redirect etc
      if (error.name !== 'CanceledError') {
        fail();
      }
      return;
    }

    // attempt a token refresh
    const client = new UsersClient();
    client.refreshUser(userId, token).then(
      () => {
        // token was refreshed
        get().then(
          // retry the initial request
          (res) => set(res.result), // retry succeeds, set state
          () => fail() // retry fails (now perform something like a redirection to the login page)
        );
      },
      () => fail() // token could not be refreshed
    );
  }

  private static isApiOptions(options: AxiosRequestConfig<unknown>): options is ApiOptions {
    return options.headers !== undefined;
  }
}
